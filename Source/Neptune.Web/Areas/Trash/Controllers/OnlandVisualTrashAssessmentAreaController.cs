using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentAreaController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [NeptuneViewFeature]
        [HttpPost]
        public JsonResult FindByName(FindAssessmentAreaByNameViewModel viewModel)
        {
            var searchString = viewModel.SearchTerm.Trim();
            var jurisdictionID = viewModel.JurisdictionID;
            var allOnlandVisualTrashAssessmentAreasMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Where(
                    x => x.StormwaterJurisdictionID == jurisdictionID && x.OnlandVisualTrashAssessmentAreaName.Contains(searchString)).ToList();

            var listItems = allOnlandVisualTrashAssessmentAreasMatchingSearchString.OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).Take(20).Select(x =>
            {
                var listItem = new ListItem(x.OnlandVisualTrashAssessmentAreaName, x.OnlandVisualTrashAssessmentAreaID.ToString(CultureInfo.InvariantCulture));
                return listItem;
            }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
    }

    public class FindAssessmentAreaByNameViewModel
    {
        public string SearchTerm { get; set; }
        public int JurisdictionID { get; set; }
    }
}
