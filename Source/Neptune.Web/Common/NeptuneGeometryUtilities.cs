using Neptune.Web.Models;
using System;
using System.Data.Entity.Spatial;
using LtInfo.Common;

namespace Neptune.Web.Common
{
    public static class NeptuneGeometryUtilities
    {
        public static DbGeometry FixSrid(this DbGeometry geometry)
        {
            if (geometry == null)
            {
                return geometry;
            }
            var wellKnownText = geometry.ToString();

            // geometry.ToString() includes the SRID at the beginning of the string but is otherwise legal WKT
            if (wellKnownText.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture) > -1)
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture));
            }
            else if (wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture) > -1)
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture));
            } else if (wellKnownText.IndexOf("LINESTRING", StringComparison.InvariantCulture) > -1)
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("LINESTRING", StringComparison.InvariantCulture));
            }

            // Since FixSrid is for storing data, State Plane is the way to go
            geometry = DbGeometry.FromText(wellKnownText, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
            return geometry;
        }
    }
}
