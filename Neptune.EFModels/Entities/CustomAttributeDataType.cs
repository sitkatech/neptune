using System;

namespace Neptune.EFModels.Entities
{
    public partial class CustomAttributeDataType
    {
        public abstract bool ValueIsCorrectDataType(string customAttributeValue);
        public abstract string ValueParsedForDataType(string customAttributeValue);
        public abstract bool HasOptions();
        public abstract bool HasMeasurementUnit();
    }

    public partial class CustomAttributeDataTypeString
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return true;
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            return customAttributeValue;
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

    public partial class CustomAttributeDataTypeInteger
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return int.TryParse(customAttributeValue, out _);
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            if (string.IsNullOrWhiteSpace(customAttributeValue))
            {
                return customAttributeValue;
            }

            if (int.TryParse(customAttributeValue, out var @_))
            {
                return _.ToString();
            }

            throw new ArgumentOutOfRangeException("Attribute value is of an incorrect data type");

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

    public partial class CustomAttributeDataTypeDecimal
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return decimal.TryParse(customAttributeValue, out _);
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            if (string.IsNullOrWhiteSpace(customAttributeValue))
            {
                return customAttributeValue;
            }

            if (decimal.TryParse(customAttributeValue, out var @_))
            {
                return _.ToString();
            }

            throw new ArgumentOutOfRangeException("Attribute value is of an incorrect data type");
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

    public partial class CustomAttributeDataTypeDateTime
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return System.DateTime.TryParse(customAttributeValue, out _);
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            if (string.IsNullOrWhiteSpace(customAttributeValue))
            {
                return customAttributeValue;
            }

            if (System.DateTime.TryParse(customAttributeValue, out var @_))
            {
                return _.ToString();
            }

            throw new ArgumentOutOfRangeException("Attribute value is of an incorrect data type");
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

    public partial class CustomAttributeDataTypePickFromList
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return true;
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            return customAttributeValue;
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

    public partial class CustomAttributeDataTypeMultiSelect
    {
        public override bool ValueIsCorrectDataType(string customAttributeValue)
        {
            return true;
        }

        public override string ValueParsedForDataType(string customAttributeValue)
        {
            return customAttributeValue;
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