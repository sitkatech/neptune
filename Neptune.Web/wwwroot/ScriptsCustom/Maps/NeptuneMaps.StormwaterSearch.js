/*-----------------------------------------------------------------------
<copyright file="NeptuneMaps.StormwaterSearch.js" company="Tahoe Regional Planning Agency">
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
NeptuneMaps.StormwaterSearch = function (stormwaterMapInitJson)
{
    NeptuneMaps.Stormwater.call(this, stormwaterMapInitJson);

    var self = this;
    
    var layerGroup = new L.LayerGroup();
    self.searchableLayerGeoJson = L.geoJson(stormwaterMapInitJson.SearchableLayerGeoJson.GeoJsonFeatureCollection,
    {
        pointToLayer: function(feature, latlng)
        {
            var icon = L.MakiMarkers.icon({
                icon: feature.properties.FeatureGlyph,
                color: feature.properties.FeatureColor,
                size: "m"
            });

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

    self.markerClusterGroup = L.markerClusterGroup({
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
    self.searchableLayerGeoJson.addTo(self.markerClusterGroup);

    self.markerClusterGroup.addTo(this.map);
    
    self.searchableLayerGeoJson.on('click', function (e) { self.markerClicked(self, e); });
};

NeptuneMaps.StormwaterSearch.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.Stormwater.prototype);

NeptuneMaps.StormwaterSearch.prototype.typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, summaryUrl) {
    var self = this;
    self.typeaheadSelector = typeaheadSelector;
    var finder = jQuery(typeaheadSelector);
    finder.typeahead({
            highlight: true,
            minLength: 1
        },
        {
            source: new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.whitespace,
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url: summaryUrl +
                        '?term=%QUERY',
                    wildcard: '%QUERY'
                }
            }),
            display: 'Text',
            limit: Number.MAX_VALUE
        });

    finder.bind('typeahead:select',
        function(ev, suggestion)
        {
            var summaryDataJson = JSON.parse(suggestion.Value);
            self.loadSummaryPanel(summaryDataJson.MapSummaryUrl);
            self.map.setView(new L.LatLng(summaryDataJson.Latitude, summaryDataJson.Longitude), 13);
            self.map.invalidateSize();
            setTimeout(function() {
                    self.apply(L.GeoJSON.geometryToLayer(summaryDataJson.GeometryJson), summaryDataJson.EntityID);
                },
                500);
        });

    jQuery(typeaheadSelectorButton).click(function () { selectFirstSuggestionFunction(finder); });

    finder.keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            selectFirstSuggestionFunction(this);
        }
    });
};

lastSelected = null; //cache for the last clicked item so we can reset it's color
NeptuneMaps.StormwaterSearch.prototype.setSelectedMarker = function(layer)
{
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.lastSelected))
    {
        this.map.removeLayer(this.lastSelected);
    }

    this.lastSelected = L.geoJson(layer.toGeoJSON(),
    {
        pointToLayer: function(feature, latlng)
        {
            var icon = L.MakiMarkers.icon({
                icon: "marker",
                color: "#FFFF00",
                size: "m",
            });

            return L.marker(latlng,
            {
                icon: icon,
                riseOnHover: true,
            });
        },
        style: function(feature)
        {
            return {
                fillColor: "#FFFF00",
                fill: true,
                fillOpacity: 0.2,
                color: "#FFFF00",
                weight: 5,
                stroke: true,
            };
        }
    });

    this.lastSelected.addTo(this.map);
}

NeptuneMaps.StormwaterSearch.prototype.loadSummaryPanel = function(mapSummaryUrl)
{
    if (!Sitka.Methods.isUndefinedNullOrEmpty(mapSummaryUrl)) {
        jQuery.get(mapSummaryUrl)
            .done(function (data) {
                jQuery('#mapSummaryResults').empty();
                jQuery('#mapSummaryResults').append(data);
            });
    }
}

NeptuneMaps.StormwaterSearch.prototype.markerClicked  = function(self, e)
{
    self.setSelectedMarker(e.layer);
    self.loadSummaryPanel(e.layer.feature.properties.MapSummaryUrl);
}
