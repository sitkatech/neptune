//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydromodificationApplies]
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
    public abstract partial class HydromodificationApplies : IHavePrimaryKey
    {
        public static readonly HydromodificationAppliesApplicable Applicable = HydromodificationAppliesApplicable.Instance;
        public static readonly HydromodificationAppliesExempt Exempt = HydromodificationAppliesExempt.Instance;

        public static readonly List<HydromodificationApplies> All;
        public static readonly ReadOnlyDictionary<int, HydromodificationApplies> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static HydromodificationApplies()
        {
            All = new List<HydromodificationApplies> { Applicable, Exempt };
            AllLookupDictionary = new ReadOnlyDictionary<int, HydromodificationApplies>(All.ToDictionary(x => x.HydromodificationAppliesID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected HydromodificationApplies(int hydromodificationAppliesID, string hydromodificationAppliesName, string hydromodificationAppliesDisplayName, int sortOrder)
        {
            HydromodificationAppliesID = hydromodificationAppliesID;
            HydromodificationAppliesName = hydromodificationAppliesName;
            HydromodificationAppliesDisplayName = hydromodificationAppliesDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int HydromodificationAppliesID { get; private set; }
        public string HydromodificationAppliesName { get; private set; }
        public string HydromodificationAppliesDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return HydromodificationAppliesID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(HydromodificationApplies other)
        {
            if (other == null)
            {
                return false;
            }
            return other.HydromodificationAppliesID == HydromodificationAppliesID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as HydromodificationApplies);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return HydromodificationAppliesID;
        }

        public static bool operator ==(HydromodificationApplies left, HydromodificationApplies right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HydromodificationApplies left, HydromodificationApplies right)
        {
            return !Equals(left, right);
        }

        public HydromodificationAppliesEnum ToEnum { get { return (HydromodificationAppliesEnum)GetHashCode(); } }

        public static HydromodificationApplies ToType(int enumValue)
        {
            return ToType((HydromodificationAppliesEnum)enumValue);
        }

        public static HydromodificationApplies ToType(HydromodificationAppliesEnum enumValue)
        {
            switch (enumValue)
            {
                case HydromodificationAppliesEnum.Applicable:
                    return Applicable;
                case HydromodificationAppliesEnum.Exempt:
                    return Exempt;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum HydromodificationAppliesEnum
    {
        Applicable = 1,
        Exempt = 2
    }

    public partial class HydromodificationAppliesApplicable : HydromodificationApplies
    {
        private HydromodificationAppliesApplicable(int hydromodificationAppliesID, string hydromodificationAppliesName, string hydromodificationAppliesDisplayName, int sortOrder) : base(hydromodificationAppliesID, hydromodificationAppliesName, hydromodificationAppliesDisplayName, sortOrder) {}
        public static readonly HydromodificationAppliesApplicable Instance = new HydromodificationAppliesApplicable(1, @"Applicable ", @"Applicable", 10);
    }

    public partial class HydromodificationAppliesExempt : HydromodificationApplies
    {
        private HydromodificationAppliesExempt(int hydromodificationAppliesID, string hydromodificationAppliesName, string hydromodificationAppliesDisplayName, int sortOrder) : base(hydromodificationAppliesID, hydromodificationAppliesName, hydromodificationAppliesDisplayName, sortOrder) {}
        public static readonly HydromodificationAppliesExempt Instance = new HydromodificationAppliesExempt(2, @"Exempt", @"Exempt", 20);
    }
}