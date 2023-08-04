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
    public abstract partial class WaterQualityManagementPlanStatus
    {
        public static readonly WaterQualityManagementPlanStatusInitialAnnualVerification InitialAnnualVerification = Neptune.EFModels.Entities.WaterQualityManagementPlanStatusInitialAnnualVerification.Instance;
        public static readonly WaterQualityManagementPlanStatusFollowupVerification FollowupVerification = Neptune.EFModels.Entities.WaterQualityManagementPlanStatusFollowupVerification.Instance;

        public static readonly List<WaterQualityManagementPlanStatus> All;
        public static readonly List<WaterQualityManagementPlanStatusDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanStatus> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanStatusDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanStatus()
        {
            All = new List<WaterQualityManagementPlanStatus> { InitialAnnualVerification, FollowupVerification };
            AllAsDto = new List<WaterQualityManagementPlanStatusDto> { InitialAnnualVerification.AsDto(), FollowupVerification.AsDto() };
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
                case WaterQualityManagementPlanStatusEnum.FollowupVerification:
                    return FollowupVerification;
                case WaterQualityManagementPlanStatusEnum.InitialAnnualVerification:
                    return InitialAnnualVerification;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum WaterQualityManagementPlanStatusEnum
    {
        InitialAnnualVerification = 1,
        FollowupVerification = 2
    }

    public partial class WaterQualityManagementPlanStatusInitialAnnualVerification : WaterQualityManagementPlanStatus
    {
        private WaterQualityManagementPlanStatusInitialAnnualVerification(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName) {}
        public static readonly WaterQualityManagementPlanStatusInitialAnnualVerification Instance = new WaterQualityManagementPlanStatusInitialAnnualVerification(1, @"Initial Annual Verification", @"Initial Annual Verification");
    }

    public partial class WaterQualityManagementPlanStatusFollowupVerification : WaterQualityManagementPlanStatus
    {
        private WaterQualityManagementPlanStatusFollowupVerification(int waterQualityManagementPlanStatusID, string waterQualityManagementPlanStatusName, string waterQualityManagementPlanStatusDisplayName) : base(waterQualityManagementPlanStatusID, waterQualityManagementPlanStatusName, waterQualityManagementPlanStatusDisplayName) {}
        public static readonly WaterQualityManagementPlanStatusFollowupVerification Instance = new WaterQualityManagementPlanStatusFollowupVerification(2, @"Follow-up Verification", @"Follow-up Verification");
    }
}