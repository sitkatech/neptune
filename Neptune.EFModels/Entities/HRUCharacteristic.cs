﻿namespace Neptune.EFModels.Entities
{
    public partial class HRUCharacteristic
    {
        public TreatmentBMP? GetTreatmentBMP()
        {
            return LoadGeneratingUnit.Delineation?.TreatmentBMP;
        }

        public WaterQualityManagementPlan? GetWaterQualityManagementPlan()
        {
            return LoadGeneratingUnit.WaterQualityManagementPlan;
        }

        public RegionalSubbasin? GetRegionalSubbasin()
        {
            return LoadGeneratingUnit.RegionalSubbasin;
        }
    }
}