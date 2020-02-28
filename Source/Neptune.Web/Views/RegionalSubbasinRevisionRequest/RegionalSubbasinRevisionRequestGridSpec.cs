using System.Web;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class RegionalSubbasinRevisionRequestGridSpec : GridSpec<Models.RegionalSubbasinRevisionRequest>
    {
        public RegionalSubbasinRevisionRequestGridSpec()
        {
            Add("", x => new HtmlString(
                $"<a class='gridButton' href='{x.GetDetailUrl()}'>View</a>"), 40, DhtmlxGridColumnFilterType.None );
            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 200, DhtmlxGridColumnFilterType.Text);
            Add("Date Submitted", x=>x.RequestDate, 120,DhtmlxGridColumnFormatType.Date);
            Add("Requested By", x=>x.RequestPerson.GetFullNameFirstLastAsUrl(), 120, DhtmlxGridColumnFilterType.Text);
            Add("Status", x=>x.RegionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusDisplayName, 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date Closed", x=>x.ClosedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Closed By", x => x.ClosedByPerson?.GetFullNameFirstLastAsUrl() ?? new HtmlString(string.Empty), 120, DhtmlxGridColumnFilterType.Text);
            Add("Notes", x=>x.Notes, 300, DhtmlxGridColumnFilterType.Text);
            Add("Close Notes", x=>x.CloseNotes, 300, DhtmlxGridColumnFilterType.Text);

        }
    }
}