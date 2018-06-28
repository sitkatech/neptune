/*-----------------------------------------------------------------------
<copyright file="NeptuneMaps.ParcelSearch.js" company="Tahoe Regional Planning Agency">
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
NeptuneMaps.ParcelSearch = function (parcelLocationSummaryMapInitJson, initialBaseLayer, geoserverUrl, parcelSummaryUrl) {
    NeptuneMaps.GeoServerMap.call(this, parcelLocationSummaryMapInitJson, initialBaseLayer, geoserverUrl);

    this.parcelSummaryUrl = parcelSummaryUrl;

    var allParcelsLayerName = "OCStormwater:Parcels";
    this.wmsParams = this.createWmsParamsWithLayerName(allParcelsLayerName);
    this.wfsParams = this.createWfsParamsWithLayerName(allParcelsLayerName);

    this.addWmsLayer(allParcelsLayerName, "All Parcels");
    this.map.on('click', this.selectParcel, this);
};

NeptuneMaps.ParcelSearch.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.ParcelSearch.prototype.findParcelAndAddToMap = function (parcelNumber) {
    var customParams = {
        cql_filter: "ParcelNumber='" + parcelNumber + "'"
    };

    this.selectParcelByWFS(customParams);
};

NeptuneMaps.ParcelSearch.prototype.selectParcelByWFS = function (customParams) {
    var self = this;
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.selectedFeature)) {
        this.map.removeLayer(this.selectedFeature);
        this.layerControl.removeLayer(this.selectedFeature);
    }

    var parameters = L.Util.extend(this.wfsParams, customParams);

    SitkaAjax.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(parameters),
        dataType: 'json',
        jsonpCallback: 'getJson'
    },
        function (response) {
            self.selectedFeature = L.geoJson(response, {
                style: function (feature) {
                    return {
                        stroke: true,
                        strokeColor: "#ff0000",
                        fillColor: 'FFFFFF',
                        fillOpacity: 0
                    };
                },
                onEachFeature: function (feature, layer) {
                    jQuery("#parcelAddressFinder").val(feature.properties.ParcelAddress);
                    SitkaAjax.load(jQuery("#parcelAddressResultDetails"), self.parcelSummaryUrl + "/" + feature.properties.ParcelNumber);
                }
            }).addTo(self.map);

            var bounds = self.selectedFeature.getBounds();
            if (bounds.isValid()) {
                self.map.fitBounds(bounds);
            }
        });
};

NeptuneMaps.ParcelSearch.prototype.selectParcel = function (evt) {
    var customParams = {
        cql_filter: 'intersects(ParcelGeometry, POINT(' + evt.latlng.lat + ' ' + evt.latlng.lng + '))'
    };
    this.selectParcelByWFS(customParams);
};
