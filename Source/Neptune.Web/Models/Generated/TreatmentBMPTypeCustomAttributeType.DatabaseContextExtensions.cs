//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]
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
        public static TreatmentBMPTypeCustomAttributeType GetTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, int treatmentBMPTypeCustomAttributeTypeID)
        {
            var treatmentBMPTypeCustomAttributeType = treatmentBMPTypeCustomAttributeTypes.SingleOrDefault(x => x.TreatmentBMPTypeCustomAttributeTypeID == treatmentBMPTypeCustomAttributeTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPTypeCustomAttributeType, "TreatmentBMPTypeCustomAttributeType", treatmentBMPTypeCustomAttributeTypeID);
            return treatmentBMPTypeCustomAttributeType;
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, List<int> treatmentBMPTypeCustomAttributeTypeIDList)
        {
            if(treatmentBMPTypeCustomAttributeTypeIDList.Any())
            {
                treatmentBMPTypeCustomAttributeTypes.Where(x => treatmentBMPTypeCustomAttributeTypeIDList.Contains(x.TreatmentBMPTypeCustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, ICollection<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypesToDelete)
        {
            if(treatmentBMPTypeCustomAttributeTypesToDelete.Any())
            {
                var treatmentBMPTypeCustomAttributeTypeIDList = treatmentBMPTypeCustomAttributeTypesToDelete.Select(x => x.TreatmentBMPTypeCustomAttributeTypeID).ToList();
                treatmentBMPTypeCustomAttributeTypes.Where(x => treatmentBMPTypeCustomAttributeTypeIDList.Contains(x.TreatmentBMPTypeCustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, int treatmentBMPTypeCustomAttributeTypeID)
        {
            DeleteTreatmentBMPTypeCustomAttributeType(treatmentBMPTypeCustomAttributeTypes, new List<int> { treatmentBMPTypeCustomAttributeTypeID });
        }

        public static void DeleteTreatmentBMPTypeCustomAttributeType(this IQueryable<TreatmentBMPTypeCustomAttributeType> treatmentBMPTypeCustomAttributeTypes, TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeTypeToDelete)
        {
            DeleteTreatmentBMPTypeCustomAttributeType(treatmentBMPTypeCustomAttributeTypes, new List<TreatmentBMPTypeCustomAttributeType> { treatmentBMPTypeCustomAttributeTypeToDelete });
        }
    }
}