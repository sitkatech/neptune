using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security;

[SecurityFeatureDescription("Can close revision requests with manager or editor role")]
public class RegionalSubbasinRevisionRequestCloseFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<RegionalSubbasinRevisionRequest>
{
    private readonly NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest> _lakeTahoeInfoFeatureWithContextImpl;

    public RegionalSubbasinRevisionRequestCloseFeature() : base(new List<RoleEnum> { RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager })
    {
        _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<RegionalSubbasinRevisionRequest>(this);
        ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
    }

    public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject, NeptuneDbContext dbContext)
    {
        return HasPermission(person, contextModelObject);
    }

    public PermissionCheckResult HasPermission(Person person, RegionalSubbasinRevisionRequest contextModelObject)
    {
        if (person.IsAdministrator())
        {
            return new PermissionCheckResult();
        }

        if (person.PersonID == contextModelObject.RequestPersonID)
        {
            return new PermissionCheckResult();
        }

        return new PermissionCheckResult("You do not have permission to close this Regional Subbasin Revision Request.");
    }
}