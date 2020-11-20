using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public static class QuickBMPModelExtensions
    {
        public static bool IsFullyParameterized(this QuickBMP x) =>
            x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule;

        // It's irritating that this code can't share the above code
        // but an answer to this SE says it should be can: https://stackoverflow.com/questions/31399955/linq-to-entities-does-not-support-extension-method
        public static List<QuickBMP> GetFullyParameterized(this IQueryable<QuickBMP> quickBMPs)
        {
            return quickBMPs.Where(x => x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule)
                .ToList();
        }
    }
}