using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Views.Parcel;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Areas.Trash.Controllers
{
    public class ParcelController : NeptuneBaseController<ParcelController>
    {
        public ParcelController(NeptuneDbContext dbContext, ILogger<ParcelController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet("{parcelPrimaryKey}")]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("parcelPrimaryKey")]
        public PartialViewResult TrashMapAssetPanel([FromRoute] ParcelPrimaryKey parcelPrimaryKey)
        {
            var parcel = parcelPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, parcel);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ContentResult Union()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public FeatureCollection Union(UnionOfParcelsViewModel viewModel)
        {
            var unionOfParcels = ParcelGeometries.UnionAggregateByParcelIDs(_dbContext, viewModel.ParcelIDs);
            var featureCollection = unionOfParcels.MultiPolygonToFeatureCollection();
            return featureCollection;
        }
    }
}
