using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public static class DelineationModelExtensions
    {
        public static string GetDelineationAreaString(this Delineation delineation)
        {
            //todo: move the sqm - ac conversion factor to a const
            return (delineation?.DelineationGeometry.Area * 2471050)?.ToString("0.00") ?? "-";
        }
    }
}