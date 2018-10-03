﻿using System;
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
    public class EditWqmpVerifyViewModel : NewWqmpVerifyViewModel
    {
        public bool DeleteStructuralDocumentFile { get; set; }

        public string StructuralDocumentFileName { get; set; }


        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpVerifyViewModel()
        {
        }

        public EditWqmpVerifyViewModel(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBMPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBMPs, Person currentPerson) : base (waterQualityManagementPlanVerify.WaterQualityManagementPlan, waterQualityManagementPlanVerify, new List<QuickBMP>(), new List<Models.TreatmentBMP>(), currentPerson)
        {

            StructuralDocumentFileName = waterQualityManagementPlanVerify.FileResource.OriginalBaseFilename + waterQualityManagementPlanVerify.FileResource.OriginalFileExtension;
            DeleteStructuralDocumentFile = false;

            WaterQualityManagementPlanVerifyQuickBMPSimples = waterQualityManagementPlanVerifyQuickBMPs.Select(x => new WaterQualityManagementPlanVerifyQuickBMPSimple(x)).ToList();
            WaterQualityManagementPlanVerifyTreatmentBMPSimples = waterQualityManagementPlanVerifyTreatmentBMPs.Select(x => new WaterQualityManagementPlanVerifyTreatmentBMPSimple(x)).ToList();

            
        }

        public void UpdateModels(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, bool deleteStructuralDocumentFile, Person currentPerson)
        {
            if (deleteStructuralDocumentFile && StructuralDocumentFile != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(StructuralDocumentFile, currentPerson);
                waterQualityManagementPlanVerify.FileResource = fileResource;
                HttpRequestStorage.DatabaseEntities.AllFileResources.Add(fileResource);
            }

            //base.UpdateModels(waterQualityManagementPlan,
            //     waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyQuickBMPSimples,  waterQualityManagementPlanVerifyTreatmentBMPSimples, currentPerson);
        }
    }
}
