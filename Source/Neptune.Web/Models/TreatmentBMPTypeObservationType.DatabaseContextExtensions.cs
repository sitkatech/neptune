/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPTypeObservationType.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<ObservationType> GetObservationTypesForTreatmentType(this IQueryable<TreatmentBMPTypeObservationType> treatmentBMPTypeObservationTypes, TreatmentBMPType treatmentBMPType)
        {
            return treatmentBMPTypeObservationTypes.Where(x => x.TreatmentBMPTypeID == treatmentBMPType.TreatmentBMPTypeID).ToList().Select(x => x.ObservationType).ToList();
        }

        public static TreatmentBMPTypeObservationType GetTreatmentBMPTypeObservationType(this TreatmentBMPType treatmentBMPType, ObservationType observationType)
        {
            var treatmentBMPTypeObservationType = treatmentBMPType.GetTreatmentBMPTypeObservationTypeOrDefault(observationType);

            Check.Assert(treatmentBMPTypeObservationType != null,
                $"The Observation Type '{observationType.ObservationTypeName}' is not applicable to the Treatment BMP Type '{treatmentBMPType.TreatmentBMPTypeName}'.");
            return treatmentBMPTypeObservationType;
        }

        public static TreatmentBMPTypeObservationType GetTreatmentBMPTypeObservationTypeOrDefault(this TreatmentBMPType treatmentBMPType, ObservationType observationType)
        {
            var treatmentBMPTypeObservationType = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeObservationTypes.SingleOrDefault(
                x => x.TreatmentBMPTypeID == treatmentBMPType.TreatmentBMPTypeID && x.ObservationTypeID == observationType.ObservationTypeID);

            return treatmentBMPTypeObservationType;
        }

    }
}
