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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public abstract class BenchmarkAndThresholdViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public string InstructionsUrl { get; }
        public string SectionName { get; }
        public UrlTemplate<int, int> BenchmarkAndThresholdUrlTemplate { get; }
        public EFModels.Entities.TreatmentBMPType TreatmentBMPType { get; }
        public List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; }

        protected BenchmarkAndThresholdViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds)
            : this(httpContext, linkGenerator, webConfiguration, currentPerson, treatmentBMP, InstructionsViewData.InstructionsSectionName, treatmentBMPType, treatmentBMPBenchmarkAndThresholds)
        {
        }

        protected BenchmarkAndThresholdViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, EFModels.Entities.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds)
            : this(httpContext, linkGenerator, webConfiguration, currentPerson, treatmentBMP, treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName, treatmentBMPType, treatmentBMPBenchmarkAndThresholds)
        {
        }

        private BenchmarkAndThresholdViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP, 
            string sectionName, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBMP = treatmentBMP;
            TreatmentBMPType = treatmentBMPType;
            TreatmentBMPBenchmarkAndThresholds = treatmentBMPBenchmarkAndThresholds;
            InstructionsUrl = SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(linkGenerator, x => x.Instructions(treatmentBMP.TreatmentBMPID));
            SectionName = sectionName;

            EntityName = FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized();
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = "Benchmark & Threshold";
            BenchmarkAndThresholdUrlTemplate = new UrlTemplate<int, int>(
                SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.EditBenchmarkAndThreshold(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        }
    }
}
