using LtInfo.Common.Views;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAttributeTypeSimple
    {
        public int TreatmentBMPAttributeTypeID { get; set; }
        public string TreatmentBMPAttributeTypeName { get; set; }   
        public string DataTypeDisplayName { get; set; }   
        public string MeasurementUnitDisplayName { get; set; }   
        public bool IsRequired { get; set; }
        public string Description { get; set; }

        public TreatmentBMPAttributeTypeSimple(TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            TreatmentBMPAttributeTypeID = treatmentBMPAttributeType.TreatmentBMPAttributeTypeID;
            TreatmentBMPAttributeTypeName = $"{treatmentBMPAttributeType.TreatmentBMPAttributeTypeName}";
            DataTypeDisplayName = treatmentBMPAttributeType.TreatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName;
            MeasurementUnitDisplayName = treatmentBMPAttributeType.GetMeasurementUnitDisplayName();
            IsRequired = treatmentBMPAttributeType.IsRequired;
            Description = treatmentBMPAttributeType.TreatmentBMPAttributeTypeDescription;
        }
    }
}