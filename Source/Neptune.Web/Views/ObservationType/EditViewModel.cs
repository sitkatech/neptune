﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int ObservationTypeID { get; set; }

        [Required]
        [StringLength(Models.ObservationType.FieldLengths.ObservationTypeName)]
        [DisplayName("Name of Observation Type")]
        public string ObservationTypeName { get; set; }      

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.ObservationThresholdType)]
        public int? ObservationThresholdTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.ObservationTargetType)]
        public int? ObservationTargetTypeID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.ObservationCollectionMethod)]
        public int? ObservationTypeCollectionMethodID { get; set; }
      
        [Required]
        [DisplayName("Measurement Label")]
        public string MeasurementLabel { get; set; }

        [Required]
        [DisplayName("What to Observe (Measurement Instrument or Property of BMP)")]
        public string LabelForWhatIsObserved { get; set; }

        [Required]
        [DisplayName("Measurement Unit")]
        public int? MeasurementUnitTypeID { get; set; }

        [DisplayName("Benchmark Instruction Text")]
        [StringLength(Models.ObservationType.FieldLengths.BenchmarkDescription)]
        public string BenchmarkDescritpion { get; set; }

        [DisplayName("Threshold Instruction Text")]
        [StringLength(Models.ObservationType.FieldLengths.ThresholdDescription)]
        public string ThresholdDescritpion { get; set; }

        [DisplayName("Assessment Instruction Text")]
        [StringLength(Models.ObservationType.FieldLengths.AssessmentDescription)]
        public string AssessmentDescritpion { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.ObservationType observationType)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            ObservationTypeName = observationType.ObservationTypeName;

            ObservationThresholdTypeID = observationType.ObservationTypeSpecification?.ObservationThresholdTypeID;
            ObservationTargetTypeID = observationType.ObservationTypeSpecification?.ObservationTargetTypeID;
            ObservationTypeCollectionMethodID = observationType.ObservationTypeSpecification?.ObservationTypeCollectionMethodID;

            MeasurementLabel = observationType.MeasurementLabel;
            LabelForWhatIsObserved = observationType.LabelForWhatIsObserved;
            MeasurementUnitTypeID = observationType.MeasurementUnitTypeID;

            BenchmarkDescritpion = observationType.BenchmarkDescription;
            ThresholdDescritpion = observationType.ThresholdDescription;
            AssessmentDescritpion = observationType.AssessmentDescription;
        }

        public void UpdateModel(Models.ObservationType observationType, Person currentPerson)
        {
            observationType.ObservationTypeName = ObservationTypeName;
            
            observationType.MeasurementLabel = MeasurementLabel;
            observationType.LabelForWhatIsObserved = LabelForWhatIsObserved;
            observationType.MeasurementUnitTypeID = MeasurementUnitTypeID.Value;

            observationType.BenchmarkDescription = BenchmarkDescritpion;
            observationType.ThresholdDescription = ThresholdDescritpion;
            observationType.AssessmentDescription = AssessmentDescritpion;

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x => 
                x.ObservationTargetTypeID == ObservationTargetTypeID && 
                x.ObservationThresholdTypeID == ObservationThresholdTypeID && 
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);

            Check.Require(observationTypeSpecification != null, "No valid combination of Target Type, Threshold Type and Collection Method found");

            observationType.ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var observationTypeSpecification = ObservationTypeSpecification.All.FirstOrDefault(x =>
                x.ObservationTargetTypeID == ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID);
            if (observationTypeSpecification == null)
            {
                validationResults.Add(new ValidationResult("Enter a valid combination of Target Type, Threshold Type and Collection Method."));
            }

            var observationThresholdTypeID = ObservationThresholdType.None.ObservationThresholdTypeID;
            if (ObservationThresholdTypeID != observationThresholdTypeID &&
                (string.IsNullOrEmpty(BenchmarkDescritpion) || string.IsNullOrEmpty(ThresholdDescritpion)))
            {
                validationResults.Add(new ValidationResult("Benchmark and Threshold Instructions are required for this Observation Type."));
            }

            return validationResults;
        }

    }
}
