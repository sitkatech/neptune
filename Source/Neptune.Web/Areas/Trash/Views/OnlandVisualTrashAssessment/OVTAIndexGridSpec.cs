using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAIndexGridSpec : GridSpec<Models.OnlandVisualTrashAssessment>
    {
        public OVTAIndexGridSpec(Person currentPerson, bool showDelete, bool showEdit)
        {
            if (showDelete)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap("", false, false), 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap("",  false), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Created By", x => x.CreatedByPerson.GetFullNameFirstLastAsUrl(), 90,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Created On", x => x.CreatedDate, 90,DhtmlxGridColumnFormatType.Date);
        }
    }
}
