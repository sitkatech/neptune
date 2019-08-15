using DotSpatial.Projections;
using NetTopologySuite.Geometries;
using System.Data.Entity.Spatial;
using System.Linq;

namespace LtInfo.Common
{
    public class CoordinateSystemHelper
    {
        public const int WGS_1984_SRID = 4326;
        public const int NAD_1983_FEET_CA_STATE_PLANE_VI_SRID = 102646;

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



            var webMercator = KnownCoordinateSystems.Geographic.World.WGS1984;

            var caStatePlane = KnownCoordinateSystems.Projected.StatePlaneNad1983Feet
                .NAD1983StatePlaneCaliforniaVIFIPS0406Feet;

            Reproject.ReprojectPoints(pointArray, zArray, webMercator, caStatePlane, 0, (pointArray.Length / 2));
            

            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }
            //**geom.GeometryChanged(); **
            

            var outputWkb = internalGeometry.AsBinary();

            return DbGeometry.FromBinary(outputWkb, NAD_1983_FEET_CA_STATE_PLANE_VI_SRID);
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



            var webMercator = KnownCoordinateSystems.Geographic.World.WGS1984;

            var caStatePlane = KnownCoordinateSystems.Projected.StatePlaneNad1983Feet
                .NAD1983StatePlaneCaliforniaVIFIPS0406Feet;

            Reproject.ReprojectPoints(pointArray, zArray, caStatePlane, webMercator, 0, (pointArray.Length / 2));
            

            counterX = 0;
            counterY = 1;
            foreach (var coordinate in internalGeometry.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }
            //**geom.GeometryChanged(); **
            

            var outputWkb = internalGeometry.AsBinary();

            return DbGeometry.FromBinary(outputWkb, NAD_1983_FEET_CA_STATE_PLANE_VI_SRID);
        }

    }
}
