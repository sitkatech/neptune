//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
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
        public static TreatmentBMP GetTreatmentBMP(this IQueryable<TreatmentBMP> treatmentBMPs, int treatmentBMPID)
        {
            var treatmentBMP = treatmentBMPs.SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNullThrowNotFound(treatmentBMP, "TreatmentBMP", treatmentBMPID);
            return treatmentBMP;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMP(this IQueryable<TreatmentBMP> treatmentBMPs, List<int> treatmentBMPIDList)
        {
            if(treatmentBMPIDList.Any())
            {
                treatmentBMPs.Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMP(this IQueryable<TreatmentBMP> treatmentBMPs, ICollection<TreatmentBMP> treatmentBMPsToDelete)
        {
            if(treatmentBMPsToDelete.Any())
            {
                var treatmentBMPIDList = treatmentBMPsToDelete.Select(x => x.TreatmentBMPID).ToList();
                treatmentBMPs.Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).Delete();
            }
        }

        public static void DeleteTreatmentBMP(this IQueryable<TreatmentBMP> treatmentBMPs, int treatmentBMPID)
        {
            DeleteTreatmentBMP(treatmentBMPs, new List<int> { treatmentBMPID });
        }

        public static void DeleteTreatmentBMP(this IQueryable<TreatmentBMP> treatmentBMPs, TreatmentBMP treatmentBMPToDelete)
        {
            DeleteTreatmentBMP(treatmentBMPs, new List<TreatmentBMP> { treatmentBMPToDelete });
        }
    }
}