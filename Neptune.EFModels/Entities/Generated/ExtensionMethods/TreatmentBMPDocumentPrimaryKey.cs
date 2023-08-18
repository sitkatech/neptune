//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPDocument


namespace Neptune.EFModels.Entities
{
    public class TreatmentBMPDocumentPrimaryKey : EntityPrimaryKey<TreatmentBMPDocument>
    {
        public TreatmentBMPDocumentPrimaryKey() : base(){}
        public TreatmentBMPDocumentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public TreatmentBMPDocumentPrimaryKey(TreatmentBMPDocument treatmentBMPDocument) : base(treatmentBMPDocument){}

        public static implicit operator TreatmentBMPDocumentPrimaryKey(int primaryKeyValue)
        {
            return new TreatmentBMPDocumentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator TreatmentBMPDocumentPrimaryKey(TreatmentBMPDocument treatmentBMPDocument)
        {
            return new TreatmentBMPDocumentPrimaryKey(treatmentBMPDocument);
        }
    }
}