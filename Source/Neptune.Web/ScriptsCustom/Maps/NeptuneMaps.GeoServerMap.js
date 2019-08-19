/*-----------------------------------------------------------------------
<copyright file="NeptuneMaps.GeoServerMap.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
NeptuneMaps.GeoServerMap = function (parcelLocationSummaryMapInitJson, initialBaseLayerShown, geoserverUrl, customOptions) {
    NeptuneMaps.Map.call(this, parcelLocationSummaryMapInitJson, initialBaseLayerShown, customOptions);

    this.geoserverUrlOWS = geoserverUrl;

    this.wmsLayers = {};

    // todo: rename to baseWmsParams to make intent explicit
    this.wmsParams = {
        service: "WMS",
        transparent: true,
        version: "1.1.1",
        format: "image/png",
        info_format: "application/json",
        tiled: true
//        tilesorigin: [this.map.getBounds().getSouthWest().lng, this.map.getBounds().getSouthWest().lat]
    };

    // todo: rename to baseWfsParams to make intent explicit
    this.wfsParams = {
        service: "WFS",
        version: "2.0",
        request: "GetFeature",
        outputFormat: "application/json",
        SrsName: "EPSG:4326"
    };
};

NeptuneMaps.GeoServerMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.Map.prototype);

NeptuneMaps.GeoServerMap.prototype.createWmsParamsWithLayerName = function (layerName) {
    var customParams = {
        layers: layerName
    };

    // deep-copy the base params and extend them by the customParams;
    // doing it this way ensures that the base params are left alone so future WMS layers aren't called with those params
    var wmsParams = jQuery.extend({}, this.wmsParams);
    L.Util.extend(wmsParams, customParams);

    return wmsParams;
};

NeptuneMaps.GeoServerMap.prototype.createWfsParamsWithLayerName = function (layerName) {
    var customParams = {
        typeName: layerName
    };

    var wfsParams = jQuery.extend({}, this.wfsParams);
    L.Util.extend(wfsParams, customParams);
    return wfsParams;
};


NeptuneMaps.GeoServerMap.prototype.addWmsLayer = function (layerName, layerControlLabelHtml, params, hide) {
    var wmsParams = this.createWmsParamsWithLayerName(layerName);

    if (params) { L.Util.extend(wmsParams, params); }

    var wmsLayer = L.tileLayer.wms(this.geoserverUrlOWS, wmsParams).addTo(this.map);
    
    this.addLayerToLayerControl(wmsLayer, layerControlLabelHtml, hide);

    return wmsLayer;
};

NeptuneMaps.GeoServerMap.prototype.selectFeatureByWfs = function(layerName, params) {
    var parameters = L.Util.extend(this.createWfsParamsWithLayerName(layerName), params);
    return jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(parameters),
        type: "GET"
    });
};

NeptuneMaps.GeoServerMap.prototype.getFeatureInfo = function (layerName, xy) {

    var x1 = xy[0];
    var y1 = xy[1];
    var x2 = x1 + 0.0001;
    var y2 = y1 + 0.0001;

    var bbox = [x1, y1, x2, y2].join(",");


    var params =  {
        service: "WMS",
        transparent: true,
        version: "1.1.1",
        request: "GetFeatureInfo",
        info_format: "application/json",
        tiled: true,
        QUERY_LAYERS: layerName,
        layers: layerName,
        X: 50,
        Y: 50,
        SRS: 'EPSG:4326',
        WIDTH: 101,
        HEIGHT: 101,
        BBOX: bbox
    };

    return jQuery.ajax({
        url: this.geoserverUrlOWS + L.Util.getParamString(params),
        type: "GET"
    });
};