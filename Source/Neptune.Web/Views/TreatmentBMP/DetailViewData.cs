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
using Neptune.Web.Views.MaintenanceActivity;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public MapInitJson MapInitJson { get; }
        public string AddBenchmarkAndThresholdUrl { get; }
        public string NewAssessmentUrl { get; }
        public bool HasSettableBenchmarkAndThresholdValues { get; }
        public bool CurrentPersonCanManage { get; }

        public bool CanEditBenchmarkAndThresholds { get; }

        public TreatmentBMPAssessmentGridSpec AssessmentGridSpec { get; }
        public string AssessmentGridName { get; }
        public string AssessmentGridDataUrl { get; }

        public string NewTreatmentBMPDocumentUrl { get; }
        public string NewTreatmentBMPImageUrl { get; }
        public string EditTreatmentBMPImagesUrl { get; }

        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditTreatmentBMPAttributesUrl { get; }

        public string MaintenanceActivityGridName { get; }
        public MaintenanceActivityGridSpec MaintenanceActivityGridSpec { get; }
        public string MaintenanceActivityGridUrl { get; }
        public string NewMaintenanceActivityUrl { get; }

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
            CurrentPersonCanManage = CurrentPerson.CanManageStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdiction);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            AssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(CurrentPerson);
            AssessmentGridName = "Assessment";

            MaintenanceActivityGridSpec = new MaintenanceActivityGridSpec(CurrentPerson, treatmentBMP);
            MaintenanceActivityGridName = "MaintenanceActivity";

            AssessmentGridDataUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.AssessmentGridJsonData(treatmentBMP));
            MaintenanceActivityGridUrl =
                SitkaRoute<MaintenanceActivityController>.BuildUrlFromExpression(c =>
                    c.MaintenanceActivitysGridJsonData(treatmentBMP));
            NewMaintenanceActivityUrl = SitkaRoute<MaintenanceActivityController>.BuildUrlFromExpression(c => c.New(treatmentBMP));
            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(t => t.New(treatmentBMP));
            NewTreatmentBMPImageUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.New(treatmentBMP));
            EditTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.Edit(treatmentBMP));
            EditTreatmentBMPAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.EditAttributes(treatmentBMP));
        }
    }
}
