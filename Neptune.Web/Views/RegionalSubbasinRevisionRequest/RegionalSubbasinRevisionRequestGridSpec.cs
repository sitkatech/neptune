using Microsoft.AspNetCore.Html;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class RegionalSubbasinRevisionRequestGridSpec : GridSpec<EFModels.Entities.RegionalSubbasinRevisionRequest>
    {
        public RegionalSubbasinRevisionRequestGridSpec(LinkGenerator linkGenerator)
        {
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
            var personDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
            var treatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add("", x => new HtmlString(
                $"<a class='gridButton' href='{detailUrlTemplate.ParameterReplace(x.RegionalSubbasinRevisionRequestID)}'>View</a>"), 40, DhtmlxGridColumnFilterType.None );
            Add("BMP Name", x => UrlTemplate.MakeHrefString(treatmentBMPDetailUrlTemplate.ParameterReplace(x.TreatmentBMPID), x.TreatmentBMP.TreatmentBMPName), 200, DhtmlxGridColumnFilterType.Text);
            Add("Date Submitted", x=> x.RequestDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Requested By", x=> UrlTemplate.MakeHrefString(personDetailUrlTemplate.ParameterReplace(x.RequestPersonID), x.RequestPerson.GetFullNameFirstLast()), 120, DhtmlxGridColumnFilterType.Text);
            Add("Status", x=> x.RegionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusDisplayName, 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Date Closed", x=> x.ClosedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Closed By", x => x.ClosedByPerson == null ? new HtmlString(string.Empty) : UrlTemplate.MakeHrefString(personDetailUrlTemplate.ParameterReplace(x.ClosedByPersonID.Value), x.ClosedByPerson.GetFullNameFirstLast()), 120, DhtmlxGridColumnFilterType.Text);
            Add("Notes", x=> x.Notes, 300, DhtmlxGridColumnFilterType.Text);
            Add("Close Notes", x=> x.CloseNotes, 300, DhtmlxGridColumnFilterType.Text);

        }
    }
}