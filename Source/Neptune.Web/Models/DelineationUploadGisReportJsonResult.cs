using System;
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
        public List<string> Errors;

        public static DelineationUploadGisReportJsonResult GetDelineationUploadGisReportFromStaging(Person person, ICollection<DelineationStaging> delineationStagings)
        {
            var errors = new List<string>();
            var stormwaterJurisdictions = delineationStagings.Select(x => x.StormwaterJurisdiction).Distinct().ToList();

            Check.Assert(stormwaterJurisdictions.Count != 0, "No Delineations were successfully staged from selected file.");
            Check.Assert(stormwaterJurisdictions.Count == 1, $"Multiple Stormwater Jurisdictions staged for user {person.PersonID}");

            var stormwaterJurisdiction = stormwaterJurisdictions.Single();

            var treatmentBMPsWithDelineationInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();

            var treatmentBMPNamesInStormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetNonPlanningModuleBMPs().Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).Select(x => x.TreatmentBMPName).ToList();

            var candidateDelineationNames = delineationStagings.Select(x => x.TreatmentBMPName).Distinct().ToList();

            var numberOfDelineations = delineationStagings.Count;
            if (candidateDelineationNames.Count != numberOfDelineations)
            {
                errors.Add("The Treatment BMP Name must be unique for each feature in the upload.");
            }

            var treatmentBMPNamesDifference = candidateDelineationNames.Except(treatmentBMPNamesInStormwaterJurisdiction, StringComparer.InvariantCultureIgnoreCase).ToList();
            if (treatmentBMPNamesDifference.Any())
            {
                errors.Add($"{treatmentBMPNamesDifference.Count} Delineations were found that do not match a Treatment BMP Name in the selected Jurisdiction: {string.Join(", ", treatmentBMPNamesDifference)}");
            }

            var delineationsWithBadGeometry = delineationStagings.Where(x => x.DelineationStagingGeometry.IsValid == false).ToList();
            if (delineationsWithBadGeometry.Any())
            {
                errors.Add("The following Delineations have bad geometries: " + string.Join(", ", delineationsWithBadGeometry.Select(x => x.TreatmentBMPName)));
            }


            var delineationsToBeUpdated = treatmentBMPsWithDelineationInStormwaterJurisdiction.Where(
                x =>
                    x.TreatmentBMP.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID && candidateDelineationNames.Contains(x.TreatmentBMP.TreatmentBMPName.ToString()));

            var numberOfDelineationsToBeUpdated = delineationsToBeUpdated.Count();
            var delineationUploadGisReport = new DelineationUploadGisReportJsonResult
            {
                StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
                NumberOfDelineations = numberOfDelineations,
                NumberOfDelineationsToBeUpdated = numberOfDelineationsToBeUpdated,
                NumberOfDelineationsToBeCreated = numberOfDelineations - numberOfDelineationsToBeUpdated - treatmentBMPNamesDifference.Count,
                Errors = errors
            };

            return delineationUploadGisReport;
        }
    }
}
