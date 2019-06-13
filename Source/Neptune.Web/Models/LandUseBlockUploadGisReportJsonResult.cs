using Neptune.Web.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Web.Models
{
    public class LandUseBlockUploadGisReportJsonResult
    {
        public int StormwaterJurisdictionID;
        public int LandUseBlockGeometryStagingID;
        public string SelectedProperty;
        public int? NumberOfLandUseBlocks;
        public int? NumberOfLandUseBlocksToBeUpdated;
        public int? NumberOfLandUseBlocksToBeCreated;
        public int NumberOfLandUseBlockNotMatchingTreatmentBMP;
        public List<string> Errors;

        public static LandUseBlockUploadGisReportJsonResult GetLandUseBlockUpoadGisReportFromStaging(Person person,
            StormwaterJurisdiction stormwaterJurisdiction,
            LandUseBlockGeometryStaging landUseBlockGeometryStaging,
            string selectedProperty)
        {
            var treatmentBMPsWithLandUseBlockInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.LandUseBlocks.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();
            var treatmentBMPNamesInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).Select(x => x.TreatmentBMPName).ToList();


            var geoJsonFeatureCollection = landUseBlockGeometryStaging.ToGeoJsonFeatureCollection();
            var candidateLandUseBlockNames = geoJsonFeatureCollection.Features.Select(x => x.Properties[selectedProperty].ToString()).Distinct().ToList();


            var numberOfLandUseBlocks = geoJsonFeatureCollection.Features.Count;


            var treatmentBMPNamesDifference = candidateLandUseBlockNames.Except(treatmentBMPNamesInStormwaterJurisdiction);
            var numberOfLandUseBlocksNotMatchingTreatmentBMPNames = treatmentBMPNamesDifference.Count();

            if (candidateLandUseBlockNames.Count != numberOfLandUseBlocks)
            {
                return new LandUseBlockUploadGisReportJsonResult
                {
                    StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                    LandUseBlockGeometryStagingID = landUseBlockGeometryStaging.LandUseBlockGeometryStagingID,
                    SelectedProperty = selectedProperty,
                    Errors = new List<string> {"The selected property must be valid and un-ambiguous."}
                };
            }

            //var landUseBlocksToBeUpdated = treatmentBMPsWithLandUseBlockInStormwaterJurisdiction.Where(
            //    x =>
            //        x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateLandUseBlockNames.Contains(x.));


            //var numberOfLandUseBlocksToBeUpdated = landUseBlocksToBeUpdated.Count();
            var landUseBlockUploadGisReport = new LandUseBlockUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                LandUseBlockGeometryStagingID = landUseBlockGeometryStaging.LandUseBlockGeometryStagingID,
                SelectedProperty = selectedProperty,
                NumberOfLandUseBlocks = numberOfLandUseBlocks,
                //NumberOfLandUseBlocksToBeUpdated =
                //    numberOfLandUseBlocksToBeUpdated,
                NumberOfLandUseBlockNotMatchingTreatmentBMP = numberOfLandUseBlocksNotMatchingTreatmentBMPNames,
                //NumberOfLandUseBlocksToBeCreated =
                //    numberOfLandUseBlocks - numberOfLandUseBlocksToBeUpdated - numberOfLandUseBlocksNotMatchingTreatmentBMPNames
            };

            return landUseBlockUploadGisReport;
        }
    }
}