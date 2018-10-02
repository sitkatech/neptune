using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Neptune.Web.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpVerifyViewModel : FormViewModel, IValidatableObject
    {

        public int WaterQualityManagementPlanID { get; set; }
        public int? WaterQualityManagementPlanVerifyID { get; set;  }

        [Required]
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }

        [Required]
        public int WaterQualityManagementPlanVisitStatusID { get; set; }

        [DisplayName("Structural BMP O&M Document")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public HttpPostedFileBase StructuralDocumentFile { get; set; }

        public bool DeleteStructuralDocumentFile { get; set; }

        public string StructuralDocumentFileName { get; set; }

        [Required]
        public int WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string SourceControlCondition { get; set; }

        public Models.WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public List<WaterQualityManagementPlanVerifyQuickBMPSimple> WaterQualityManagementPlanVerifyQuickBMPSimples { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> WaterQualityManagementPlanVerifyTreatmentBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(Models.WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlanVerify.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;

            WaterQualityManagementPlanVerifyTypeID =
                waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeID;
            WaterQualityManagementPlanVisitStatusID =
                waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusID;
            WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerify
                .WaterQualityManagementPlanVerifyStatus.WaterQualityManagementPlanVerifyStatusID;

            StructuralDocumentFileName = waterQualityManagementPlanVerify.FileResource.OriginalBaseFilename + waterQualityManagementPlanVerify.FileResource.OriginalFileExtension;
            DeleteStructuralDocumentFile = false;

            WaterQualityManagementPlanVerifyQuickBMPSimples = waterQualityManagementPlanVerifyQuickBMPs.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = waterQualityManagementPlanVerifyTreatmentBMPs.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMPSimple(x)).ToList();

            EnforcementOrFollowupActions = waterQualityManagementPlanVerify.EnforcementOrFollowupActions;
            SourceControlCondition = waterQualityManagementPlanVerify.SourceControlCondition;
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, bool deleteStructuralDocumentFile, Person currentPerson)
        {
            if (deleteStructuralDocumentFile && StructuralDocumentFile != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);
                waterQualityManagementPlanVerify.FileResource = fileResource;
                HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            }


            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
            waterQualityManagementPlanVerify.SourceControlCondition = SourceControlCondition;

            
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
