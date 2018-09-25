//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.QuickBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class QuickBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<QuickBMP>
    {
        public QuickBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public QuickBMPPrimaryKey(QuickBMP quickBMP) : base(quickBMP){}

        public static implicit operator QuickBMPPrimaryKey(int primaryKeyValue)
        {
            return new QuickBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator QuickBMPPrimaryKey(QuickBMP quickBMP)
        {
            return new QuickBMPPrimaryKey(quickBMP);
        }
    }
}