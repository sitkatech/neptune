//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]
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
        public static WaterQualityManagementPlanDocument GetWaterQualityManagementPlanDocument(this IQueryable<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, int waterQualityManagementPlanDocumentID)
        {
            var waterQualityManagementPlanDocument = waterQualityManagementPlanDocuments.SingleOrDefault(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID);
            Check.RequireNotNullThrowNotFound(waterQualityManagementPlanDocument, "WaterQualityManagementPlanDocument", waterQualityManagementPlanDocumentID);
            return waterQualityManagementPlanDocument;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteWaterQualityManagementPlanDocument(this IQueryable<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, List<int> waterQualityManagementPlanDocumentIDList)
        {
            if(waterQualityManagementPlanDocumentIDList.Any())
            {
                waterQualityManagementPlanDocuments.Where(x => waterQualityManagementPlanDocumentIDList.Contains(x.WaterQualityManagementPlanDocumentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteWaterQualityManagementPlanDocument(this IQueryable<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, ICollection<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocumentsToDelete)
        {
            if(waterQualityManagementPlanDocumentsToDelete.Any())
            {
                var waterQualityManagementPlanDocumentIDList = waterQualityManagementPlanDocumentsToDelete.Select(x => x.WaterQualityManagementPlanDocumentID).ToList();
                waterQualityManagementPlanDocuments.Where(x => waterQualityManagementPlanDocumentIDList.Contains(x.WaterQualityManagementPlanDocumentID)).Delete();
            }
        }

        public static void DeleteWaterQualityManagementPlanDocument(this IQueryable<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, int waterQualityManagementPlanDocumentID)
        {
            DeleteWaterQualityManagementPlanDocument(waterQualityManagementPlanDocuments, new List<int> { waterQualityManagementPlanDocumentID });
        }

        public static void DeleteWaterQualityManagementPlanDocument(this IQueryable<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, WaterQualityManagementPlanDocument waterQualityManagementPlanDocumentToDelete)
        {
            DeleteWaterQualityManagementPlanDocument(waterQualityManagementPlanDocuments, new List<WaterQualityManagementPlanDocument> { waterQualityManagementPlanDocumentToDelete });
        }
    }
}