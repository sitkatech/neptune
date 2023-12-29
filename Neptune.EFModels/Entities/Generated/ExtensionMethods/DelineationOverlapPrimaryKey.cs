//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.DelineationOverlap


namespace Neptune.EFModels.Entities
{
    public class DelineationOverlapPrimaryKey : EntityPrimaryKey<DelineationOverlap>
    {
        public DelineationOverlapPrimaryKey() : base(){}
        public DelineationOverlapPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationOverlapPrimaryKey(DelineationOverlap delineationOverlap) : base(delineationOverlap){}

        public static implicit operator DelineationOverlapPrimaryKey(int primaryKeyValue)
        {
            return new DelineationOverlapPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationOverlapPrimaryKey(DelineationOverlap delineationOverlap)
        {
            return new DelineationOverlapPrimaryKey(delineationOverlap);
        }
    }
}