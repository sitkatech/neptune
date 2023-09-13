using Microsoft.AspNetCore.Mvc.Rendering;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class EditViewData
    {
        public IEnumerable<SelectListItem> AllDocumentTypes { get; }

        public EditViewData(IEnumerable<SelectListItem> allDocumentTypes)
        {
            AllDocumentTypes = allDocumentTypes;
        }
    }
}
