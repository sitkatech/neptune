//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
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
        public static TreatmentBMPType GetTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, int treatmentBMPTypeID)
        {
            var treatmentBMPType = treatmentBMPTypes.SingleOrDefault(x => x.TreatmentBMPTypeID == treatmentBMPTypeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPType, "TreatmentBMPType", treatmentBMPTypeID);
            return treatmentBMPType;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, List<int> treatmentBMPTypeIDList)
        {
            if(treatmentBMPTypeIDList.Any())
            {
                treatmentBMPTypes.Where(x => treatmentBMPTypeIDList.Contains(x.TreatmentBMPTypeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, ICollection<TreatmentBMPType> treatmentBMPTypesToDelete)
        {
            if(treatmentBMPTypesToDelete.Any())
            {
                var treatmentBMPTypeIDList = treatmentBMPTypesToDelete.Select(x => x.TreatmentBMPTypeID).ToList();
                treatmentBMPTypes.Where(x => treatmentBMPTypeIDList.Contains(x.TreatmentBMPTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, int treatmentBMPTypeID)
        {
            DeleteTreatmentBMPType(treatmentBMPTypes, new List<int> { treatmentBMPTypeID });
        }

        public static void DeleteTreatmentBMPType(this IQueryable<TreatmentBMPType> treatmentBMPTypes, TreatmentBMPType treatmentBMPTypeToDelete)
        {
            DeleteTreatmentBMPType(treatmentBMPTypes, new List<TreatmentBMPType> { treatmentBMPTypeToDelete });
        }
    }
}