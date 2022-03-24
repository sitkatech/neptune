/*-----------------------------------------------------------------------
<copyright file="treatmentBMPModelExtensions.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Models
{
    public static class ProjectModelExtensions
    {
        public static List<int> GetRegionalSubbasinIDs (this Project project, DatabaseEntities dbContext)
        {
            var projectTreatmentBMPs = project.TreatmentBMPs.ToList();
            var regionalSubbasinIDs = new List<int>();

            foreach (var treatmentBMP in projectTreatmentBMPs)
            {
                var regionalSubbasinID = treatmentBMP.RegionalSubbasinID;

                //Double check for a regional subbasin
                if (regionalSubbasinID == null)
                {
                    regionalSubbasinID = dbContext.RegionalSubbasins.SingleOrDefault(x =>
                    x.CatchmentGeometry.Contains(treatmentBMP.LocationPoint))?.RegionalSubbasinID;
                }

                if (regionalSubbasinID != null && !regionalSubbasinIDs.Contains(regionalSubbasinID.Value))
                {
                    regionalSubbasinIDs.Add(regionalSubbasinID.Value);
                    regionalSubbasinIDs.AddRange(treatmentBMP.GetRegionalSubbasin().TraceUpstreamCatchmentsReturnIDList(dbContext));
                }
            }

            return regionalSubbasinIDs.Distinct().ToList();
        }
    }
}
