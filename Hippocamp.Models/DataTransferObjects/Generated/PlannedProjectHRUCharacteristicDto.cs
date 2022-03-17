//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectHRUCharacteristic]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PlannedProjectHRUCharacteristicDto
    {
        public int PlannedProjectHRUCharacteristicID { get; set; }
        public ProjectDto Project { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public HRUCharacteristicLandUseCodeDto HRUCharacteristicLandUseCode { get; set; }
        public PlannedProjectLoadGeneratingUnitDto PlannedProjectLoadGeneratingUnit { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public HRUCharacteristicLandUseCodeDto BaselineHRUCharacteristicLandUseCode { get; set; }
    }

    public partial class PlannedProjectHRUCharacteristicSimpleDto
    {
        public int PlannedProjectHRUCharacteristicID { get; set; }
        public int ProjectID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }
    }

}