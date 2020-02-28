using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.Email;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.RegionalSubbasinRevisionRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Newtonsoft.Json;

namespace Neptune.Web.Controllers
{
    public class RegionalSubbasinRevisionRequestController : NeptuneBaseController
    {

        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, new RegionalSubbasinRevisionRequestGridSpec());
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequestGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            var regionalSubbasinRevisionRequests = GetTreatmentBmpsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult =
                new GridJsonNetJObjectResult<RegionalSubbasinRevisionRequest>(regionalSubbasinRevisionRequests,
                    gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<RegionalSubbasinRevisionRequest> GetTreatmentBmpsAndGridSpec(
            out RegionalSubbasinRevisionRequestGridSpec gridSpec, Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new RegionalSubbasinRevisionRequestGridSpec();
            return HttpRequestStorage.DatabaseEntities.RegionalSubbasinRevisionRequests.ToList()
                .Where(x => x.CanView(CurrentPerson)).ToList();
        }

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
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(geometry);

            var layerGeoJson = new LayerGeoJson("centralizedDelineationLayer",
                new FeatureCollection(new List<Feature> { feature }), "#ffff00", .5m, LayerInitialVisibility.Show);

            var mapInitJson = new RegionalSubbasinRevisionRequestMapInitJson("revisionRequestMap",
                MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>(),
                new BoundingBox(new List<DbGeometry> { geometry }), layerGeoJson);

            var viewData = new NewViewData(CurrentPerson, treatmentBMP, mapInitJson);

            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var openRequest = treatmentBMP.RegionalSubbasinRevisionRequests.SingleOrDefault(x => x.RegionalSubbasinRevisionRequestStatusID == RegionalSubbasinRevisionRequestStatus.Open.RegionalSubbasinRevisionRequestStatusID);
            Check.Assert(openRequest == null, "You cannot open a new revision request for this BMP because there is already an open revision request.");

            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, treatmentBMP);
            }

            var dbGeometrys = viewModel.WktAndAnnotations.Select(x =>
                DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.WGS_1984_SRID).ToSqlGeometry().MakeValid()
                    .ToDbGeometry());
            var unionListGeometries =
                dbGeometrys.ToList().UnionListGeometries().FixSrid(CoordinateSystemHelper.WGS_1984_SRID);

            var regionalSubbasinRevisionRequest = new RegionalSubbasinRevisionRequest(
                    treatmentBMPPrimaryKey.PrimaryKeyValue, unionListGeometries, CurrentPerson.PersonID,
                    RegionalSubbasinRevisionRequestStatus.Open.RegionalSubbasinRevisionRequestStatusID, DateTime.Now)
            { Notes = viewModel.Notes };

            HttpRequestStorage.DatabaseEntities.RegionalSubbasinRevisionRequests.Add(regionalSubbasinRevisionRequest);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SendRSBRevisionRequestSubmittedEmail(CurrentPerson, treatmentBMP, regionalSubbasinRevisionRequest);

            SetMessageForDisplay("Successfully submitted the Regional Subbasin Revision Request");

            return RedirectToAction(
                new SitkaRoute<RegionalSubbasinRevisionRequestController>(
                    x => x.Detail(regionalSubbasinRevisionRequest)));
        }


        [HttpGet]
        [RegionalSubbasinRevisionRequestViewFeature]
        public ViewResult Detail(RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var treatmentBMP = regionalSubbasinRevisionRequestPrimaryKey.EntityObject;

            return ViewDetail(treatmentBMP);
        }

        private ViewResult ViewDetail(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            var geometry = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestGeometry;
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(geometry);

            var layerGeoJson = new LayerGeoJson("centralizedDelineationLayer",
                new FeatureCollection(new List<Feature> { feature }), "#ffff00", .5m, LayerInitialVisibility.Show);

            var mapInitJson = new RegionalSubbasinRevisionRequestMapInitJson("revisionRequestMap",
                MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>(),
                new BoundingBox(new List<DbGeometry> { geometry }), layerGeoJson);

            var viewData = new DetailViewData(CurrentPerson, regionalSubbasinRevisionRequest, mapInitJson);

            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [RegionalSubbasinRevisionRequestCloseFeature]
        public PartialViewResult Close(
            RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var viewModel = new CloseViewModel();

            return ViewClose(viewModel);
        }

        private PartialViewResult ViewClose(CloseViewModel viewModel)
        {

            return RazorPartialView<Close, CloseViewData, CloseViewModel>(new CloseViewData(), viewModel);
        }

        [HttpPost]
        [RegionalSubbasinRevisionRequestCloseFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Close(RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey,
            CloseViewModel viewModel)
        {
            var regionalSubbasinRevisionRequest = regionalSubbasinRevisionRequestPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewClose(viewModel);
            }

            regionalSubbasinRevisionRequest.CloseNotes = viewModel.CloseNotes;
            regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestStatusID =
                RegionalSubbasinRevisionRequestStatus.Closed.RegionalSubbasinRevisionRequestStatusID;
            regionalSubbasinRevisionRequest.ClosedByPersonID = CurrentPerson.PersonID;
            regionalSubbasinRevisionRequest.ClosedDate = DateTime.Now;
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SendRSBRevisionRequestClosedEmail(CurrentPerson, regionalSubbasinRevisionRequest);

                SetMessageForDisplay("Successfully closed the Regional Subbasin Revision Request");

            return new ModalDialogFormJsonResult(
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x =>
                    x.Detail(regionalSubbasinRevisionRequestPrimaryKey)));
        }

        [HttpGet]
        [RegionalSubbasinRevisionRequestDownloadFeature]
        public FileResult Download(RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var geometry = regionalSubbasinRevisionRequestPrimaryKey.EntityObject
                .RegionalSubbasinRevisionRequestGeometry;

            var reprojectedGeometry = CoordinateSystemHelper.ProjectWebMercatorTo2230(geometry);

            var geoJson = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(reprojectedGeometry);

            var serializedGeoJson = JsonConvert.SerializeObject(geoJson);

            var outputLayerName = $"BMP_{regionalSubbasinRevisionRequestPrimaryKey.EntityObject.TreatmentBMP.TreatmentBMPID}_RevisionRequest";

            using (var workingDirectory = new DisposableTempDirectory())
            {
                var outputPathForLayer =
                    Path.Combine(workingDirectory.DirectoryInfo.FullName, outputLayerName);


                var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                    CoordinateSystemHelper.NAD_83_CA_ZONE_VI_SRID,
                    NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

                ogr2OgrCommandLineRunner.ImportGeoJsonToFileGdb(serializedGeoJson, outputPathForLayer,
                    outputLayerName, false, true);

                using (var zipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".zip"))
                {
                    ZipFile.CreateFromDirectory(workingDirectory.DirectoryInfo.FullName, zipFile.FileInfo.FullName);
                    var fileStream = zipFile.FileInfo.OpenRead();
                    var bytes = fileStream.ReadFully();
                    fileStream.Close();
                    fileStream.Dispose();
                    return File(bytes, "application/zip", $"{outputLayerName}.zip");
                }
            }
        }


        private static void SendRSBRevisionRequestSubmittedEmail(Models.Person requestPerson,
            Models.TreatmentBMP requestBMP, RegionalSubbasinRevisionRequest request)
        {
            var subject = $"A new Regional Subbasin Revision Request was submitted";
            var firstName = requestPerson.FirstName;
            var lastName = requestPerson.LastName;
            var organizationName = requestPerson.Organization.OrganizationName;
            var bmpName = requestBMP.TreatmentBMPName;
            var requestDetails = request.GetDetailUrl();
            var requestPersonEmail = requestPerson.Email;
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
A new Regional Subbasin Revision Request was just submitted by {firstName} {lastName} ({organizationName}) for BMP {bmpName}.
Please review it, make revisions, and close it at your earliest convenience. <a href='{requestDetails}'>View this Request</a>

<div>You received this email because you are assigned to receive Regional Subbasin Revision Request notifications within the Orange County Stormwater Tools. </div>.
</div>
";
            // Create Notification
            var mailMessage = new MailMessage
            {
                From = new MailAddress(Common.NeptuneWebConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.CC.Add(requestPersonEmail);

            foreach (var revisionRequestPeople in HttpRequestStorage.DatabaseEntities.People
                .GetPeopleWhoReceiveRSBRevisionRequests())
            {
                mailMessage.To.Add(revisionRequestPeople.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }

        private static void SendRSBRevisionRequestClosedEmail(Models.Person closingPerson,
            Models.RegionalSubbasinRevisionRequest request)
        {

            var subject = $"A Regional Subbasin Revision Request was closed";
            var firstName = closingPerson.FirstName;
            var lastName = closingPerson.LastName;
            var organizationName = closingPerson.Organization.OrganizationName;
            var bmpName = request.TreatmentBMP.TreatmentBMPName;
            var delineationMapUrl = request.TreatmentBMP.GetDelineationMapUrl();
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
The Regional Subbasin Revision Request for BMP {bmpName} was just closed by {firstName} {lastName} ({organizationName}). <a href='{delineationMapUrl}'>Revise the delineation for BMP {bmpName} on the Delineation map</a>. 

 <div>Thanks for keeping the Regional Subbasin Network up to date within the Orange County Stormwater Tools.</div>
</div>
";
            // Create Notification
            var mailMessage = new MailMessage
            {
                From = new MailAddress(Common.NeptuneWebConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(request.RequestPerson.Email);

            foreach (var revisionRequestPeople in HttpRequestStorage.DatabaseEntities.People
                .GetPeopleWhoReceiveRSBRevisionRequests())
            {
                mailMessage.CC.Add(revisionRequestPeople.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }
    }
}
namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class CloseViewData : NeptuneUserControlViewData
    {

    }

    public class CloseViewModel: FormViewModel
    {
        public string CloseNotes { get; set; }
    }

    public abstract class Close : TypedWebPartialViewPage<CloseViewData, CloseViewModel>
    {

    }
}
