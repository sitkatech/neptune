using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public static class HRUCharacteristicModelExtensions
    {
        public static TreatmentBMP GetTreatmentBMP(this HRUCharacteristic hruCharacteristic)
        {
            return hruCharacteristic.LoadGeneratingUnit.Delineation.TreatmentBMP;
        }

        public static WaterQualityManagementPlan GetWaterQualityManagementPlan(this HRUCharacteristic hruCharacteristic)
        {
            return hruCharacteristic.LoadGeneratingUnit.WaterQualityManagementPlan;
        }

        public static RegionalSubbasin GetRegionalSubbasin(this HRUCharacteristic hruCharacteristic)
        {
            return hruCharacteristic.LoadGeneratingUnit.RegionalSubbasin;
        }
    }
}