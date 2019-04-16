using Neptune.Web.Models;
using System;
using System.Data.Entity.Spatial;

namespace Neptune.Web.Common
{
    public static class NeptuneGeometryUtilities
    {
        public static DbGeometry FixSrid(this DbGeometry geometry)
        {
            var wellKnownText = geometry.ToString();

            // geometry.ToString() includes the SRID at the beginning of the string but is otherwise legal WKT
            if (wellKnownText.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture) > -1)
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture));
            }
            else
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture));
            }

            geometry = DbGeometry.FromText(wellKnownText, MapInitJson.CoordinateSystemId);
            return geometry;
        }
    }
}
