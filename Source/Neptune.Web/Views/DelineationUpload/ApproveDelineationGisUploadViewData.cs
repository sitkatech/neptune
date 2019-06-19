using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.DelineationUpload
{
    public class ApproveDelineationGisUploadViewData : NeptuneViewData
    {
        public Dictionary<int, string> DelineationGeometryFeatureClassNames { get; }
        public MapInitJson MapInitJson { get; }
        public Dictionary<int, string> LayerColors { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public Dictionary<int, IEnumerable<SelectListItem>> DelineationGeometryLayerSelectProperties { get; }
        public string UploadGisReportUrlTemplate { get; }
        public string DelineationIndexUrl { get; }
        public Dictionary<int, string> DelineationStagingGeoJsons { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }

        public ApproveDelineationGisUploadViewData(Person currentPerson,
            MapInitJson mapInitJson,
            Dictionary<int, string> layerColors,
            IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            string uploadGisReportUrlTemplate,
            string delienationIndexUrl)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            //DelineationGeometryFeatureClassNames = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID, x => x.FeatureClassName);
            //MapInitJson = mapInitJson;
            //LayerColors = layerColors;
            //StormwaterJurisdictionSelectListItems =
            //    stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
            //        .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            //DelineationGeometryLayerSelectProperties = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID,
            //    x => x.ToGeoJsonFeatureCollection().GetFeaturePropertyNames().ToSelectListWithEmptyFirstRow(y => y, y => y));
            //UploadGisReportUrlTemplate = uploadGisReportUrlTemplate;
            //DelineationIndexUrl = delienationIndexUrl;
            //DelineationStagingGeoJsons = currentPerson.DelineationGeometryStagings.ToDictionary(x => x.DelineationGeometryStagingID, x => x.DelineationGeometryStagingGeoJson);
        }

        public class ViewDataForAngularClass
        {
            
        }
    }
}