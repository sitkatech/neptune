//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitSection]
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
    public abstract partial class FieldVisitSection : IHavePrimaryKey
    {
        public static readonly FieldVisitSectionInventory Inventory = FieldVisitSectionInventory.Instance;
        public static readonly FieldVisitSectionAssessment Assessment = FieldVisitSectionAssessment.Instance;
        public static readonly FieldVisitSectionMaintain Maintain = FieldVisitSectionMaintain.Instance;
        public static readonly FieldVisitSectionPostMaintenanceAssessment PostMaintenanceAssessment = FieldVisitSectionPostMaintenanceAssessment.Instance;
        public static readonly FieldVisitSectionWrapUpVisit WrapUpVisit = FieldVisitSectionWrapUpVisit.Instance;
        public static readonly FieldVisitSectionManageVisit ManageVisit = FieldVisitSectionManageVisit.Instance;

        public static readonly List<FieldVisitSection> All;
        public static readonly ReadOnlyDictionary<int, FieldVisitSection> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static FieldVisitSection()
        {
            All = new List<FieldVisitSection> { Inventory, Assessment, Maintain, PostMaintenanceAssessment, WrapUpVisit, ManageVisit };
            AllLookupDictionary = new ReadOnlyDictionary<int, FieldVisitSection>(All.ToDictionary(x => x.FieldVisitSectionID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected FieldVisitSection(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder)
        {
            FieldVisitSectionID = fieldVisitSectionID;
            FieldVisitSectionName = fieldVisitSectionName;
            FieldVisitSectionDisplayName = fieldVisitSectionDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int FieldVisitSectionID { get; private set; }
        public string FieldVisitSectionName { get; private set; }
        public string FieldVisitSectionDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldVisitSectionID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(FieldVisitSection other)
        {
            if (other == null)
            {
                return false;
            }
            return other.FieldVisitSectionID == FieldVisitSectionID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as FieldVisitSection);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return FieldVisitSectionID;
        }

        public static bool operator ==(FieldVisitSection left, FieldVisitSection right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FieldVisitSection left, FieldVisitSection right)
        {
            return !Equals(left, right);
        }

        public FieldVisitSectionEnum ToEnum { get { return (FieldVisitSectionEnum)GetHashCode(); } }

        public static FieldVisitSection ToType(int enumValue)
        {
            return ToType((FieldVisitSectionEnum)enumValue);
        }

        public static FieldVisitSection ToType(FieldVisitSectionEnum enumValue)
        {
            switch (enumValue)
            {
                case FieldVisitSectionEnum.Assessment:
                    return Assessment;
                case FieldVisitSectionEnum.Inventory:
                    return Inventory;
                case FieldVisitSectionEnum.Maintain:
                    return Maintain;
                case FieldVisitSectionEnum.ManageVisit:
                    return ManageVisit;
                case FieldVisitSectionEnum.PostMaintenanceAssessment:
                    return PostMaintenanceAssessment;
                case FieldVisitSectionEnum.WrapUpVisit:
                    return WrapUpVisit;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum FieldVisitSectionEnum
    {
        Inventory = 1,
        Assessment = 2,
        Maintain = 3,
        PostMaintenanceAssessment = 4,
        WrapUpVisit = 5,
        ManageVisit = 6
    }

    public partial class FieldVisitSectionInventory : FieldVisitSection
    {
        private FieldVisitSectionInventory(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionInventory Instance = new FieldVisitSectionInventory(1, @"Inventory", @"Inventory", 10);
    }

    public partial class FieldVisitSectionAssessment : FieldVisitSection
    {
        private FieldVisitSectionAssessment(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionAssessment Instance = new FieldVisitSectionAssessment(2, @"Assessment", @"Assess", 20);
    }

    public partial class FieldVisitSectionMaintain : FieldVisitSection
    {
        private FieldVisitSectionMaintain(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionMaintain Instance = new FieldVisitSectionMaintain(3, @"Maintain", @"Maintain", 30);
    }

    public partial class FieldVisitSectionPostMaintenanceAssessment : FieldVisitSection
    {
        private FieldVisitSectionPostMaintenanceAssessment(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionPostMaintenanceAssessment Instance = new FieldVisitSectionPostMaintenanceAssessment(4, @"PostMaintenanceAssessment", @"Post-Maintenance Assessment", 40);
    }

    public partial class FieldVisitSectionWrapUpVisit : FieldVisitSection
    {
        private FieldVisitSectionWrapUpVisit(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionWrapUpVisit Instance = new FieldVisitSectionWrapUpVisit(5, @"WrapUpVisit", @"Wrap-up Visit", 50);
    }

    public partial class FieldVisitSectionManageVisit : FieldVisitSection
    {
        private FieldVisitSectionManageVisit(int fieldVisitSectionID, string fieldVisitSectionName, string fieldVisitSectionDisplayName, int sortOrder) : base(fieldVisitSectionID, fieldVisitSectionName, fieldVisitSectionDisplayName, sortOrder) {}
        public static readonly FieldVisitSectionManageVisit Instance = new FieldVisitSectionManageVisit(6, @"ManageVisit", @"Manage Visit", 60);
    }
}