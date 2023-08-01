﻿using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewViewData : EditViewData
    {

        public NewViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions,
            List<HydrologicSubarea> hydrologicSubareas, IEnumerable<TrashCaptureStatusType> trashCaptureStatusTypes) : base(stormwaterJurisdictions, hydrologicSubareas, trashCaptureStatusTypes)
        {
            
        }
    }
}
