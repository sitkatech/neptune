/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int TreatmentBMPID { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.TreatmentBMPName)]
        public string TreatmentBMPName { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.Jurisdiction)]
        public int? StormwaterJurisdictionID { get; set; }

        [Required(ErrorMessage = "Choose a BMP Type")]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TreatmentBMPType)]
        public int? TreatmentBMPTypeID { get; set; }

        [DisplayName("Notes")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.Notes)]
        public string Notes { get; set; }

        [DisplayName("ID in System of Record")]
        [StringLength(EFModels.Entities.TreatmentBMP.FieldLengths.SystemOfRecordID)]
        public string SystemOfRecordID { get; set; }

        [DisplayName("Owner")]
        public int? OwnerOrganizationID { get; set; }

        [DisplayName("Year Built")]
        public int? YearBuilt { get; set; }

        [DisplayName("Water Quality Management Plan")]
        public int? WaterQualityManagementPlanID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredLifespanOfInstallation)]
        public int? TreatmentBMPLifespanTypeID { get; set; }

        [DisplayName("Lifespan End Date")]
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredFieldVisitsPerYear)]
        [Range(0, int.MaxValue, ErrorMessage = "Required Field Visits Per Year cannot be negative")]
        public int? RequiredFieldVisitsPerYear { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.RequiredPostStormFieldVisitsPerYear)]
        [Range(0, int.MaxValue, ErrorMessage = "Required Post Storm Field Visits Per Year cannot be negative")]
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TrashCaptureStatus)]
        public int? TrashCaptureStatusTypeID { get; set; }

        [DisplayName("Trash Capture Effectiveness")]
        [Range(1, 99, ErrorMessage = "The Trash Effectiveness must be between 1 and 99, if the score is 100 please select Full")]
        public int? TrashCaptureEffectiveness { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.SizingBasis)]
        public int? SizingBasisTypeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            TreatmentBMPName = treatmentBMP.TreatmentBMPName;
            StormwaterJurisdictionID = treatmentBMP.StormwaterJurisdictionID;
            TreatmentBMPTypeID = treatmentBMP.TreatmentBMPTypeID;
            Notes = treatmentBMP.Notes;
            SystemOfRecordID = treatmentBMP.SystemOfRecordID;
            OwnerOrganizationID = treatmentBMP.OwnerOrganizationID;
            YearBuilt = treatmentBMP.YearBuilt;
            WaterQualityManagementPlanID = treatmentBMP.WaterQualityManagementPlanID;
            TreatmentBMPLifespanTypeID = treatmentBMP.TreatmentBMPLifespanTypeID;
            TreatmentBMPLifespanEndDate = treatmentBMP.TreatmentBMPLifespanEndDate;
            RequiredFieldVisitsPerYear = treatmentBMP.RequiredFieldVisitsPerYear;
            RequiredPostStormFieldVisitsPerYear = treatmentBMP.RequiredPostStormFieldVisitsPerYear;
            TrashCaptureStatusTypeID = treatmentBMP.TrashCaptureStatusTypeID;
            SizingBasisTypeID = treatmentBMP.SizingBasisTypeID;
            TrashCaptureEffectiveness = treatmentBMP.TrashCaptureEffectiveness;
        }

        public void UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            treatmentBMP.TreatmentBMPName = TreatmentBMPName;
            treatmentBMP.Notes = Notes;
            treatmentBMP.RequiredFieldVisitsPerYear = RequiredFieldVisitsPerYear;
            treatmentBMP.RequiredPostStormFieldVisitsPerYear = RequiredPostStormFieldVisitsPerYear;
            treatmentBMP.TrashCaptureStatusTypeID = TrashCaptureStatusTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute
            treatmentBMP.SizingBasisTypeID = SizingBasisTypeID.GetValueOrDefault(); // will never be null due to RequiredAttribute
            treatmentBMP.SystemOfRecordID = SystemOfRecordID;
            if (OwnerOrganizationID.HasValue)
            {
                treatmentBMP.OwnerOrganizationID = OwnerOrganizationID.Value;
            }
            else
            {
                var stormwaterJurisdiction = StormwaterJurisdictions.GetByID(dbContext, treatmentBMP.StormwaterJurisdictionID);
                treatmentBMP.OwnerOrganizationID = stormwaterJurisdiction.OrganizationID;
            }
            
            treatmentBMP.YearBuilt = YearBuilt;
            treatmentBMP.WaterQualityManagementPlanID = WaterQualityManagementPlanID;

            treatmentBMP.TreatmentBMPLifespanTypeID = TreatmentBMPLifespanTypeID;
            treatmentBMP.TreatmentBMPLifespanEndDate = TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID ? TreatmentBMPLifespanEndDate : null;
            treatmentBMP.TrashCaptureEffectiveness = TrashCaptureStatusTypeID == TrashCaptureStatusType.Partial.TrashCaptureStatusTypeID ? TrashCaptureEffectiveness : null;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            var treatmentBmPsWithSameName = dbContext.TreatmentBMPs.AsNoTracking().Where(x => x.TreatmentBMPName == TreatmentBMPName);
            if (treatmentBmPsWithSameName.Any(x => x.TreatmentBMPID != TreatmentBMPID))
            {
                yield return new SitkaValidationResult<EditViewModel, string>("A BMP with this name already exists.",
                    x => x.TreatmentBMPName);
            }

            if (TreatmentBMPLifespanTypeID == TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID &&
                !TreatmentBMPLifespanEndDate.HasValue)
            {
                yield return new SitkaValidationResult<EditViewModel, DateTime?>(
                    "The Lifespan End Date must be set if the Lifespan Type is Fixed End Date.",
                    x => x.TreatmentBMPLifespanEndDate);
            }
        }
    }
}
