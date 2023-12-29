//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptuneArea


namespace Neptune.EFModels.Entities
{
    public class NeptuneAreaPrimaryKey : EntityPrimaryKey<NeptuneArea>
    {
        public NeptuneAreaPrimaryKey() : base(){}
        public NeptuneAreaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptuneAreaPrimaryKey(NeptuneArea neptuneArea) : base(neptuneArea){}

        public static implicit operator NeptuneAreaPrimaryKey(int primaryKeyValue)
        {
            return new NeptuneAreaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptuneAreaPrimaryKey(NeptuneArea neptuneArea)
        {
            return new NeptuneAreaPrimaryKey(neptuneArea);
        }
    }
}