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
    }
}