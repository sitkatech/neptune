using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: entity-scoped edit gate for assessment-routed endpoints (observations, copy, delete,
    /// photos) — Editor and up, plus a jurisdiction match against the routed assessment's BMP. Photo
    /// endpoints are covered too: every photo query also filters on the routed assessment ID.
    /// </summary>
    public class TreatmentBMPAssessmentEditFeature() : JurisdictionScopedFeature(EditRoles)
    {
        protected override string RouteKey => "treatmentBMPAssessmentID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            TreatmentBMPAssessments.GetStormwaterJurisdictionIDForFeatureContextCheck(dbContext, id);
    }
}
