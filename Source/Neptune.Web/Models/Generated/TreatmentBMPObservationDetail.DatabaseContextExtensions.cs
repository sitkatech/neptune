//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservationDetail]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPObservationDetail GetTreatmentBMPObservationDetail(this IQueryable<TreatmentBMPObservationDetail> treatmentBMPObservationDetails, int treatmentBMPObservationDetailID)
        {
            var treatmentBMPObservationDetail = treatmentBMPObservationDetails.SingleOrDefault(x => x.TreatmentBMPObservationDetailID == treatmentBMPObservationDetailID);
            Check.RequireNotNullThrowNotFound(treatmentBMPObservationDetail, "TreatmentBMPObservationDetail", treatmentBMPObservationDetailID);
            return treatmentBMPObservationDetail;
        }

        public static void DeleteTreatmentBMPObservationDetail(this List<int> treatmentBMPObservationDetailIDList)
        {
            if(treatmentBMPObservationDetailIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Where(x => treatmentBMPObservationDetailIDList.Contains(x.TreatmentBMPObservationDetailID)));
            }
        }

        public static void DeleteTreatmentBMPObservationDetail(this ICollection<TreatmentBMPObservationDetail> treatmentBMPObservationDetailsToDelete)
        {
            if(treatmentBMPObservationDetailsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.RemoveRange(treatmentBMPObservationDetailsToDelete);
            }
        }

        public static void DeleteTreatmentBMPObservationDetail(this int treatmentBMPObservationDetailID)
        {
            DeleteTreatmentBMPObservationDetail(new List<int> { treatmentBMPObservationDetailID });
        }

        public static void DeleteTreatmentBMPObservationDetail(this TreatmentBMPObservationDetail treatmentBMPObservationDetailToDelete)
        {
            DeleteTreatmentBMPObservationDetail(new List<TreatmentBMPObservationDetail> { treatmentBMPObservationDetailToDelete });
        }
    }
}