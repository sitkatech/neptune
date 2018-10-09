using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using Neptune.Web.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewWqmpVerifyViewModel : FormViewModel, IValidatableObject
    {

        public int WaterQualityManagementPlanID { get; set; }
        public int? WaterQualityManagementPlanVerifyID { get; set;  }

        [Required]
        [DisplayName("Type of Verification")]
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }

        [Required]
        [DisplayName("Visit Status")]
        public int WaterQualityManagementPlanVisitStatusID { get; set; }

        [DisplayName("Structural BMP O&M Document")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public HttpPostedFileBase StructuralDocumentFile { get; set; }

        
        public int? WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string SourceControlCondition { get; set; }

        public List<WaterQualityManagementPlanVerifyQuickBMPSimple> WaterQualityManagementPlanVerifyQuickBMPSimples { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> WaterQualityManagementPlanVerifyTreatmentBMPSimples { get; set; }

        public bool HiddenIsFinalizeVerificationInput { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewWqmpVerifyViewModel()
        {
        }

        public NewWqmpVerifyViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<QuickBMP> quickBMPs, List<Models.TreatmentBMP> treatmentBMPs, Person currentPerson)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTypeID;
            WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVisitStatusID;
            WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatusID;
            EnforcementOrFollowupActions = waterQualityManagementPlanVerify.EnforcementOrFollowupActions;
            SourceControlCondition = waterQualityManagementPlanVerify.SourceControlCondition;

            WaterQualityManagementPlanVerifyQuickBMPSimples = quickBMPs?.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).OrderBy(x => x.QuickBMPName).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = treatmentBMPs?.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMPSimple(x)).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMPSimple> waterQualityManagementPlanVerifyQuickBMPSimples, List<WaterQualityManagementPlanVerifyTreatmentBMPSimple> waterQualityManagementPlanVerifyTreatmentBMPSimples, Person currentPerson)
        {

            if (waterQualityManagementPlanVerify.FileResource == null && StructuralDocumentFile != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);
                waterQualityManagementPlanVerify.FileResource = fileResource;
                HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            }
            else if (StructuralDocumentFile != null)
            {
                waterQualityManagementPlanVerify.FileResource.DeleteFileResource();
                var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);
                waterQualityManagementPlanVerify.FileResource = fileResource;
                HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            }
            

            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
            waterQualityManagementPlanVerify.SourceControlCondition = SourceControlCondition;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatusID = WaterQualityManagementPlanVerifyStatusID;
            waterQualityManagementPlanVerify.LastEditedByPerson = currentPerson;
            waterQualityManagementPlanVerify.LastEditedDate = DateTime.Now;


            var allWaterQualityManagementPlanVerifyQuickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyQuickBMPs.Local;
            var waterQualityManagementPlanVerifyQuickBmpsForThisVerification = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyQuickBMPs.ToList();
            var waterQualityManagementPlanVerifyQuickBMPsToUpdate = waterQualityManagementPlanVerifyQuickBMPSimples?.Select(x => new WaterQualityManagementPlanVerifyQuickBMP(x,
                waterQualityManagementPlan.TenantID, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID)).ToList() ?? new List<WaterQualityManagementPlanVerifyQuickBMP>();

            waterQualityManagementPlanVerifyQuickBmpsForThisVerification.Merge(waterQualityManagementPlanVerifyQuickBMPsToUpdate, allWaterQualityManagementPlanVerifyQuickBMPsInDatabase, (x, y) => x.WaterQualityManagementPlanVerifyQuickBMPID == y.WaterQualityManagementPlanVerifyQuickBMPID,
                (x, y) =>
                {
                    x.IsAdequate = y.IsAdequate;
                    x.WaterQualityManagementPlanVerifyQuickBMPNote = y.WaterQualityManagementPlanVerifyQuickBMPNote;
                });


            var allWaterQualityManagementPlanVerifyTreatmentBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllWaterQualityManagementPlanVerifyTreatmentBMPs.Local;
            var waterQualityManagementPlanVerifyTreatmentBmpsForThisVerification = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTreatmentBMPs.ToList();
            var waterQualityManagementPlanVerifyTreatmentBMPsToUpdate = waterQualityManagementPlanVerifyTreatmentBMPSimples?.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMP(x,
                waterQualityManagementPlan.TenantID, waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID)).ToList() ?? new List<WaterQualityManagementPlanVerifyTreatmentBMP>();


            waterQualityManagementPlanVerifyTreatmentBmpsForThisVerification.Merge(waterQualityManagementPlanVerifyTreatmentBMPsToUpdate, allWaterQualityManagementPlanVerifyTreatmentBMPsInDatabase, (x, y) => x.WaterQualityManagementPlanVerifyTreatmentBMPID == y.WaterQualityManagementPlanVerifyTreatmentBMPID,
                (x, y) =>
                {
                    x.IsAdequate = y.IsAdequate;
                    x.WaterQualityManagementPlanVerifyTreatmentBMPNote = y.WaterQualityManagementPlanVerifyTreatmentBMPNote;
                });
        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            var waterQualityManagementPlanVerifyQuickBMPNoteMaxLength = WaterQualityManagementPlanVerifyQuickBMP.FieldLengths.WaterQualityManagementPlanVerifyQuickBMPNote;
            var waterQualityManagementPlanVerifyTreatmentBMPNoteMaxLength = WaterQualityManagementPlanVerifyTreatmentBMP.FieldLengths.WaterQualityManagementPlanVerifyTreatmentBMPNote;
            var sourceControlConditionMaxLength = WaterQualityManagementPlanVerify.FieldLengths.SourceControlCondition;
            var enforcementOrFollowupActionsMaxLength = WaterQualityManagementPlanVerify.FieldLengths.EnforcementOrFollowupActions;

            foreach (var waterQualityManagementPlanVerifyQuickBMPSimple in WaterQualityManagementPlanVerifyQuickBMPSimples ?? new List<WaterQualityManagementPlanVerifyQuickBMPSimple>())
            {
                var waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength = waterQualityManagementPlanVerifyQuickBMPSimple.WaterQualityManagementPlanVerifyQuickBMPNote?.Length;
                if (waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength != null && waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength > waterQualityManagementPlanVerifyQuickBMPNoteMaxLength)
                {
                    validationResults.Add(new ValidationResult($"\"{waterQualityManagementPlanVerifyQuickBMPSimple.QuickBMPName}\"'s note is too long. Notes have a maximum of {waterQualityManagementPlanVerifyQuickBMPNoteMaxLength} characters and is {waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength - waterQualityManagementPlanVerifyQuickBMPNoteMaxLength} over the limit."));

                }
            }

            foreach (var waterQualityManagementPlanVerifyTreatmentBMPSimple in WaterQualityManagementPlanVerifyTreatmentBMPSimples ?? new List<WaterQualityManagementPlanVerifyTreatmentBMPSimple>())
            {
                var waterQualityManagementPlanVerifyTreatmentBMPSimpleNoteLength = waterQualityManagementPlanVerifyTreatmentBMPSimple.WaterQualityManagementPlanVerifyTreatmentBMPNote?.Length;
                if (waterQualityManagementPlanVerifyTreatmentBMPSimpleNoteLength != null && waterQualityManagementPlanVerifyTreatmentBMPSimpleNoteLength > waterQualityManagementPlanVerifyTreatmentBMPNoteMaxLength)
                {
                    validationResults.Add(new ValidationResult($"\"{waterQualityManagementPlanVerifyTreatmentBMPSimple.TreatmentBMPName}\"'s note is too long. Notes have a maximum of {waterQualityManagementPlanVerifyTreatmentBMPNoteMaxLength} characters and is {waterQualityManagementPlanVerifyTreatmentBMPSimpleNoteLength - waterQualityManagementPlanVerifyTreatmentBMPNoteMaxLength} over the limit."));

                }
            }

            if (SourceControlCondition != null && SourceControlCondition.Length > sourceControlConditionMaxLength)
            {
                validationResults.Add(new ValidationResult($"\"Source control BMPs\"'s message is too long. It has a maximum of {sourceControlConditionMaxLength} characters and is {SourceControlCondition.Length - sourceControlConditionMaxLength} over the limit."));
            }


            if (EnforcementOrFollowupActions != null && EnforcementOrFollowupActions.Length > enforcementOrFollowupActionsMaxLength)
            {
                validationResults.Add(new ValidationResult($"\"Enforcement or Follow-up Actions\"'s message is too long. It has a maximum of {enforcementOrFollowupActionsMaxLength} characters and is {EnforcementOrFollowupActions.Length - enforcementOrFollowupActionsMaxLength} over the limit."));
            }

            return validationResults;
        }
    }
}
