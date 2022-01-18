//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class HRUCharacteristicDto
    {
        public int HRUCharacteristicID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public HRUCharacteristicLandUseCodeDto HRUCharacteristicLandUseCode { get; set; }
        public LoadGeneratingUnitDto LoadGeneratingUnit { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public HRUCharacteristicLandUseCodeDto BaselineHRUCharacteristicLandUseCode { get; set; }
    }

    public partial class HRUCharacteristicSimpleDto
    {
        public int HRUCharacteristicID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int LoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }
    }

}