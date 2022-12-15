using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using NetTopologySuite.Geometries;

namespace Neptune.Web.Common;

public static class GeometryHelpers
{
    public static IEnumerable<DbGeometry> GeometryToDbGeometryAndMakeValidAndExplodeIfNeeded(Geometry geometry, int coordinateSystemID)
    {
        var dbGeometries = new List<DbGeometry>();
        var sqlGeometry = SqlGeometry.STGeomFromText(new SqlChars(geometry.AsText()),
            coordinateSystemID);
        if (!sqlGeometry.STIsValid())
        {
            sqlGeometry = sqlGeometry.MakeValid();
            var dbGeometry = DbGeometry.FromText(
                sqlGeometry.STAsText().ToSqlString().ToString(),
                coordinateSystemID);
            for (var i = 1; i <= dbGeometry.ElementCount; i++)
            {
                var geometryPart = dbGeometry.ElementAt(i);
                if (geometryPart.SpatialTypeName.ToUpper() == "POLYGON")
                {
                    dbGeometries.Add(geometryPart);
                }
            }
        }
        else
        {
            dbGeometries.Add(DbGeometry.FromText(sqlGeometry.STAsText().ToSqlString().ToString(),
                coordinateSystemID));
        }

        return dbGeometries;
    }
}