﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;
using Neptune.Web.Services;

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
        public IFormFile StructuralDocumentFile { get; set; }

        [Required]
        [DisplayName("Verification Date")]
        public DateTime VerificationDate { get; set; }

        public int? WaterQualityManagementPlanVerifyStatusID { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string SourceControlCondition { get; set; }

        public List<WaterQualityManagementPlanVerifyQuickBMPSimpleDto> WaterQualityManagementPlanVerifyQuickBMPSimples { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto> WaterQualityManagementPlanVerifyTreatmentBMPSimples { get; set; }

        public bool HiddenIsFinalizeVerificationInput { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public NewWqmpVerifyViewModel()
        {
        }

        public NewWqmpVerifyViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<QuickBMP> quickBMPs, List<EFModels.Entities.TreatmentBMP> treatmentBMPs)
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID;
            WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTypeID;
            WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVisitStatusID;
            WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatusID;
            VerificationDate = waterQualityManagementPlanVerify.VerificationDate;
            EnforcementOrFollowupActions = waterQualityManagementPlanVerify.EnforcementOrFollowupActions;
            SourceControlCondition = waterQualityManagementPlanVerify.SourceControlCondition;

            WaterQualityManagementPlanVerifyQuickBMPSimples = quickBMPs.Select(x => 
                x.AsWaterQualityManagementPlanVerifyQuickBMPSimpleDto()).OrderBy(x => x.QuickBMPName).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = treatmentBMPs.Select(x => x.AsWaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public async Task UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMPSimpleDto> waterQualityManagementPlanVerifyQuickBMPSimples, List<WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto> waterQualityManagementPlanVerifyTreatmentBMPSimples, Person currentPerson, NeptuneDbContext dbContext, FileResourceService fileResourceService)
        {
            if (StructuralDocumentFile != null)
            {
                var fileResource = await fileResourceService.CreateNewFromIFormFile(StructuralDocumentFile, currentPerson);
                if (waterQualityManagementPlanVerify.FileResource == null)
                {
                    waterQualityManagementPlanVerify.FileResource = fileResource;
                }
                else
                {
                    await fileResourceService.DeleteBlobForFileResource(waterQualityManagementPlanVerify.FileResource);
                    dbContext.FileResources.Remove(waterQualityManagementPlanVerify.FileResource);
                    waterQualityManagementPlanVerify.FileResource = fileResource;
                }
            }

            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
            waterQualityManagementPlanVerify.SourceControlCondition = SourceControlCondition;
            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatusID = WaterQualityManagementPlanVerifyStatusID;
            waterQualityManagementPlanVerify.LastEditedByPerson = currentPerson;
            waterQualityManagementPlanVerify.LastEditedDate = DateTime.Now;
            waterQualityManagementPlanVerify.VerificationDate = VerificationDate;


            var allWaterQualityManagementPlanVerifyQuickBMPsInDatabase = dbContext.WaterQualityManagementPlanVerifyQuickBMPs;
            var waterQualityManagementPlanVerifyQuickBmpsForThisVerification = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyQuickBMPs.ToList();
            var waterQualityManagementPlanVerifyQuickBMPsToUpdate = waterQualityManagementPlanVerifyQuickBMPSimples
                ?.Select(x => new WaterQualityManagementPlanVerifyQuickBMP
                    {
                        WaterQualityManagementPlanVerifyQuickBMPID = x.WaterQualityManagementPlanVerifyQuickBMPID,
                        WaterQualityManagementPlanVerifyID =
                            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID,
                        QuickBMPID = x.QuickBMPID,
                        IsAdequate = x.IsAdequate,
                        WaterQualityManagementPlanVerifyQuickBMPNote = x.WaterQualityManagementPlanVerifyQuickBMPNote

                    }
                ).ToList() ?? new List<WaterQualityManagementPlanVerifyQuickBMP>();

            waterQualityManagementPlanVerifyQuickBmpsForThisVerification.Merge(waterQualityManagementPlanVerifyQuickBMPsToUpdate, allWaterQualityManagementPlanVerifyQuickBMPsInDatabase, (x, y) => x.WaterQualityManagementPlanVerifyQuickBMPID == y.WaterQualityManagementPlanVerifyQuickBMPID,
                (x, y) =>
                {
                    x.IsAdequate = y.IsAdequate;
                    x.WaterQualityManagementPlanVerifyQuickBMPNote = y.WaterQualityManagementPlanVerifyQuickBMPNote;
                });


            var allWaterQualityManagementPlanVerifyTreatmentBMPsInDatabase = dbContext.WaterQualityManagementPlanVerifyTreatmentBMPs;
            var waterQualityManagementPlanVerifyTreatmentBmpsForThisVerification = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTreatmentBMPs.ToList();
            var waterQualityManagementPlanVerifyTreatmentBMPsToUpdate =
                waterQualityManagementPlanVerifyTreatmentBMPSimples?.Select(x =>
                    new WaterQualityManagementPlanVerifyTreatmentBMP
                    {
                        WaterQualityManagementPlanVerifyTreatmentBMPID =
                            x.WaterQualityManagementPlanVerifyTreatmentBMPID,
                        WaterQualityManagementPlanVerifyID =
                            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID,
                        TreatmentBMPID = x.TreatmentBMPID,
                        IsAdequate = x.IsAdequate,
                        WaterQualityManagementPlanVerifyTreatmentBMPNote =
                            x.WaterQualityManagementPlanVerifyTreatmentBMPNote
                    }).ToList() ?? new List<WaterQualityManagementPlanVerifyTreatmentBMP>();


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

            foreach (var waterQualityManagementPlanVerifyQuickBMPSimple in WaterQualityManagementPlanVerifyQuickBMPSimples)
            {
                var waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength = waterQualityManagementPlanVerifyQuickBMPSimple.WaterQualityManagementPlanVerifyQuickBMPNote?.Length;
                if (waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength != null && waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength > waterQualityManagementPlanVerifyQuickBMPNoteMaxLength)
                {
                    validationResults.Add(new ValidationResult($"\"{waterQualityManagementPlanVerifyQuickBMPSimple.QuickBMPName}\"'s note is too long. Notes have a maximum of {waterQualityManagementPlanVerifyQuickBMPNoteMaxLength} characters and is {waterQualityManagementPlanVerifyQuickBMPSimpleNoteLength - waterQualityManagementPlanVerifyQuickBMPNoteMaxLength} over the limit."));

                }
            }

            foreach (var waterQualityManagementPlanVerifyTreatmentBMPSimple in WaterQualityManagementPlanVerifyTreatmentBMPSimples)
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
