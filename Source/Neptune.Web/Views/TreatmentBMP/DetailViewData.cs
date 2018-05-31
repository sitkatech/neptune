﻿/*-----------------------------------------------------------------------
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
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public bool UserHasCustomAttributeTypeManagePermissions { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public MapInitJson MapInitJson { get; }
        public string AddBenchmarkAndThresholdUrl { get; }

        public bool HasSettableBenchmarkAndThresholdValues { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CurrentPersonCanEditTreatmentBMP { get; }

        public bool CanEditBenchmarkAndThresholds { get; }

        public FieldVisitGridSpec FieldVisitGridSpec { get; }
        public string FieldVisitGridName { get; }
        public string FieldVisitGridDataUrl { get; }

        public string NewTreatmentBMPDocumentUrl { get; }
        public string NewTreatmentBMPImageUrl { get; }
        public string EditTreatmentBMPImagesUrl { get; }
        public string NewFundingSourcesUrl { get; }

        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditTreatmentBMPPerformanceAndModelingAttributesUrl { get; }
        public string EditTreatmentBMPOtherDesignAttributesUrl { get; }
        public string NewFieldVisitUrl { get; }

        public DetailViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, MapInitJson mapInitJson, ImageCarouselViewData imageCarouselViewData)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, null)
        {
            TreatmentBMP = treatmentBMP;
            PageTitle = treatmentBMP.TreatmentBMPName;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            MapInitJson = mapInitJson;
            ImageCarouselViewData = imageCarouselViewData;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(t => t.Instructions(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = TreatmentBMP.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = new TreatmentBMPManageFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
            CurrentPersonCanEditTreatmentBMP = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            UserHasCustomAttributeTypeManagePermissions =
                new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            FieldVisitGridSpec = new FieldVisitGridSpec(CurrentPerson);
            FieldVisitGridName = "FieldVisit";
            FieldVisitGridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.FieldVisitGridJsonData(treatmentBMP));
            NewFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.New(treatmentBMP));

            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(t => t.New(treatmentBMP));
            NewTreatmentBMPImageUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.New(treatmentBMP));
            EditTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(c => c.Edit(treatmentBMP));
            NewFundingSourcesUrl = SitkaRoute<FundingEventController>.BuildUrlFromExpression(c => c.NewFundingEvent(treatmentBMP));
            EditTreatmentBMPPerformanceAndModelingAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.EditAttributes(treatmentBMP, CustomAttributeTypePurpose.PerformanceAndModelingAttributes));
            EditTreatmentBMPOtherDesignAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.EditAttributes(treatmentBMP, CustomAttributeTypePurpose.OtherDesignAttributes));
        }

        
    }
}
