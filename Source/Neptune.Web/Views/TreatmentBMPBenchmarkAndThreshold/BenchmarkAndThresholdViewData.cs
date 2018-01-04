/*-----------------------------------------------------------------------
<copyright file="BenchmarkAndThresholdViewData.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public abstract class BenchmarkAndThresholdViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP;
        public string InstructionsUrl;
        public string SectionName;

        public string DefaultBenchmarkText;
        public string DefaultThresholdText;
        public string DefaultBenchmarkPlaceholder;
        public string DefaultThresholdPlaceholder;

        protected BenchmarkAndThresholdViewData(Person currentPerson,
            Models.TreatmentBMP treatmentBMP)
            : this(currentPerson, treatmentBMP, InstructionsViewData.InstructionsSectionName)
        {
        }

        private BenchmarkAndThresholdViewData(Person currentPerson,  
            Models.TreatmentBMP treatmentBMP, 
            string sectionName)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            TreatmentBMP = treatmentBMP;
            InstructionsUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(x => x.Instructions(treatmentBMP.TreatmentBMPID));
            SectionName = sectionName;

            SubEntityName = treatmentBMP.FormattedNameAndType;
            SubEntityUrl = treatmentBMP.GetDetailUrl();
            PageTitle = "Benchmark & Threshold";
        }

        protected BenchmarkAndThresholdViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, Models.ObservationType observationType)
            : this(currentPerson, treatmentBMP, observationType.ObservationTypeName)
        {
            var treatmentBMPTypeObservationType = treatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(observationType);
            DefaultBenchmarkPlaceholder = treatmentBMPTypeObservationType.DefaultBenchmarkValue.HasValue ? "default is " + treatmentBMPTypeObservationType.DefaultBenchmarkValue.Value : string.Empty;
            DefaultBenchmarkText = treatmentBMPTypeObservationType.DefaultBenchmarkValue.HasValue ? "The default value is " + ObservationTypeHelper.FormattedDefaultBenchmarkValue(observationType, treatmentBMPTypeObservationType.DefaultBenchmarkValue.Value) + "." : string.Empty;
            
            DefaultThresholdPlaceholder = treatmentBMPTypeObservationType.DefaultThresholdValue.HasValue ? "default is " + treatmentBMPTypeObservationType.DefaultThresholdValue.Value : string.Empty;
            DefaultThresholdText = treatmentBMPTypeObservationType.DefaultThresholdValue.HasValue ? "The default value is " + ObservationTypeHelper.ApplyThresholdFormatting(observationType, treatmentBMPTypeObservationType.DefaultThresholdValue.Value) + "." : string.Empty;    
        }
    }

  
}
