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

L.Control.NeighborhoodControl = L.Control.extend({
    onAdd: function (map) {
        this.neptuneMap = this.options.neptuneMap;
        var parentElement = L.DomUtil.create("div",
            "leaflet-bar leaflet-control neptune-leaflet-control neighborhood-control");

        var instructionText = L.DomUtil.create("p");

        instructionText.innerHTML = "Select a neighborhood or search by address to see neighborhood details (coming soon)";
        parentElement.append(instructionText);

        this.selectedNeighborhoodText = L.DomUtil.create("p", "selectedNeighborhoodText");

        this.selectedNeighborhoodText.style.display = "none";
        parentElement.append(this.selectedNeighborhoodText);

        var input = L.DomUtil.create("input", "form-control nominatimInput");
        parentElement.append(input);

        parentElement.append(L.DomUtil.create("br"));
        parentElement.append(L.DomUtil.create("br"));

        var searchButton = L.DomUtil.create("button", "btn-sm btn btn-neptune");
        searchButton.innerHTML = "Search";

        console.log(this.options);
        var self = this;

        function search() {
            var q = jQuery(".nominatimInput").val();

            jQuery.ajax({
                url: self.makeNominatimRequestUrl(q),
                jsonp: false,
                method: 'GET'
            }).then(function (response) {
                if (response.length === 0) {
                    window.alert("There was an error retrieving the address. Please check that you typed it correctly and try again. If the issue persists, please contact support.")
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
                    window.alert("This neighborhood is not within the Urban Drool Tool reporting area. If you wish to be included in the Urban Drool Tool, please contact your local water district.")
                }
                self.neptuneMap.SelectNeighborhood(responseGeoJson);
                self.SelectNeighborhood(responseGeoJson);
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


        parentElement.append(searchButton);

        return parentElement;
    },

    makeNominatimRequestUrl: function (q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base + this.options.nominatimApiKey + "&format=json&q=" + q;
    },

    SelectNeighborhood: function (geoJson) {
        console.log(geoJson);
        this.selectedNeighborhoodText.innerHTML =
            "Selected Neighborhood: " + geoJson.features[0].properties.NetworkCatchmentID;
        this.selectedNeighborhoodText.style.display = "initial";
    }
});

L.control.neighborhoodControl = function (options) { return new L.Control.NeighborhoodControl(options); };


NeptuneMaps.DroolToolMap = function(mapInitJson, initialBaseLayerShown, geoServerUrl, config) {
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, {});

    var neighborhoodPane = this.map.createPane("neighborhoodPane");
    neighborhoodPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    var hide = false;
    this.neighborhoodLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "<span><img src='/Content/img/legendImages/networkCatchment.png' height='12px' style='margin-bottom:3px;' /> Neighborhoods</span>",
            { pane: "neighborhoodPane", styles: "neighborhood" },
            hide);


    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>",
        hide);

    this.neighborhoodLayerParams = this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments");

    this.neighborhoodControl = L.control.neighborhoodControl({
        position: "topleft",
        nominatimApiKey: config.NominatimApiKey,
        geoserverUrl: config.GeoServerUrl,
        wfsParams: this.neighborhoodLayerParams,
        neptuneMap: this
    });
    this.neighborhoodControl.addTo(this.map);
};

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DroolToolMap.prototype.SelectNeighborhood = function(geoJson) {
    this.setSelectedFeature(geoJson);
    this.map.fitBounds(this.lastSelected.getBounds());
}