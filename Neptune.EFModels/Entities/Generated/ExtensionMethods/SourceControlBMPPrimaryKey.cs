//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SourceControlBMP


namespace Neptune.EFModels.Entities
{
    public class SourceControlBMPPrimaryKey : EntityPrimaryKey<SourceControlBMP>
    {
        public SourceControlBMPPrimaryKey() : base(){}
        public SourceControlBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SourceControlBMPPrimaryKey(SourceControlBMP sourceControlBMP) : base(sourceControlBMP){}

        public static implicit operator SourceControlBMPPrimaryKey(int primaryKeyValue)
        {
            return new SourceControlBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator SourceControlBMPPrimaryKey(SourceControlBMP sourceControlBMP)
        {
            return new SourceControlBMPPrimaryKey(sourceControlBMP);
        }
    }
}