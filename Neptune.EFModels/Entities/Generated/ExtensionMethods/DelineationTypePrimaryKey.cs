//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationType


namespace Neptune.EFModels.Entities
{
    public class DelineationTypePrimaryKey : EntityPrimaryKey<DelineationType>
    {
        public DelineationTypePrimaryKey() : base(){}
        public DelineationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationTypePrimaryKey(DelineationType delineationType) : base(delineationType){}

        public static implicit operator DelineationTypePrimaryKey(int primaryKeyValue)
        {
            return new DelineationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationTypePrimaryKey(DelineationType delineationType)
        {
            return new DelineationTypePrimaryKey(delineationType);
        }
    }
}