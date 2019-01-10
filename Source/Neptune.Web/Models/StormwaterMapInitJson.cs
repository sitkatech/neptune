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

        public StormwaterMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : base(mapDivID, zoomLevel, layers, boundingBox, true)
        {
        }

        public static LayerGeoJson MakeTreatmentBMPLayerGeoJson(IEnumerable<TreatmentBMP> treatmentBMPs, bool isGeneric, bool enablePopups)
        {
            var featureCollection = isGeneric ? treatmentBMPs.ToGeoJsonFeatureCollectionGeneric() : treatmentBMPs.ToGeoJsonFeatureCollection();

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return treatmentBMPLayerGeoJson;
        }

        public static LayerGeoJson MakeTreatmentBMPLayerGeoJsonForTrashMap(IEnumerable<TreatmentBMP> treatmentBMPs, bool enablePopups)
        {
            var featureCollection = treatmentBMPs.ToGeoJsonFeatureCollectionForTrashMap();

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return treatmentBMPLayerGeoJson;
        }

        public static LayerGeoJson MakeModeledCatchmentLayerGeoJson(IEnumerable<ModeledCatchment> modeledCatchments, bool isGeneric, bool enablePopups)
        {
            var featureCollection = isGeneric ? modeledCatchments.ToGeoJsonFeatureCollectionGeneric() : modeledCatchments.ToGeoJsonFeatureCollection();

            var catchmentLayerGeoJson = new LayerGeoJson("Modeled Catchments", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = false};
            return catchmentLayerGeoJson;
        }

       
    }
}