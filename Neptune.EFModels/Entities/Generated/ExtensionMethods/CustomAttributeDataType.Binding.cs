//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeDataType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class CustomAttributeDataType : IHavePrimaryKey
    {
        public static readonly CustomAttributeDataTypeString String = Neptune.EFModels.Entities.CustomAttributeDataTypeString.Instance;
        public static readonly CustomAttributeDataTypeInteger Integer = Neptune.EFModels.Entities.CustomAttributeDataTypeInteger.Instance;
        public static readonly CustomAttributeDataTypeDecimal Decimal = Neptune.EFModels.Entities.CustomAttributeDataTypeDecimal.Instance;
        public static readonly CustomAttributeDataTypeDateTime DateTime = Neptune.EFModels.Entities.CustomAttributeDataTypeDateTime.Instance;
        public static readonly CustomAttributeDataTypePickFromList PickFromList = Neptune.EFModels.Entities.CustomAttributeDataTypePickFromList.Instance;
        public static readonly CustomAttributeDataTypeMultiSelect MultiSelect = Neptune.EFModels.Entities.CustomAttributeDataTypeMultiSelect.Instance;

        public static readonly List<CustomAttributeDataType> All;
        public static readonly List<CustomAttributeDataTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, CustomAttributeDataType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, CustomAttributeDataTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static CustomAttributeDataType()
        {
            All = new List<CustomAttributeDataType> { String, Integer, Decimal, DateTime, PickFromList, MultiSelect };
            AllAsDto = new List<CustomAttributeDataTypeDto> { String.AsDto(), Integer.AsDto(), Decimal.AsDto(), DateTime.AsDto(), PickFromList.AsDto(), MultiSelect.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, CustomAttributeDataType>(All.ToDictionary(x => x.CustomAttributeDataTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, CustomAttributeDataTypeDto>(AllAsDto.ToDictionary(x => x.CustomAttributeDataTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected CustomAttributeDataType(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName)
        {
            CustomAttributeDataTypeID = customAttributeDataTypeID;
            CustomAttributeDataTypeName = customAttributeDataTypeName;
            CustomAttributeDataTypeDisplayName = customAttributeDataTypeDisplayName;
        }

        [Key]
        public int CustomAttributeDataTypeID { get; private set; }
        public string CustomAttributeDataTypeName { get; private set; }
        public string CustomAttributeDataTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return CustomAttributeDataTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(CustomAttributeDataType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.CustomAttributeDataTypeID == CustomAttributeDataTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as CustomAttributeDataType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return CustomAttributeDataTypeID;
        }

        public static bool operator ==(CustomAttributeDataType left, CustomAttributeDataType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomAttributeDataType left, CustomAttributeDataType right)
        {
            return !Equals(left, right);
        }

        public CustomAttributeDataTypeEnum ToEnum => (CustomAttributeDataTypeEnum)GetHashCode();

        public static CustomAttributeDataType ToType(int enumValue)
        {
            return ToType((CustomAttributeDataTypeEnum)enumValue);
        }

        public static CustomAttributeDataType ToType(CustomAttributeDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case CustomAttributeDataTypeEnum.DateTime:
                    return DateTime;
                case CustomAttributeDataTypeEnum.Decimal:
                    return Decimal;
                case CustomAttributeDataTypeEnum.Integer:
                    return Integer;
                case CustomAttributeDataTypeEnum.MultiSelect:
                    return MultiSelect;
                case CustomAttributeDataTypeEnum.PickFromList:
                    return PickFromList;
                case CustomAttributeDataTypeEnum.String:
                    return String;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum CustomAttributeDataTypeEnum
    {
        String = 1,
        Integer = 2,
        Decimal = 3,
        DateTime = 4,
        PickFromList = 5,
        MultiSelect = 6
    }

    public partial class CustomAttributeDataTypeString : CustomAttributeDataType
    {
        private CustomAttributeDataTypeString(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypeString Instance = new CustomAttributeDataTypeString(1, @"String", @"String");
    }

    public partial class CustomAttributeDataTypeInteger : CustomAttributeDataType
    {
        private CustomAttributeDataTypeInteger(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypeInteger Instance = new CustomAttributeDataTypeInteger(2, @"Integer", @"Integer");
    }

    public partial class CustomAttributeDataTypeDecimal : CustomAttributeDataType
    {
        private CustomAttributeDataTypeDecimal(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypeDecimal Instance = new CustomAttributeDataTypeDecimal(3, @"Decimal", @"Decimal");
    }

    public partial class CustomAttributeDataTypeDateTime : CustomAttributeDataType
    {
        private CustomAttributeDataTypeDateTime(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypeDateTime Instance = new CustomAttributeDataTypeDateTime(4, @"DateTime", @"Date/Time");
    }

    public partial class CustomAttributeDataTypePickFromList : CustomAttributeDataType
    {
        private CustomAttributeDataTypePickFromList(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypePickFromList Instance = new CustomAttributeDataTypePickFromList(5, @"PickFromList", @"Pick One from List");
    }

    public partial class CustomAttributeDataTypeMultiSelect : CustomAttributeDataType
    {
        private CustomAttributeDataTypeMultiSelect(int customAttributeDataTypeID, string customAttributeDataTypeName, string customAttributeDataTypeDisplayName) : base(customAttributeDataTypeID, customAttributeDataTypeName, customAttributeDataTypeDisplayName) {}
        public static readonly CustomAttributeDataTypeMultiSelect Instance = new CustomAttributeDataTypeMultiSelect(6, @"MultiSelect", @"Select Many from List");
    }
}