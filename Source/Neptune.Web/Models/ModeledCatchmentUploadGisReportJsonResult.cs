using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ModeledCatchmentUploadGisReportJsonResult
    {
        public int StormwaterJurisdictionID;
        public int ModeledCatchmentGeometryStagingID;
        public string SelectedProperty;
        public int? NumberOfCatchments;
        public int? NumberOfCatchmentsToBeUpdated;
        public int? NumberOfCatchmentsToBeCreated;
        public int? NumberOfCatchmentsInActiveBMPRegistration;
        public List<string> Errors;

        public static ModeledCatchmentUploadGisReportJsonResult GetModeledCatchmentUpoadGisReportFromStaging(Person person,
            StormwaterJurisdiction stormwaterJurisdiction,
            ModeledCatchmentGeometryStaging modeledCatchmentGeometryStaging,
            string selectedProperty)
        {
            var existingModeledCatchments = HttpRequestStorage.DatabaseEntities.ModeledCatchments.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();

            var geoJsonFeatureCollection = modeledCatchmentGeometryStaging.ToGeoJsonFeatureCollection();
            var candidateModeledCatchmentNames = geoJsonFeatureCollection.Features.Select(x => x.Properties[selectedProperty].ToString()).Distinct().ToList();
            var numberOfCatchments = geoJsonFeatureCollection.Features.Count;
            if (candidateModeledCatchmentNames.Count != numberOfCatchments)
            {
                return new ModeledCatchmentUploadGisReportJsonResult
                {
                    StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                    ModeledCatchmentGeometryStagingID = modeledCatchmentGeometryStaging.ModeledCatchmentGeometryStagingID,
                    SelectedProperty = selectedProperty,
                    Errors = new List<string> {"The selected property must be valid and un-ambiguous."}
                };
            }

            var modeledCatchmentUploadGisReport = new ModeledCatchmentUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                ModeledCatchmentGeometryStagingID = modeledCatchmentGeometryStaging.ModeledCatchmentGeometryStagingID,
                SelectedProperty = selectedProperty,
                NumberOfCatchments = numberOfCatchments,
                NumberOfCatchmentsToBeUpdated =
                    existingModeledCatchments.Count(
                        x =>
                            x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateModeledCatchmentNames.Contains(x.ModeledCatchmentName)),
                NumberOfCatchmentsToBeCreated =
                    candidateModeledCatchmentNames.Count(
                        x => !existingModeledCatchments.Exists(y => y.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && y.ModeledCatchmentName == x)),
                NumberOfCatchmentsInActiveBMPRegistration =
                    existingModeledCatchments.Count(
                        x =>
                            x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateModeledCatchmentNames.Contains(x.ModeledCatchmentName))
            };

            // Assert that the numbers all add up
            Check.Assert(
                modeledCatchmentUploadGisReport.NumberOfCatchments ==
                modeledCatchmentUploadGisReport.NumberOfCatchmentsToBeUpdated + modeledCatchmentUploadGisReport.NumberOfCatchmentsToBeCreated +
                modeledCatchmentUploadGisReport.NumberOfCatchmentsInActiveBMPRegistration,
                "Modeled catchment upload GIS report results must sum up to the total number of catchments candidates being considered.");
            return modeledCatchmentUploadGisReport;
        }
    }
}