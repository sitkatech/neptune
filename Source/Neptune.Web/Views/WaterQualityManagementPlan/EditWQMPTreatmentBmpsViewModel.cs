﻿using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpTreatmentBmpsViewModel : FormViewModel
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpTreatmentBmpsViewModel()
        {
        }

        public EditWqmpTreatmentBmpsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            TreatmentBmpIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            waterQualityManagementPlan.TreatmentBMPs.ToList().ForEach(x => { x.WaterQualityManagementPlan = null; });
            TreatmentBmpIDs = TreatmentBmpIDs ?? new List<int>();
            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }
    }
}
