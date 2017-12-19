//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteTreatmentBMP(this List<int> treatmentBMPIDList)
        {
            if(treatmentBMPIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPs.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)));
            }
        }

        public static void DeleteTreatmentBMP(this ICollection<TreatmentBMP> treatmentBMPsToDelete)
        {
            if(treatmentBMPsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPs.RemoveRange(treatmentBMPsToDelete);
            }
        }

        public static void DeleteTreatmentBMP(this int treatmentBMPID)
        {
            DeleteTreatmentBMP(new List<int> { treatmentBMPID });
        }

        public static void DeleteTreatmentBMP(this TreatmentBMP treatmentBMPToDelete)
        {
            DeleteTreatmentBMP(new List<TreatmentBMP> { treatmentBMPToDelete });
        }
    }
}