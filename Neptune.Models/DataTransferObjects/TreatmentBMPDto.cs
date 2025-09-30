using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPDto
    {
        // Basic Info
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string StormwaterJurisdictionName { get; set; }
        public int? OwnerOrganizationID { get; set; }
        public string OwnerOrganizationName { get; set; }
        public int? YearBuilt { get; set; }
        public string Notes { get; set; }
        public bool InventoryIsVerified { get; set; }
        public int? ProjectID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Modeling/Parameterization
        public bool IsFullyParameterized { get; set; }
        public bool HasModelingAttributes { get; set; }
        public string TrashCaptureEffectiveness { get; set; }
        public vTreatmentBMPModelingAttributeDto TreatmentBMPModelingAttribute { get; set; }

        // HRU Characteristics
        public object HRUCharacteristics { get; set; } // Replace with actual DTO if available
        public object ModeledPerformance { get; set; } // Replace with actual DTO if available

        // Related Entities
        public bool OtherTreatmentBmpsExistInSubbasin { get; set; }
        public List<CustomAttributeUpsertDto> CustomAttributes { get; set; }
        public object Delineation { get; set; } // Replace with actual DTO if available
        public object UpstreamestBMP { get; set; } // Replace with actual DTO if available
        public bool IsUpstreamestBMPAnalyzedInModelingModule { get; set; }
        public object RegionalSubbasinRevisionRequest { get; set; } // Replace with actual DTO if available
        public object Watershed { get; set; } // Replace with actual DTO if available
        public string WatershedFieldDefinitionText { get; set; }

        // Errors
        public List<string> DelineationErrors { get; set; }
        public List<string> ParameterizationErrors { get; set; }
    }
}
