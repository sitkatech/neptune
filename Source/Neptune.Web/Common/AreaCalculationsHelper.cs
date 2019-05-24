using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class AreaCalculationsHelper
    {
        private const int DecimalPlacesToDisplay = 0;

        public static double GetArea(this IEnumerable<TrashGeneratingUnit> trashGeneratingUnits)
        {
            return Math.Round(trashGeneratingUnits
                .Select(x => x.TrashGeneratingUnitGeometry.Area * DbSpatialHelper.SqlGeometryAreaToAcres).Sum().GetValueOrDefault(), DecimalPlacesToDisplay); // will never be null
        }
    }
}