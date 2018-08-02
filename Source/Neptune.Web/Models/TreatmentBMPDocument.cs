using System;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPDocument : IAuditableEntity
    {
        public TreatmentBMPDocument(TreatmentBMP treatmentBMP)
            : this(ModelObjectHelpers.NotYetAssignedID, treatmentBMP.TreatmentBMPID, string.Empty, DateTime.Now)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            TreatmentBMP = treatmentBMP;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

        }

        public string GetAuditDescriptionString()
        {
            return null;
        }

        public string GetDeleteTreatmentBMPDocumentUrl()
        {
            return SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(t =>
                t.Delete(TreatmentBMPDocumentID));
        }

        public string GetEditTreatmentBMPDocumentUrl()
        {
            return SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(
                t => t.Edit(TreatmentBMPDocumentID));
        }
    }
}