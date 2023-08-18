//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OVTASection


namespace Neptune.EFModels.Entities
{
    public class OVTASectionPrimaryKey : EntityPrimaryKey<OVTASection>
    {
        public OVTASectionPrimaryKey() : base(){}
        public OVTASectionPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OVTASectionPrimaryKey(OVTASection oVTASection) : base(oVTASection){}

        public static implicit operator OVTASectionPrimaryKey(int primaryKeyValue)
        {
            return new OVTASectionPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OVTASectionPrimaryKey(OVTASection oVTASection)
        {
            return new OVTASectionPrimaryKey(oVTASection);
        }
    }
}