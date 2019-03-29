//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptuneHomePageImage
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeptuneHomePageImagePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NeptuneHomePageImage>
    {
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