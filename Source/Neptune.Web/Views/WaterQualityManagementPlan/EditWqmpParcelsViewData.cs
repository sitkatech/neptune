using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpParcelsViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public EditWqmpParcelsViewDataForAngular ViewDataForAngular { get; }
        public decimal? RecordedWQMPAreaInAcres { get; }
        public string RefineAreaUrl { get; set; }

        public EditWqmpParcelsViewData(Person currentPerson,
            Models.WaterQualityManagementPlan waterQualityManagementPlan,
            MapInitJson mapInitJson)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = waterQualityManagementPlan.GetDetailUrl();
            RefineAreaUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpBoundary(WaterQualityManagementPlan));
            PageTitle = "Edit Associated Parcels";
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            ViewDataForAngular = new EditWqmpParcelsViewDataForAngular(mapInitJson,
                waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.Parcel).ToList());
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

            public EditWqmpParcelsViewDataForAngular(MapInitJson mapInitJson, List<Models.Parcel> parcelsInViewModel)
            {
                MapInitJson = mapInitJson;
                FindParcelByNameUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(c => c.FindSimpleByAPN(null));
                FindParcelByAddress = SitkaRoute<ParcelController>.BuildUrlFromExpression(c => c.FindSimpleByAddress(null));
                TypeAheadInputId = "parcelSearch";
                ParcelNumberByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelNumber);
                ParcelAddressByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelAddress);
                ParcelMapServiceLayerName = NeptuneWebConfiguration.ParcelLayerName;
                MapServiceUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
                ParcelFieldDefinitionLabel = FieldDefinitionType.Parcel.GetFieldDefinitionLabel();
            }
        }
    }
}
