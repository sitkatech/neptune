using Microsoft.AspNetCore.Mvc.Rendering;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class NewViewData
    {
        public IEnumerable<SelectListItem> AllDocumentTypes { get; }

        public NewViewData(IEnumerable<SelectListItem> allDocumentTypes)
        {
            AllDocumentTypes = allDocumentTypes;
        }
    }
}
