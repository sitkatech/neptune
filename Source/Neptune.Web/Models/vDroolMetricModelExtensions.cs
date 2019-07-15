using System;

namespace Neptune.Web.Models
{
    // ReSharper disable once InconsistentNaming
    public static class vDroolMetricModelExtensions
    {
        public static DroolMetricSimple ToDroolMetricSimple(this vDroolMetric vDroolMetric)
        {
            return new DroolMetricSimple(vDroolMetric);
        }
    }
}
