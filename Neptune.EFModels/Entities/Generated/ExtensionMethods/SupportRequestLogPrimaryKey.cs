//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SupportRequestLog


namespace Neptune.EFModels.Entities
{
    public class SupportRequestLogPrimaryKey : EntityPrimaryKey<SupportRequestLog>
    {
        public SupportRequestLogPrimaryKey() : base(){}
        public SupportRequestLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SupportRequestLogPrimaryKey(SupportRequestLog supportRequestLog) : base(supportRequestLog){}

        public static implicit operator SupportRequestLogPrimaryKey(int primaryKeyValue)
        {
            return new SupportRequestLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator SupportRequestLogPrimaryKey(SupportRequestLog supportRequestLog)
        {
            return new SupportRequestLogPrimaryKey(supportRequestLog);
        }
    }
}