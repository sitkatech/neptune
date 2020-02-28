using Neptune.Web.Common;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace Neptune.Web.Models
{
    public class DelineationUploadGisReportJsonResult
    {
        public int StormwaterJurisdictionID;
        public int? NumberOfDelineations;
        public int? NumberOfDelineationsToBeUpdated;
        public int? NumberOfDelineationsToBeCreated;
        public int NumberOfDelineationNotMatchingTreatmentBMP;
        public List<string> Errors;

        public static DelineationUploadGisReportJsonResult GetDelineationUploadGisReportFromStaging(Person person,

            ICollection<DelineationStaging> delineationStagings)
        {
            var stormwaterJurisdictions = delineationStagings.Select(x => x.StormwaterJurisdiction).Distinct().ToList();

            Check.Assert(stormwaterJurisdictions.Count == 1, $"Multiple Stormwater Jurisdictions staged for user {person.PersonID}");

            var stormwaterJurisdiction = stormwaterJurisdictions.Single();

            var treatmentBMPsWithDelineationInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();

            var treatmentBMPNamesInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).Select(x => x.TreatmentBMPName).ToList();


            var candidateDelineationNames = delineationStagings.Select(x => x.TreatmentBMPName).Distinct().ToList();


            var numberOfDelineations = delineationStagings.Count;


            var treatmentBMPNamesDifference = candidateDelineationNames.Except(treatmentBMPNamesInStormwaterJurisdiction);

            var numberOfDelineationsNotMatchingTreatmentBMPNames = treatmentBMPNamesDifference.Count();

            if (candidateDelineationNames.Count != numberOfDelineations)
            {
                return new DelineationUploadGisReportJsonResult
                {
                    StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                    Errors = new List<string> { "The Treatment BMP Name must be unique for each feature in the upload." }
                };
            }

            var delineationsToBeUpdated = treatmentBMPsWithDelineationInStormwaterJurisdiction.Where(
                x =>
                    x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateDelineationNames.Contains(x.TreatmentBMP.TreatmentBMPName.ToString()));

            var numberOfDelineationsToBeUpdated = delineationsToBeUpdated.Count();
            var delineationUploadGisReport = new DelineationUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                NumberOfDelineations = numberOfDelineations,
                NumberOfDelineationsToBeUpdated =
                    numberOfDelineationsToBeUpdated,
                NumberOfDelineationNotMatchingTreatmentBMP = numberOfDelineationsNotMatchingTreatmentBMPNames,
                NumberOfDelineationsToBeCreated =
                    numberOfDelineations - numberOfDelineationsToBeUpdated - numberOfDelineationsNotMatchingTreatmentBMPNames,
                Errors = new List<string>()
            };

            return delineationUploadGisReport;
        }
    }
}
