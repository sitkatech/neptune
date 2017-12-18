//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NeptunePage
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NeptunePagePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NeptunePage>
    {
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