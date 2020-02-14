using LtInfo.Common.Email;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.RegionalSubbasinRevisionRequest;
using System.Net.Mail;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinRevisionRequestController : NeptuneBaseController
    {
        public ViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var viewModel = new NewViewModel();

            return ViewNew(viewModel);
        }

        private ViewResult ViewNew(NewViewModel viewModel)
        {
            var viewData = new NewViewData(CurrentPerson);

            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }


        private void SendRSBRevisionRequestSubmittedEmail(Models.Person requestPerson, Models.TreatmentBMP requestBMP)
        {
            var subject = $"A new Regional Subbasin Revision Request was submitted";
            var firstName = requestPerson.FirstName;
            var lastName = requestPerson.LastName;
            var organizationName = requestPerson.Organization.OrganizationName;
            var bmpName = requestBMP.TreatmentBMPName;
            var bigTodo = "big TODO";
            string requestPersonEmail = requestPerson.Email;
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
A new Regional Subbasin Revision Request was just submitted by {firstName} {lastName} ({organizationName}) for BMP {bmpName}.
Please review it, make revisions, and close it at your earliest convenience. <a href='{bigTodo}'>View this Request</a>

<div>You received this email because you are assigned to receive Regional Subbasin Revision Request notifications within the Orange County Stormwater Tools. </div>.
</div>
";
            // Create Notification
            var mailMessage = new MailMessage { From = new MailAddress(Common.NeptuneWebConfiguration.DoNotReplyEmail), Subject = subject, Body = message, IsBodyHtml = true };

            mailMessage.CC.Add(requestPersonEmail);

            foreach (var revisionRequestPeople in HttpRequestStorage.DatabaseEntities.People.GetPeopleWhoReceiveRSBRevisionRequests())
            {
                mailMessage.To.Add(revisionRequestPeople.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }
        private void SendRSBRevisionRequestClosedEmail(Models.Person closingPerson, Models.RegionalSubbasinRevisionRequest request)
        {

            var subject = $"A Regional Subbasin Revision Request was closed";
            var firstName = closingPerson.FirstName;
            var lastName = closingPerson.LastName;
            var organizationName = closingPerson.Organization.OrganizationName;
            var bmpName = request.TreatmentBMP.TreatmentBMPName;
            var bigTodo = "big TODO";
            string requestPersonEmail = closingPerson.Email;
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
The Regional Subbasin Revision Request for BMP {bmpName} was just closed by {firstName} {lastName} ({organizationName}). <a href='{bigTodo}'>Revise the delineation for BMP {bmpName} on the Delineation map</a>. 

 <div>Thanks for keeping the Regional Subbasin Network up to date within the Orange County Stormwater Tools.</div>
</div>
";
            // Create Notification
            var mailMessage = new MailMessage { From = new MailAddress(Common.NeptuneWebConfiguration.DoNotReplyEmail), Subject = subject, Body = message, IsBodyHtml = true };

            mailMessage.To.Add(request.RequestPerson.Email);

            foreach (var revisionRequestPeople in HttpRequestStorage.DatabaseEntities.People.GetPeopleWhoReceiveRSBRevisionRequests())
            {
                mailMessage.CC.Add(revisionRequestPeople.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }
    }
}