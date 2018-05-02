//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeValue]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPAttributeValue GetTreatmentBMPAttributeValue(this IQueryable<TreatmentBMPAttributeValue> treatmentBMPAttributeValues, int treatmentBMPAttributeValueID)
        {
            var treatmentBMPAttributeValue = treatmentBMPAttributeValues.SingleOrDefault(x => x.TreatmentBMPAttributeValueID == treatmentBMPAttributeValueID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAttributeValue, "TreatmentBMPAttributeValue", treatmentBMPAttributeValueID);
            return treatmentBMPAttributeValue;
        }

        public static void DeleteTreatmentBMPAttributeValue(this List<int> treatmentBMPAttributeValueIDList)
        {
            if(treatmentBMPAttributeValueIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeValues.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributeValues.Where(x => treatmentBMPAttributeValueIDList.Contains(x.TreatmentBMPAttributeValueID)));
            }
        }

        public static void DeleteTreatmentBMPAttributeValue(this ICollection<TreatmentBMPAttributeValue> treatmentBMPAttributeValuesToDelete)
        {
            if(treatmentBMPAttributeValuesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeValues.RemoveRange(treatmentBMPAttributeValuesToDelete);
            }
        }

        public static void DeleteTreatmentBMPAttributeValue(this int treatmentBMPAttributeValueID)
        {
            DeleteTreatmentBMPAttributeValue(new List<int> { treatmentBMPAttributeValueID });
        }

        public static void DeleteTreatmentBMPAttributeValue(this TreatmentBMPAttributeValue treatmentBMPAttributeValueToDelete)
        {
            DeleteTreatmentBMPAttributeValue(new List<TreatmentBMPAttributeValue> { treatmentBMPAttributeValueToDelete });
        }
    }
}