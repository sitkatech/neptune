﻿/*-----------------------------------------------------------------------
<copyright file="SummaryForMapViewData.cs" company="Tahoe Regional Planning Agency">
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

using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class SummaryForMapViewData : NeptuneViewData
    {

        public Models.TreatmentBMP TreatmentBMP{ get; }
        public string AssessmentUrl{ get; }
        public Models.TreatmentBMPImage KeyPhoto { get; }
        public string FieldVisitUrl { get; }
        public bool UserHasFieldVisitPermissions { get; }

        public SummaryForMapViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP) : base(currentPerson)
        {
            TreatmentBMP = treatmentBMP;
            AssessmentUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.New(treatmentBMP.TreatmentBMPID));
            // todo: we don't have the concept of a keyphoto yet
            //KeyPhoto = treatmentBMP.TreatmentBMPImages.SingleOrDefault(x => x.IsKeyPhoto);
            KeyPhoto = treatmentBMP.TreatmentBMPImages.FirstOrDefault();
            FieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.New(TreatmentBMP));
            UserHasFieldVisitPermissions = new FieldVisitCreateFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
        }
    }
}
