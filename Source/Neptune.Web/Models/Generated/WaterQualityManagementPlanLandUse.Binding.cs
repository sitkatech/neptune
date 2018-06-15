//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanLandUse]
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
    public abstract partial class WaterQualityManagementPlanLandUse : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanLandUseResidential Residential = WaterQualityManagementPlanLandUseResidential.Instance;
        public static readonly WaterQualityManagementPlanLandUseCommercial Commercial = WaterQualityManagementPlanLandUseCommercial.Instance;
        public static readonly WaterQualityManagementPlanLandUseIndustrial Industrial = WaterQualityManagementPlanLandUseIndustrial.Instance;
        public static readonly WaterQualityManagementPlanLandUseOther Other = WaterQualityManagementPlanLandUseOther.Instance;

        public static readonly List<WaterQualityManagementPlanLandUse> All;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanLandUse> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanLandUse()
        {
            All = new List<WaterQualityManagementPlanLandUse> { Residential, Commercial, Industrial, Other };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanLandUse>(All.ToDictionary(x => x.WaterQualityManagementPlanLandUseID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanLandUse(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder)
        {
            WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUseID;
            WaterQualityManagementPlanLandUseName = waterQualityManagementPlanLandUseName;
            WaterQualityManagementPlanLandUseDisplayName = waterQualityManagementPlanLandUseDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int WaterQualityManagementPlanLandUseID { get; private set; }
        public string WaterQualityManagementPlanLandUseName { get; private set; }
        public string WaterQualityManagementPlanLandUseDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanLandUseID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanLandUse other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanLandUseID == WaterQualityManagementPlanLandUseID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanLandUse);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanLandUseID;
        }

        public static bool operator ==(WaterQualityManagementPlanLandUse left, WaterQualityManagementPlanLandUse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanLandUse left, WaterQualityManagementPlanLandUse right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanLandUseEnum ToEnum { get { return (WaterQualityManagementPlanLandUseEnum)GetHashCode(); } }

        public static WaterQualityManagementPlanLandUse ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanLandUseEnum)enumValue);
        }

        public static WaterQualityManagementPlanLandUse ToType(WaterQualityManagementPlanLandUseEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanLandUseEnum.Commercial:
                    return Commercial;
                case WaterQualityManagementPlanLandUseEnum.Industrial:
                    return Industrial;
                case WaterQualityManagementPlanLandUseEnum.Other:
                    return Other;
                case WaterQualityManagementPlanLandUseEnum.Residential:
                    return Residential;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum WaterQualityManagementPlanLandUseEnum
    {
        Residential = 1,
        Commercial = 2,
        Industrial = 3,
        Other = 4
    }

    public partial class WaterQualityManagementPlanLandUseResidential : WaterQualityManagementPlanLandUse
    {
        private WaterQualityManagementPlanLandUseResidential(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : base(waterQualityManagementPlanLandUseID, waterQualityManagementPlanLandUseName, waterQualityManagementPlanLandUseDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanLandUseResidential Instance = new WaterQualityManagementPlanLandUseResidential(1, @"Residential", @"Residential", 10);
    }

    public partial class WaterQualityManagementPlanLandUseCommercial : WaterQualityManagementPlanLandUse
    {
        private WaterQualityManagementPlanLandUseCommercial(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : base(waterQualityManagementPlanLandUseID, waterQualityManagementPlanLandUseName, waterQualityManagementPlanLandUseDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanLandUseCommercial Instance = new WaterQualityManagementPlanLandUseCommercial(2, @"Commercial", @"Commercial", 20);
    }

    public partial class WaterQualityManagementPlanLandUseIndustrial : WaterQualityManagementPlanLandUse
    {
        private WaterQualityManagementPlanLandUseIndustrial(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : base(waterQualityManagementPlanLandUseID, waterQualityManagementPlanLandUseName, waterQualityManagementPlanLandUseDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanLandUseIndustrial Instance = new WaterQualityManagementPlanLandUseIndustrial(3, @"Industrial", @"Industrial", 30);
    }

    public partial class WaterQualityManagementPlanLandUseOther : WaterQualityManagementPlanLandUse
    {
        private WaterQualityManagementPlanLandUseOther(int waterQualityManagementPlanLandUseID, string waterQualityManagementPlanLandUseName, string waterQualityManagementPlanLandUseDisplayName, int sortOrder) : base(waterQualityManagementPlanLandUseID, waterQualityManagementPlanLandUseName, waterQualityManagementPlanLandUseDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanLandUseOther Instance = new WaterQualityManagementPlanLandUseOther(4, @"Other", @"Other", 40);
    }
}