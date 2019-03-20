//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PreliminarySourceIdentificationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PreliminarySourceIdentificationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PreliminarySourceIdentificationType>
    {
        public PreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PreliminarySourceIdentificationTypePrimaryKey(PreliminarySourceIdentificationType preliminarySourceIdentificationType) : base(preliminarySourceIdentificationType){}

        public static implicit operator PreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue)
        {
            return new PreliminarySourceIdentificationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PreliminarySourceIdentificationTypePrimaryKey(PreliminarySourceIdentificationType preliminarySourceIdentificationType)
        {
            return new PreliminarySourceIdentificationTypePrimaryKey(preliminarySourceIdentificationType);
        }
    }
}