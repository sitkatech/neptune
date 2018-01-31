//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPAttributeType GetTreatmentBMPAttributeType(this IQueryable<TreatmentBMPAttributeType> treatmentBMPAttributeTypes, int treatmentBMPAttributeTypeID)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypes.SingleOrDefault(x => x.TreatmentBMPAttributeTypeID == treatmentBMPAttributeTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPAttributeType, "TreatmentBMPAttributeType", treatmentBMPAttributeTypeID);
            return treatmentBMPAttributeType;
        }

        public static void DeleteTreatmentBMPAttributeType(this List<int> treatmentBMPAttributeTypeIDList)
        {
            if(treatmentBMPAttributeTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributeTypes.Where(x => treatmentBMPAttributeTypeIDList.Contains(x.TreatmentBMPAttributeTypeID)));
            }
        }

        public static void DeleteTreatmentBMPAttributeType(this ICollection<TreatmentBMPAttributeType> treatmentBMPAttributeTypesToDelete)
        {
            if(treatmentBMPAttributeTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeTypes.RemoveRange(treatmentBMPAttributeTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPAttributeType(this int treatmentBMPAttributeTypeID)
        {
            DeleteTreatmentBMPAttributeType(new List<int> { treatmentBMPAttributeTypeID });
        }

        public static void DeleteTreatmentBMPAttributeType(this TreatmentBMPAttributeType treatmentBMPAttributeTypeToDelete)
        {
            DeleteTreatmentBMPAttributeType(new List<TreatmentBMPAttributeType> { treatmentBMPAttributeTypeToDelete });
        }
    }
}