using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
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
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap("", false, true), 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(),  true), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Created By", x => x.CreatedByPerson.GetFullNameFirstLastAsUrl(), 90,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Created On", x => x.CreatedDate, 90,DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => x.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 170);
        }
    }
}
