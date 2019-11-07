using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Modeling.Views.HRUCharacteristic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Modeling.Controllers
{
    public class HRUCharacteristicController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<HRUCharacteristic> HRUCharacteristicGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            List<HRUCharacteristic> treatmentBMPs = GetHRUCharacteristicsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<HRUCharacteristic>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<HRUCharacteristic> GetHRUCharacteristicsAndGridSpec(out HRUCharacteristicGridSpec gridSpec)
        {
            gridSpec = new HRUCharacteristicGridSpec();

            return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.ToList();
        }
    }
}