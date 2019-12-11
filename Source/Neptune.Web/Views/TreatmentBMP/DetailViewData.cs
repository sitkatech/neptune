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

using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.HRUCharacteristics;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class DetailViewData : NeptuneViewData
    {
        public bool UserIsAdmin { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public TreatmentBMPDetailMapInitJson MapInitJson { get; }
        public string AddBenchmarkAndThresholdUrl { get; }

        public bool HasSettableBenchmarkAndThresholdValues { get; }
        public bool CurrentPersonCanManage { get; }
        public bool CanManageStormwaterJurisdiction { get; }
        public bool CanEditStormwaterJurisdiction { get; }

        public bool CanEditBenchmarkAndThresholds { get; }

        public FieldVisitGridSpec FieldVisitGridSpec { get; }
        public string FieldVisitGridName { get; }
        public string FieldVisitGridDataUrl { get; }

        public string NewTreatmentBMPDocumentUrl { get; }
        public string NewFundingSourcesUrl { get; }

        public ImageCarouselViewData ImageCarouselViewData { get; }
        public string EditTreatmentBMPPerformanceAndModelingAttributesUrl { get; }
        public string EditTreatmentBMPOtherDesignAttributesUrl { get; }
        public string NewFieldVisitUrl { get; }
        public string LocationEditUrl { get; }
        public string DelineationMapUrl { get; }
        public string ManageTreatmentBMPImagesUrl { get; }
        public string DelineationArea { get; }
        public string DelineationStatus { get; }
        public bool DisplayTrashCaptureEffectiveness { get; }

        public readonly string VerifiedUnverifiedUrl;
        public string TrashCaptureEffectiveness { get; }
        public string ChangeTreatmentBMPTypeUrl { get; }
        public string HRURefreshUrl { get; }

        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }

        public DetailViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, TreatmentBMPDetailMapInitJson mapInitJson, ImageCarouselViewData imageCarouselViewData, string verifiedUnverifiedUrl, HRUCharacteristicsViewData hruCharacteristicsViewData)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            PageTitle = treatmentBMP.TreatmentBMPName;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            MapInitJson = mapInitJson;
            ImageCarouselViewData = imageCarouselViewData;
            AddBenchmarkAndThresholdUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x => x.Instructions(treatmentBMP.TreatmentBMPID));
            HasSettableBenchmarkAndThresholdValues = TreatmentBMP.HasSettableBenchmarkAndThresholdValues();
            CurrentPersonCanManage = new TreatmentBMPManageFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(treatmentBMP.StormwaterJurisdiction);
            CanEditStormwaterJurisdiction = currentPerson.CanEditStormwaterJurisdiction(treatmentBMP.StormwaterJurisdiction);
            UserIsAdmin = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);

            CanEditBenchmarkAndThresholds = CurrentPersonCanManage && HasSettableBenchmarkAndThresholdValues;

            FieldVisitGridSpec = new FieldVisitGridSpec(CurrentPerson, true);
            FieldVisitGridName = "FieldVisit";
            FieldVisitGridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.FieldVisitGridJsonData(treatmentBMP));
            NewFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.New(treatmentBMP));

            NewTreatmentBMPDocumentUrl = SitkaRoute<TreatmentBMPDocumentController>.BuildUrlFromExpression(x => x.New(treatmentBMP));
            NewFundingSourcesUrl = SitkaRoute<FundingEventController>.BuildUrlFromExpression(x => x.NewFundingEvent(treatmentBMP));
            EditTreatmentBMPPerformanceAndModelingAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditAttributes(treatmentBMP, CustomAttributeTypePurpose.PerformanceAndModelingAttributes));
            EditTreatmentBMPOtherDesignAttributesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditAttributes(treatmentBMP, CustomAttributeTypePurpose.OtherDesignAttributes));

            LocationEditUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditLocation(treatmentBMP));
            ManageTreatmentBMPImagesUrl = SitkaRoute<TreatmentBMPImageController>.BuildUrlFromExpression(x => x.ManageTreatmentBMPImages(TreatmentBMP));

            VerifiedUnverifiedUrl = verifiedUnverifiedUrl;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;

            DelineationArea = (TreatmentBMP.Delineation?.DelineationGeometry.Area * DbSpatialHelper.SquareMetersToAcres)?.ToString("0.00") ?? "-";
            DelineationStatus = TreatmentBMP.Delineation?.IsVerified == false ? "Provisional" : "Verified";
            DisplayTrashCaptureEffectiveness = TreatmentBMP.TrashCaptureStatusTypeID ==
                                               TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID;

            TrashCaptureEffectiveness = TreatmentBMP.TrashCaptureEffectiveness == null ? "Not Provided" : TreatmentBMP.TrashCaptureEffectiveness + "%";

            DelineationMapUrl = treatmentBMP.GetDelineationMapUrl();

            ChangeTreatmentBMPTypeUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.ConvertTreatmentBMPType(treatmentBMP));

            HRURefreshUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(
                    x => x.RefreshHRUCharacteristics(treatmentBMP));
        }
    }
}
