//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePage


namespace Neptune.EFModels.Entities
{
    public class NeptunePagePrimaryKey : EntityPrimaryKey<NeptunePage>
    {
        public NeptunePagePrimaryKey() : base(){}
        public NeptunePagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptunePagePrimaryKey(NeptunePage neptunePage) : base(neptunePage){}

        public static implicit operator NeptunePagePrimaryKey(int primaryKeyValue)
        {
            return new NeptunePagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptunePagePrimaryKey(NeptunePage neptunePage)
        {
            return new NeptunePagePrimaryKey(neptunePage);
        }
    }
}