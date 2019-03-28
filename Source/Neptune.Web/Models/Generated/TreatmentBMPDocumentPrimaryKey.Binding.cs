//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.TreatmentBMPDocument
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class TreatmentBMPDocumentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<TreatmentBMPDocument>
    {
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