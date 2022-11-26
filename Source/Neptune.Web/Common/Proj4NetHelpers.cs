using System.Collections.Generic;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Neptune.Web.Common;

public static class Proj4NetHelpers
{
    private static readonly Dictionary<int, string> CoordinateSystemsWkTs = new Dictionary<int, string>
    {
        [2771] = @"
PROJCS[""NAD83(HARN) / California zone 6"",
    GEOGCS[""NAD83(HARN)"",
        DATUM[""NAD83_High_Accuracy_Reference_Network"",
            SPHEROID[""GRS 1980"",6378137,298.257222101],
            TOWGS84[-0.991,1.9072,0.5129,-1.25033E-07,-4.6785E-08,-5.6529E-08,0]],
        PRIMEM[""Greenwich"",0,
            AUTHORITY[""EPSG"",""8901""]],
        UNIT[""degree"",0.0174532925199433,
            AUTHORITY[""EPSG"",""9122""]],
        AUTHORITY[""EPSG"",""4152""]],
    PROJECTION[""Lambert_Conformal_Conic_2SP""],
    PARAMETER[""latitude_of_origin"",32.1666666666667],
    PARAMETER[""central_meridian"",-116.25],
    PARAMETER[""standard_parallel_1"",33.8833333333333],
    PARAMETER[""standard_parallel_2"",32.7833333333333],
    PARAMETER[""false_easting"",2000000],
    PARAMETER[""false_northing"",500000],
    UNIT[""metre"",1,
        AUTHORITY[""EPSG"",""9001""]],
    AXIS[""Easting"",EAST],
    AXIS[""Northing"",NORTH],
    AUTHORITY[""EPSG"",""2771""]]
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

