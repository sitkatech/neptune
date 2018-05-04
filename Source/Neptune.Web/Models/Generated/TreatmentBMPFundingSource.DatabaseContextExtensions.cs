//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPFundingSource]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPFundingSource GetTreatmentBMPFundingSource(this IQueryable<TreatmentBMPFundingSource> treatmentBMPFundingSources, int treatmentBMPFundingSourceID)
        {
            var treatmentBMPFundingSource = treatmentBMPFundingSources.SingleOrDefault(x => x.TreatmentBMPFundingSourceID == treatmentBMPFundingSourceID);
            Check.RequireNotNullThrowNotFound(treatmentBMPFundingSource, "TreatmentBMPFundingSource", treatmentBMPFundingSourceID);
            return treatmentBMPFundingSource;
        }

        public static void DeleteTreatmentBMPFundingSource(this List<int> treatmentBMPFundingSourceIDList)
        {
            if(treatmentBMPFundingSourceIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPFundingSources.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPFundingSources.Where(x => treatmentBMPFundingSourceIDList.Contains(x.TreatmentBMPFundingSourceID)));
            }
        }

        public static void DeleteTreatmentBMPFundingSource(this ICollection<TreatmentBMPFundingSource> treatmentBMPFundingSourcesToDelete)
        {
            if(treatmentBMPFundingSourcesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPFundingSources.RemoveRange(treatmentBMPFundingSourcesToDelete);
            }
        }

        public static void DeleteTreatmentBMPFundingSource(this int treatmentBMPFundingSourceID)
        {
            DeleteTreatmentBMPFundingSource(new List<int> { treatmentBMPFundingSourceID });
        }

        public static void DeleteTreatmentBMPFundingSource(this TreatmentBMPFundingSource treatmentBMPFundingSourceToDelete)
        {
            DeleteTreatmentBMPFundingSource(new List<TreatmentBMPFundingSource> { treatmentBMPFundingSourceToDelete });
        }
    }
}