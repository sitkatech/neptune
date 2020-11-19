namespace Neptune.Web.Models
{
    public static class QuickBMPModelExtensions
    {
        public static bool IsFullyParameterized(this QuickBMP x)
        {
            return x.PercentOfSiteTreated != null && x.TreatmentBMPType.IsAnalyzedInModelingModule;
        }
    }
}