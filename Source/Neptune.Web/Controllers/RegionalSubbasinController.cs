using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Email;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.RegionalSubbasin;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Index = Neptune.Web.Views.RegionalSubbasin.Index;
using IndexViewData = Neptune.Web.Views.RegionalSubbasin.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var geoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var regionalSubbasinLayerName = NeptuneWebConfiguration.RegionalSubbasinLayerName;

            var viewData = new IndexViewData(CurrentPerson, new RegionalSubbasinMapInitJson("regionalSubbasinMap"), geoServerUrl, regionalSubbasinLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult UpstreamCatchments(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return Json(new {regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList()}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult UpstreamDelineation(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinIDs = regionalSubbasin.TraceUpstreamCatchmentsReturnIDList();

            regionalSubbasinIDs.Add(regionalSubbasin.RegionalSubbasinID);

            var unionOfUpstreamRegionalSubbasins = HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .Where(x => regionalSubbasinIDs.Contains(x.RegionalSubbasinID)).Select(x => x.CatchmentGeometry)
                .ToList().UnionListGeometries();

            // Remove interior slivers introduced in the case that the non-cascading union strategy is used (see UnionListGeometries for more info)
            var dbGeometry = unionOfUpstreamRegionalSubbasins.Buffer(0);

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(dbGeometry);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshHRUCharacteristics(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            return ViewRefreshHRUCharacteristics(regionalSubbasin, new ConfirmDialogFormViewModel());
        }


        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshHRUCharacteristics(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRefreshHRUCharacteristics(regionalSubbasin, viewModel);
            }

            HRUHelper.RetrieveAndSaveHRUCharacteristics(regionalSubbasin, x => x.RegionalSubbasinID = regionalSubbasin.RegionalSubbasinID);
            SetMessageForDisplay($"Successfully updated HRU Characteristics for {regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshHRUCharacteristics(RegionalSubbasin regionalSubbasin, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to refresh the HRU Statistics for {regionalSubbasin.Watershed} {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Detail(RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
        {
            var regionalSubbasin = regionalSubbasinPrimaryKey.EntityObject;
            var regionalSubbasinCatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry4326;

            var geoJson = DbGeometryToGeoJsonHelper.FromDbGeometryNoReproject(regionalSubbasinCatchmentGeometry4326);
            var geoJsonFeatureCollection = new FeatureCollection(new List<Feature> {geoJson});
            var layerGeoJson = new LayerGeoJson("Catchment Boundary", geoJsonFeatureCollection,"#000000", 1, LayerInitialVisibility.Show, false );
            var stormwaterMapInitJson = new StormwaterMapInitJson("map", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>{layerGeoJson}, new BoundingBox(regionalSubbasinCatchmentGeometry4326));


            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson,
                regionalSubbasin,
                new HRUCharacteristicsViewData(regionalSubbasin),
                stormwaterMapInitJson));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshFromOCSurvey()
        {
            return ViewRefreshFromOCSurvey(new ConfirmDialogFormViewModel());
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult RefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewRefreshFromOCSurvey(viewModel);
            }

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunRegionalSubbasinRefreshBackgroundJob(CurrentPerson.PersonID));
            SetMessageForDisplay("Regional Subbasins refresh will run in the background.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewRefreshFromOCSurvey(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to refresh the Regional Subbasins layer from OC Survey?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var viewData = new Views.RegionalSubbasin.GridViewData(CurrentPerson);
            return RazorView<Views.RegionalSubbasin.Grid, Views.RegionalSubbasin.GridViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<RegionalSubbasin> RegionalSubbasinGridJsonData()
        {
            var gridSpec = new RegionalSubbasinGridSpec();
            var regionalSubbasins = HttpRequestStorage.DatabaseEntities.RegionalSubbasins.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<RegionalSubbasin>(regionalSubbasins, gridSpec);
            return gridJsonNetJObjectResult;
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
