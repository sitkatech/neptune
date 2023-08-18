//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanStatus]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class WaterQualityManagementPlanStatus : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanStatusActive Active = Neptune.EFModels.Entities.WaterQualityManagementPlanStatusActive.Instance;
        public static readonly WaterQualityManagementPlanStatusInactive Inactive = Neptune.EFModels.Entities.WaterQualityManagementPlanStatusInactive.Instance;

        public static readonly List<WaterQualityManagementPlanStatus> All;
        public static readonly List<WaterQualityManagementPlanStatusDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanStatus> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanStatusDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanStatus()
        {
            All = new List<WaterQualityManagementPlanStatus> { Active, Inactive };
            AllAsDto = new List<WaterQualityManagementPlanStatusDto> { Active.AsDto(), Inactive.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanStatus>(All.ToDictionary(x => x.WaterQualityManagementPlanStatusID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanStatusDto>(AllAsDto.ToDictionary(x => x.WaterQualityManagementPlanStatusID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanStatus(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName)
        {
            WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatusID;
            WaterQualityManagementPlanStatusName = waterQualityManagementPlanStatusName;
            WaterQualityManagementPlanStatusDisplayName = waterQualityManagementPlanStatusDisplayName;
        }

        [Key]
        public int WaterQualityManagementPlanStatusID { get; private set; }
        public string WaterQualityManagementPlanStatusName { get; private set; }
        public string WaterQualityManagementPlanStatusDisplayName { get; private set; }
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

        public WaterQualityManagementPlanStatusEnum ToEnum => (WaterQualityManagementPlanStatusEnum)GetHashCode();

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
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
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
        private WaterQualityManagementPlanStatusActive(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName) {}
        public static readonly WaterQualityManagementPlanStatusActive Instance = new WaterQualityManagementPlanStatusActive(1, @"Active", @"Active");
    }

    public partial class WaterQualityManagementPlanStatusInactive : WaterQualityManagementPlanStatus
    {
        private WaterQualityManagementPlanStatusInactive(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName) {}
        public static readonly WaterQualityManagementPlanStatusInactive Instance = new WaterQualityManagementPlanStatusInactive(2, @"Inactive", @"Inactive");
    }
}