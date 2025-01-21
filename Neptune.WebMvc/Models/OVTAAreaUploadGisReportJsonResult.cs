using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models;

public class OVTAAreaUploadGisReportJsonResult
{
    public int StormwaterJurisdictionID { get; set; }
    public int? NumberOfDelineations { get; set; }
    public int? NumberOfDelineationsToBeUpdated { get; set; }
    public int? NumberOfDelineationsToBeCreated { get; set; }
    public List<string> Errors { get; set; }

    public static OVTAAreaUploadGisReportJsonResult GetOVTAAreaUploadGisReportFromStaging(NeptuneDbContext dbContext, Person person, ICollection<OnlandVisualTrashAssessmentAreaStaging> ovtaAreaStagings)
    {
        var errors = new List<string>();
        var stormwaterJurisdictions = ovtaAreaStagings.Select(x => x.StormwaterJurisdiction).Distinct().ToList();

        Check.Assert(stormwaterJurisdictions.Count != 0, "No Delineations were successfully staged from selected file.");
        Check.Assert(stormwaterJurisdictions.Count == 1, $"Multiple Stormwater Jurisdictions staged for user {person.PersonID}");

        var stormwaterJurisdiction = stormwaterJurisdictions.Single();

        var treatmentBMPNamesInStormwaterJurisdiction = TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext).Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).Select(x => x.TreatmentBMPName).ToList();

        var candidateDelineationNames = ovtaAreaStagings.Select(x => x.AreaName).Distinct().ToList();

        var numberOfDelineations = ovtaAreaStagings.Count;
        if (candidateDelineationNames.Count != numberOfDelineations)
        {
            errors.Add("The OVTA Area Name must be unique for each feature in the upload.");
        }

        var delineationsWithBadGeometry = ovtaAreaStagings.Where(x => x.Geometry.IsValid == false).ToList();
        if (delineationsWithBadGeometry.Any())
        {
            errors.Add("The following Delineations have bad geometries: " + string.Join(", ", delineationsWithBadGeometry.Select(x => x.AreaName)));
        }


        var delineationsToBeUpdated = ovtaAreaStagings.Select(x => x.AreaName).Intersect(dbContext
            .OnlandVisualTrashAssessmentAreas
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID)
            .Select(x => x.OnlandVisualTrashAssessmentAreaName));

        var numberOfDelineationsToBeUpdated = delineationsToBeUpdated.Count();
        var delineationUploadGisReport = new OVTAAreaUploadGisReportJsonResult
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
            NumberOfDelineations = numberOfDelineations,
            NumberOfDelineationsToBeUpdated = numberOfDelineationsToBeUpdated,
            NumberOfDelineationsToBeCreated = numberOfDelineations - numberOfDelineationsToBeUpdated,
            Errors = errors
        };

        return delineationUploadGisReport;
    }
}