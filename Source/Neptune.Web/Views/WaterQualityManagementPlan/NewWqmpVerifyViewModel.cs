﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
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


            WaterQualityManagementPlanVerifyQuickBMPs = new List<WaterQualityManagementPlanVerifyQuickBMP>();
            foreach (var quickBMP in quickBMPs)
            {
                WaterQualityManagementPlanVerifyQuickBMPs.Add(new WaterQualityManagementPlanVerifyQuickBMP(WaterQualityManagementPlanVerify, quickBMP));
            }

            WaterQualityManagementPlanVerifyTreatmentBMPs = new List<WaterQualityManagementPlanVerifyTreatmentBMP>();
            foreach (var treatmentBMP in treatmentBMPs )
            {
                WaterQualityManagementPlanVerifyTreatmentBMPs.Add(new WaterQualityManagementPlanVerifyTreatmentBMP(WaterQualityManagementPlanVerify, treatmentBMP));
            }
        }

        public virtual void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, Person currentPerson)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);


            waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID = WaterQualityManagementPlanVerifyID ?? ModelObjectHelpers.NotYetAssignedID;
            waterQualityManagementPlanVerify.FileResource = fileResource;
            waterQualityManagementPlanVerify.EnforcementOrFollowupActions = EnforcementOrFollowupActions;
            waterQualityManagementPlanVerify.SourceControlCondition = SourceControlCondition;

            HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
