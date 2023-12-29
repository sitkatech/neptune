//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.MonthsOfOperation


namespace Neptune.EFModels.Entities
{
    public class MonthsOfOperationPrimaryKey : EntityPrimaryKey<MonthsOfOperation>
    {
        public MonthsOfOperationPrimaryKey() : base(){}
        public MonthsOfOperationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public MonthsOfOperationPrimaryKey(MonthsOfOperation monthsOfOperation) : base(monthsOfOperation){}

        public static implicit operator MonthsOfOperationPrimaryKey(int primaryKeyValue)
        {
            return new MonthsOfOperationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator MonthsOfOperationPrimaryKey(MonthsOfOperation monthsOfOperation)
        {
            return new MonthsOfOperationPrimaryKey(monthsOfOperation);
        }
    }
}