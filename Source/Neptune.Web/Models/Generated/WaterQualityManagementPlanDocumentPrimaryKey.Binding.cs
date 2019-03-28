//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDocument
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanDocumentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanDocument>
    {
        public WaterQualityManagementPlanDocumentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDocumentPrimaryKey(WaterQualityManagementPlanDocument waterQualityManagementPlanDocument) : base(waterQualityManagementPlanDocument){}

        public static implicit operator WaterQualityManagementPlanDocumentPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDocumentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDocumentPrimaryKey(WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            return new WaterQualityManagementPlanDocumentPrimaryKey(waterQualityManagementPlanDocument);
        }
    }
}