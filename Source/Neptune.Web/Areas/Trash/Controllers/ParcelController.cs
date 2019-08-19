using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Views.Parcel;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class ParcelController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult TrashMapAssetPanel(ParcelPrimaryKey parcelPrimaryKey)
        {
            var parcel = parcelPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(CurrentPerson, parcel);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult Union()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewFeature]
        public ContentResult Union(UnionOfParcelsViewModel viewModel)
        {
            var unionOfParcels = HttpRequestStorage.DatabaseEntities.Parcels
                .Where(x => viewModel.ParcelIDs.Contains(x.ParcelID)).Select(x => x.ParcelGeometry).ToList()
                .UnionListGeometries();

            var featureCollection = new FeatureCollection();

            // Leaflet.Draw does not support multipolgyon editing because its dev team decided it wasn't necessary.
            // Unless https://github.com/Leaflet/Leaflet.draw/issues/268 is resolved, we have to break into separate polys.
            // On an unrelated note, DbGeometry.ElementAt is 1-indexed instead of 0-indexed, which is terrible.
            for (var i = 1; i <= unionOfParcels.ElementCount.GetValueOrDefault(); i++)
            {
                var dbGeometry = unionOfParcels.ElementAt(i);
                    // Reduce is SQL Server's implementation of the Douglas–Peucker downsampling algorithm
                    dbGeometry = dbGeometry.ToSqlGeometry().Reduce(.0000025).ToDbGeometry();
                
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChecc(dbGeometry);
                featureCollection.Features.Add(feature);
            }
            
            return Content(JObject.FromObject(featureCollection).ToString(Formatting.None));
        }
    }

    public class UnionOfParcelsViewModel
    {
        public List<int> ParcelIDs { get; set; }
    }
}
