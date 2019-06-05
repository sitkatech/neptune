var resizeHandler = function(mapInitJson) {
    var totalHeaderHeight = (jQuery("header").height() +
        jQuery(".neptuneNavbar").height());
    jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
        totalHeaderHeight);

    window.totalHeaderHeight = totalHeaderHeight;
};

function initResizeHandler(mapInitJson) {
    $(window).on("load",
        function() { resizeHandler(mapInitJson); }
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
        this.parentElement = L.DomUtil.create("div", "leaflet-bar leaflet-control neptune-leaflet-control");

        var h4 = L.DomUtil.create("h4");
        h4.innerHTML = "Selected Neighborhood";
        this.parentElement.append(h4);

        this.selectedNeighborhoodText = L.DomUtil.create("p");

        this.parentElement.append(this.selectedNeighborhoodText);

        this.hide();

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
        var input = jQuery("#nominatimSearchInput").get(0);
        var searchButton = jQuery("#nominatimSearchButton").get(0);

        this.neptuneMap = this.options.neptuneMap;
        var parentElement = L.DomUtil.create("div",
            "neighborhood-search-control");
        parentElement.append(jQuery("#nominatimSearchWrapper").get(0));
        jQuery("#nominatimSearchWrapper").css("display", "block");

        var self = this;

        function search() {
            var q = jQuery("#nominatimSearchInput").val();

            jQuery.ajax({
                url: self.makeNominatimRequestUrl(q),
                jsonp: false,
                method: 'GET'
            }).then(function (response) {
                if (response.length === 0) {
                    return null;
                }

                // NP/JHB June 2019 deliberate decision not to invest in deciding between multiple results
                var lat = response[0].lat;
                var lon = response[0].lon;

                var customParams = {
                    cql_filter: 'intersects(CatchmentGeometry, POINT(' + lat + ' ' + lon + '))'
                };

                L.Util.extend(customParams, self.options.wfsParams);

                return searchGeoserver(self.options.geoserverUrl, customParams);
            }).then(function (responseGeoJson) {
                if (!responseGeoJson || responseGeoJson.totalFeatures === 0) {
                    toast(NEIGHBORHOOD_NOT_FOUND);
                    return;
                }
                self.neptuneMap.SelectNeighborhood(responseGeoJson);
            }).fail(function () {
                toast(NOMINATIM_ERROR);
            });
        }

        L.DomEvent.on(searchButton, "click", function () {
            search();
        });

        L.DomEvent.on(input, "keyup", function (event) {
            if (event.keyCode === 13) {
                search();
            }
        });

        this.parentElement = parentElement;

        window.stopClickPropagation(this.parentElement);

        return parentElement;
    },

    makeNominatimRequestUrl: function (q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base +
            this.options.nominatimApiKey +
            "&format=json&q=" +
            q + "&viewbox=-117.82019474260474,33.440338462792681,-117.61081200648763,33.670204787351004" + "&bounded=1";
    },
});

L.control.nominatimSearchControl = function (options) { return new L.Control.NominatimSearchControl(options); };
L.control.neighborhoodDetailControl = function (options) { return new L.Control.NeighborhoodDetailControl(options); };


NeptuneMaps.DroolToolMap = function (mapInitJson, initialBaseLayerShown, geoServerUrl, config) {
    this.config = config;

    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, { collapseLayerControl: true });

    var neighborhoodPane = this.map.createPane("neighborhoodPane");
    neighborhoodPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    this.neighborhoodLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "<span><img src='/Content/img/legendImages/networkCatchment.png' height='12px' style='margin-bottom:3px;' /> Neighborhoods</span>",
            { pane: "neighborhoodPane", styles: "neighborhood" },
            true);


    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>",
        false);

    this.neighborhoodLayerParams = this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments");

    this.nominatimSearchControl = L.control.nominatimSearchControl({
        position: "topleft",
        nominatimApiKey: config.NominatimApiKey,
        geoserverUrl: config.GeoServerUrl,
        wfsParams: this.neighborhoodLayerParams,
        neptuneMap: this
    });

    


    window.nominatimSearchControl = this.nominatimSearchControl;
    this.nominatimSearchControl.addTo(this.map);



    this.neighborhoodDetailControl = L.control.neighborhoodDetailControl({ position: "topleft" });


    this.neighborhoodDetailControl.addTo(this.map);
    var self =this;
    this.map.on("click",
        function(evt) {
            if (window.freeze) {
                return;
            }

            var customParams = {
                cql_filter: 'intersects(CatchmentGeometry, POINT(' + evt.latlng.lat + ' ' + evt.latlng.lng + '))'
            };

            L.Util.extend(customParams, self.neighborhoodLayerParams);

            searchGeoserver(config.GeoServerUrl, customParams).then(function(geoJsonResponse) {
                if (geoJsonResponse.totalFeatures === 0) {
                    return null;
                }
                self.SelectNeighborhood(geoJsonResponse);
            });

        });
};

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DroolToolMap.prototype.SelectNeighborhood = function (geoJson) {
    this.setSelectedFeature(geoJson);
    this.neighborhoodDetailControl.selectNeighborhood(geoJson.features[0].properties);
    this.map.fitBounds(this.lastSelected.getBounds());
}

function searchGeoserver(geoServerUrl, params) {
    return jQuery.ajax({
        url: geoServerUrl + L.Util.getParamString(params),
        method: 'GET'
    });
}

function getBackbone(neighborhoodID, urlTemplate) {
    var backboneUrl = new Sitka.UrlTemplate(urlTemplate).ParameterReplace(neighborhoodID);


    jQuery.ajax({
        url: backboneUrl
    }).then(function(response) {
        console.log(response);
    });
}

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