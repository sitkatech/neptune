//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePageImage
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeptunePageImagePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NeptunePageImage>
    {
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