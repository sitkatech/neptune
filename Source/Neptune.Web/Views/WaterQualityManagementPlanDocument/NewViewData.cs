using System.Collections.Generic;
using System.Web.Mvc;

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
