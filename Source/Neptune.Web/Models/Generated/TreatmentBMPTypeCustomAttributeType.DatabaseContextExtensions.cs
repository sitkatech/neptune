//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPTypeCustomAttributeType GetTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, int treatmentBMPTypeCustomAttributeTypeID)
        {
            var treatmentBMPTypeCustomAttributeType = treatmentBMPTypeCustomAttributeTypes.SingleOrDefault(x => x.TreatmentBMPTypeCustomAttributeTypeID == treatmentBMPTypeCustomAttributeTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeCustomAttributeType, "TreatmentBMPTypeCustomAttributeType", treatmentBMPTypeCustomAttributeTypeID);
            return treatmentBMPTypeCustomAttributeType;
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this List<int> treatmentBMPTypeCustomAttributeTypeIDList)
        {
            if(treatmentBMPTypeCustomAttributeTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeCustomAttributeTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeCustomAttributeTypes.Where(x => treatmentBMPTypeCustomAttributeTypeIDList.Contains(x.TreatmentBMPTypeCustomAttributeTypeID)));
            }
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this ICollection<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypesToDelete)
        {
            if(treatmentBMPTypeCustomAttributeTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeCustomAttributeTypes.RemoveRange(treatmentBMPTypeCustomAttributeTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this int treatmentBMPTypeCustomAttributeTypeID)
        {
            DeleteTreatmentBMPTypeCustomAttributeType(new List<int> { treatmentBMPTypeCustomAttributeTypeID });
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeTypeToDelete)
        {
            DeleteTreatmentBMPTypeCustomAttributeType(new List<TreatmentBMPTypeCustomAttributeType> { treatmentBMPTypeCustomAttributeTypeToDelete });
        }
    }
}