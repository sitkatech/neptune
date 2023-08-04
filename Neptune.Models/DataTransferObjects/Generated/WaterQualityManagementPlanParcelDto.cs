//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanParcelDto
    {
        public int WaterQualityManagementPlanParcelID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public ParcelDto Parcel { get; set; }
    }

    public partial class WaterQualityManagementPlanParcelSimpleDto
    {
        public int WaterQualityManagementPlanParcelID { get; set; }
        public System.Int32 WaterQualityManagementPlanID { get; set; }
        public System.Int32 ParcelID { get; set; }
    }

}