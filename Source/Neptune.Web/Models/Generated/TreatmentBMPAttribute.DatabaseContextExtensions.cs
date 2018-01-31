//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttribute]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPAttribute GetTreatmentBMPAttribute(this IQueryable<TreatmentBMPAttribute> treatmentBMPAttributes, int treatmentBMPAttributeID)
        {
            var treatmentBMPAttribute = treatmentBMPAttributes.SingleOrDefault(x => x.TreatmentBMPAttributeID == treatmentBMPAttributeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAttribute, "TreatmentBMPAttribute", treatmentBMPAttributeID);
            return treatmentBMPAttribute;
        }

        public static void DeleteTreatmentBMPAttribute(this List<int> treatmentBMPAttributeIDList)
        {
            if(treatmentBMPAttributeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributes.Where(x => treatmentBMPAttributeIDList.Contains(x.TreatmentBMPAttributeID)));
            }
        }

        public static void DeleteTreatmentBMPAttribute(this ICollection<TreatmentBMPAttribute> treatmentBMPAttributesToDelete)
        {
            if(treatmentBMPAttributesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributes.RemoveRange(treatmentBMPAttributesToDelete);
            }
        }

        public static void DeleteTreatmentBMPAttribute(this int treatmentBMPAttributeID)
        {
            DeleteTreatmentBMPAttribute(new List<int> { treatmentBMPAttributeID });
        }

        public static void DeleteTreatmentBMPAttribute(this TreatmentBMPAttribute treatmentBMPAttributeToDelete)
        {
            DeleteTreatmentBMPAttribute(new List<TreatmentBMPAttribute> { treatmentBMPAttributeToDelete });
        }
    }
}