using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class NeptuneArea
    {
        public abstract IRole GetRole(int roleID);
        public abstract string GetHomeUrl();
        public abstract bool IsAreaVisibleToPerson(Person person);
        public abstract string GetIconUrl();

        protected abstract Func<Person, IRole> GetPersonRoleToUseFunc();

        public IRole GetPersonRoleToUse(Person person)
        {
            return GetPersonRoleToUseFunc().Invoke(person);
        }

        public bool HasPermissionByPerson(Person person, IList<IRole> rolesToCheck)
        {
            return person != null && (rolesToCheck?.Any(x => x.RoleID == GetPersonRoleToUseFunc().Invoke(person).RoleID) ?? false);
        }

    }

    public partial class NeptuneAreaTrash
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Areas.Trash.Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index());
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/trashIcon.png";
        }
        public override IRole GetRole(int roleID)
        {
            return Role.AllLookupDictionary[roleID];
        }

        protected override Func<Person, IRole> GetPersonRoleToUseFunc()
        {
            return x => x.Role;
        }
    }

    public partial class NeptuneAreaOCStormwaterTools
    {
        public override IRole GetRole(int roleID)
        {
            return Role.AllLookupDictionary[roleID];
        }

        public override string GetHomeUrl()
        {
            return SitkaRoute<Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index()) + "#welcome";
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/inventoryIcon.png";
        }

        protected override Func<Person, IRole> GetPersonRoleToUseFunc()
        {
            return x => x.Role;
        }
    }

    public partial class NeptuneAreaModeling
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Areas.Modeling.Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index());
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/modelingIcon.png";
        }

        public override IRole GetRole(int roleID)
        {
            return Role.AllLookupDictionary[roleID];
        }

        protected override Func<Person, IRole> GetPersonRoleToUseFunc()
        {
            return x => x.Role;
        }
    }

    public partial class NeptuneAreaDroolTool
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Areas.DroolTool.Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index());
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return new DroolToolViewFeature().HasPermissionByPerson(person);
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/droolToolIcon.png";
        }
        public override IRole GetRole(int roleID)
        {
            return DroolToolRole.AllLookupDictionary[roleID];
        }
        protected override Func<Person, IRole> GetPersonRoleToUseFunc()
        {
            return x => x.DroolToolRole;
        }
    }
}