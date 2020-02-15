using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.Email;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.RegionalSubbasinRevisionRequest;
using System.Net.Mail;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Charts;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Security;
using Neptune.Web.Views.Delineation;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinRevisionRequestController : NeptuneBaseController
    {
        [HttpGet]
        [TreatmentBMPEditFeature]
        public ViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            var viewModel = new NewViewModel();

            return ViewNew(viewModel, treatmentBMP);
        }

        private ViewResult ViewNew(NewViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var geometry = treatmentBMP.GetCentralizedDelineationGeometry();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(geometry);

            var layerGeoJson = new LayerGeoJson("centralizedDelineationLayer",
                new FeatureCollection(new List<Feature>{feature}), "#ffff00", .5m, LayerInitialVisibility.Show);

            var mapInitJson = new RegionalSubbasinRevisionRequestMapInitJson("revisionRequestMap",
                MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>(),
                new BoundingBox(new List<DbGeometry> {geometry}), layerGeoJson);

            var viewData = new NewViewData(CurrentPerson, treatmentBMP, mapInitJson);

            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, treatmentBMP);
            }

            var dbGeometrys = viewModel.WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.WGS_1984_SRID).ToSqlGeometry().MakeValid().ToDbGeometry());
            var unionListGeometries = dbGeometrys.ToList().UnionListGeometries().FixSrid(CoordinateSystemHelper.WGS_1984_SRID);

            var regionalSubbasinRevisionRequest = new RegionalSubbasinRevisionRequest(treatmentBMPPrimaryKey.PrimaryKeyValue, unionListGeometries, CurrentPerson.PersonID,
                RegionalSubbasinRevisionRequestStatus.Open.RegionalSubbasinRevisionRequestStatusID, DateTime.Now);

            HttpRequestStorage.DatabaseEntities.RegionalSubbasinRevisionRequests.Add(regionalSubbasinRevisionRequest);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SendRSBRevisionRequestSubmittedEmail(CurrentPerson, treatmentBMP);

            SetMessageForDisplay("Successfully submitted the Regional Subbasin Revision Request");
            // TODO - redirect to the request page instead of back to the delineation map
            return Redirect(treatmentBMP.GetDelineationMapUrl());
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