using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class DroolToolRole : IRole
    {
        public int RoleID => DroolToolRoleID;
        public string RoleName => DroolToolRoleName;
        public string RoleDisplayName => DroolToolRoleDisplayName;
        public string RoleDescription => DroolToolRoleDescription;

        public List<FeaturePermission> GetFeaturePermissions()
        {
            var featurePermissions = this.GetFeaturePermissions(typeof(DroolToolFeature));
            featurePermissions.AddRange(this.GetFeaturePermissions(typeof(DroolToolFeatureWithContext)));
            return featurePermissions;
        }

        public List<Person> GetPeopleWithRole()
        {
            return HttpRequestStorage.DatabaseEntities.People.Where(x => x.IsActive && x.DroolToolRoleID == RoleID).ToList();
        }

        public NeptuneAreaEnum? NeptuneAreaEnum => Models.NeptuneAreaEnum.DroolTool;
        public string NeptuneAreaDisplayName => Models.NeptuneArea.DroolTool.NeptuneAreaDisplayName;
    }
}