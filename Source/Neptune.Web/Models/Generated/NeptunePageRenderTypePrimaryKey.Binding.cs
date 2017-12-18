//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePageRenderType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeptunePageRenderTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NeptunePageRenderType>
    {
        public NeptunePageRenderTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NeptunePageRenderTypePrimaryKey(NeptunePageRenderType neptunePageRenderType) : base(neptunePageRenderType){}

        public static implicit operator NeptunePageRenderTypePrimaryKey(int primaryKeyValue)
        {
            return new NeptunePageRenderTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator NeptunePageRenderTypePrimaryKey(NeptunePageRenderType neptunePageRenderType)
        {
            return new NeptunePageRenderTypePrimaryKey(neptunePageRenderType);
        }
    }
}