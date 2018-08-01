using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Edit {0}", FieldDefinitionEnum.FundingSource)]
    public class FundingSourceEditFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FundingSource>
    {
        private readonly NeptuneFeatureWithContextImpl<FundingSource> _firmaFeatureWithContextImpl;

        public FundingSourceEditFeature() : base(new List<Role> {Role.Admin, Role.SitkaAdmin})
        {
            _firmaFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FundingSource>(this);
            ActionFilter = _firmaFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, FundingSource contextModelObject)
        {
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult($"You don't have permission to edit or delete {FieldDefinition.FundingSource.GetFieldDefinitionLabel()} {contextModelObject.GetDisplayName()}");
            }

            return new PermissionCheckResult();
        }

        public void DemandPermission(Person person, FundingSource contextModelObject)
        {
            _firmaFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
