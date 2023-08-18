//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Role


namespace Neptune.EFModels.Entities
{
    public class RolePrimaryKey : EntityPrimaryKey<Role>
    {
        public RolePrimaryKey() : base(){}
        public RolePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RolePrimaryKey(Role role) : base(role){}

        public static implicit operator RolePrimaryKey(int primaryKeyValue)
        {
            return new RolePrimaryKey(primaryKeyValue);
        }

        public static implicit operator RolePrimaryKey(Role role)
        {
            return new RolePrimaryKey(role);
        }
    }
}