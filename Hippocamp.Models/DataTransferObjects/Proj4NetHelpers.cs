using System.Collections.Generic;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Hippocamp.Models.DataTransferObjects
{
    public static class Proj4NetHelpers
    {
        private static readonly Dictionary<int, string> CoordinateSystemsWkTs = new Dictionary<int, string>
        {
            [2771] = @"
PROJCS[""NAD83(HARN) / California zone 6"",
    GEOGCS[""NAD83(HARN)"",
        DATUM[""NAD83_High_Accuracy_Regional_Network"",
            SPHEROID[""GRS 1980"", 6378137, 298.257222101,
                AUTHORITY[""EPSG"", ""7019""]],
            AUTHORITY[""EPSG"", ""6152""]],
        PRIMEM[""Greenwich"", 0,
            AUTHORITY[""EPSG"", ""8901""]],
        UNIT[""degree"", 0.01745329251994328,
            AUTHORITY[""EPSG"", ""9122""]],
        AUTHORITY[""EPSG"", ""4152""]],
            UNIT[""metre"", 1,
        AUTHORITY[""EPSG"", ""9001""]],
            PROJECTION[""Lambert_Conformal_Conic_2SP""],
            PARAMETER[""standard_parallel_1"", 33.88333333333333],
            PARAMETER[""standard_parallel_2"", 32.78333333333333],
            PARAMETER[""latitude_of_origin"", 32.16666666666666],
            PARAMETER[""central_meridian"", -116.25],
            PARAMETER[""false_easting"", 2000000],
            PARAMETER[""false_northing"", 500000],
            AUTHORITY[""EPSG"", ""2771""],
            AXIS[""X"", EAST],
            AXIS[""Y"", NORTH]]
"
        };

        private static Geometry Transform(Geometry geom, MathTransform transform, int targetSrid)
        {
            geom = geom.Copy();
            geom.Apply(new MathTransformFilter(transform));
            geom.SRID = targetSrid;
            return geom;
        }

        public static Geometry ProjectTo4326(this Geometry geometry)
        {
            var sourceCoordinateSystem = new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[geometry.SRID]);
            var transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(sourceCoordinateSystem, GeographicCoordinateSystem.WGS84);
            return Transform(geometry, transformation.MathTransform, 4326);
        }
    }

    internal sealed class MathTransformFilter : ICoordinateSequenceFilter
    {
        private readonly MathTransform _mathTransform;

        public MathTransformFilter(MathTransform mathTransform) => _mathTransform = mathTransform;

        public bool Done => false;
        public bool GeometryChanged => true;
        public void Filter(CoordinateSequence seq, int i)
        {
            var x = seq.GetX(i);
            var y = seq.GetY(i);
            //var z = seq.GetZ(i);
            //_mathTransform.Transform(ref x, ref y, ref z);
            _mathTransform.Transform(ref x, ref y);
            seq.SetX(i, x);
            seq.SetY(i, y);
            //seq.SetZ(i, z);
        }
    }
}