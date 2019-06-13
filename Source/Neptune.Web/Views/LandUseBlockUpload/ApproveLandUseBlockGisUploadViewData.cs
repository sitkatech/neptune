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
        public readonly Dictionary<int, string> LandUseBlockGeometryFeatureClassNames;
        public readonly MapInitJson MapInitJson;
        public readonly Dictionary<int, string> LayerColors;
        public readonly IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems;
        public readonly Dictionary<int, IEnumerable<SelectListItem>> LandUseBlockGeometryLayerSelectProperties;
        public readonly string UploadGisReportUrlTemplate;
        public readonly string LandUseBlockIndexUrl;
        public readonly Dictionary<int, string> LandUseBlockStagingGeoJsons;

        public ApproveLandUseBlockGisUploadViewData(Person currentPerson,
            MapInitJson mapInitJson,
            Dictionary<int, string> layerColors,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            string uploadGisReportUrlTemplateTemplate,
            string landUseBlockIndexUrl)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            LandUseBlockGeometryFeatureClassNames = currentPerson.LandUseBlockGeometryStagings.ToDictionary(x => x.LandUseBlockGeometryStagingID, x => x.FeatureClassName);
            MapInitJson = mapInitJson;
            LayerColors = layerColors;
            StormwaterJurisdictionSelectListItems =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            LandUseBlockGeometryLayerSelectProperties = currentPerson.LandUseBlockGeometryStagings.ToDictionary(x => x.LandUseBlockGeometryStagingID,
                x => x.ToGeoJsonFeatureCollection().GetFeaturePropertyNames().ToSelectListWithEmptyFirstRow(y => y, y => y));
            UploadGisReportUrlTemplate = uploadGisReportUrlTemplateTemplate;
            LandUseBlockIndexUrl = landUseBlockIndexUrl;
            LandUseBlockStagingGeoJsons = currentPerson.LandUseBlockGeometryStagings.ToDictionary(x => x.LandUseBlockGeometryStagingID, x => x.LandUseBlockGeometryStagingGeoJson);
        }
    }
}