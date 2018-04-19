namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAttributeDataType
    {
        public abstract bool ValueIsCorrectDataType(string treatmentBMPAttributeValue);
        public abstract bool HasOptions();
        public abstract bool HasMeasurementUnit();
    }

    public partial class TreatmentBMPAttributeDataTypeString
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return true;
        }

        public override bool HasOptions()
        {
            return false;
        }

        public override bool HasMeasurementUnit()
        {
            return false;
        }
    }

    public partial class TreatmentBMPAttributeDataTypeInteger
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return int.TryParse(treatmentBMPAttributeValue, out var _);
        }

        public override bool HasOptions()
        {
            return false;
        }

        public override bool HasMeasurementUnit()
        {
            return true;
        }
    }

    public partial class TreatmentBMPAttributeDataTypeDecimal
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return decimal.TryParse(treatmentBMPAttributeValue, out var _);
        }

        public override bool HasOptions()
        {
            return false;
        }

        public override bool HasMeasurementUnit()
        {
            return true;
        }
    }

    public partial class TreatmentBMPAttributeDataTypeDateTime
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return System.DateTime.TryParse(treatmentBMPAttributeValue, out var _);
        }

        public override bool HasOptions()
        {
            return false;
        }

        public override bool HasMeasurementUnit()
        {
            return false;
        }
    }

    public partial class TreatmentBMPAttributeDataTypePickFromList
    {
        public override bool ValueIsCorrectDataType(string treatmentBMPAttributeValue)
        {
            return true;
        }

        public override bool HasOptions()
        {
            return true;
        }

        public override bool HasMeasurementUnit()
        {
            return false;
        }
    }

}