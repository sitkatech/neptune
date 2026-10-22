using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.Common.GeoSpatial;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Neptune.Tests
{
    /// <summary>
    /// NPT-1140 — the parcel sync repairs each OCGIS parcel geometry in C# before staging it;
    /// anything that isn't a valid, non-empty Polygon/MultiPolygon after repair is skipped.
    /// </summary>
    [TestClass]
    public class PolygonalGeometryRepairTests
    {
        private const int Srid = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID;

        private static Geometry Read(string wkt)
        {
            var geometry = new WKTReader().Read(wkt);
            geometry.SRID = Srid;
            return geometry;
        }

        [TestMethod]
        public void ValidPolygon_IsKeptUnchanged()
        {
            var square = Read("POLYGON ((0 0, 10 0, 10 10, 0 10, 0 0))");

            var result = PolygonalGeometryRepair.Repair(square);

            Assert.AreEqual(PolygonalRepairOutcome.Valid, result.Outcome);
            Assert.AreSame(square, result.Geometry);
        }

        [TestMethod]
        public void SelfIntersectingPolygon_IsRepairedToValidPolygonal()
        {
            var bowtie = Read("POLYGON ((0 0, 10 10, 10 0, 0 10, 0 0))");
            Assert.IsFalse(bowtie.IsValid);

            var result = PolygonalGeometryRepair.Repair(bowtie);

            Assert.AreEqual(PolygonalRepairOutcome.Repaired, result.Outcome);
            Assert.IsNotNull(result.Geometry);
            Assert.IsTrue(result.Geometry.IsValid);
            Assert.IsTrue(result.Geometry is Polygon or MultiPolygon, result.Geometry.GeometryType);
            // two 25-unit triangles meeting at (5 5)
            Assert.AreEqual(50, result.Geometry.Area, 1e-9);
            Assert.AreEqual(Srid, result.Geometry.SRID);
        }

        [TestMethod]
        public void EmptyPolygon_IsSkipped()
        {
            var result = PolygonalGeometryRepair.Repair(Read("POLYGON EMPTY"));

            Assert.AreEqual(PolygonalRepairOutcome.Skipped, result.Outcome);
            Assert.IsNull(result.Geometry);
            Assert.IsNotNull(result.SkipReason);
        }

        [TestMethod]
        public void NullGeometry_IsSkipped()
        {
            var result = PolygonalGeometryRepair.Repair(null);

            Assert.AreEqual(PolygonalRepairOutcome.Skipped, result.Outcome);
        }

        [TestMethod]
        public void CollapsedZeroAreaPolygon_IsSkipped()
        {
            var sliver = Read("POLYGON ((0 0, 10 0, 20 0, 0 0))");

            var result = PolygonalGeometryRepair.Repair(sliver);

            Assert.AreEqual(PolygonalRepairOutcome.Skipped, result.Outcome);
            Assert.IsNull(result.Geometry);
        }

        [TestMethod]
        public void LineString_IsSkippedAsNonPolygonal()
        {
            var result = PolygonalGeometryRepair.Repair(Read("LINESTRING (0 0, 10 10)"));

            Assert.AreEqual(PolygonalRepairOutcome.Skipped, result.Outcome);
            StringAssert.Contains(result.SkipReason, "not a polygon");
        }

        [TestMethod]
        public void MixedCollection_KeepsOnlyPolygonalParts()
        {
            var collection = Read("GEOMETRYCOLLECTION (POLYGON ((0 0, 10 0, 10 10, 0 10, 0 0)), LINESTRING (20 20, 30 30))");

            var result = PolygonalGeometryRepair.Repair(collection);

            Assert.AreEqual(PolygonalRepairOutcome.Repaired, result.Outcome);
            Assert.IsInstanceOfType(result.Geometry, typeof(Polygon));
            Assert.AreEqual(100, result.Geometry.Area, 1e-9);
            Assert.AreEqual(Srid, result.Geometry.SRID);
        }
    }
}
