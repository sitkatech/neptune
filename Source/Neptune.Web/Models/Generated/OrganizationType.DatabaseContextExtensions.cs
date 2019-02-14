//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static OrganizationType GetOrganizationType(this IQueryable<OrganizationType> organizationTypes, int organizationTypeID)
        {
            var organizationType = organizationTypes.SingleOrDefault(x => x.OrganizationTypeID == organizationTypeID);
            Check.RequireNotNullThrowNotFound(organizationType, "OrganizationType", organizationTypeID);
            return organizationType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteOrganizationType(this IQueryable<OrganizationType> organizationTypes, List<int> organizationTypeIDList)
        {
            if(organizationTypeIDList.Any())
            {
                organizationTypes.Where(x => organizationTypeIDList.Contains(x.OrganizationTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteOrganizationType(this IQueryable<OrganizationType> organizationTypes, ICollection<OrganizationType> organizationTypesToDelete)
        {
            if(organizationTypesToDelete.Any())
            {
                var organizationTypeIDList = organizationTypesToDelete.Select(x => x.OrganizationTypeID).ToList();
                organizationTypes.Where(x => organizationTypeIDList.Contains(x.OrganizationTypeID)).Delete();
            }
        }

        public static void DeleteOrganizationType(this IQueryable<OrganizationType> organizationTypes, int organizationTypeID)
        {
            DeleteOrganizationType(organizationTypes, new List<int> { organizationTypeID });
        }

        public static void DeleteOrganizationType(this IQueryable<OrganizationType> organizationTypes, OrganizationType organizationTypeToDelete)
        {
            DeleteOrganizationType(organizationTypes, new List<OrganizationType> { organizationTypeToDelete });
        }
    }
}