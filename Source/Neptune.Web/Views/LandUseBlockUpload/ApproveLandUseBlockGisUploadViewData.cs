using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class ApproveLandUseBlockGisUploadViewData : NeptuneViewData
    {
        public readonly Dictionary<int, string> DelineationGeometryFeatureClassNames;
        public readonly MapInitJson MapInitJson;
        public readonly Dictionary<int, string> LayerColors;
        public readonly IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems;
        public readonly Dictionary<int, IEnumerable<SelectListItem>> DelineationGeometryLayerSelectProperties;
        public readonly string UploadGisReportUrlTemplate;
        public readonly string DelineationIndexUrl;
        public readonly Dictionary<int, string> DelineationStagingGeoJsons;

        public ApproveLandUseBlockGisUploadViewData(Person currentPerson,
            MapInitJson mapInitJson,
            Dictionary<int, string> layerColors,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            string uploadGisReportUrlTemplateTemplate,
            string delineationIndexUrl)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            DelineationGeometryFeatureClassNames = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID, x => x.FeatureClassName);
            MapInitJson = mapInitJson;
            LayerColors = layerColors;
            StormwaterJurisdictionSelectListItems =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            DelineationGeometryLayerSelectProperties = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID,
                x => x.ToGeoJsonFeatureCollection().GetFeaturePropertyNames().ToSelectListWithEmptyFirstRow(y => y, y => y));
            UploadGisReportUrlTemplate = uploadGisReportUrlTemplateTemplate;
            DelineationIndexUrl = delineationIndexUrl;
            DelineationStagingGeoJsons = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID, x => x.DelineationGeometryStagingGeoJson);
        }
    }
}