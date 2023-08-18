//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Delineation


namespace Neptune.EFModels.Entities
{
    public class DelineationPrimaryKey : EntityPrimaryKey<Delineation>
    {
        public DelineationPrimaryKey() : base(){}
        public DelineationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public DelineationPrimaryKey(Delineation delineation) : base(delineation){}

        public static implicit operator DelineationPrimaryKey(int primaryKeyValue)
        {
            return new DelineationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator DelineationPrimaryKey(Delineation delineation)
        {
            return new DelineationPrimaryKey(delineation);
        }
    }
}