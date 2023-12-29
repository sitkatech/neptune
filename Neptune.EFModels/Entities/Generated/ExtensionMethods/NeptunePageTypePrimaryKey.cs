//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePageType


namespace Neptune.EFModels.Entities
{
    public class NeptunePageTypePrimaryKey : EntityPrimaryKey<NeptunePageType>
    {
        public NeptunePageTypePrimaryKey() : base(){}
        public NeptunePageTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptunePageTypePrimaryKey(NeptunePageType neptunePageType) : base(neptunePageType){}

        public static implicit operator NeptunePageTypePrimaryKey(int primaryKeyValue)
        {
            return new NeptunePageTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptunePageTypePrimaryKey(NeptunePageType neptunePageType)
        {
            return new NeptunePageTypePrimaryKey(neptunePageType);
        }
    }
}