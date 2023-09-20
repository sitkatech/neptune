using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPDocument;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPDocumentController : NeptuneBaseController<TreatmentBMPDocumentController>
    {
        private readonly FileResourceService _fileResourceService;

        public TreatmentBMPDocumentController(NeptuneDbContext dbContext, ILogger<TreatmentBMPDocumentController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, FileResourceService fileResourceService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _fileResourceService = fileResourceService;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ActionResult New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var viewModel = new NewViewModel();
            return ViewNewTreatmentBMPDocument(viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> New([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewTreatmentBMPDocument(viewModel);
            }
            var treatmentBMPDocument = new TreatmentBMPDocument()
            {
                TreatmentBMP = treatmentBMP
            };
            await viewModel.UpdateModel(treatmentBMPDocument, CurrentPerson, _fileResourceService);
            await _dbContext.TreatmentBMPDocuments.AddAsync(treatmentBMPDocument);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewTreatmentBMPDocument(NewViewModel viewModel)
        {
            var viewData = new NewViewData();
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPDocumentPrimaryKey}")]
        [TreatmentBMPDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPDocumentPrimaryKey")]
        public PartialViewResult Delete([FromRoute] TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPDocument.TreatmentBMPID);
            return ViewDelete(treatmentBMPDocument, viewModel);
        }

        private PartialViewResult ViewDelete(TreatmentBMPDocument treatmentBMPDocument, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = true;
            var confirmMessage = canDelete
                ? "Are you sure you want to delete this Treatment BMP Document?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP Document",
                     UrlTemplate.MakeHrefString(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Detail(treatmentBMPDocument.TreatmentBMPID)), "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPDocumentPrimaryKey}")]
        [TreatmentBMPDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPDocumentPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(treatmentBMPDocument, viewModel);
            }
            _dbContext.TreatmentBMPDocuments.Remove(treatmentBMPDocument);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{treatmentBMPDocumentPrimaryKey}")]
        [TreatmentBMPDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPDocumentPrimaryKey")]
        public ActionResult Edit([FromRoute] TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMPDocument);
            return ViewEdit(viewModel);
        }

        [HttpPost("{treatmentBMPDocumentPrimaryKey}")]
        [TreatmentBMPDocumentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPDocumentPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] TreatmentBMPDocumentPrimaryKey treatmentBMPDocumentPrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPDocument = treatmentBMPDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(treatmentBMPDocument, CurrentPerson);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var viewData = new EditViewData();
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

    }
}