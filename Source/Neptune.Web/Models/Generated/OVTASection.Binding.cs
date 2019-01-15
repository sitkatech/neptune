//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OVTASection]
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
    public abstract partial class OVTASection : IHavePrimaryKey
    {
        public static readonly OVTASectionInventory Inventory = OVTASectionInventory.Instance;
        public static readonly OVTASectionAssessment Assessment = OVTASectionAssessment.Instance;
        public static readonly OVTASectionMaintenance Maintenance = OVTASectionMaintenance.Instance;
        public static readonly OVTASectionPostMaintenanceAssessment PostMaintenanceAssessment = OVTASectionPostMaintenanceAssessment.Instance;
        public static readonly OVTASectionVisitSummary VisitSummary = OVTASectionVisitSummary.Instance;

        public static readonly List<OVTASection> All;
        public static readonly ReadOnlyDictionary<int, OVTASection> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static OVTASection()
        {
            All = new List<OVTASection> { Inventory, Assessment, Maintenance, PostMaintenanceAssessment, VisitSummary };
            AllLookupDictionary = new ReadOnlyDictionary<int, OVTASection>(All.ToDictionary(x => x.OVTASectionID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected OVTASection(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder)
        {
            OVTASectionID = oVTASectionID;
            OVTASectionName = oVTASectionName;
            OVTASectionDisplayName = oVTASectionDisplayName;
            SectionHeader = sectionHeader;
            SortOrder = sortOrder;
        }

        [Key]
        public int OVTASectionID { get; private set; }
        public string OVTASectionName { get; private set; }
        public string OVTASectionDisplayName { get; private set; }
        public string SectionHeader { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return OVTASectionID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(OVTASection other)
        {
            if (other == null)
            {
                return false;
            }
            return other.OVTASectionID == OVTASectionID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as OVTASection);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return OVTASectionID;
        }

        public static bool operator ==(OVTASection left, OVTASection right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OVTASection left, OVTASection right)
        {
            return !Equals(left, right);
        }

        public OVTASectionEnum ToEnum { get { return (OVTASectionEnum)GetHashCode(); } }

        public static OVTASection ToType(int enumValue)
        {
            return ToType((OVTASectionEnum)enumValue);
        }

        public static OVTASection ToType(OVTASectionEnum enumValue)
        {
            switch (enumValue)
            {
                case OVTASectionEnum.Assessment:
                    return Assessment;
                case OVTASectionEnum.Inventory:
                    return Inventory;
                case OVTASectionEnum.Maintenance:
                    return Maintenance;
                case OVTASectionEnum.PostMaintenanceAssessment:
                    return PostMaintenanceAssessment;
                case OVTASectionEnum.VisitSummary:
                    return VisitSummary;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum OVTASectionEnum
    {
        Inventory = 1,
        Assessment = 2,
        Maintenance = 3,
        PostMaintenanceAssessment = 4,
        VisitSummary = 5
    }

    public partial class OVTASectionInventory : OVTASection
    {
        private OVTASectionInventory(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionInventory Instance = new OVTASectionInventory(1, @"Inventory", @"Inventory", @"Review and Update Inventory?", 10);
    }

    public partial class OVTASectionAssessment : OVTASection
    {
        private OVTASectionAssessment(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionAssessment Instance = new OVTASectionAssessment(2, @"Assessment", @"Assessment", @"Assessment", 20);
    }

    public partial class OVTASectionMaintenance : OVTASection
    {
        private OVTASectionMaintenance(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionMaintenance Instance = new OVTASectionMaintenance(3, @"Maintenance", @"Maintenance", @"Maintenance", 30);
    }

    public partial class OVTASectionPostMaintenanceAssessment : OVTASection
    {
        private OVTASectionPostMaintenanceAssessment(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionPostMaintenanceAssessment Instance = new OVTASectionPostMaintenanceAssessment(4, @"PostMaintenanceAssessment", @"Post-Maintenance Assessment", @"Post-Maintenance Assessment", 40);
    }

    public partial class OVTASectionVisitSummary : OVTASection
    {
        private OVTASectionVisitSummary(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionVisitSummary Instance = new OVTASectionVisitSummary(5, @"VisitSummary", @"Visit Summary", @"Visit Summary", 50);
    }
}