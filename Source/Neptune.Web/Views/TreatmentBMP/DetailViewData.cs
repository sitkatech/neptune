/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public bool UserHasTreatmentBMPAttributeTypeManagePermissions { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public MapInitJson MapInitJson { get; }
        public string AddBenchmarkAndThresholdUrl { get; }
        public string NewAssessmentUrl { get; }
        public bool HasSettableBenchmarkAndThresholdValues { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CurrentPersonCanEditTreatmentBMP { get; }

        public bool CanEditBenchmarkAndThresholds { get; }

        public TreatmentBMPAssessmentGridSpec AssessmentGridSpec { get; }
        public string AssessmentGridName { get; }
        public string AssessmentGridDataUrl { get; }

        public string NewTreatmentBMPDocumentUrl { get; }
        public string NewTreatmentBMPImageUrl { get; }
        public string EditTreatmentBMPImagesUrl { get; }
        public string EditFundingSourcesUrl { get; }

        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditTreatmentBMPPerformanceAndModelingAttributesUrl { get; }
        public string EditTreatmentBMPOtherDesignAttributesUrl { get; }

        public string MaintenanceRecordGridName { get; }
        public MaintenanceRecordGridSpec MaintenanceRecordGridSpec { get; }
        public string MaintenanceRecordGridUrl { get; }
        public string NewMaintenanceRecordUrl { get; }

        public DetailViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, MapInitJson mapInitJson, ImageCarouselViewData imageCarouselViewData)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, null)
        {
            TreatmentBMP = treatmentBMP;
            PageTitle = treatmentBMP.FormattedNameAndType;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            MapInitJson = mapInitJson;
            ImageCarouselViewData = imageCarouselViewData;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(treatmentBMP.TreatmentBMPID));
            NewAssessmentUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.New(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = TreatmentBMP.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = new TreatmentBMPManageFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
            CurrentPersonCanEditTreatmentBMP = new NeptuneEditFeature().HasPermissionByPerson(currentPerson);
            UserHasTreatmentBMPAttributeTypeManagePermissions =
                new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            AssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson);
            AssessmentGridName = "Assessment";

            MaintenanceRecordGridSpec = new MaintenanceRecordGridSpec(CurrentPerson, treatmentBMP);
            MaintenanceRecordGridName = "MaintenanceRecord";

            AssessmentGridDataUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.AssessmentGridJsonData(treatmentBMP));
            MaintenanceRecordGridUrl =
                SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(c =>
                    c.MaintenanceRecordsGridJsonData(treatmentBMP));
            NewMaintenanceRecordUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(c => c.New(treatmentBMP));
            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(t => t.New(treatmentBMP));
            NewTreatmentBMPImageUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.New(treatmentBMP));
            EditTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.Edit(treatmentBMP));
            EditFundingSourcesUrl = SitkaRoute<TreatmentBMPFundingSourceController>.BuildUrlFromExpression(c => c.EditTreatmentBMPFundingSourcesForTreatmentBMP(treatmentBMP));
            EditTreatmentBMPPerformanceAndModelingAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.EditAttributes(treatmentBMP, TreatmentBMPAttributeTypePurpose.PerformanceAndModelingAttributes));
            EditTreatmentBMPOtherDesignAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.EditAttributes(treatmentBMP, TreatmentBMPAttributeTypePurpose.OtherDesignAttributes));
        }

        
    }
}
