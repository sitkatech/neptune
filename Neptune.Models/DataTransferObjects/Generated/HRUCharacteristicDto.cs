//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32 HRUCharacteristicLandUseCodeID { get; set; }
        public System.Int32 LoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public System.Int32 BaselineHRUCharacteristicLandUseCodeID { get; set; }
    }

}