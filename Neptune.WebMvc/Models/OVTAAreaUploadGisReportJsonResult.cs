using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models;

public class OVTAAreaUploadGisReportJsonResult
{
    public int StormwaterJurisdictionID { get; set; }
    public int? NumberOfOVTAAreas { get; set; }
    public int? NumberOfOVTAAreasToBeUpdated { get; set; }
    public int? NumberOfOVTAAreasToBeCreated { get; set; }
    public List<string> Errors { get; set; }

    public static OVTAAreaUploadGisReportJsonResult GetOVTAAreaUploadGisReportFromStaging(NeptuneDbContext dbContext, Person person, ICollection<OnlandVisualTrashAssessmentAreaStaging> ovtaAreaStagings)
    {
        var errors = new List<string>();
        var stormwaterJurisdictions = ovtaAreaStagings.Select(x => x.StormwaterJurisdiction).Distinct().ToList();

        Check.Assert(stormwaterJurisdictions.Count != 0, "No Delineations were successfully staged from selected file.");
        Check.Assert(stormwaterJurisdictions.Count == 1, $"Multiple Stormwater Jurisdictions staged for user {person.PersonID}");

        var stormwaterJurisdiction = stormwaterJurisdictions.Single();

        var candidateOVTAAreaNames = ovtaAreaStagings.Select(x => x.AreaName).Distinct().ToList();

        var numberOfOVTAAreas = ovtaAreaStagings.Count;
        if (candidateOVTAAreaNames.Count != numberOfOVTAAreas)
        {
            errors.Add("The OVTA Area Name must be unique for each feature in the upload.");
        }

        var ovtaAreasWithBadGeometry = ovtaAreaStagings.Where(x => x.Geometry.IsValid == false).ToList();
        if (ovtaAreasWithBadGeometry.Any())
        {
            errors.Add("The following Delineations have bad geometries: " + string.Join(", ", ovtaAreasWithBadGeometry.Select(x => x.AreaName)));
        }


        var ovtaAreasToBeUpdated = ovtaAreaStagings.Select(x => x.AreaName).Intersect(dbContext
            .OnlandVisualTrashAssessmentAreas
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID)
            .Select(x => x.OnlandVisualTrashAssessmentAreaName));

        var numberOfDelineationsToBeUpdated = ovtaAreasToBeUpdated.Count();
        var ovtaAreaUploadGisReport = new OVTAAreaUploadGisReportJsonResult
        {
            StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID,
            NumberOfOVTAAreas = numberOfOVTAAreas,
            NumberOfOVTAAreasToBeUpdated = numberOfDelineationsToBeUpdated,
            NumberOfOVTAAreasToBeCreated = numberOfOVTAAreas - numberOfDelineationsToBeUpdated,
            Errors = errors
        };

        return ovtaAreaUploadGisReport;
    }
}