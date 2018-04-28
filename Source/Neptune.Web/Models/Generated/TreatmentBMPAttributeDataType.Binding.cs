//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeDataType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class TreatmentBMPAttributeDataType : IHavePrimaryKey
    {
        public static readonly TreatmentBMPAttributeDataTypeString String = TreatmentBMPAttributeDataTypeString.Instance;
        public static readonly TreatmentBMPAttributeDataTypeInteger Integer = TreatmentBMPAttributeDataTypeInteger.Instance;
        public static readonly TreatmentBMPAttributeDataTypeDecimal Decimal = TreatmentBMPAttributeDataTypeDecimal.Instance;
        public static readonly TreatmentBMPAttributeDataTypeDateTime DateTime = TreatmentBMPAttributeDataTypeDateTime.Instance;
        public static readonly TreatmentBMPAttributeDataTypePickFromList PickFromList = TreatmentBMPAttributeDataTypePickFromList.Instance;
        public static readonly TreatmentBMPAttributeDataTypeMultiSelect MultiSelect = TreatmentBMPAttributeDataTypeMultiSelect.Instance;

        public static readonly List<TreatmentBMPAttributeDataType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPAttributeDataType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPAttributeDataType()
        {
            All = new List<TreatmentBMPAttributeDataType> { String, Integer, Decimal, DateTime, PickFromList, MultiSelect };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPAttributeDataType>(All.ToDictionary(x => x.TreatmentBMPAttributeDataTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPAttributeDataType(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName)
        {
            TreatmentBMPAttributeDataTypeID = treatmentBMPAttributeDataTypeID;
            TreatmentBMPAttributeDataTypeName = treatmentBMPAttributeDataTypeName;
            TreatmentBMPAttributeDataTypeDisplayName = treatmentBMPAttributeDataTypeDisplayName;
        }

        [Key]
        public int TreatmentBMPAttributeDataTypeID { get; private set; }
        public string TreatmentBMPAttributeDataTypeName { get; private set; }
        public string TreatmentBMPAttributeDataTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAttributeDataTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPAttributeDataType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPAttributeDataTypeID == TreatmentBMPAttributeDataTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPAttributeDataType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPAttributeDataTypeID;
        }

        public static bool operator ==(TreatmentBMPAttributeDataType left, TreatmentBMPAttributeDataType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPAttributeDataType left, TreatmentBMPAttributeDataType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPAttributeDataTypeEnum ToEnum { get { return (TreatmentBMPAttributeDataTypeEnum)GetHashCode(); } }

        public static TreatmentBMPAttributeDataType ToType(int enumValue)
        {
            return ToType((TreatmentBMPAttributeDataTypeEnum)enumValue);
        }

        public static TreatmentBMPAttributeDataType ToType(TreatmentBMPAttributeDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentBMPAttributeDataTypeEnum.DateTime:
                    return DateTime;
                case TreatmentBMPAttributeDataTypeEnum.Decimal:
                    return Decimal;
                case TreatmentBMPAttributeDataTypeEnum.Integer:
                    return Integer;
                case TreatmentBMPAttributeDataTypeEnum.MultiSelect:
                    return MultiSelect;
                case TreatmentBMPAttributeDataTypeEnum.PickFromList:
                    return PickFromList;
                case TreatmentBMPAttributeDataTypeEnum.String:
                    return String;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPAttributeDataTypeEnum
    {
        String = 1,
        Integer = 2,
        Decimal = 3,
        DateTime = 4,
        PickFromList = 5,
        MultiSelect = 6
    }

    public partial class TreatmentBMPAttributeDataTypeString : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypeString(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypeString Instance = new TreatmentBMPAttributeDataTypeString(1, @"String", @"String");
    }

    public partial class TreatmentBMPAttributeDataTypeInteger : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypeInteger(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypeInteger Instance = new TreatmentBMPAttributeDataTypeInteger(2, @"Integer", @"Integer");
    }

    public partial class TreatmentBMPAttributeDataTypeDecimal : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypeDecimal(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypeDecimal Instance = new TreatmentBMPAttributeDataTypeDecimal(3, @"Decimal", @"Decimal");
    }

    public partial class TreatmentBMPAttributeDataTypeDateTime : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypeDateTime(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypeDateTime Instance = new TreatmentBMPAttributeDataTypeDateTime(4, @"DateTime", @"Date/Time");
    }

    public partial class TreatmentBMPAttributeDataTypePickFromList : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypePickFromList(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypePickFromList Instance = new TreatmentBMPAttributeDataTypePickFromList(5, @"PickFromList", @"Pick One from List");
    }

    public partial class TreatmentBMPAttributeDataTypeMultiSelect : TreatmentBMPAttributeDataType
    {
        private TreatmentBMPAttributeDataTypeMultiSelect(int treatmentBMPAttributeDataTypeID, string treatmentBMPAttributeDataTypeName, string treatmentBMPAttributeDataTypeDisplayName) : base(treatmentBMPAttributeDataTypeID, treatmentBMPAttributeDataTypeName, treatmentBMPAttributeDataTypeDisplayName) {}
        public static readonly TreatmentBMPAttributeDataTypeMultiSelect Instance = new TreatmentBMPAttributeDataTypeMultiSelect(6, @"MultiSelect", @"Select Many from List");
    }
}