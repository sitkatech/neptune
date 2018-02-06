namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAttributeDataType
    {
        public abstract bool ValueIsCorrectDataType(string treatmentBMPAttributeValue);
    }

    public partial class TreatmentBMPAttributeDataTypeString
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return true;
        }
    }

    public partial class TreatmentBMPAttributeDataTypeInteger
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return int.TryParse(treatmentBMPAttributeValue, out var _);
        }
    }

    public partial class TreatmentBMPAttributeDataTypeDecimal
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return decimal.TryParse(treatmentBMPAttributeValue, out var _);
        }
    }

    public partial class TreatmentBMPAttributeDataTypeDateTime
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return System.DateTime.TryParse(treatmentBMPAttributeValue, out var _);
        }
    }

}