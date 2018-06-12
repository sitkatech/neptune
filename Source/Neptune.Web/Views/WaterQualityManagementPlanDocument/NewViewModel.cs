using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlanDocument
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
        }

        [Required]
        [DisplayName("Water Quality Management Plan")]
        public int WaterQualityManagementPlanID { get; set; }

        [Required]
        [DisplayName("File")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public HttpPostedFileBase File { get; set; }

        [Required]
        [DisplayName("Display Name")]
        [MaxLength(Models.WaterQualityManagementPlanDocument.FieldLengths.DisplayName)]
        public string DisplayName { get; set; }

        [DisplayName("Description")]
        [MaxLength(Models.WaterQualityManagementPlanDocument.FieldLengths.Description)]
        public string Description { get; set; }

        public void UpdateModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, Person currentPerson)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFile(File, currentPerson);
            HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            var waterQualityManagementPlanDocument =
                new Models.WaterQualityManagementPlanDocument(waterQualityManagementPlan, fileResource, DisplayName,
                    DateTime.Now) {Description = Description};
            HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanDocuments.Add(waterQualityManagementPlanDocument);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanDocuments.Any(x =>
                x.WaterQualityManagementPlanID == WaterQualityManagementPlanID && x.DisplayName == DisplayName))
            {
                yield return new SitkaValidationResult<NewViewModel, string>(
                    "Display Name is already in use for this plan.", m => m.DisplayName);
            }
        }
    }
}
