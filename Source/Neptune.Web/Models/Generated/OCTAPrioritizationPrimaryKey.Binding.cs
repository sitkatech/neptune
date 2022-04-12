//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OCTAPrioritization
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OCTAPrioritizationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OCTAPrioritization>
    {
        public OCTAPrioritizationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OCTAPrioritizationPrimaryKey(OCTAPrioritization oCTAPrioritization) : base(oCTAPrioritization){}

        public static implicit operator OCTAPrioritizationPrimaryKey(int primaryKeyValue)
        {
            return new OCTAPrioritizationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OCTAPrioritizationPrimaryKey(OCTAPrioritization oCTAPrioritization)
        {
            return new OCTAPrioritizationPrimaryKey(oCTAPrioritization);
        }
    }
}