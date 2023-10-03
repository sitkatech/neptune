/*-----------------------------------------------------------------------
<copyright file="FieldVisit.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models
{
    public partial class FieldVisit
    {
        //public TreatmentBMPAssessment GetAssessmentByType(NeptuneDbContext dbContext, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
        //{
        //    return dbContext.TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentTypeID == (int) treatmentBMPAssessmentTypeEnum);
        //}

        //public void MarkFieldVisitAsProvisionalIfNonManager(Person person)
        //{
        //    var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        //    if (!isAssignedToStormwaterJurisdiction)
        //    {
        //        IsFieldVisitVerified = false;
        //    }
        //}
        //public void VerifyFieldVisit(Person person)
        //{
        //    IsFieldVisitVerified = true;
        //    FieldVisitStatusID = FieldVisitStatus.Complete.FieldVisitStatusID;
        //}

        //public void MarkFieldVisitAsProvisional()
        //{
        //    IsFieldVisitVerified = false;
        //}

        //public void ReturnFieldVisitToEdit()
        //{
        //    IsFieldVisitVerified = false;
        //    FieldVisitStatusID = FieldVisitStatus.ReturnedToEdit.FieldVisitStatusID;
        //}
    }
}