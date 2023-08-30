﻿using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Neptune.Common.GeoSpatial
{
    public static class Proj4NetHelper
    {
        private static readonly Dictionary<int, string> CoordinateSystemsWkTs = new()
        {
            [2225] = @"
                PROJCS[""NAD83 / California zone 1 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101,
                                AUTHORITY[""EPSG"",""7019""]],
                            TOWGS84[0,0,0,0,0,0,0],
                            AUTHORITY[""EPSG"",""6269""]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""standard_parallel_1"",41.66666666666666],
                    PARAMETER[""standard_parallel_2"",40],
                    PARAMETER[""latitude_of_origin"",39.33333333333334],
                    PARAMETER[""central_meridian"",-122],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.3048006096012192,
                        AUTHORITY[""EPSG"",""9003""]],
                    AXIS[""X"",EAST],
                    AXIS[""Y"",NORTH],
                    AUTHORITY[""EPSG"",""2225""]]",
            [2226] = @"
                PROJCS[""NAD83 / California zone 2 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101,
                                AUTHORITY[""EPSG"",""7019""]],
                            TOWGS84[0,0,0,0,0,0,0],
                            AUTHORITY[""EPSG"",""6269""]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""standard_parallel_1"",39.83333333333334],
                    PARAMETER[""standard_parallel_2"",38.33333333333334],
                    PARAMETER[""latitude_of_origin"",37.66666666666666],
                    PARAMETER[""central_meridian"",-122],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.3048006096012192,
                        AUTHORITY[""EPSG"",""9003""]],
                    AXIS[""X"",EAST],
                    AXIS[""Y"",NORTH],
                    AUTHORITY[""EPSG"",""2226""]]",
            [2227] = @"
                PROJCS[""NAD83 / California zone 3 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101,
                                AUTHORITY[""EPSG"",""7019""]],
                            TOWGS84[0,0,0,0,0,0,0],
                            AUTHORITY[""EPSG"",""6269""]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""standard_parallel_1"",38.43333333333333],
                    PARAMETER[""standard_parallel_2"",37.06666666666667],
                    PARAMETER[""latitude_of_origin"",36.5],
                    PARAMETER[""central_meridian"",-120.5],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.3048006096012192,
                        AUTHORITY[""EPSG"",""9003""]],
                    AXIS[""X"",EAST],
                    AXIS[""Y"",NORTH],
                    AUTHORITY[""EPSG"",""2227""]]",
            [2228] = @"
                PROJCS[""NAD83 / California zone 4 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101],
                            TOWGS84[0,0,0,0,0,0,0]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""latitude_of_origin"",35.3333333333333],
                    PARAMETER[""central_meridian"",-119],
                    PARAMETER[""standard_parallel_1"",37.25],
                    PARAMETER[""standard_parallel_2"",36],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.304800609601219],
                    AXIS[""Easting"",EAST],
                    AXIS[""Northing"",NORTH],
                    AUTHORITY[""EPSG"",""2228""]]",
            [2229] = @"
                PROJCS[""NAD83 / California zone 5 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101,
                                AUTHORITY[""EPSG"",""7019""]],
                            TOWGS84[0,0,0,0,0,0,0],
                            AUTHORITY[""EPSG"",""6269""]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""standard_parallel_1"",35.46666666666667],
                    PARAMETER[""standard_parallel_2"",34.03333333333333],
                    PARAMETER[""latitude_of_origin"",33.5],
                    PARAMETER[""central_meridian"",-118],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.3048006096012192,
                        AUTHORITY[""EPSG"",""9003""]],
                    AXIS[""X"",EAST],
                    AXIS[""Y"",NORTH],
                    AUTHORITY[""EPSG"",""2229""]]",
            [2230] = @"
                PROJCS[""NAD83 / California zone 6 (ftUS)"",
                    GEOGCS[""NAD83"",
                        DATUM[""North_American_Datum_1983"",
                            SPHEROID[""GRS 1980"",6378137,298.257222101,
                                AUTHORITY[""EPSG"",""7019""]],
                            TOWGS84[0,0,0,0,0,0,0],
                            AUTHORITY[""EPSG"",""6269""]],
                        PRIMEM[""Greenwich"",0,
                            AUTHORITY[""EPSG"",""8901""]],
                        UNIT[""degree"",0.0174532925199433,
                            AUTHORITY[""EPSG"",""9122""]],
                        AUTHORITY[""EPSG"",""4269""]],
                    PROJECTION[""Lambert_Conformal_Conic_2SP""],
                    PARAMETER[""standard_parallel_1"",33.88333333333333],
                    PARAMETER[""standard_parallel_2"",32.78333333333333],
                    PARAMETER[""latitude_of_origin"",32.16666666666666],
                    PARAMETER[""central_meridian"",-116.25],
                    PARAMETER[""false_easting"",6561666.667],
                    PARAMETER[""false_northing"",1640416.667],
                    UNIT[""US survey foot"",0.3048006096012192,
                        AUTHORITY[""EPSG"",""9003""]],
                    AXIS[""X"",EAST],
                    AXIS[""Y"",NORTH],
                    AUTHORITY[""EPSG"",""2230""]]",
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
                    AUTHORITY[""EPSG"",""2771""]]"
        };

        private static Geometry Transform(Geometry geom, MathTransform transform, int targetSrid)
        {
            geom = geom.Copy();
            geom.Apply(new MathTransformFilter(transform));
            geom.SRID = targetSrid;
            return geom;
        }

        public static Geometry ProjectTo2771(this Geometry geometry)
        {
            if (geometry.SRID == 2771)
            {
                return geometry;
            }

            var targetCoordinateSystem = new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[2771]);
            var fromCoordinateSystem = geometry.SRID == 4326 
                ? GeographicCoordinateSystem.WGS84 
                : new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[geometry.SRID]);
            var transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(fromCoordinateSystem, targetCoordinateSystem);
            return Transform(geometry, transformation.MathTransform, 2771);
        }

        public static Geometry ProjectTo2230(this Geometry geometry)
        {
            if (geometry.SRID == 2230)
            {
                return geometry;
            }

            var targetCoordinateSystem = new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[2230]);
            var fromCoordinateSystem = geometry.SRID == 4326 
                ? GeographicCoordinateSystem.WGS84 
                : new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[geometry.SRID]);
            var transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(fromCoordinateSystem, targetCoordinateSystem);
            return Transform(geometry, transformation.MathTransform, 2230);
        }

        public static Geometry ProjectTo4326(this Geometry geometry)
        {
            if (geometry.SRID == 4326)
            {
                return geometry;
            }

            var sourceCoordinateSystem = new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[geometry.SRID]);
            var transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(sourceCoordinateSystem, GeographicCoordinateSystem.WGS84);
            return Transform(geometry, transformation.MathTransform, 4326);
        }

        public static Geometry ProjectToSrid(this Geometry geometry, int targetSrid)
        {
            var targetCoordinateSystem = new CoordinateSystemFactory().CreateFromWkt(CoordinateSystemsWkTs[targetSrid]);
            var transformation = new CoordinateTransformationFactory().CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, targetCoordinateSystem);
            return Transform(geometry, transformation.MathTransform, targetSrid);
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