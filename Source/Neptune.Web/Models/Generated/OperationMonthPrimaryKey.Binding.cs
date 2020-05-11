//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OperationMonth
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OperationMonthPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OperationMonth>
    {
        public OperationMonthPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OperationMonthPrimaryKey(OperationMonth operationMonth) : base(operationMonth){}

        public static implicit operator OperationMonthPrimaryKey(int primaryKeyValue)
        {
            return new OperationMonthPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OperationMonthPrimaryKey(OperationMonth operationMonth)
        {
            return new OperationMonthPrimaryKey(operationMonth);
        }
    }
}