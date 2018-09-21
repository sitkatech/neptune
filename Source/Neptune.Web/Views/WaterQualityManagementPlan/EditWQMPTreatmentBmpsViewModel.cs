using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using LtInfo.Common.Models;
using MoreLinq;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpTreatmentBmpsViewModel : FormViewModel
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }
        public List<QuickBMPSimple> QuickBmpSimples { get; set; }
        public List<SourceControlBMPSimple> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpTreatmentBmpsViewModel()
        {
        }

        public EditWqmpTreatmentBmpsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<SourceControlBMPAttribute> sourceControlBMPAttributes)
        {
            TreatmentBmpIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
            QuickBmpSimples = waterQualityManagementPlan.QuickBMPs.Select(x => new QuickBMPSimple(x)).ToList();
            SourceControlBMPSimples = waterQualityManagementPlan.SourceControlBMPs.Select(x => new SourceControlBMPSimple(x)).ToList();

            foreach (var sourceControlBMPAttribute in sourceControlBMPAttributes)
            {
                if (!SourceControlBMPSimples.Select(x => x.SourceControlBMPAttributeName).Contains(sourceControlBMPAttribute.SourceControlBMPAttributeName))
                {
                    SourceControlBMPSimples.Add(new SourceControlBMPSimple(sourceControlBMPAttribute));
                }
            }
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMPSimple> quickBMPSimples)
        {
            waterQualityManagementPlan.TreatmentBMPs.ToList().ForEach(x => { x.WaterQualityManagementPlan = null; });
            TreatmentBmpIDs = TreatmentBmpIDs ?? new List<int>();

            var quickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllQuickBMPs.Local;
            var quickBMPsToUpdate = quickBMPSimples.Select(x => new QuickBMP(x, waterQualityManagementPlan.TenantID, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList();

            waterQualityManagementPlan.QuickBMPs.ToList().Merge(quickBMPsToUpdate, quickBMPsInDatabase,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID &&
                          x.QuickBMPID == y.QuickBMPID, (x, y) =>
                {
                    x.QuickBMPName = y.QuickBMPName;
                    x.QuickBMPNote = y.QuickBMPNote;
                    x.TreatmentBMPType = y.TreatmentBMPType;
                });

            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }
    }
}
