//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.QuickBMP


namespace Neptune.EFModels.Entities
{
    public class QuickBMPPrimaryKey : EntityPrimaryKey<QuickBMP>
    {
        public QuickBMPPrimaryKey() : base(){}
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