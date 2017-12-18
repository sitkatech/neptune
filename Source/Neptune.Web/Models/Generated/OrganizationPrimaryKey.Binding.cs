//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Organization
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OrganizationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<Organization>
    {
        public OrganizationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OrganizationPrimaryKey(Organization organization) : base(organization){}

        public static implicit operator OrganizationPrimaryKey(int primaryKeyValue)
        {
            return new OrganizationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OrganizationPrimaryKey(Organization organization)
        {
            return new OrganizationPrimaryKey(organization);
        }
    }
}