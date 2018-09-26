//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyTreatmentBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifyTreatmentBMP>
    {
        public WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP) : base(waterQualityManagementPlanVerifyTreatmentBMP){}

        public static implicit operator WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            return new WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(waterQualityManagementPlanVerifyTreatmentBMP);
        }
    }
}