//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDocumentType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanDocumentTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanDocumentType>
    {
        public WaterQualityManagementPlanDocumentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDocumentTypePrimaryKey(WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType) : base(waterQualityManagementPlanDocumentType){}

        public static implicit operator WaterQualityManagementPlanDocumentTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDocumentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDocumentTypePrimaryKey(WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType)
        {
            return new WaterQualityManagementPlanDocumentTypePrimaryKey(waterQualityManagementPlanDocumentType);
        }
    }
}