//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeTypePurpose]
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
    public abstract partial class CustomAttributeTypePurpose : IHavePrimaryKey
    {
        public static readonly CustomAttributeTypePurposePerformanceAndModelingAttributes PerformanceAndModelingAttributes = CustomAttributeTypePurposePerformanceAndModelingAttributes.Instance;
        public static readonly CustomAttributeTypePurposeOtherDesignAttributes OtherDesignAttributes = CustomAttributeTypePurposeOtherDesignAttributes.Instance;
        public static readonly CustomAttributeTypePurposeMaintenance Maintenance = CustomAttributeTypePurposeMaintenance.Instance;

        public static readonly List<CustomAttributeTypePurpose> All;
        public static readonly ReadOnlyDictionary<int, CustomAttributeTypePurpose> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static CustomAttributeTypePurpose()
        {
            All = new List<CustomAttributeTypePurpose> { PerformanceAndModelingAttributes, OtherDesignAttributes, Maintenance };
            AllLookupDictionary = new ReadOnlyDictionary<int, CustomAttributeTypePurpose>(All.ToDictionary(x => x.CustomAttributeTypePurposeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected CustomAttributeTypePurpose(int customAttributeTypePurposeID, string customAttributeTypePurposeName, string customAttributeTypePurposeDisplayName)
        {
            CustomAttributeTypePurposeID = customAttributeTypePurposeID;
            CustomAttributeTypePurposeName = customAttributeTypePurposeName;
            CustomAttributeTypePurposeDisplayName = customAttributeTypePurposeDisplayName;
        }

        [Key]
        public int CustomAttributeTypePurposeID { get; private set; }
        public string CustomAttributeTypePurposeName { get; private set; }
        public string CustomAttributeTypePurposeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return CustomAttributeTypePurposeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(CustomAttributeTypePurpose other)
        {
            if (other == null)
            {
                return false;
            }
            return other.CustomAttributeTypePurposeID == CustomAttributeTypePurposeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as CustomAttributeTypePurpose);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return CustomAttributeTypePurposeID;
        }

        public static bool operator ==(CustomAttributeTypePurpose left, CustomAttributeTypePurpose right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomAttributeTypePurpose left, CustomAttributeTypePurpose right)
        {
            return !Equals(left, right);
        }

        public CustomAttributeTypePurposeEnum ToEnum { get { return (CustomAttributeTypePurposeEnum)GetHashCode(); } }

        public static CustomAttributeTypePurpose ToType(int enumValue)
        {
            return ToType((CustomAttributeTypePurposeEnum)enumValue);
        }

        public static CustomAttributeTypePurpose ToType(CustomAttributeTypePurposeEnum enumValue)
        {
            switch (enumValue)
            {
                case CustomAttributeTypePurposeEnum.Maintenance:
                    return Maintenance;
                case CustomAttributeTypePurposeEnum.OtherDesignAttributes:
                    return OtherDesignAttributes;
                case CustomAttributeTypePurposeEnum.PerformanceAndModelingAttributes:
                    return PerformanceAndModelingAttributes;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum CustomAttributeTypePurposeEnum
    {
        PerformanceAndModelingAttributes = 1,
        OtherDesignAttributes = 2,
        Maintenance = 3
    }

    public partial class CustomAttributeTypePurposePerformanceAndModelingAttributes : CustomAttributeTypePurpose
    {
        private CustomAttributeTypePurposePerformanceAndModelingAttributes(int customAttributeTypePurposeID, string customAttributeTypePurposeName, string customAttributeTypePurposeDisplayName) : base(customAttributeTypePurposeID, customAttributeTypePurposeName, customAttributeTypePurposeDisplayName) {}
        public static readonly CustomAttributeTypePurposePerformanceAndModelingAttributes Instance = new CustomAttributeTypePurposePerformanceAndModelingAttributes(1, @"PerformanceAndModelingAttributes", @"Performance / Modeling Attributes");
    }

    public partial class CustomAttributeTypePurposeOtherDesignAttributes : CustomAttributeTypePurpose
    {
        private CustomAttributeTypePurposeOtherDesignAttributes(int customAttributeTypePurposeID, string customAttributeTypePurposeName, string customAttributeTypePurposeDisplayName) : base(customAttributeTypePurposeID, customAttributeTypePurposeName, customAttributeTypePurposeDisplayName) {}
        public static readonly CustomAttributeTypePurposeOtherDesignAttributes Instance = new CustomAttributeTypePurposeOtherDesignAttributes(2, @"OtherDesignAttributes", @"Other Design Attributes");
    }

    public partial class CustomAttributeTypePurposeMaintenance : CustomAttributeTypePurpose
    {
        private CustomAttributeTypePurposeMaintenance(int customAttributeTypePurposeID, string customAttributeTypePurposeName, string customAttributeTypePurposeDisplayName) : base(customAttributeTypePurposeID, customAttributeTypePurposeName, customAttributeTypePurposeDisplayName) {}
        public static readonly CustomAttributeTypePurposeMaintenance Instance = new CustomAttributeTypePurposeMaintenance(3, @"Maintenance", @"Maintenance Attributes");
    }
}