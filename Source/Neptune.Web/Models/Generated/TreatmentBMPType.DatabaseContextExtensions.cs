//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPType GetTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, int treatmentBMPTypeID)
        {
            var treatmentBMPType = treatmentBMPTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPType, "TreatmentBMPType", treatmentBMPTypeID);
            return treatmentBMPType;
        }

        public static void DeleteTreatmentBMPType(this List<int> treatmentBMPTypeIDList)
        {
            if(treatmentBMPTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Where(x => treatmentBMPTypeIDList.Contains(x.TreatmentBMPTypeID)));
            }
        }

        public static void DeleteTreatmentBMPType(this ICollection<TreatmentBMPType> treatmentBMPTypesToDelete)
        {
            if(treatmentBMPTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypes.RemoveRange(treatmentBMPTypesToDelete);
            }
        }

        public static void DeleteTreatmentBMPType(this int treatmentBMPTypeID)
        {
            DeleteTreatmentBMPType(new List<int> { treatmentBMPTypeID });
        }

        public static void DeleteTreatmentBMPType(this TreatmentBMPType treatmentBMPTypeToDelete)
        {
            DeleteTreatmentBMPType(new List<TreatmentBMPType> { treatmentBMPTypeToDelete });
        }
    }
}