using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewViewData : EditViewData
    {

        public NewViewData(IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(stormwaterJurisdictions)
        {
            
        }
    }
}
