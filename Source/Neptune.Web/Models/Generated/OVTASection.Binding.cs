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
        public static readonly OVTASectionInstructions Instructions = OVTASectionInstructions.Instance;
        public static readonly OVTASectionRecordObservations RecordObservations = OVTASectionRecordObservations.Instance;
        public static readonly OVTASectionVerifyOVTAArea VerifyOVTAArea = OVTASectionVerifyOVTAArea.Instance;
        public static readonly OVTASectionFinalizeOVTA FinalizeOVTA = OVTASectionFinalizeOVTA.Instance;

        public static readonly List<OVTASection> All;
        public static readonly ReadOnlyDictionary<int, OVTASection> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static OVTASection()
        {
            All = new List<OVTASection> { Instructions, RecordObservations, VerifyOVTAArea, FinalizeOVTA };
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
                case OVTASectionEnum.FinalizeOVTA:
                    return FinalizeOVTA;
                case OVTASectionEnum.Instructions:
                    return Instructions;
                case OVTASectionEnum.RecordObservations:
                    return RecordObservations;
                case OVTASectionEnum.VerifyOVTAArea:
                    return VerifyOVTAArea;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum OVTASectionEnum
    {
        Instructions = 1,
        RecordObservations = 2,
        VerifyOVTAArea = 3,
        FinalizeOVTA = 4
    }

    public partial class OVTASectionInstructions : OVTASection
    {
        private OVTASectionInstructions(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionInstructions Instance = new OVTASectionInstructions(1, @"Instructions", @"Instructions", @"Instructions?", 10);
    }

    public partial class OVTASectionRecordObservations : OVTASection
    {
        private OVTASectionRecordObservations(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionRecordObservations Instance = new OVTASectionRecordObservations(2, @"RecordObservations", @"Record Observations", @"Record Observations", 20);
    }

    public partial class OVTASectionVerifyOVTAArea : OVTASection
    {
        private OVTASectionVerifyOVTAArea(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionVerifyOVTAArea Instance = new OVTASectionVerifyOVTAArea(3, @"VerifyOVTAArea", @"Verify OVTA Area", @"Verify OVTA Area", 30);
    }

    public partial class OVTASectionFinalizeOVTA : OVTASection
    {
        private OVTASectionFinalizeOVTA(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder) {}
        public static readonly OVTASectionFinalizeOVTA Instance = new OVTASectionFinalizeOVTA(4, @"FinalizeOVTA", @"Finalize OVTA", @"Finalize OVTA", 40);
    }
}