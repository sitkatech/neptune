/*-----------------------------------------------------------------------
<copyright file="NeptuneMaps.Stormwater.js" company="Tahoe Regional Planning Agency">
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
NeptuneMaps.Stormwater = function (stormwaterMapInitJson) {
    NeptuneMaps.Map.call(this, stormwaterMapInitJson);
    var self = this;
    if (stormwaterMapInitJson.ClusteredLayerGeoJson != null)
    {
        this.addClusteredLayer(stormwaterMapInitJson.ClusteredLayerGeoJson.GeoJsonFeatureCollection);
    }  
};

NeptuneMaps.Stormwater.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.Map.prototype);

NeptuneMaps.Stormwater.prototype.addClusteredLayer = function(clusteredLayerGeoJsonFeatureCollection)
{
    var clusteredLayerGeoJson = L.geoJson(clusteredLayerGeoJsonFeatureCollection,
    {
        pointToLayer: function(feature, latlng)
        {
            var icon = this.buildDefaultLeafletMarkerFromMarkerPath('/Content/leaflet/images/marker-icon-orange.png');

            return L.marker(latlng,
            {
                icon: icon,
                title: feature.properties.Name,
                alt: feature.properties.Name
            });
        },
        style: function(feature)
        {
            return {
                color: feature.properties.FeatureColor = feature.properties.FeatureColor,
                weight: feature.properties.FeatureWeight = feature.properties.FeatureWeight,
                fill: feature.properties.FillPolygon = feature.properties.FillPolygon,
                fillOpacity: feature.properties.FillOpacity = feature.properties.FillOpacity
            };
        }
    });    

    var markerClusterGroup = L.markerClusterGroup({
        maxClusterRadius: 40,
        showCoverageOnHover: false,
        iconCreateFunction: function(cluster)
        {
            return new L.DivIcon({
                html: '<div><span>' + cluster.getChildCount() + '</span></div>',
                className: 'treatmentBMPCluster',
                iconSize: new L.Point(40, 40),
            });
        }
    });

    clusteredLayerGeoJson.addTo(markerClusterGroup);
    markerClusterGroup.addTo(this.map);
}

NeptuneMaps.Stormwater.prototype.formatLayerProperty = function (propertyName, propertyValue) {
    if (Sitka.Methods.isUndefinedNullOrEmpty(propertyValue)) {
        propertyValue = "&nbsp";
    }
    return "<div>" + propertyName + ": &nbsp;" + propertyValue + "</div>";
};
