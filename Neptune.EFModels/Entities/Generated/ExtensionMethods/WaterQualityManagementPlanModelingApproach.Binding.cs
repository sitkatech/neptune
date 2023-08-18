//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanModelingApproach]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class WaterQualityManagementPlanModelingApproach : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanModelingApproachDetailed Detailed = Neptune.EFModels.Entities.WaterQualityManagementPlanModelingApproachDetailed.Instance;
        public static readonly WaterQualityManagementPlanModelingApproachSimplified Simplified = Neptune.EFModels.Entities.WaterQualityManagementPlanModelingApproachSimplified.Instance;

        public static readonly List<WaterQualityManagementPlanModelingApproach> All;
        public static readonly List<WaterQualityManagementPlanModelingApproachDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanModelingApproach> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanModelingApproachDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanModelingApproach()
        {
            All = new List<WaterQualityManagementPlanModelingApproach> { Detailed, Simplified };
            AllAsDto = new List<WaterQualityManagementPlanModelingApproachDto> { Detailed.AsDto(), Simplified.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanModelingApproach>(All.ToDictionary(x => x.WaterQualityManagementPlanModelingApproachID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanModelingApproachDto>(AllAsDto.ToDictionary(x => x.WaterQualityManagementPlanModelingApproachID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanModelingApproach(int waterQualityManagementPlanModelingApproachID, string waterQualityManagementPlanModelingApproachName, string waterQualityManagementPlanModelingApproachDisplayName, string waterQualityManagementPlanModelingApproachDescription)
        {
            WaterQualityManagementPlanModelingApproachID = waterQualityManagementPlanModelingApproachID;
            WaterQualityManagementPlanModelingApproachName = waterQualityManagementPlanModelingApproachName;
            WaterQualityManagementPlanModelingApproachDisplayName = waterQualityManagementPlanModelingApproachDisplayName;
            WaterQualityManagementPlanModelingApproachDescription = waterQualityManagementPlanModelingApproachDescription;
        }

        [Key]
        public int WaterQualityManagementPlanModelingApproachID { get; private set; }
        public string WaterQualityManagementPlanModelingApproachName { get; private set; }
        public string WaterQualityManagementPlanModelingApproachDisplayName { get; private set; }
        public string WaterQualityManagementPlanModelingApproachDescription { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanModelingApproachID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanModelingApproach other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanModelingApproachID == WaterQualityManagementPlanModelingApproachID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanModelingApproach);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanModelingApproachID;
        }

        public static bool operator ==(WaterQualityManagementPlanModelingApproach left, WaterQualityManagementPlanModelingApproach right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanModelingApproach left, WaterQualityManagementPlanModelingApproach right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanModelingApproachEnum ToEnum => (WaterQualityManagementPlanModelingApproachEnum)GetHashCode();

        public static WaterQualityManagementPlanModelingApproach ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanModelingApproachEnum)enumValue);
        }

        public static WaterQualityManagementPlanModelingApproach ToType(WaterQualityManagementPlanModelingApproachEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanModelingApproachEnum.Detailed:
                    return Detailed;
                case WaterQualityManagementPlanModelingApproachEnum.Simplified:
                    return Simplified;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum WaterQualityManagementPlanModelingApproachEnum
    {
        Detailed = 1,
        Simplified = 2
    }

    public partial class WaterQualityManagementPlanModelingApproachDetailed : WaterQualityManagementPlanModelingApproach
    {
        private WaterQualityManagementPlanModelingApproachDetailed(int waterQualityManagementPlanModelingApproachID, string waterQualityManagementPlanModelingApproachName, string waterQualityManagementPlanModelingApproachDisplayName, string waterQualityManagementPlanModelingApproachDescription) : base(waterQualityManagementPlanModelingApproachID, waterQualityManagementPlanModelingApproachName, waterQualityManagementPlanModelingApproachDisplayName, waterQualityManagementPlanModelingApproachDescription) {}
        public static readonly WaterQualityManagementPlanModelingApproachDetailed Instance = new WaterQualityManagementPlanModelingApproachDetailed(1, @"Detailed", @"Detailed", @"This WQMP is modeled by inventorying the associated structural BMPs and defining their delineations. The performance of each BMP is modeled based on its modeling parameters and the attributes of the delineated tributary area.");
    }

    public partial class WaterQualityManagementPlanModelingApproachSimplified : WaterQualityManagementPlanModelingApproach
    {
        private WaterQualityManagementPlanModelingApproachSimplified(int waterQualityManagementPlanModelingApproachID, string waterQualityManagementPlanModelingApproachName, string waterQualityManagementPlanModelingApproachDisplayName, string waterQualityManagementPlanModelingApproachDescription) : base(waterQualityManagementPlanModelingApproachID, waterQualityManagementPlanModelingApproachName, waterQualityManagementPlanModelingApproachDisplayName, waterQualityManagementPlanModelingApproachDescription) {}
        public static readonly WaterQualityManagementPlanModelingApproachSimplified Instance = new WaterQualityManagementPlanModelingApproachSimplified(2, @"Simplified", @"Simplified", @"This WQMP is modeled by entering simplified structural BMP modeling parameters directly on this WQMP page.");
    }
}