﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public EditViewModel()
        {
        }

        public EditViewModel(Models.WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            WaterQualityManagementPlanDocumentID =
                waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentID;
            DisplayName = waterQualityManagementPlanDocument.DisplayName;
            Description = waterQualityManagementPlanDocument.Description;
        }

        [Required]
        [DisplayName("Document")]
        public int WaterQualityManagementPlanDocumentID { get; set; }

        [Required]
        [DisplayName("Display Name")]
        [MaxLength(Models.WaterQualityManagementPlanDocument.FieldLengths.DisplayName)]
        public string DisplayName { get; set; }

        [DisplayName("Description")]
        [MaxLength(Models.WaterQualityManagementPlanDocument.FieldLengths.Description)]
        public string Description { get; set; }

        public void UpdateModel(Models.WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            waterQualityManagementPlanDocument.DisplayName = DisplayName;
            waterQualityManagementPlanDocument.Description = Description;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanDocuments
                .Single(x => x.WaterQualityManagementPlanDocumentID == WaterQualityManagementPlanDocumentID)
                .WaterQualityManagementPlan.WaterQualityManagementPlanDocuments.Any(x =>
                    x.DisplayName == DisplayName &&
                    x.WaterQualityManagementPlanDocumentID != WaterQualityManagementPlanDocumentID))
            {
                yield return new SitkaValidationResult<EditViewModel, string>(
                    "Display Name is already in use on this plan.", m => m.DisplayName);
            }
        }
    }
}
