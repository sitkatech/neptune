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

        public EditWqmpParcelsViewData(Person currentPerson,
            Models.WaterQualityManagementPlan waterQualityManagementPlan,
            MapInitJson mapInitJson,
            SystemAttribute systemAttribute)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = waterQualityManagementPlan.GetDetailUrl();
            PageTitle = "Edit Associated Parcels";
            RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres;
            ViewDataForAngular = new EditWqmpParcelsViewDataForAngular(mapInitJson,
                waterQualityManagementPlan.WaterQualityManagementPlanParcels.Select(x => x.Parcel).ToList(),
                systemAttribute);
        }

        public class EditWqmpParcelsViewDataForAngular
        {
            public readonly MapInitJson MapInitJson;
            public readonly string FindParcelByNameUrl;
            public readonly string FindParcelByAddress;
            public readonly string TypeAheadInputId;
            public IDictionary<int, string> ParcelNumberByID;
            public IDictionary<int, string> ParcelAddressByID;
            public readonly string ParcelMapServiceLayerName;
            public readonly string MapServiceUrl;
            public readonly string ParcelFieldDefinitionLabel;

            public EditWqmpParcelsViewDataForAngular(MapInitJson mapInitJson, List<Models.Parcel> parcelsInViewModel, SystemAttribute systemAttribute)
            {
                MapInitJson = mapInitJson;
                FindParcelByNameUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(c => c.FindSimpleByAPN(null));
                FindParcelByAddress = SitkaRoute<ParcelController>.BuildUrlFromExpression(c => c.FindSimpleByAddress(null));
                TypeAheadInputId = "parcelSearch";
                ParcelNumberByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelNumber);
                ParcelAddressByID = parcelsInViewModel.ToDictionary(x => x.ParcelID, x => x.ParcelAddress);
                ParcelMapServiceLayerName = systemAttribute.ParcelLayerName;
                MapServiceUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
                ParcelFieldDefinitionLabel = Models.FieldDefinition.Parcel.GetFieldDefinitionLabel();
            }
        }
    }
}
