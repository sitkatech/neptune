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

            EntityName = Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            SubEntityName = treatmentBMP.FormattedNameAndType;
            SubEntityUrl = treatmentBMP.GetDetailUrl();
            PageTitle = "Benchmark & Threshold";
        }

        protected BenchmarkAndThresholdViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
            : this(currentPerson, treatmentBMP, TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName)
        {            
        }
    }

  
}
