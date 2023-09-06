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
            MapInitJson mapInitJson)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan));
            PageTitle = "Edit Associated Parcels";
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            var findParcelByNameUrl = "";// todo: SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.FindSimpleByAPN(null));
            var findParcelByAddressUrl = ""; // todo: SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.FindSimpleByAddress(null));
            ViewDataForAngular = new EditWqmpParcelsViewDataForAngular(mapInitJson,
                waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.Parcel).ToList(), findParcelByNameUrl, findParcelByAddressUrl);
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

            public EditWqmpParcelsViewDataForAngular(MapInitJson mapInitJson, List<Parcel> parcelsInViewModel, string findParcelByNameUrl, string findParcelByAddressUrl)
            {
                MapInitJson = mapInitJson;
                FindParcelByNameUrl = findParcelByNameUrl;
                FindParcelByAddress = findParcelByAddressUrl;
                TypeAheadInputId = "parcelSearch";
                ParcelNumberByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelNumber);
                ParcelAddressByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelAddress);
                ParcelMapServiceLayerName = "";//todo: NeptuneWebConfiguration.ParcelLayerName;
                MapServiceUrl = "";//todo: NeptuneWebConfiguration.ParcelMapServiceUrl;
                ParcelFieldDefinitionLabel = FieldDefinitionType.Parcel.GetFieldDefinitionLabel();
            }
        }
    }
}
