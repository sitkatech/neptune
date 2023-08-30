//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMP : IHavePrimaryKey
    {
        public int PrimaryKey => TreatmentBMPID;
        public TreatmentBMPLifespanType TreatmentBMPLifespanType => TreatmentBMPLifespanTypeID.HasValue ? TreatmentBMPLifespanType.AllLookupDictionary[TreatmentBMPLifespanTypeID.Value] : null;
        public TrashCaptureStatusType TrashCaptureStatusType => TrashCaptureStatusType.AllLookupDictionary[TrashCaptureStatusTypeID];
        public SizingBasisType SizingBasisType => SizingBasisType.AllLookupDictionary[SizingBasisTypeID];

        public static class FieldLengths
        {
            public const int TreatmentBMPName = 200;
            public const int Notes = 1000;
            public const int SystemOfRecordID = 100;
        }
    }
}