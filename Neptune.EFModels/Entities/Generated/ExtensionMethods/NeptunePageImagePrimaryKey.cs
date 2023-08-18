//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePageImage


namespace Neptune.EFModels.Entities
{
    public class NeptunePageImagePrimaryKey : EntityPrimaryKey<NeptunePageImage>
    {
        public NeptunePageImagePrimaryKey() : base(){}
        public NeptunePageImagePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptunePageImagePrimaryKey(NeptunePageImage neptunePageImage) : base(neptunePageImage){}

        public static implicit operator NeptunePageImagePrimaryKey(int primaryKeyValue)
        {
            return new NeptunePageImagePrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptunePageImagePrimaryKey(NeptunePageImage neptunePageImage)
        {
            return new NeptunePageImagePrimaryKey(neptunePageImage);
        }
    }
}