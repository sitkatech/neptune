using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;
using Neptune.Web.Services;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {

        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("File")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public IFormFile File { get; set; }

        [Required]
        [DisplayName("Display Name")]
        [MaxLength(EFModels.Entities.WaterQualityManagementPlanDocument.FieldLengths.DisplayName)]
        public string DisplayName { get; set; }

        [DisplayName("Description")]
        [MaxLength(EFModels.Entities.WaterQualityManagementPlanDocument.FieldLengths.Description)]
        public string Description { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.WaterQualityManagementPlanDocumentType)]
        [Required]
        public int? WaterQualityManagementPlanDocumentTypeID { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
        }

        public async Task UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, Person currentPerson, NeptuneDbContext dbContext, FileResourceService fileResourceService)
        {
            var fileResource = await fileResourceService.CreateNewFromIFormFile(File, currentPerson);
            var newPhoto = new EFModels.Entities.WaterQualityManagementPlanDocument
            {
                WaterQualityManagementPlan = waterQualityManagementPlan, FileResource = fileResource,
                DisplayName = DisplayName,
                WaterQualityManagementPlanDocumentTypeID = WaterQualityManagementPlanDocumentTypeID.Value,
                UploadDate = DateTime.Now, Description = Description
            };
            await dbContext.WaterQualityManagementPlanDocuments.AddAsync(newPhoto);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            var errors = new List<ValidationResult>();

            if (dbContext.WaterQualityManagementPlanDocuments.AsNoTracking().Any(x =>
                x.WaterQualityManagementPlanID == WaterQualityManagementPlanID && x.DisplayName == DisplayName))
            {
                errors.Add(new SitkaValidationResult<NewViewModel, string>(
                    "Display Name is already in use for this plan.", m => m.DisplayName));
            }

            FileResourceService.ValidateFileSize(File, errors, "File");
            return errors;
        }
    }
}
