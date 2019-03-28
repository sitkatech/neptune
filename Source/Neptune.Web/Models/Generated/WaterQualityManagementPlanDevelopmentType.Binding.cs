//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]
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
    public abstract partial class WaterQualityManagementPlanDevelopmentType : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanDevelopmentTypeNewDevelopment NewDevelopment = WaterQualityManagementPlanDevelopmentTypeNewDevelopment.Instance;
        public static readonly WaterQualityManagementPlanDevelopmentTypeRedevelopment Redevelopment = WaterQualityManagementPlanDevelopmentTypeRedevelopment.Instance;

        public static readonly List<WaterQualityManagementPlanDevelopmentType> All;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanDevelopmentType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanDevelopmentType()
        {
            All = new List<WaterQualityManagementPlanDevelopmentType> { NewDevelopment, Redevelopment };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanDevelopmentType>(All.ToDictionary(x => x.WaterQualityManagementPlanDevelopmentTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanDevelopmentType(int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanDevelopmentTypeName, string waterQualityManagementPlanDevelopmentTypeDisplayName, int sortOrder)
        {
            WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentTypeID;
            WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentTypeName;
            WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentTypeDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int WaterQualityManagementPlanDevelopmentTypeID { get; private set; }
        public string WaterQualityManagementPlanDevelopmentTypeName { get; private set; }
        public string WaterQualityManagementPlanDevelopmentTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanDevelopmentTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanDevelopmentType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanDevelopmentTypeID == WaterQualityManagementPlanDevelopmentTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanDevelopmentType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanDevelopmentTypeID;
        }

        public static bool operator ==(WaterQualityManagementPlanDevelopmentType left, WaterQualityManagementPlanDevelopmentType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanDevelopmentType left, WaterQualityManagementPlanDevelopmentType right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanDevelopmentTypeEnum ToEnum { get { return (WaterQualityManagementPlanDevelopmentTypeEnum)GetHashCode(); } }

        public static WaterQualityManagementPlanDevelopmentType ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanDevelopmentTypeEnum)enumValue);
        }

        public static WaterQualityManagementPlanDevelopmentType ToType(WaterQualityManagementPlanDevelopmentTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanDevelopmentTypeEnum.NewDevelopment:
                    return NewDevelopment;
                case WaterQualityManagementPlanDevelopmentTypeEnum.Redevelopment:
                    return Redevelopment;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum WaterQualityManagementPlanDevelopmentTypeEnum
    {
        NewDevelopment = 1,
        Redevelopment = 2
    }

    public partial class WaterQualityManagementPlanDevelopmentTypeNewDevelopment : WaterQualityManagementPlanDevelopmentType
    {
        private WaterQualityManagementPlanDevelopmentTypeNewDevelopment(int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanDevelopmentTypeName, string waterQualityManagementPlanDevelopmentTypeDisplayName, int sortOrder) : base(waterQualityManagementPlanDevelopmentTypeID, waterQualityManagementPlanDevelopmentTypeName, waterQualityManagementPlanDevelopmentTypeDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanDevelopmentTypeNewDevelopment Instance = new WaterQualityManagementPlanDevelopmentTypeNewDevelopment(1, @"NewDevelopment", @"New Development", 10);
    }

    public partial class WaterQualityManagementPlanDevelopmentTypeRedevelopment : WaterQualityManagementPlanDevelopmentType
    {
        private WaterQualityManagementPlanDevelopmentTypeRedevelopment(int waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanDevelopmentTypeName, string waterQualityManagementPlanDevelopmentTypeDisplayName, int sortOrder) : base(waterQualityManagementPlanDevelopmentTypeID, waterQualityManagementPlanDevelopmentTypeName, waterQualityManagementPlanDevelopmentTypeDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanDevelopmentTypeRedevelopment Instance = new WaterQualityManagementPlanDevelopmentTypeRedevelopment(2, @"Redevelopment", @"Redevelopment", 20);
    }
}