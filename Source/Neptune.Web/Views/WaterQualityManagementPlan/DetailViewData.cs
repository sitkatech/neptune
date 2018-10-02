using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public bool CurrentPersonCanManageWaterQualityManagementPlans { get; }
        public bool CurrentPersonCanManageBMPs { get; }
        public string EditWaterQualityManagementPlanTreatmentBmpsUrl { get; }
        public string EditWaterQualityManagementPlanParcelsUrl { get; }
        public string NewWaterQualityManagementPlanDocumentUrl { get; }
        public MapInitJson MapInitJson { get; }
        public ParcelGridSpec ParcelGridSpec { get; }
        public string ParcelGridName { get; }
        public string ParcelGridDataUrl { get; }

        public string NewWqmpOMVerificationRecordUrl { get; }
        public string EditWqmpOMVerificationRecordUrl { get; }

        public List<Models.TreatmentBMP> TreatmentBMPs { get; }
        public List<QuickBMP> QuickBMPs { get; }

        public IEnumerable<IGrouping<int, SourceControlBMP>> SourceControlBMPs { get; }
        public List<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs  { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }

        public DetailViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan, MapInitJson mapInitJson, ParcelGridSpec parcelGridSpec, List<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs)
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            CurrentPersonCanManageWaterQualityManagementPlans = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan)
                .HasPermission;
            CurrentPersonCanManageBMPs = currentPerson.IsManagerOrAdmin();
            EditWaterQualityManagementPlanTreatmentBmpsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpBmps(WaterQualityManagementPlan));
            EditWaterQualityManagementPlanParcelsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpParcels(WaterQualityManagementPlan));
            NewWaterQualityManagementPlanDocumentUrl =
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                    c.New(waterQualityManagementPlan));
            MapInitJson = mapInitJson;
            ParcelGridSpec = parcelGridSpec;
            ParcelGridName = "parcelGrid";
            ParcelGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.ParcelsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));

            NewWqmpOMVerificationRecordUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.NewWqmpVerify(waterQualityManagementPlan));
            EditWqmpOMVerificationRecordUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.EditWqmpVerify(waterQualityManagementPlan));
            SourceControlBMPs = waterQualityManagementPlan.SourceControlBMPs.Where(x => x.IsPresent == true || x.SourceControlBMPNote != null).OrderBy(x => x.SourceControlBMPAttributeID).GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);
            WaterQualityManagementPlanVerifies = waterQualityManagementPlanVerifies;
            WaterQualityManagementPlanVerifyQuickBMPs = waterQualityManagementPlanVerifyQuickBmPs;
            WaterQualityManagementPlanVerifyTreatmentBMPs = waterQualityManagementPlanVerifyTreatmentBmPs;

            TreatmentBMPs = waterQualityManagementPlan.TreatmentBMPs.OrderBy(x => x.TreatmentBMPName).ToList();
            QuickBMPs = waterQualityManagementPlan.QuickBMPs.OrderBy(x => x.QuickBMPName).ToList();
            SourceControlBMPs = waterQualityManagementPlan.SourceControlBMPs.Where(x => x.SourceControlBMPNote != null || (x.IsPresent != null && x.IsPresent == true)).OrderBy(x => x.SourceControlBMPAttributeID).GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);


        }
    }
}
