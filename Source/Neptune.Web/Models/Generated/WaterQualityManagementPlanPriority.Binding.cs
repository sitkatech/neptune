//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]
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
    public abstract partial class WaterQualityManagementPlanPriority : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanPriorityHigh High = WaterQualityManagementPlanPriorityHigh.Instance;
        public static readonly WaterQualityManagementPlanPriorityLow Low = WaterQualityManagementPlanPriorityLow.Instance;

        public static readonly List<WaterQualityManagementPlanPriority> All;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanPriority> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanPriority()
        {
            All = new List<WaterQualityManagementPlanPriority> { High, Low };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanPriority>(All.ToDictionary(x => x.WaterQualityManagementPlanPriorityID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanPriority(int waterQualityManagementPlanPriorityID, string waterQualityManagementPlanPriorityName, string waterQualityManagementPlanPriorityDisplayName, int sortOrder)
        {
            WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriorityID;
            WaterQualityManagementPlanPriorityName = waterQualityManagementPlanPriorityName;
            WaterQualityManagementPlanPriorityDisplayName = waterQualityManagementPlanPriorityDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int WaterQualityManagementPlanPriorityID { get; private set; }
        public string WaterQualityManagementPlanPriorityName { get; private set; }
        public string WaterQualityManagementPlanPriorityDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanPriorityID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanPriority other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanPriorityID == WaterQualityManagementPlanPriorityID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanPriority);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanPriorityID;
        }

        public static bool operator ==(WaterQualityManagementPlanPriority left, WaterQualityManagementPlanPriority right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanPriority left, WaterQualityManagementPlanPriority right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanPriorityEnum ToEnum { get { return (WaterQualityManagementPlanPriorityEnum)GetHashCode(); } }

        public static WaterQualityManagementPlanPriority ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanPriorityEnum)enumValue);
        }

        public static WaterQualityManagementPlanPriority ToType(WaterQualityManagementPlanPriorityEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanPriorityEnum.High:
                    return High;
                case WaterQualityManagementPlanPriorityEnum.Low:
                    return Low;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum WaterQualityManagementPlanPriorityEnum
    {
        High = 1,
        Low = 2
    }

    public partial class WaterQualityManagementPlanPriorityHigh : WaterQualityManagementPlanPriority
    {
        private WaterQualityManagementPlanPriorityHigh(int waterQualityManagementPlanPriorityID, string waterQualityManagementPlanPriorityName, string waterQualityManagementPlanPriorityDisplayName, int sortOrder) : base(waterQualityManagementPlanPriorityID, waterQualityManagementPlanPriorityName, waterQualityManagementPlanPriorityDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPriorityHigh Instance = new WaterQualityManagementPlanPriorityHigh(1, @"High", @"High", 10);
    }

    public partial class WaterQualityManagementPlanPriorityLow : WaterQualityManagementPlanPriority
    {
        private WaterQualityManagementPlanPriorityLow(int waterQualityManagementPlanPriorityID, string waterQualityManagementPlanPriorityName, string waterQualityManagementPlanPriorityDisplayName, int sortOrder) : base(waterQualityManagementPlanPriorityID, waterQualityManagementPlanPriorityName, waterQualityManagementPlanPriorityDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPriorityLow Instance = new WaterQualityManagementPlanPriorityLow(2, @"Low", @"Low", 20);
    }
}