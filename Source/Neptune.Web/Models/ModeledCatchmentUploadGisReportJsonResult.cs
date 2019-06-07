using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class DelineationUploadGisReportJsonResult
    {
        public int StormwaterJurisdictionID;
        public int DelineationGeometryStagingID;
        public string SelectedProperty;
        public int? NumberOfCatchments;
        public int? NumberOfCatchmentsToBeUpdated;
        public int? NumberOfCatchmentsToBeCreated;
        public int? NumberOfCatchmentsInActiveBMPRegistration;
        public List<string> Errors;

        public static DelineationUploadGisReportJsonResult GetDelineationUpoadGisReportFromStaging(Person person,
            StormwaterJurisdiction stormwaterJurisdiction,
            DelineationGeometryStaging delineationGeometryStaging,
            string selectedProperty)
        {
            var existingDelineations = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();

            var geoJsonFeatureCollection = delineationGeometryStaging.ToGeoJsonFeatureCollection();
            var candidateDelineationNames = geoJsonFeatureCollection.Features.Select(x => x.Properties[selectedProperty].ToString()).Distinct().ToList();
            var numberOfCatchments = geoJsonFeatureCollection.Features.Count;
            if (candidateDelineationNames.Count != numberOfCatchments)
            {
                return new DelineationUploadGisReportJsonResult
                {
                    StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                    DelineationGeometryStagingID = delineationGeometryStaging.DelineationGeometryStagingID,
                    SelectedProperty = selectedProperty,
                    Errors = new List<string> {"The selected property must be valid and un-ambiguous."}
                };
            }

            var delineationUploadGisReport = new DelineationUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                DelineationGeometryStagingID = delineationGeometryStaging.DelineationGeometryStagingID,
                SelectedProperty = selectedProperty,
                NumberOfCatchments = numberOfCatchments,
                NumberOfCatchmentsToBeUpdated =
                    existingDelineations.Count(
                        x =>
                            x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateDelineationNames.Contains(x.DelineationID.ToString())),
                NumberOfCatchmentsToBeCreated =
                    candidateDelineationNames.Count(
                        x => !existingDelineations.Exists(y => y.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && y.DelineationID.ToString() == x)),
                NumberOfCatchmentsInActiveBMPRegistration =
                    existingDelineations.Count(
                        x =>
                            x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateDelineationNames.Contains(x.DelineationID.ToString()))
            };

            // Assert that the numbers all add up
            Check.Assert(
                delineationUploadGisReport.NumberOfCatchments ==
                delineationUploadGisReport.NumberOfCatchmentsToBeUpdated + delineationUploadGisReport.NumberOfCatchmentsToBeCreated +
                delineationUploadGisReport.NumberOfCatchmentsInActiveBMPRegistration,
                "Modeled catchment upload GIS report results must sum up to the total number of catchments candidates being considered.");
            return delineationUploadGisReport;
        }
    }
}