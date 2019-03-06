﻿/*-----------------------------------------------------------------------
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
NeptuneMaps.GeoServerMap = function (parcelLocationSummaryMapInitJson, initialBaseLayerShown, geoserverUrl) {
    NeptuneMaps.Map.call(this, parcelLocationSummaryMapInitJson, initialBaseLayerShown);

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
    var wmsParams = Object.assign({}, this.wmsParams);
    L.Util.extend(wmsParams, customParams);

    return wmsParams;
};

NeptuneMaps.GeoServerMap.prototype.createWfsParamsWithLayerName = function (layerName) {
    var customParams = {
        typeName: layerName
    };

    var wfsParams = Object.assign({}, this.wfsParams);
    L.Util.extend(wfsParams, customParams);
    return wfsParams;
};

// click is a function taking an event argument; it will be registered as a click handler for the map object and
// stored in the wmsLayers object of this GeoServerMap. This allows the handler to be conveniently toggled on and off later
NeptuneMaps.GeoServerMap.prototype.addWmsLayer = function (layerName, layerControlDisplayName, params, click) {
    var wmsParams = this.createWmsParamsWithLayerName(layerName);

    if (params) { L.Util.extend(wmsParams, params); }

    var wmsLayer = L.tileLayer.wms(this.geoserverUrlOWS, wmsParams).addTo(this.map);
    this.addLayerToLayerControl(wmsLayer, layerControlDisplayName);


    if (click) {
        this.map.on("click", click);
    }
    this.wmsLayers[layerName] = { layer: wmsLayer, click: click };

    return wmsLayer;
};

// DEPRECATED. Use addWmsLayer instead.
NeptuneMaps.GeoServerMap.prototype.addWmsLayerWithParams = function (layerName, layerControlDisplayName, params) {
    return this.addWmsLayer(layerName, layerControlDisplayName, params);
};
