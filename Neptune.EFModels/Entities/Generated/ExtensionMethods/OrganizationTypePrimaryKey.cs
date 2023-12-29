//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OrganizationType


namespace Neptune.EFModels.Entities
{
    public class OrganizationTypePrimaryKey : EntityPrimaryKey<OrganizationType>
    {
        public OrganizationTypePrimaryKey() : base(){}
        public OrganizationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OrganizationTypePrimaryKey(OrganizationType organizationType) : base(organizationType){}

        public static implicit operator OrganizationTypePrimaryKey(int primaryKeyValue)
        {
            return new OrganizationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator OrganizationTypePrimaryKey(OrganizationType organizationType)
        {
            return new OrganizationTypePrimaryKey(organizationType);
        }
    }
}