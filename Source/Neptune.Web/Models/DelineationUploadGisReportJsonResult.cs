using Neptune.Web.Common;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public class DelineationUploadGisReportJsonResult
    {
        public int StormwaterJurisdictionID;
        public string SelectedProperty;
        public int? NumberOfDelineations;
        public int? NumberOfDelineationsToBeUpdated;
        public int? NumberOfDelineationsToBeCreated;
        public int NumberOfDelineationNotMatchingTreatmentBMP;
        public List<string> Errors;

        public static DelineationUploadGisReportJsonResult GetDelineationUpoadGisReportFromStaging(Person person,
            StormwaterJurisdiction stormwaterJurisdiction,
            DelineationGeometryStaging delineationGeometryStaging,
            string selectedProperty)
        {
            var treatmentBMPsWithDelineationInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();
            var treatmentBMPNamesInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).Select(x => x.TreatmentBMPName).ToList();


            var geoJsonFeatureCollection = delineationGeometryStaging.ToGeoJsonFeatureCollection();
            var candidateDelineationNames = geoJsonFeatureCollection.Features.Select(x => x.Properties[selectedProperty].ToString()).Distinct().ToList();


            var numberOfDelineations = geoJsonFeatureCollection.Features.Count;


            var treatmentBMPNamesDifference = candidateDelineationNames.Except(treatmentBMPNamesInStormwaterJurisdiction);
            var numberOfDelineationsNotMatchingTreatmentBMPNames = treatmentBMPNamesDifference.Count();

            if (candidateDelineationNames.Count != numberOfDelineations)
            {
                return new DelineationUploadGisReportJsonResult
                {
                    StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                    //DelineationGeometryStagingID = delineationGeometryStaging.DelineationGeometryStagingID,
                    SelectedProperty = selectedProperty,
                    Errors = new List<string> {"The selected property must be valid and un-ambiguous."}
                };
            }

            var delineationsToBeUpdated = treatmentBMPsWithDelineationInStormwaterJurisdiction.Where(
                x =>
                    x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateDelineationNames.Contains(x.TreatmentBMP.TreatmentBMPName.ToString()));


            var numberOfDelineationsToBeUpdated = delineationsToBeUpdated.Count();
            var delineationUploadGisReport = new DelineationUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                //DelineationGeometryStagingID = delineationGeometryStaging.DelineationGeometryStagingID,
                SelectedProperty = selectedProperty,
                NumberOfDelineations = numberOfDelineations,
                NumberOfDelineationsToBeUpdated =
                    numberOfDelineationsToBeUpdated,
                NumberOfDelineationNotMatchingTreatmentBMP = numberOfDelineationsNotMatchingTreatmentBMPNames,
                NumberOfDelineationsToBeCreated =
                    numberOfDelineations - numberOfDelineationsToBeUpdated - numberOfDelineationsNotMatchingTreatmentBMPNames
            };

            return delineationUploadGisReport;
        }
    }
}