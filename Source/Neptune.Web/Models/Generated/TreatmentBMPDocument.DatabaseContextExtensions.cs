//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPDocument GetTreatmentBMPDocument(this IQueryable<TreatmentBMPDocument> treatmentBMPDocuments, int treatmentBMPDocumentID)
        {
            var treatmentBMPDocument = treatmentBMPDocuments.SingleOrDefault(x => x.TreatmentBMPDocumentID == treatmentBMPDocumentID);
            Check.RequireNotNullThrowNotFound(treatmentBMPDocument, "TreatmentBMPDocument", treatmentBMPDocumentID);
            return treatmentBMPDocument;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPDocument(this IQueryable<TreatmentBMPDocument> treatmentBMPDocuments, List<int> treatmentBMPDocumentIDList)
        {
            if(treatmentBMPDocumentIDList.Any())
            {
                treatmentBMPDocuments.Where(x => treatmentBMPDocumentIDList.Contains(x.TreatmentBMPDocumentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPDocument(this IQueryable<TreatmentBMPDocument> treatmentBMPDocuments, ICollection<TreatmentBMPDocument> treatmentBMPDocumentsToDelete)
        {
            if(treatmentBMPDocumentsToDelete.Any())
            {
                var treatmentBMPDocumentIDList = treatmentBMPDocumentsToDelete.Select(x => x.TreatmentBMPDocumentID).ToList();
                treatmentBMPDocuments.Where(x => treatmentBMPDocumentIDList.Contains(x.TreatmentBMPDocumentID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPDocument(this IQueryable<TreatmentBMPDocument> treatmentBMPDocuments, int treatmentBMPDocumentID)
        {
            DeleteTreatmentBMPDocument(treatmentBMPDocuments, new List<int> { treatmentBMPDocumentID });
        }

        public static void DeleteTreatmentBMPDocument(this IQueryable<TreatmentBMPDocument> treatmentBMPDocuments, TreatmentBMPDocument treatmentBMPDocumentToDelete)
        {
            DeleteTreatmentBMPDocument(treatmentBMPDocuments, new List<TreatmentBMPDocument> { treatmentBMPDocumentToDelete });
        }
    }
}