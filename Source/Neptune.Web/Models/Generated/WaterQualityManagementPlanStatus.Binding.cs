//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]
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
    public abstract partial class WaterQualityManagementPlanStatus : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanStatusActive Active = WaterQualityManagementPlanStatusActive.Instance;
        public static readonly WaterQualityManagementPlanStatusInactive Inactive = WaterQualityManagementPlanStatusInactive.Instance;

        public static readonly List<WaterQualityManagementPlanStatus> All;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanStatus> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanStatus()
        {
            All = new List<WaterQualityManagementPlanStatus> { Active, Inactive };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanStatus>(All.ToDictionary(x => x.WaterQualityManagementPlanStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanStatus(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName, int sortOrder)
        {
            WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatusID;
            WaterQualityManagementPlanStatusName = waterQualityManagementPlanStatusName;
            WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatusDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int WaterQualityManagementPlanStatusID { get; private set; }
        public string WaterQualityManagementPlanStatusName { get; private set; }
        public string WaterQualityManagementPlanStatusDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanStatusID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanStatus other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanStatusID == WaterQualityManagementPlanStatusID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanStatus);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanStatusID;
        }

        public static bool operator ==(WaterQualityManagementPlanStatus left, WaterQualityManagementPlanStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanStatus left, WaterQualityManagementPlanStatus right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanStatusEnum ToEnum { get { return (WaterQualityManagementPlanStatusEnum)GetHashCode(); } }

        public static WaterQualityManagementPlanStatus ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanStatusEnum)enumValue);
        }

        public static WaterQualityManagementPlanStatus ToType(WaterQualityManagementPlanStatusEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanStatusEnum.Active:
                    return Active;
                case WaterQualityManagementPlanStatusEnum.Inactive:
                    return Inactive;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum WaterQualityManagementPlanStatusEnum
    {
        Active = 1,
        Inactive = 2
    }

    public partial class WaterQualityManagementPlanStatusActive : WaterQualityManagementPlanStatus
    {
        private WaterQualityManagementPlanStatusActive(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName, int sortOrder) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanStatusActive Instance = new WaterQualityManagementPlanStatusActive(1, @"Active", @"Active", 10);
    }

    public partial class WaterQualityManagementPlanStatusInactive : WaterQualityManagementPlanStatus
    {
        private WaterQualityManagementPlanStatusInactive(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName, int sortOrder) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanStatusInactive Instance = new WaterQualityManagementPlanStatusInactive(2, @"Inactive", @"Inactive", 20);
    }
}