using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class OrganizationType : IAuditableEntity
    {
        public static bool IsOrganizationTypeNameUnique(IEnumerable<OrganizationType> organizationTypes, string organizationTypeName, int currentOrganizationTypeID)
        {
            var organizationType = organizationTypes.SingleOrDefault(x => x.OrganizationTypeID != currentOrganizationTypeID && String.Equals(x.OrganizationTypeName, organizationTypeName, StringComparison.InvariantCultureIgnoreCase));
            return organizationType == null;
        }

        public static bool IsOrganizationTypeAbbreviationUnique(IEnumerable<OrganizationType> organizationTypes, string organizationAbbreviation, int currentOrganizationTypeID)
        {
            var organizationType = organizationTypes.SingleOrDefault(x => x.OrganizationTypeID != currentOrganizationTypeID && String.Equals(x.OrganizationTypeAbbreviation, organizationAbbreviation, StringComparison.InvariantCultureIgnoreCase));
            return organizationType == null;
        }

        public string GetAuditDescriptionString() => OrganizationTypeName;
    }
}