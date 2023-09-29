using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {

        [Required]
        [DisplayName("Document")]
        public int WaterQualityManagementPlanDocumentID { get; set; }

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

        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            WaterQualityManagementPlanDocumentID =
                waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentID;
            DisplayName = waterQualityManagementPlanDocument.DisplayName;
            Description = waterQualityManagementPlanDocument.Description;
            WaterQualityManagementPlanDocumentTypeID =
                waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentTypeID;
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            waterQualityManagementPlanDocument.DisplayName = DisplayName;
            waterQualityManagementPlanDocument.Description = Description;
            waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentTypeID =
                WaterQualityManagementPlanDocumentTypeID.GetValueOrDefault(); // will never default due to RequiredAttribute
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            var waterQualityManagementPlanDocument = dbContext.WaterQualityManagementPlanDocuments.AsNoTracking()
                .Single(x => x.WaterQualityManagementPlanDocumentID == WaterQualityManagementPlanDocumentID);
            if (WaterQualityManagementPlanDocuments.ListByWaterQualityManagementPlanID(dbContext, waterQualityManagementPlanDocument.WaterQualityManagementPlanID).Any(x =>
                    x.DisplayName == DisplayName &&
                    x.WaterQualityManagementPlanDocumentID != WaterQualityManagementPlanDocumentID))
            {
                yield return new SitkaValidationResult<EditViewModel, string>(
                    "Display Name is already in use on this plan.", m => m.DisplayName);
            }
        }
    }
}
