using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class DelineationModelExtensions
    {
        public static string GetDelineationAreaString(this Delineation delineation)
        {
            //todo: move the sqm - ac conversion factor to a const
            return (delineation?.DelineationGeometry.Area * 2471050)?.ToString("0.00") ?? "-";
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate =
            new UrlTemplate<int>(
                SitkaRoute<DelineationController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this Delineation delineation)
        {
            return DeleteUrlTemplate.ParameterReplace(delineation.DelineationID);
        }

        public static string GetDetailUrl(this Delineation delineation)
        {
            return delineation.TreatmentBMP.GetDelineationMapUrl();
        }

        public static HtmlString GetDetailUrlForGrid(this Delineation delineation)
        {
            return new HtmlString($"<a href={GetDetailUrl(delineation)} class='gridButton'>View</a>");
        }
    }
}