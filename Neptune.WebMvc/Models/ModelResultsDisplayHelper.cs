using System;
using Newtonsoft.Json.Linq;

namespace Neptune.WebMvc.Models
{
    public static class ModelResultsDisplayHelper
    {
        public static double RoundToSignificantDigits(this double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return (double)((decimal) scale * (decimal) Math.Round(d / scale, digits));
        }

        public static double ExtractDoubleValue(this JObject jobject, string key)
        {
            return jobject[key]?.Value<double>() ?? 0;
        }
    }
}