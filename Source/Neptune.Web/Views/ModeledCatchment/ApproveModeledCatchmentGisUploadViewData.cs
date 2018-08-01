using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ModeledCatchment
{
    public class ApproveModeledCatchmentGisUploadViewData : NeptuneViewData
    {
        public readonly Dictionary<int, string> ModeledCatchmentGeometryFeatureClassNames;
        public readonly MapInitJson MapInitJson;
        public readonly Dictionary<int, string> LayerColors;
        public readonly IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems;
        public readonly Dictionary<int, IEnumerable<SelectListItem>> ModeledCatchmentGeomertyLayerSelectProperties;
        public readonly string UploadGisReportUrlTemplate;
        public readonly string ModeledCatchmentIndexUrl;
        public readonly Dictionary<int, string> ModeledCatchmentStagingGeoJsons;

        public ApproveModeledCatchmentGisUploadViewData(Person currentPerson,
            MapInitJson mapInitJson,
            Dictionary<int, string> layerColors,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            string uploadGisReportUrlTemplateTemplate,
            string modeledCatchmentIndexUrl)
            : base(currentPerson)
        {
            ModeledCatchmentGeometryFeatureClassNames = currentPerson.ModeledCatchmentGeometryStagings.ToDictionary(x => x.ModeledCatchmentGeometryStagingID, x => x.FeatureClassName);
            MapInitJson = mapInitJson;
            LayerColors = layerColors;
            StormwaterJurisdictionSelectListItems =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            ModeledCatchmentGeomertyLayerSelectProperties = currentPerson.ModeledCatchmentGeometryStagings.ToDictionary(x => x.ModeledCatchmentGeometryStagingID,
                x => x.ToGeoJsonFeatureCollection().GetFeaturePropertyNames().ToSelectListWithEmptyFirstRow(y => y, y => y));
            UploadGisReportUrlTemplate = uploadGisReportUrlTemplateTemplate;
            ModeledCatchmentIndexUrl = modeledCatchmentIndexUrl;
            ModeledCatchmentStagingGeoJsons = currentPerson.ModeledCatchmentGeometryStagings.ToDictionary(x => x.ModeledCatchmentGeometryStagingID, x => x.GeoJson);
        }
    }
}