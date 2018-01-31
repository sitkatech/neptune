//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAttributeType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPTypeAttributeType GetTreatmentBMPTypeAttributeType(this IQueryable<TreatmentBMPTypeAttributeType> treatmentBMPTypeAttributeTypes, int treatmentBMPTypeAttributeTypeID)
        {
            var treatmentBMPTypeAttributeType = treatmentBMPTypeAttributeTypes.SingleOrDefault(x => x.TreatmentBMPTypeAttributeTypeID == treatmentBMPTypeAttributeTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeAttributeType, "TreatmentBMPTypeAttributeType", treatmentBMPTypeAttributeTypeID);
            return treatmentBMPTypeAttributeType;
        }

        public static void DeleteTreatmentBMPTypeAttributeType(this List<int> treatmentBMPTypeAttributeTypeIDList)
        {
            if(treatmentBMPTypeAttributeTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAttributeTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAttributeTypes.Where(x => treatmentBMPTypeAttributeTypeIDList.Contains(x.TreatmentBMPTypeAttributeTypeID)));
            }
        }

        public static void DeleteTreatmentBMPTypeAttributeType(this ICollection<TreatmentBMPTypeAttributeType> treatmentBMPTypeAttributeTypesToDelete)
        {
            if(treatmentBMPTypeAttributeTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAttributeTypes.RemoveRange(treatmentBMPTypeAttributeTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPTypeAttributeType(this int treatmentBMPTypeAttributeTypeID)
        {
            DeleteTreatmentBMPTypeAttributeType(new List<int> { treatmentBMPTypeAttributeTypeID });
        }

        public static void DeleteTreatmentBMPTypeAttributeType(this TreatmentBMPTypeAttributeType treatmentBMPTypeAttributeTypeToDelete)
        {
            DeleteTreatmentBMPTypeAttributeType(new List<TreatmentBMPTypeAttributeType> { treatmentBMPTypeAttributeTypeToDelete });
        }
    }
}