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
    public class NewWqmpVerifyViewModel : FormViewModel, IValidatableObject
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

        [Required]
        public int WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string SourceControlCondition { get; set; }

        public Models.WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
        public List<WaterQualityManagementPlanVerifyQuickBMPSimple> WaterQualityManagementPlanVerifyQuickBMPSimples { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewWqmpVerifyViewModel()
        {
        }

        public NewWqmpVerifyViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMP> quickBMPs, List<Models.TreatmentBMP> treatmentBMPs, Person currentPerson)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = ModelObjectHelpers.NotYetAssignedID;
            WaterQualityManagementPlanVerify = new WaterQualityManagementPlanVerify(
                waterQualityManagementPlan.WaterQualityManagementPlanID,
                WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVisitStatusID,
                WaterQualityManagementPlanVerifyStatusID,
                currentPerson.PersonID,
                DateTime.Now);


            WaterQualityManagementPlanVerifyQuickBMPSimples = quickBMPs?.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).ToList();


            WaterQualityManagementPlanVerifyTreatmentBMPs = new List<WaterQualityManagementPlanVerifyTreatmentBMP>();
            foreach (var treatmentBMP in treatmentBMPs )
            {
                WaterQualityManagementPlanVerifyTreatmentBMPs.Add(new WaterQualityManagementPlanVerifyTreatmentBMP(WaterQualityManagementPlanVerify, treatmentBMP));
            }
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, Person currentPerson)
        {
            if (StructuralDocumentFile != null) { 
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
