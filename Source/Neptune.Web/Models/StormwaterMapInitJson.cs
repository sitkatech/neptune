/*-----------------------------------------------------------------------
<copyright file="StormwaterMapInitJson.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using GeoJSON.Net.Feature;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;

namespace Neptune.Web.Models
{
    public class StormwaterMapInitJson : MapInitJson
    {
        public StormwaterMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), BoundingBox.MakeNewDefaultBoundingBox())
        {
        }

        public StormwaterMapInitJson(string mapDivID, DbGeometry locationPoint) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), new BoundingBox(locationPoint))
        {
        }

        public StormwaterMapInitJson(string mapDivID, BoundingBox boundingBox) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), boundingBox)
        {
        }

        public StormwaterMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : base(mapDivID, zoomLevel, layers, boundingBox, true)
        {
        }

        public static LayerGeoJson MakeTreatmentBMPLayerGeoJson(IEnumerable<TreatmentBMP> treatmentBMPs, bool isGeneric, bool enablePopups)
        {
            var featureCollection = isGeneric ? treatmentBMPs.ToGeoJsonFeatureCollectionGeneric() : treatmentBMPs.ToGeoJsonFeatureCollection();

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return treatmentBMPLayerGeoJson;
        }

        /// <summary>
        /// Creates a LayerGeoJson with features for each TreatmentBMP in treatmentBMPs.
        /// The FeatureCollection of the resultant LayerGeoJson will have some common properties,
        /// which can be extended with the onEachFeature parameter.
        /// </summary>
        /// <param name="treatmentBMPs"></param>
        /// <param name="onEachFeature"></param>
        /// <param name="enablePopups"></param>
        /// <returns></returns>
        public static LayerGeoJson MakeTreatmentBMPLayerGeoJson(IEnumerable<TreatmentBMP> treatmentBMPs, Action<Feature, TreatmentBMP> onEachFeature, bool enablePopups )
        {
            var featureCollection = treatmentBMPs.ToGeoJsonFeatureCollectionGeneric(onEachFeature);
            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return treatmentBMPLayerGeoJson;
        }

        public static LayerGeoJson MakeDelineationLayerGeoJson(IEnumerable<Delineation> delineation)
        {
            var featureCollection = delineation.ToGeoJsonFeatureCollection();

            var catchmentLayerGeoJson = new LayerGeoJson("Delineation", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};
            return catchmentLayerGeoJson;
        }


        public static LayerGeoJson MakeTreatmentBMPDelineationLayerGeoJson(TreatmentBMP treatmentBMP)
        {
            Check.Require(treatmentBMP.Delineation?.DelineationGeometry != null, "Tried to build delineation layer when delineation was null");
            var featureCollection = new FeatureCollection();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(treatmentBMP.Delineation?.DelineationGeometry);
            featureCollection.Features.Add(feature);

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Delineation", featureCollection, "blue", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return treatmentBMPLayerGeoJson;
        }
    }
}