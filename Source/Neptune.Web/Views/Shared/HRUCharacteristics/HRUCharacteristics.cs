using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public abstract class HRUCharacteristics : TypedWebViewPage<HRUCharacteristicsViewData>
    {
    }

    public class HRUCharacteristicsViewData : NeptuneUserControlViewData
    {
        public IHaveHRUCharacteristics EntityWithHRUCharacteristics { get; }
        
        public HRUCharacteristicsViewData(IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            EntityWithHRUCharacteristics = entityWithHRUCharacteristics;
        }
    }
}