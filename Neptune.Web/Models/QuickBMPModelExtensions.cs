using Neptune.EFModels.Entities;

namespace Neptune.Web.Models
{
    public static class QuickBMPModelExtensions
    {
        public static bool IsFullyParameterized(this QuickBMP x) =>
            x.PercentOfSiteTreated != null && x.PercentCaptured != null && x.PercentRetained != null && x.TreatmentBMPType.IsAnalyzedInModelingModule;
    }
}