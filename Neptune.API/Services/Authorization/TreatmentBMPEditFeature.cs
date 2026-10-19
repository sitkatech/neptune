using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public class TreatmentBMPEditFeature() : JurisdictionScopedFeature(EditRoles)
    {
        protected override string RouteKey => "treatmentBMPID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, id)?.StormwaterJurisdictionID;
    }
}
