//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OVTASection]
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class OVTASection : IHavePrimaryKey
    {
        public static readonly OVTASectionInstructions Instructions = OVTASectionInstructions.Instance;
        public static readonly OVTASectionInitiateOVTA InitiateOVTA = OVTASectionInitiateOVTA.Instance;
        public static readonly OVTASectionRecordObservations RecordObservations = OVTASectionRecordObservations.Instance;
        public static readonly OVTASectionAddOrRemoveParcels AddOrRemoveParcels = OVTASectionAddOrRemoveParcels.Instance;
        public static readonly OVTASectionRefineAssessmentArea RefineAssessmentArea = OVTASectionRefineAssessmentArea.Instance;
        public static readonly OVTASectionFinalizeOVTA FinalizeOVTA = OVTASectionFinalizeOVTA.Instance;

        public static readonly List<OVTASection> All;
        public static readonly List<OVTASectionSimpleDto> AllAsSimpleDto;
        public static readonly ReadOnlyDictionary<int, OVTASection> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, OVTASectionSimpleDto> AllAsSimpleDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static OVTASection()
        {
            All = new List<OVTASection> { Instructions, InitiateOVTA, RecordObservations, AddOrRemoveParcels, RefineAssessmentArea, FinalizeOVTA };
            AllAsSimpleDto = new List<OVTASectionSimpleDto> { Instructions.AsSimpleDto(), InitiateOVTA.AsSimpleDto(), RecordObservations.AsSimpleDto(), AddOrRemoveParcels.AsSimpleDto(), RefineAssessmentArea.AsSimpleDto(), FinalizeOVTA.AsSimpleDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, OVTASection>(All.ToDictionary(x => x.OVTASectionID));
            AllAsSimpleDtoLookupDictionary = new ReadOnlyDictionary<int, OVTASectionSimpleDto>(AllAsSimpleDto.ToDictionary(x => x.OVTASectionID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected OVTASection(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus)
        {
            OVTASectionID = oVTASectionID;
            OVTASectionName = oVTASectionName;
            OVTASectionDisplayName = oVTASectionDisplayName;
            SectionHeader = sectionHeader;
            SortOrder = sortOrder;
            HasCompletionStatus = hasCompletionStatus;
        }

        [Key]
        public int OVTASectionID { get; private set; }
        public string OVTASectionName { get; private set; }
        public string OVTASectionDisplayName { get; private set; }
        public string SectionHeader { get; private set; }
        public int SortOrder { get; private set; }
        public bool HasCompletionStatus { get; private set; }
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

        public OVTASectionEnum ToEnum => (OVTASectionEnum)GetHashCode();

        public static OVTASection ToType(int enumValue)
        {
            return ToType((OVTASectionEnum)enumValue);
        }

        public static OVTASection ToType(OVTASectionEnum enumValue)
        {
            switch (enumValue)
            {
                case OVTASectionEnum.AddOrRemoveParcels:
                    return AddOrRemoveParcels;
                case OVTASectionEnum.FinalizeOVTA:
                    return FinalizeOVTA;
                case OVTASectionEnum.InitiateOVTA:
                    return InitiateOVTA;
                case OVTASectionEnum.Instructions:
                    return Instructions;
                case OVTASectionEnum.RecordObservations:
                    return RecordObservations;
                case OVTASectionEnum.RefineAssessmentArea:
                    return RefineAssessmentArea;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum OVTASectionEnum
    {
        Instructions = 1,
        InitiateOVTA = 2,
        RecordObservations = 3,
        AddOrRemoveParcels = 4,
        RefineAssessmentArea = 5,
        FinalizeOVTA = 6
    }

    public partial class OVTASectionInstructions : OVTASection
    {
        private OVTASectionInstructions(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionInstructions Instance = new OVTASectionInstructions(1, @"Instructions", @"Instructions", @"Instructions", 10, false);
    }

    public partial class OVTASectionInitiateOVTA : OVTASection
    {
        private OVTASectionInitiateOVTA(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionInitiateOVTA Instance = new OVTASectionInitiateOVTA(2, @"InitiateOVTA", @"Initiate OVTA", @"Initiate OVTA", 20, true);
    }

    public partial class OVTASectionRecordObservations : OVTASection
    {
        private OVTASectionRecordObservations(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionRecordObservations Instance = new OVTASectionRecordObservations(3, @"RecordObservations", @"Record Observations", @"Record Observations", 30, true);
    }

    public partial class OVTASectionAddOrRemoveParcels : OVTASection
    {
        private OVTASectionAddOrRemoveParcels(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionAddOrRemoveParcels Instance = new OVTASectionAddOrRemoveParcels(4, @"AddOrRemoveParcels", @"Add or Remove Parcels", @"Add or Remove Parcels", 40, true);
    }

    public partial class OVTASectionRefineAssessmentArea : OVTASection
    {
        private OVTASectionRefineAssessmentArea(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionRefineAssessmentArea Instance = new OVTASectionRefineAssessmentArea(5, @"RefineAssessmentArea", @"Refine Assessment Area", @"Refine Assessment Area", 50, false);
    }

    public partial class OVTASectionFinalizeOVTA : OVTASection
    {
        private OVTASectionFinalizeOVTA(int oVTASectionID, string oVTASectionName, string oVTASectionDisplayName, string sectionHeader, int sortOrder, bool hasCompletionStatus) : base(oVTASectionID, oVTASectionName, oVTASectionDisplayName, sectionHeader, sortOrder, hasCompletionStatus) {}
        public static readonly OVTASectionFinalizeOVTA Instance = new OVTASectionFinalizeOVTA(6, @"FinalizeOVTA", @"Review and Finalize OVTA", @"Finalize OVTA", 60, false);
    }
}