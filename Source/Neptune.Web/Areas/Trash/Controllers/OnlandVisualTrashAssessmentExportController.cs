using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using static System.String;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentExportController : NeptuneBaseController
    {

        private string BuildGeoserverRequestUrl(OnlandVisualTrashAssessmentExportTypeEnum exportType,
            StormwaterJurisdiction stormwaterJurisdiction)
        {
            string typeName;

            switch (exportType)
            {
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportAreas:
                    typeName = "OCStormwater:OnlandVisualTrashAssessmentAreas";
                    break;
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportTransects:
                    typeName = "OCStormwater:OnlandVisualTrashAssessmentTransects";
                    break;
                case OnlandVisualTrashAssessmentExportTypeEnum.ExportObservationPoints:
                    typeName = "OCStormwater:OnlandVisualTrashAssessmentObservationPoints";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(exportType), exportType, null);
            }

            var cqlFilter = $"StormwaterJurisdictionID={stormwaterJurisdiction.StormwaterJurisdictionID}";

            var parameters = new
            {
                service = "WFS",
                version = "1.0.0",
                request = "GetFeature",
                typeName,
                outputFormat = "shape-zip",
                cql_filter = cqlFilter
            };

            return $"{NeptuneWebConfiguration.ParcelMapServiceUrl}?{GetQueryString(parameters)}";
        }

        // see: https://stackoverflow.com/questions/6848296/how-do-i-serialize-an-object-into-query-string-format
        private static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return Join("&", properties.ToArray());
        }
    }

    public enum OnlandVisualTrashAssessmentExportTypeEnum
    {
        ExportAreas,
        ExportTransects,
        ExportObservationPoints
    }
}