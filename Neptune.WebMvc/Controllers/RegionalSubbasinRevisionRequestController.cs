using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest;
using System.IO.Compression;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Neptune.Common.Email;

namespace Neptune.WebMvc.Controllers
{
    public class RegionalSubbasinRevisionRequestController : NeptuneBaseController<RegionalSubbasinRevisionRequestController>
    {
        private readonly SitkaSmtpClientService _sitkaSmtpClientService;

        public RegionalSubbasinRevisionRequestController(NeptuneDbContext dbContext, ILogger<RegionalSubbasinRevisionRequestController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, SitkaSmtpClientService sitkaSmtpClientService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _sitkaSmtpClientService = sitkaSmtpClientService;
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var gridSpec = new RegionalSubbasinRevisionRequestGridSpec(_linkGenerator);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, gridSpec);
            return RazorView<Views.RegionalSubbasinRevisionRequest.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<RegionalSubbasinRevisionRequest> IndexGridJsonData()
        {
            var gridSpec = new RegionalSubbasinRevisionRequestGridSpec(_linkGenerator);
            var regionalSubbasinRevisionRequests = RegionalSubbasinRevisionRequests.List(_dbContext, CurrentPerson);
            return new GridJsonNetJObjectResult<RegionalSubbasinRevisionRequest>(regionalSubbasinRevisionRequests, gridSpec);
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new NewViewModel();
            return ViewNew(viewModel, treatmentBMP);
        }

        private ViewResult ViewNew(NewViewModel viewModel, TreatmentBMP treatmentBMP)
        {
            var geometry = treatmentBMP.GetCentralizedDelineationGeometry4326(_dbContext);
            var featureCollection = new FeatureCollection { new Feature(geometry, new AttributesTable()) };
            var layerGeoJson = new LayerGeoJson("centralizedDelineationLayer",
                featureCollection, "#ffff00", .5f, LayerInitialVisibility.Show);

            var mapInitJson = new RegionalSubbasinRevisionRequestMapInitJson("revisionRequestMap",
                MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>(),
                new BoundingBoxDto(new List<Geometry> { geometry }), layerGeoJson);

            var viewData = new NewViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, treatmentBMP, mapInitJson, _webConfiguration.ParcelMapServiceUrl);

            return RazorView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var openRequest = RegionalSubbasinRevisionRequests.ListByTreatmentBMPID(_dbContext, treatmentBMP.TreatmentBMPID).SingleOrDefault(x => x.RegionalSubbasinRevisionRequestStatusID == (int) RegionalSubbasinRevisionRequestStatusEnum.Open);
            Check.Assert(openRequest == null, "You cannot open a new revision request for this BMP because there is already an open revision request.");

            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel, treatmentBMP);
            }

            var unionListGeometries = viewModel.WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR).Buffer(0)).ToList().UnionListGeometries();

            var regionalSubbasinRevisionRequest = new RegionalSubbasinRevisionRequest
            {
                TreatmentBMPID = treatmentBMPPrimaryKey.PrimaryKeyValue,
                RegionalSubbasinRevisionRequestGeometry = unionListGeometries,
                RequestPersonID = CurrentPerson.PersonID,
                RequestDate = DateTime.Now,
                RegionalSubbasinRevisionRequestStatusID = (int) RegionalSubbasinRevisionRequestStatusEnum.Open,
                Notes = viewModel.Notes
            };

            await _dbContext.RegionalSubbasinRevisionRequests.AddAsync(regionalSubbasinRevisionRequest);
            await _dbContext.SaveChangesAsync();

            await SendRSBRevisionRequestSubmittedEmail(CurrentPerson, treatmentBMP, regionalSubbasinRevisionRequest);

            SetMessageForDisplay("Successfully submitted the Regional Subbasin Revision Request");

            return RedirectToAction(
                new SitkaRoute<RegionalSubbasinRevisionRequestController>(_linkGenerator, x => x.Detail(regionalSubbasinRevisionRequest)));
        }


        [HttpGet("{regionalSubbasinRevisionRequestPrimaryKey}")]
        [RegionalSubbasinRevisionRequestViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinRevisionRequestPrimaryKey")]
        public ViewResult Detail([FromRoute] RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var regionalSubbasinRevisionRequest = RegionalSubbasinRevisionRequests.GetByID(_dbContext, regionalSubbasinRevisionRequestPrimaryKey);
            return ViewDetail(regionalSubbasinRevisionRequest);
        }

        private ViewResult ViewDetail(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            var geometry = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestGeometry;
            var feature = new Feature(geometry, new AttributesTable());
            var featureCollection = new FeatureCollection { feature };
            var layerGeoJson = new LayerGeoJson("centralizedDelineationLayer", featureCollection, "#ffff00", .5f, LayerInitialVisibility.Show);

            var mapInitJson = new RegionalSubbasinRevisionRequestMapInitJson("revisionRequestMap", MapInitJson.DefaultZoomLevel, new List<LayerGeoJson>(), new BoundingBoxDto(new List<Geometry> { geometry }), layerGeoJson);

            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, regionalSubbasinRevisionRequest, mapInitJson, _webConfiguration.ParcelMapServiceUrl);

            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{regionalSubbasinRevisionRequestPrimaryKey}")]
        [RegionalSubbasinRevisionRequestCloseFeature]
        public PartialViewResult Close([FromRoute] RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var viewModel = new CloseViewModel();
            return ViewClose(viewModel);
        }

        private PartialViewResult ViewClose(CloseViewModel viewModel)
        {

            return RazorPartialView<Close, CloseViewData, CloseViewModel>(new CloseViewData(), viewModel);
        }

        [HttpPost("{regionalSubbasinRevisionRequestPrimaryKey}")]
        [RegionalSubbasinRevisionRequestCloseFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinRevisionRequestPrimaryKey")]
        public async Task<IActionResult> Close([FromRoute] RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey,
            CloseViewModel viewModel)
        {
            var regionalSubbasinRevisionRequest = RegionalSubbasinRevisionRequests.GetByIDWithChangeTracking(_dbContext, regionalSubbasinRevisionRequestPrimaryKey);

            if (!ModelState.IsValid)
            {
                return ViewClose(viewModel);
            }

            regionalSubbasinRevisionRequest.CloseNotes = viewModel.CloseNotes;
            regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestStatusID = (int) RegionalSubbasinRevisionRequestStatusEnum.Closed;
            regionalSubbasinRevisionRequest.ClosedByPersonID = CurrentPerson.PersonID;
            regionalSubbasinRevisionRequest.ClosedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            await SendRSBRevisionRequestClosedEmail(CurrentPerson, regionalSubbasinRevisionRequest);

            SetMessageForDisplay("Successfully closed the Regional Subbasin Revision Request");

            return new ModalDialogFormJsonResult(
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(_linkGenerator, x =>
                    x.Detail(regionalSubbasinRevisionRequestPrimaryKey)));
        }

        [HttpGet("{regionalSubbasinRevisionRequestPrimaryKey}")]
        [RegionalSubbasinRevisionRequestDownloadFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("regionalSubbasinRevisionRequestPrimaryKey")]
        public FileResult Download([FromRoute] RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
        {
            var regionalSubbasinRevisionRequest =  RegionalSubbasinRevisionRequests.GetByID(_dbContext, regionalSubbasinRevisionRequestPrimaryKey);
            var geometry = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestGeometry;

            var reprojectedGeometry = geometry.ProjectTo2230();
            var geoJson = new Feature(reprojectedGeometry, new AttributesTable());
            var serializedGeoJson = GeoJsonSerializer.Serialize(geoJson);

            var outputLayerName = $"BMP_{regionalSubbasinRevisionRequest.TreatmentBMP.TreatmentBMPID}_RevisionRequest";

            //todo: ogr
            return File(Stream.Null, "application/zip", $"{outputLayerName}.zip");

            //using (var workingDirectory = new DisposableTempDirectory())
            //{
            //    var outputPathForLayer =
            //        Path.Combine(workingDirectory.DirectoryInfo.FullName, outputLayerName);


            //    var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(_webConfiguration.Ogr2OgrExecutable,
            //        CoordinateSystemHelper.NAD_83_CA_ZONE_VI_SRID,
            //        _webConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            //    ogr2OgrCommandLineRunner.ImportGeoJsonToFileGdb(serializedGeoJson, outputPathForLayer,
            //        outputLayerName, false, true);

            //    using (var zipFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".zip"))
            //    {
            //        ZipFile.CreateFromDirectory(workingDirectory.DirectoryInfo.FullName, zipFile.FileInfo.FullName);
            //        var fileStream = zipFile.FileInfo.OpenRead();
            //        var bytes = fileStream.ReadFully();
            //        fileStream.Close();
            //        fileStream.Dispose();
            //        return File(bytes, "application/zip", $"{outputLayerName}.zip");
            //    }
            //}
        }


        private async Task SendRSBRevisionRequestSubmittedEmail(Person requestPerson,
            TreatmentBMP requestBMP, RegionalSubbasinRevisionRequest request)
        {
            var subject = $"A new Regional Subbasin Revision Request was submitted";
            var firstName = requestPerson.FirstName;
            var lastName = requestPerson.LastName;
            var organizationName = requestPerson.Organization.OrganizationName;
            var bmpName = requestBMP.TreatmentBMPName;
            var requestDetails = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(request.RegionalSubbasinRevisionRequestID));
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
                From = new MailAddress(_webConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.CC.Add(requestPersonEmail);

            foreach (var revisionRequestPeople in GetPeopleWhoReceiveRSBRevisionRequests(_dbContext))
            {
                mailMessage.To.Add(revisionRequestPeople.Email);
            }

            await _sitkaSmtpClientService.Send(mailMessage);
        }

        private async Task SendRSBRevisionRequestClosedEmail(Person closingPerson, RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {

            var subject = $"A Regional Subbasin Revision Request was closed";
            var firstName = closingPerson.FirstName;
            var lastName = closingPerson.LastName;
            var organizationName = closingPerson.Organization.OrganizationName;
            var treatmentBMP = regionalSubbasinRevisionRequest.TreatmentBMP;
            var treatmentBMPName = treatmentBMP.TreatmentBMPName;
            var delineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(_linkGenerator, x => x.DelineationMap(treatmentBMP.TreatmentBMPID));
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
The Regional Subbasin Revision Request for BMP {treatmentBMPName.Trim()} was just closed by {firstName} {lastName} ({organizationName}).
<br /><br />
The changes resulting from this update will be available for your use next Monday. At that time you will be able to revise the delineation for {treatmentBMPName.Trim()}.
<br /><br />
<a href='{delineationMapUrl}'>Revise the delineation for BMP {treatmentBMPName.Trim()} on the Delineation map</a>. 
<br/><br />
 <div>Thanks for keeping the Regional Subbasin Network up to date within the Orange County Stormwater Tools.</div>
</div>
";
            // Create Notification
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_webConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(regionalSubbasinRevisionRequest.RequestPerson.Email);

            foreach (var revisionRequestPeople in GetPeopleWhoReceiveRSBRevisionRequests(_dbContext))
            {
                mailMessage.CC.Add(revisionRequestPeople.Email);
            }

            await _sitkaSmtpClientService.Send(mailMessage);
        }

        private static List<Person> GetPeopleWhoReceiveRSBRevisionRequests(NeptuneDbContext dbContext)
        {
            return dbContext.People.AsNoTracking().Where(x => x.ReceiveRSBRevisionRequestEmails && x.IsActive).ToList()
                .OrderBy(ht => ht.GetFullNameLastFirst()).ToList();
        }
    }
}
namespace Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest
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
