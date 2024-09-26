//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NereidLog


namespace Neptune.EFModels.Entities
{
    public class NereidLogPrimaryKey : EntityPrimaryKey<NereidLog>
    {
        public NereidLogPrimaryKey() : base(){}
        public NereidLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NereidLogPrimaryKey(NereidLog nereidLog) : base(nereidLog){}

        public static implicit operator NereidLogPrimaryKey(int primaryKeyValue)
        {
            return new NereidLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NereidLogPrimaryKey(NereidLog nereidLog)
        {
            return new NereidLogPrimaryKey(nereidLog);
        }
    }
}