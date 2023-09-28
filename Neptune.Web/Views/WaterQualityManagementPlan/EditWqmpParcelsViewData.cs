using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpParcelsViewData : NeptuneViewData
    {
        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public EditWqmpParcelsViewDataForAngular ViewDataForAngular { get; }
        public decimal? RecordedWQMPAreaInAcres { get; }

        public EditWqmpParcelsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan,
            MapInitJson mapInitJson, string mapServiceUrl, string parcelMapServiceLayerName, IEnumerable<WaterQualityManagementPlanParcel> waterQualityManagementPlanParcels)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan));
            PageTitle = "Edit Associated Parcels";
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            var findParcelByNameUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.FindSimpleByAPN(null));
            var findParcelByAddressUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.FindSimpleByAddress(null));
            ViewDataForAngular = new EditWqmpParcelsViewDataForAngular(mapInitJson,
                waterQualityManagementPlanParcels.Select(x => x.Parcel).ToList(), findParcelByNameUrl, findParcelByAddressUrl, mapServiceUrl, parcelMapServiceLayerName);
        }

        public class EditWqmpParcelsViewDataForAngular
        {
            public MapInitJson MapInitJson { get; }
            public string FindParcelByNameUrl { get; }
            public string FindParcelByAddress { get; }
            public string TypeAheadInputId { get; }
            public IDictionary<int, string> ParcelNumberByID { get; }
            public IDictionary<int, string> ParcelAddressByID { get; }
            public string ParcelMapServiceLayerName { get; }
            public string MapServiceUrl { get; }
            public string ParcelFieldDefinitionLabel { get; }

            public EditWqmpParcelsViewDataForAngular(MapInitJson mapInitJson, List<EFModels.Entities.Parcel> parcelsInViewModel, string findParcelByNameUrl, string findParcelByAddressUrl, string mapServiceUrl, string parcelMapServiceLayerName)
            {
                MapInitJson = mapInitJson;
                FindParcelByNameUrl = findParcelByNameUrl;
                FindParcelByAddress = findParcelByAddressUrl;
                TypeAheadInputId = "parcelSearch";
                ParcelNumberByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelNumber);
                ParcelAddressByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelAddress);
                ParcelMapServiceLayerName = parcelMapServiceLayerName;
                MapServiceUrl = mapServiceUrl;
                ParcelFieldDefinitionLabel = FieldDefinitionType.Parcel.GetFieldDefinitionLabel();
            }
        }
    }
}
