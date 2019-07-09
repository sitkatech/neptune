﻿var resizeHandler = function (mapInitJson) {
    var totalHeaderHeight = (jQuery("header").height());

    if (jQuery("header").is(":hidden")){
        jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height());
    } else {
        jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
            totalHeaderHeight);
    }

    window.totalHeaderHeight = totalHeaderHeight;
};

function initResizeHandler(mapInitJson) {
    $(window).on("load",
        function () { resizeHandler(mapInitJson); }
    );

    $(window).on("resize",
        function () { resizeHandler(mapInitJson); });
}

var NEIGHBORHOOD_NOT_FOUND =
    "This neighborhood is not within the Urban Drool Tool reporting area. If you wish to be included in the Urban Drool Tool, please contact your local water district.";
var NOMINATIM_ERROR =
    "There was an error retrieving the address. The address location service may be unavailable. If the issue persists, please contact support.";

L.Control.NeighborhoodDetailControl = L.Control.extend({
    onAdd: function (map) {
        this.parentElement = L.DomUtil.create("div", "leaflet-bar leaflet-control neptune-leaflet-control neighborhood-detail-control");
        this.neptuneMap = this.options.neptuneMap;

        var h4 = L.DomUtil.create("h4");
        h4.innerHTML = "Selected Neighborhood";
        this.parentElement.append(h4);

        this.selectedNeighborhoodText = L.DomUtil.create("p");

        this.parentElement.append(this.selectedNeighborhoodText);

        this.hide();

        window.stopClickPropagation(this.parentElement);

        var highlightFlowButton = L.DomUtil.create("button", "btn btn-neptune btn-sm");
        highlightFlowButton.innerHTML = "Where does my irrigation runoff go?";

        var self = this;
        L.DomEvent.on(highlightFlowButton,
            "click",
            function () {
                self.neptuneMap.highlightFlow();
            });

        this.parentElement.append(highlightFlowButton);

        window.stopClickPropagation(this.parentElement);
        return this.parentElement;
    },

    hide: function () {
        this.parentElement.style.display = "none";
    },

    show: function () {
        this.parentElement.style.display = "block";
    },

    selectNeighborhood: function (properties) {

        this.selectedNeighborhoodText.innerHTML =
            "Selected Neighborhood: " + properties.NetworkCatchmentID;
        this.show();
    }
});

L.Control.NominatimSearchControl = L.Control.extend({
    onAdd: function (map) {
        var input = jQuery(".nominatimSearchInput").get(0);
        var searchButton = jQuery(".nominatimSearchButton").get(0);

        this.neptuneMap = this.options.neptuneMap;
        this.parentElement = L.DomUtil.create("div",
            "neighborhood-search-control");
        this.parentElement.append(jQuery("#nominatimSearchWrapper").get(0));
        jQuery("#nominatimSearchWrapper").css("display", "block");

        L.DomEvent.on(searchButton, "click", function () {
            var q = jQuery(".nominatimSearchInput").val();

            RemoteService.nominatimLookup(q);
        });
        L.DomEvent.on(input, "keyup", function (event) {
            if (event.keyCode === 13) {
                var q = jQuery(".nominatimSearchInput").val();

                RemoteService.nominatimLookup(q);
            }
        });
        
        window.stopClickPropagation(this.parentElement);
        return this.parentElement;
    },
});

L.Control.ExplorerTrayControl = L.Control.extend({
    onAdd: function(map) {
        this.parentElement = L.DomUtil.create("div", "explorerTray");

        this.parentElement.innerHTML = "<div class='row'>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'><img src='/Areas/DroolTool/Content/chevvy.png' class='img-circle' /></div>" +
                        "<div class='col-sm-8'>Where does my irrigation runoff go? (Start animation)</div>" +
                    "</div>" +
                "</div>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'><img src='/Areas/DroolTool/Content/piggy.png' class='img-circle' /></div>" +
                        "<div class='col-sm-8'>View rebates and find out about water efficiency</div>" +
                    "</div>" +
                "</div>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'><img src='/Areas/DroolTool/Content/moneywater.png' class='img-circle' /></div>" +
                        "<div class='col-sm-8'>Access my Water Bill (via Moulton Niguel Water District)</div>" +
                    "</div>" +
                "</div>" +
            "</div>";

        window.stopClickPropagation(this.parentElement);
        return this.parentElement;
    }

});

L.control.nominatimSearchControl = function (options) { return new L.Control.NominatimSearchControl(options); };
L.control.neighborhoodDetailControl = function (options) { return new L.Control.NeighborhoodDetailControl(options); };
L.control.explorerTrayControl = function (options) { return new L.Control.ExplorerTrayControl(options); };

NeptuneMaps.DroolToolMap = function (mapInitJson, initialBaseLayerShown, geoServerUrl, config) {
    this.config = config;

    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, { collapseLayerControl: true });
    
    this.neighborhoodLayerWfsParams = this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments");
    this.backboneLayerWmsParams = this.createWmsParamsWithLayerName("OCStormwater:Backbone");
    this.configureRemoteService();
    this.initializeOverlays();
    this.initializeControls();
    this.initializeMask(mapInitJson.WatershedCoverage);
    
    var self = this;
    this.map.on("click",
        function (evt) {
            if (window.freeze) {
                return;
            }

            var customParams = {
                cql_filter: 'intersects(CatchmentGeometry, POINT(' + evt.latlng.lat + ' ' + evt.latlng.lng + '))'
            };

            L.Util.extend(customParams, self.neighborhoodLayerWfsParams);

            RemoteService.geoserverLookup(self.config.GeoServerUrl, customParams).then(function (geoJsonResponse) {
                if (geoJsonResponse.totalFeatures === 0) {
                    return null;
                }
                self.SelectNeighborhood(geoJsonResponse);
            });

            self.SetClickMarker(evt.latlng.lat, evt.latlng.lng);

        });
};

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DroolToolMap.prototype.DisplayStormshedAndBackboneDetail = function (selectedNeighborhoodID) {
    var self = this;

    return RemoteService.getStormshed(selectedNeighborhoodID).then(function (response) {
        var geoJsonResponse = JSON.parse(response);
        
        if (geoJsonResponse.totalFeatures === 0) {
            return null;
        }

        if (self.stormshedLayer) {
            self.map.removeLayer(self.stormshedLayer);
        }

        self.stormshedLayer = L.geoJson(geoJsonResponse,
            {
                style: function(feature) {
                    return {
                        fillColor: "#ffbd38",
                        fill: true,
                        fillOpacity: 0.4,
                        color: "#ffaf0f",
                        weight: 5,
                        stroke: true
                    };
                }
            });
        self.stormshedLayer.addTo(self.map);
        self.stormshedLayer.bringToBack();

        if (self.backboneDetailLayer) {
            self.map.removeLayer(self.backboneDetailLayer);
        }

        var ids = _.map(geoJsonResponse.features,
            function(f) {
                return f.properties.NetworkCatchmentID
            });

        var cql_filter = "NetworkCatchmentID in (" + ids.join(",") + ")";

        var wmsParams = { styles: "backbone_narrow", wmsParameterThatDoesNotExist: Date.now(), pane: "droolToolOverlayPane", cql_filter: cql_filter };

        L.Util.extend(wmsParams, self.backboneLayerWmsParams);

        self.backboneDetailLayer = L.tileLayer.wms(self.config.GeoServerUrl, wmsParams).addTo(self.map);

    });
};

NeptuneMaps.DroolToolMap.prototype.zoomToStormshed = function() {
    if (!this.stormshedLayer) {
        return;
    }

    this.map.fitBounds(Object.values(this.lastSelected._layers)[0].getBounds());
};

NeptuneMaps.DroolToolMap.prototype.setSelectedNeighborhood = function (feature) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.lastSelected)) {
        this.map.removeLayer(this.lastSelected);
    }

    this.lastSelected = L.geoJson(feature,
        {
            style: function (feature) {
                return {
                    fillColor: "#ffad0a",
                    fill: true,
                    fillOpacity: 0.4,
                    color: "#ffff00",
                    weight: 5,
                    stroke: true
                };
            }
        });

    this.lastSelected.addTo(this.map);
    this.lastSelected.bringToFront();
};

NeptuneMaps.DroolToolMap.prototype.SetClickMarker = function(lat, lon) {
    if (this.clickMarker) {
        this.map.removeLayer(this.clickMarker);
    }

    var icon = L.MakiMarkers.icon({
        icon: "marker",
        color: "#FFFF00",
        size: "m"
    });

    this.clickMarker = L.marker({ lat: lat, lon: lon }, {icon:icon});
    this.clickMarker.addTo(this.map);
}

NeptuneMaps.DroolToolMap.prototype.SelectNeighborhood = function (geoJson) {
    this.selectedNeighborhoodID = geoJson.features[0].properties.NetworkCatchmentID;
    
    this.setSelectedNeighborhood(geoJson);
    this.neighborhoodDetailControl.selectNeighborhood(geoJson.features[0].properties);

    if (this.traceLayer) {
        this.traceLayer.remove();
        this.traceLayer = null;
    }

    return this.DisplayStormshedAndBackboneDetail(this.selectedNeighborhoodID);
};

NeptuneMaps.DroolToolMap.prototype.highlightFlow = function () {
    var self = this;
    RemoteService.getTrace(this.selectedNeighborhoodID, this.config.BackboneTraceUrlTemplate).then(function (response) {
        var backboneFeatureCollection = JSON.parse(response);
        if (self.traceLayer) {
            self.traceLayer.remove();
            self.traceLayer = null;
        }

        self.traceLayer = L.geoJSON(backboneFeatureCollection,
            {
                style: function (feature) {
                    return {
                        color: "#FFFF00",
                        weight: 8,
                        stroke: true
                    };
                }
            });
        self.traceLayer.addTo(self.map);
    });
};

var RemoteService = {
    options: {},

    geoserverLookup: function (geoServerUrl, params) {
        
        return jQuery.ajax({
            url: geoServerUrl + L.Util.getParamString(params),
            method: 'GET'
        });
    },

    getTrace: function(neighborhoodID, urlTemplate) {
        var backboneUrl = new Sitka.UrlTemplate(urlTemplate).ParameterReplace(neighborhoodID);

        return jQuery.ajax({
            url: backboneUrl
        });
    },

    getStormshed: function(neighborhoodID) {
        var stormshedUrl = new Sitka.UrlTemplate(this.options.stormshedUrlTemplate).ParameterReplace(neighborhoodID);

        return jQuery.ajax({
            url: stormshedUrl
        });
    },

    makeNominatimRequestUrl: function (q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base +
            this.options.nominatimApiKey +
            "&format=json&q=" +
            q + "&viewbox=-117.82019474260474,33.440338462792681,-117.61081200648763,33.670204787351004" + "&bounded=1";
    },

    nominatimLookup: function(q) {
        var self = this;
        var neptuneMap = self.options.neptuneMap;

        return jQuery.ajax({
            url: RemoteService.makeNominatimRequestUrl(q),
            jsonp: false,
            method: 'GET'
        }).then(function(response) {

            if (response.length === 0) {
                return null;
            }

            // NP/JHB June 2019 deliberate decision not to invest in deciding between multiple results
            var lat = response[0].lat;
            var lon = response[0].lon;
            neptuneMap.SetClickMarker(lat, lon);

            var customParams = {
                cql_filter: 'intersects(CatchmentGeometry, POINT(' + lat + ' ' + lon + '))'
            };

            L.Util.extend(customParams, self.options.neighborhoodLayerWfsParams);

            return RemoteService.geoserverLookup(self.options.geoserverUrl, customParams);
        }).then(function(responseGeoJson) {
            if (!responseGeoJson || responseGeoJson.totalFeatures === 0) {
                toast(NEIGHBORHOOD_NOT_FOUND);
                return;
            }
            neptuneMap.SelectNeighborhood(responseGeoJson).then(function() {
                neptuneMap.zoomToStormshed();
            });
        }).fail(function() {
            toast(NOMINATIM_ERROR);
        });
    }

};

/* Initializers--relatively boring and static*/

NeptuneMaps.DroolToolMap.prototype.configureRemoteService = function () {

    RemoteService.options.nominatimApiKey = this.config.NominatimApiKey;
    RemoteService.options.neighborhoodLayerWfsParams = this.neighborhoodLayerWfsParams;
    RemoteService.options.neptuneMap = this;
    RemoteService.options.geoserverUrl = this.config.GeoServerUrl;
    RemoteService.options.stormshedUrlTemplate = this.config.StormshedUrlTemplate;
};

NeptuneMaps.DroolToolMap.prototype.initializeOverlays = function () {
    var droolToolOverlayPane = this.map.createPane("droolToolOverlayPane");
    droolToolOverlayPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    this.neighborhoodLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "<span><img src='/Content/img/legendImages/networkCatchment.png' height='12px' style='margin-bottom:3px;' /> Neighborhoods</span>",
            { pane: "droolToolOverlayPane", styles: "neighborhood" },
            true);

    this.backboneLayer =
        this.addWmsLayer("OCStormwater:Backbone",
            "<span><img src='/Content/img/legendImages/backbone.png' height='12px' style='margin-bottom:3px;' /> Streams</span>",
            { pane: "droolToolOverlayPane", styles: "backbone_wide" },
            false);

    this.watershedLayer =
        this.addWmsLayer("OCStormwater:Watersheds",
            "<span><img src='/Content/img/legendImages/backbone.png' height='12px' style='margin-bottom:3px;' /> Watersheds</span>",
            { pane: "droolToolOverlayPane", styles: "watershed" },
            false);

    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>",
        false);
};

NeptuneMaps.DroolToolMap.prototype.intializeTray = function () {
    this.explorerTrayControl = L.control.explorerTrayControl({
        position: "bottomright",
        neptuneMap: this
    });

    this.explorerTrayControl.addTo(this.map);
}



NeptuneMaps.DroolToolMap.prototype.initializeControls = function () {
    var config = this.config;
    this.nominatimSearchControl = L.control.nominatimSearchControl({
        position: "topleft",
        nominatimApiKey: config.NominatimApiKey,
        geoserverUrl: config.GeoServerUrl,
        wfsParams: this.neighborhoodLayerWfsParams,
        neptuneMap: this
    });

    window.nominatimSearchControl = this.nominatimSearchControl;
    this.nominatimSearchControl.addTo(this.map);

    this.neighborhoodDetailControl = L.control.neighborhoodDetailControl({
        position: "topleft",
        neptuneMap: this
    });

    this.neighborhoodDetailControl.addTo(this.map);

    this.showLocationControl = L.control.showLocationControl({
        position: "topleft"
    });
    this.showLocationControl.addTo(this.map);
};

NeptuneMaps.DroolToolMap.prototype.initializeMask = function(watershedCoverageLayer) {
    L.geoJson(watershedCoverageLayer.GeoJsonFeatureCollection,
        {
            invert: true,
            style: function (feature) {
                return {
                    fillColor: "#323232",
                    fill: true,
                    fillOpacity: 0.4,
                    color: "#3388ff",
                    weight: 5,
                    stroke: true
                };
            }
        }).addTo(this.map);
};

/* Utility functions */

function toast(toastText) {
    jQuery.toast({
        top: window.totalHeaderHeight + 8,
        text: toastText,
        hideAfter: 20000,
        stack: 1,
        icon: 'error',
        bgColor: "#707070",
        loaderBg: "#77cfdc"
    });
}

window.stopClickPropagation = function (parentElement) {
    L.DomEvent.on(parentElement, "mouseover", function (e) {
        // todo: still not the best way to handle this event-pausing stuff
        window.freeze = true;
    });

    L.DomEvent.on(parentElement,
        "mouseout",
        function (e) {
            window.freeze = false;
        });
};
