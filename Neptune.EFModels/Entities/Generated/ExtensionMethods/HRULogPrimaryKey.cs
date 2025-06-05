//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HRULog


namespace Neptune.EFModels.Entities
{
    public class HRULogPrimaryKey : EntityPrimaryKey<HRULog>
    {
        public HRULogPrimaryKey() : base(){}
        public HRULogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HRULogPrimaryKey(HRULog hRULog) : base(hRULog){}

        public static implicit operator HRULogPrimaryKey(int primaryKeyValue)
        {
            return new HRULogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator HRULogPrimaryKey(HRULog hRULog)
        {
            return new HRULogPrimaryKey(hRULog);
        }
    }
}