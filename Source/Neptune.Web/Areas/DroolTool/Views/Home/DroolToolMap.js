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
        var el = L.DomUtil.create("div",
            "leaflet-bar leaflet-control neptune-leaflet-control neighborhood-control");

        var h = L.DomUtil.create("p");

        h.innerHTML = "Select a neighborhood or search by address to see neighborhood details (coming soon)";
        el.append(h);

        el.append(L.DomUtil.create("input", "form-control nominatimInput"));

        var button = L.DomUtil.create("button", "btn-sm btn btn-neptune");
        button.innerHTML = "Test Nominatim";

        console.log(this.options);
        var self = this;
        L.DomEvent.on(button, "click", function () {
            var q = jQuery(".nominatimInput").val();

            jQuery.ajax({
                url: self.makeNominatimRequestUrl(q),
                jsonp: false,
                method: 'GET'
            }).then(function(response) {
                console.log(response);
                window.alert("Check console");
                });
            });

        el.append(button);

        return el;
    },

    makeNominatimRequestUrl: function(q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base + "mSMCuGwTHIiqgLeT7ODwVM1udF9RaL2H" + "&format=json&q=" + q;
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
            { pane: "neighborhoodPane", styles: "neighborhood" }, hide);


    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>", hide);

    this.neighborhoodControl = L.control.neighborhoodControl({
        position: "topleft",
        nominationApiKey: config.NominatimApiKey
       });
    this.neighborhoodControl.addTo(this.map);
}

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);