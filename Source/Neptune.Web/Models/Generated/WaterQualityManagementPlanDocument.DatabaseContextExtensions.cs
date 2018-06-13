//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteWaterQualityManagementPlanDocument(this List<int> waterQualityManagementPlanDocumentIDList)
        {
            if(waterQualityManagementPlanDocumentIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanDocuments.RemoveRange(HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanDocuments.Where(x => waterQualityManagementPlanDocumentIDList.Contains(x.WaterQualityManagementPlanDocumentID)));
            }
        }

        public static void DeleteWaterQualityManagementPlanDocument(this ICollection<WaterQualityManagementPlanDocument> waterQualityManagementPlanDocumentsToDelete)
        {
            if(waterQualityManagementPlanDocumentsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanDocuments.RemoveRange(waterQualityManagementPlanDocumentsToDelete);
            }
        }

        public static void DeleteWaterQualityManagementPlanDocument(this int waterQualityManagementPlanDocumentID)
        {
            DeleteWaterQualityManagementPlanDocument(new List<int> { waterQualityManagementPlanDocumentID });
        }

        public static void DeleteWaterQualityManagementPlanDocument(this WaterQualityManagementPlanDocument waterQualityManagementPlanDocumentToDelete)
        {
            DeleteWaterQualityManagementPlanDocument(new List<WaterQualityManagementPlanDocument> { waterQualityManagementPlanDocumentToDelete });
        }
    }
}