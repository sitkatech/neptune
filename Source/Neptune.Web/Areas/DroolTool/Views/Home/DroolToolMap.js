function initResizeHandler(mapInitJson) {
    $(window).on("load",
        function () {
            jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
                jQuery("header").height() -
                jQuery(".neptuneNavbar").height());
        });

    $(window).on("resize",
        function () {
            jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
                jQuery("header").height() -
                jQuery(".neptuneNavbar").height());
        });
}

var NEIGHBORHOOD_NOT_FOUND =
    "This neighborhood is not within the Urban Drool Tool reporting area. If you wish to be included in the Urban Drool Tool, please contact your local water district.";
var NOMINATIM_ERROR =
    "There was an error retrieving the address. The address location service may be unavailable. If the issue persists, please contact support.";

L.Control.NominatimSearchControl = L.Control.extend({
    onAdd: function (map) {
        var input = jQuery("#nominatimSearchInput").get(0);
        var searchButton = jQuery("#nominatimSearchButton").get(0);

        this.neptuneMap = this.options.neptuneMap;
        var parentElement = L.DomUtil.create("div",
            "neighborhood-control");
        parentElement.append(jQuery("#nominatimSearchWrapper").get(0));
        jQuery("#nominatimSearchWrapper").css("display", "block");
        
        var self = this;

        function search() {
            var q = jQuery(".nominatimInput").val();

            jQuery.ajax({
                url: self.makeNominatimRequestUrl(q),
                jsonp: false,
                method: 'GET'
            }).then(function (response) {
                if (response.length === 0) {
                    self.toast(NEIGHBORHOOD_NOT_FOUND);
                }
                // todo: what do if get more one?
                var lat = response[0].lat;
                var lon = response[0].lon;

                var customParams = {
                    cql_filter: 'intersects(CatchmentGeometry, POINT(' + lat + ' ' + lon + '))'
                };

                L.Util.extend(customParams, self.options.wfsParams);

                return jQuery.ajax({
                    url: self.options.geoserverUrl + L.Util.getParamString(customParams),
                    method: 'GET'
                });
            }).then(function (responseGeoJson) {
                if (responseGeoJson.totalFeatures === 0) {
                    self.toast(NEIGHBORHOOD_NOT_FOUND);
                }
                self.neptuneMap.SelectNeighborhood(responseGeoJson);
                self.SelectNeighborhood(responseGeoJson);
                }).fail(function() {
                self.toast(NOMINATIM_ERROR);
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
        
        return parentElement;
    },

    makeNominatimRequestUrl: function (q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base + this.options.nominatimApiKey + "&format=json&q=" + q + "&viewbox=-117.82019474260474,33.440338462792681,-117.61081200648763,33.670204787351004" + "&bounded=1";
    },

    SelectNeighborhood: function (geoJson) {
        console.log(geoJson);
        this.selectedNeighborhoodText.innerHTML =
            "Selected Neighborhood: " + geoJson.features[0].properties.NetworkCatchmentID;
        this.selectedNeighborhoodText.style.display = "initial";
    },

    toast: function (toastText) {
        jQuery.toast({
            position: "top-center",
            text: toastText,
            hideAfter: 10000,
            stack: 1
        });
    }
});

L.control.neighborhoodSearchControl = function (options) { return new L.Control.NominatimSearchControl(options); };


NeptuneMaps.DroolToolMap = function(mapInitJson, initialBaseLayerShown, geoServerUrl, config) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, { collapseLayerControl: true});

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

    this.neighborhoodSearchControl = L.control.neighborhoodSearchControl({
        position: "topleft",
        nominatimApiKey: config.NominatimApiKey,
        geoserverUrl: config.GeoServerUrl,
        wfsParams: this.neighborhoodLayerParams,
        neptuneMap: this
    });
    window.neighborhoodSearchControl = this.neighborhoodSearchControl;
    this.neighborhoodSearchControl.addTo(this.map);
};

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DroolToolMap.prototype.SelectNeighborhood = function(geoJson) {
    this.setSelectedFeature(geoJson);
    this.map.fitBounds(this.lastSelected.getBounds());
}