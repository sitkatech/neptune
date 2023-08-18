//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptuneHomePageImage


namespace Neptune.EFModels.Entities
{
    public class NeptuneHomePageImagePrimaryKey : EntityPrimaryKey<NeptuneHomePageImage>
    {
        public NeptuneHomePageImagePrimaryKey() : base(){}
        public NeptuneHomePageImagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptuneHomePageImagePrimaryKey(NeptuneHomePageImage neptuneHomePageImage) : base(neptuneHomePageImage){}

        public static implicit operator NeptuneHomePageImagePrimaryKey(int primaryKeyValue)
        {
            return new NeptuneHomePageImagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptuneHomePageImagePrimaryKey(NeptuneHomePageImage neptuneHomePageImage)
        {
            return new NeptuneHomePageImagePrimaryKey(neptuneHomePageImage);
        }
    }
}