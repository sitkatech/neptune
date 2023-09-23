using Neptune.EFModels.Entities;

namespace Neptune.Web.Models
{
    public static class QuickBMPModelExtensions
    {
        public static bool IsFullyParameterized(this QuickBMP x) =>
            x is { PercentOfSiteTreated: not null, PercentCaptured: not null, PercentRetained: not null, TreatmentBMPType.IsAnalyzedInModelingModule: true };
    }
}