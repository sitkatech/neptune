using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace Neptune.Web.Common
{
    public class CoordinateSystemTransformHelper
    {
        public const string baa = "EPSG:4326";
        public const string moo = "EPSG:2771";

        public static Geometry Project_EPSG25832_To_EPSG3857(byte[] wkb)
        {
            NetTopologySuite.IO.WKBReader reader = new NetTopologySuite.IO.WKBReader();
            Geometry geom = (Geometry)reader.Read(wkb);

            double[] pointArray = new double[geom.Coordinates.Count() * 2];
            double[] zArray = new double[1];
            zArray[0] = 1;

            int counterX = 0;
            int counterY = 1;
            foreach (var coordinate in geom.Coordinates)
            {
                pointArray[counterX] = coordinate.X;
                pointArray[counterY] = coordinate.Y;

                counterX = counterX + 2;
                counterY = counterY + 2;
            }

            var epsg25832 = new DotSpatial.Projections.ProjectionInfo();
            var epsg3857 = new DotSpatial.Projections.ProjectionInfo();
            epsg25832.ParseEsriString(baa);
            epsg3857.ParseEsriString(moo);

            DotSpatial.Projections.Reproject.ReprojectPoints(pointArray, zArray, epsg25832, epsg3857, 0, (pointArray.Length / 2));

            counterX = 0;
            counterY = 1;
            foreach (var coordinate in geom.Coordinates)
            {
                coordinate.X = pointArray[counterX];
                coordinate.Y = pointArray[counterY];

                counterX = counterX + 2;
                counterY = counterY + 2;
            }
            //**geom.GeometryChanged(); **
            return geom;
        }

    }
}
