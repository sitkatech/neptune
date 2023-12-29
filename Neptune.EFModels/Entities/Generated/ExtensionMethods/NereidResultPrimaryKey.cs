//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NereidResult


namespace Neptune.EFModels.Entities
{
    public class NereidResultPrimaryKey : EntityPrimaryKey<NereidResult>
    {
        public NereidResultPrimaryKey() : base(){}
        public NereidResultPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NereidResultPrimaryKey(NereidResult nereidResult) : base(nereidResult){}

        public static implicit operator NereidResultPrimaryKey(int primaryKeyValue)
        {
            return new NereidResultPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NereidResultPrimaryKey(NereidResult nereidResult)
        {
            return new NereidResultPrimaryKey(nereidResult);
        }
    }
}