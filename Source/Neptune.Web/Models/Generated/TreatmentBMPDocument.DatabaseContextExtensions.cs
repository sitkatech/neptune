//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteTreatmentBMPDocument(this List<int> treatmentBMPDocumentIDList)
        {
            if(treatmentBMPDocumentIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPDocuments.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPDocuments.Where(x => treatmentBMPDocumentIDList.Contains(x.TreatmentBMPDocumentID)));
            }
        }

        public static void DeleteTreatmentBMPDocument(this ICollection<TreatmentBMPDocument> treatmentBMPDocumentsToDelete)
        {
            if(treatmentBMPDocumentsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPDocuments.RemoveRange(treatmentBMPDocumentsToDelete);
            }
        }

        public static void DeleteTreatmentBMPDocument(this int treatmentBMPDocumentID)
        {
            DeleteTreatmentBMPDocument(new List<int> { treatmentBMPDocumentID });
        }

        public static void DeleteTreatmentBMPDocument(this TreatmentBMPDocument treatmentBMPDocumentToDelete)
        {
            DeleteTreatmentBMPDocument(new List<TreatmentBMPDocument> { treatmentBMPDocumentToDelete });
        }
    }
}