using System;
using DotSpatial.Projections;
using NetTopologySuite.Geometries;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DbSpatial;

namespace LtInfo.Common
{
    public class CoordinateSystemHelper
    {
        public const int WGS_1984_SRID = 4326;
        public const int NAD_83_HARN_CA_ZONE_VI_SRID = 2771;
        public const int NAD_83_CA_ZONE_VI_SRID = 2230;

        public const double SquareFeetToAcresDivisor = 43560;

        public static ProjectionInfo WebMercator => KnownCoordinateSystems.Geographic.World.WGS1984;

        public static ProjectionInfo CaStatePlane => KnownCoordinateSystems.Projected.StatePlaneNad1983Harn.NAD1983HARNStatePlaneCaliforniaVIFIPS0406;

        public static ProjectionInfo CaStatePlane2230 => KnownCoordinateSystems.Projected.StatePlaneNad1983Feet
            .NAD1983StatePlaneCaliforniaVIFIPS0406Feet;


        public static DbGeometry ProjectWebMercatorToCaliforniaStatePlaneVI(DbGeometry inputGeometry)
        {
            var wkb = inputGeometry.AsBinary();

            NetTopologySuite.IO.WKBReader reader = new NetTopologySuite.IO.WKBReader();
            Geometry internalGeometry = (Geometry)reader.Read(wkb);

            double[] pointArray = new double[internalGeometry.Coordinates.Count() * 2];
            double[] zArray = new double[1];
            zArray[0] = 1;

            int counterX = 0;
            int counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                pointArray[counterX] = coordinate.X;
                pointArray[counterY] = coordinate.Y;

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            Reproject.ReprojectPoints(pointArray, zArray, WebMercator, CaStatePlane, 0, (pointArray.Length / 2));
            
            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            var outputWkb = internalGeometry.AsBinary();

            return DbGeometry.FromBinary(outputWkb, NAD_83_HARN_CA_ZONE_VI_SRID);
        }

        public static DbGeometry ProjectCaliforniaStatePlaneVIToWebMercator(DbGeometry inputGeometry)
        {
            var wkb = inputGeometry.AsBinary();

            NetTopologySuite.IO.WKBReader reader = new NetTopologySuite.IO.WKBReader();
            Geometry internalGeometry = (Geometry)reader.Read(wkb);

            double[] pointArray = new double[internalGeometry.Coordinates.Count() * 2];
            double[] zArray = new double[1];
            zArray[0] = 1;

            int counterX = 0;
            int counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                pointArray[counterX] = coordinate.X;
                pointArray[counterY] = coordinate.Y;

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            Reproject.ReprojectPoints(pointArray, zArray, CaStatePlane, WebMercator, 0, (pointArray.Length / 2));
            
            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            var outputWkb = internalGeometry.AsBinary();

            var dbGeometry = DbGeometry.FromBinary(outputWkb, WGS_1984_SRID);

            if (!dbGeometry.IsValid)
            {
                dbGeometry = dbGeometry.ToSqlGeometry().MakeValid().ToDbGeometry().FixSrid(WGS_1984_SRID);
            }

            return dbGeometry;
        }

        /// <summary>
        /// Should only ever be used for the RSB revision request download
        /// </summary>
        /// <param name="inputGeometry"></param>
        /// <returns></returns>
        public static DbGeometry ProjectWebMercatorTo2230(DbGeometry inputGeometry)
        {
            var wkb = inputGeometry.AsBinary();

            NetTopologySuite.IO.WKBReader reader = new NetTopologySuite.IO.WKBReader();
            Geometry internalGeometry = (Geometry)reader.Read(wkb);

            double[] pointArray = new double[internalGeometry.Coordinates.Count() * 2];
            double[] zArray = new double[1];
            zArray[0] = 1;

            int counterX = 0;
            int counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                pointArray[counterX] = coordinate.X;
                pointArray[counterY] = coordinate.Y;

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            Reproject.ReprojectPoints(pointArray, zArray, WebMercator, CaStatePlane2230, 0, (pointArray.Length / 2));

            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            var outputWkb = internalGeometry.AsBinary();

            var dbGeometry = DbGeometry.FromBinary(outputWkb, NAD_83_CA_ZONE_VI_SRID);

            if (!dbGeometry.IsValid)
            {
                dbGeometry = dbGeometry.ToSqlGeometry().MakeValid().ToDbGeometry().FixSrid(NAD_83_CA_ZONE_VI_SRID);
            }

            return dbGeometry;
        }
        
        public static DbGeometry Project2771To2230(DbGeometry inputGeometry)
        {
            var wkb = inputGeometry.AsBinary();

            NetTopologySuite.IO.WKBReader reader = new NetTopologySuite.IO.WKBReader();
            Geometry internalGeometry = (Geometry)reader.Read(wkb);

            double[] pointArray = new double[internalGeometry.Coordinates.Count() * 2];
            double[] zArray = new double[1];
            zArray[0] = 1;

            int counterX = 0;
            int counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                pointArray[counterX] = coordinate.X;
                pointArray[counterY] = coordinate.Y;

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            Reproject.ReprojectPoints(pointArray, zArray, CaStatePlane, CaStatePlane2230, 0, (pointArray.Length / 2));

            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            var outputWkb = internalGeometry.AsBinary();

            var dbGeometry = DbGeometry.FromBinary(outputWkb, NAD_83_CA_ZONE_VI_SRID);

            if (!dbGeometry.IsValid)
            {
                dbGeometry = dbGeometry.ToSqlGeometry().MakeValid().ToDbGeometry();//.FixSrid(NAD_83_CA_ZONE_VI_SRID);
            }

            return dbGeometry;
        }
    }

    public static class GeometryExtensions
    {
        public static DbGeometry FixSrid(this DbGeometry geometry, int srid)
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
            }
            else if (wellKnownText.IndexOf("LINESTRING", StringComparison.InvariantCulture) > -1)
            {
                wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("LINESTRING", StringComparison.InvariantCulture));
            }

            geometry = DbGeometry.FromText(wellKnownText, srid);
            return geometry;
        }
    }
}
