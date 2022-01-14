//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydromodificationAppliesType]
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
    public abstract partial class HydromodificationAppliesType : IHavePrimaryKey
    {
        public static readonly HydromodificationAppliesTypeApplicable Applicable = HydromodificationAppliesTypeApplicable.Instance;
        public static readonly HydromodificationAppliesTypeExempt Exempt = HydromodificationAppliesTypeExempt.Instance;

        public static readonly List<HydromodificationAppliesType> All;
        public static readonly ReadOnlyDictionary<int, HydromodificationAppliesType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static HydromodificationAppliesType()
        {
            All = new List<HydromodificationAppliesType> { Applicable, Exempt };
            AllLookupDictionary = new ReadOnlyDictionary<int, HydromodificationAppliesType>(All.ToDictionary(x => x.HydromodificationAppliesTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected HydromodificationAppliesType(int hydromodificationAppliesTypeID, string hydromodificationAppliesTypeName, string hydromodificationAppliesTypeDisplayName, int sortOrder)
        {
            HydromodificationAppliesTypeID = hydromodificationAppliesTypeID;
            HydromodificationAppliesTypeName = hydromodificationAppliesTypeName;
            HydromodificationAppliesTypeDisplayName = hydromodificationAppliesTypeDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int HydromodificationAppliesTypeID { get; private set; }
        public string HydromodificationAppliesTypeName { get; private set; }
        public string HydromodificationAppliesTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return HydromodificationAppliesTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(HydromodificationAppliesType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.HydromodificationAppliesTypeID == HydromodificationAppliesTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as HydromodificationAppliesType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return HydromodificationAppliesTypeID;
        }

        public static bool operator ==(HydromodificationAppliesType left, HydromodificationAppliesType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HydromodificationAppliesType left, HydromodificationAppliesType right)
        {
            return !Equals(left, right);
        }

        public HydromodificationAppliesTypeEnum ToEnum { get { return (HydromodificationAppliesTypeEnum)GetHashCode(); } }

        public static HydromodificationAppliesType ToType(int enumValue)
        {
            return ToType((HydromodificationAppliesTypeEnum)enumValue);
        }

        public static HydromodificationAppliesType ToType(HydromodificationAppliesTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case HydromodificationAppliesTypeEnum.Applicable:
                    return Applicable;
                case HydromodificationAppliesTypeEnum.Exempt:
                    return Exempt;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum HydromodificationAppliesTypeEnum
    {
        Applicable = 1,
        Exempt = 2
    }

    public partial class HydromodificationAppliesTypeApplicable : HydromodificationAppliesType
    {
        private HydromodificationAppliesTypeApplicable(int hydromodificationAppliesTypeID, string hydromodificationAppliesTypeName, string hydromodificationAppliesTypeDisplayName, int sortOrder) : base(hydromodificationAppliesTypeID, hydromodificationAppliesTypeName, hydromodificationAppliesTypeDisplayName, sortOrder) {}
        public static readonly HydromodificationAppliesTypeApplicable Instance = new HydromodificationAppliesTypeApplicable(1, @"Applicable ", @"Applicable", 10);
    }

    public partial class HydromodificationAppliesTypeExempt : HydromodificationAppliesType
    {
        private HydromodificationAppliesTypeExempt(int hydromodificationAppliesTypeID, string hydromodificationAppliesTypeName, string hydromodificationAppliesTypeDisplayName, int sortOrder) : base(hydromodificationAppliesTypeID, hydromodificationAppliesTypeName, hydromodificationAppliesTypeDisplayName, sortOrder) {}
        public static readonly HydromodificationAppliesTypeExempt Instance = new HydromodificationAppliesTypeExempt(2, @"Exempt", @"Exempt", 20);
    }
}