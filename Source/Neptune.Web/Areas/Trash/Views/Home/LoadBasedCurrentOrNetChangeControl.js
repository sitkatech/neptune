L.Control.LoadBasedCurrentOrNetChangeControl = L.Control.extend({
    onAdd: function (map) {
        this.map = map;

        var div = L.DomUtil.create('div', 'neptune-leaflet-control leaflet-bar');
        div.innerHTML = "<form>" +
            "<label>Loading rate<br />(gal/ac/year)</label><br />" +
            "<div class='radio-inline'>" +
            "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='true' checked='checked'>Current</label>" +
            "</div>" + "<br />" + 
            "<div class='radio-inline'>" +
            "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='false'>Net Change </label>" +
            "</div>" +
            "<img style='display:block' id='shownLayerLegendImg'/>" +
            "</form>";
        div.id = "CurrentOrNetChangeLoadingBool";

        window.stopClickPropagation(div);

        return div;
    },

    toggleLoad: function (neptuneMap) {
        toggleCurrentOrNetChangeLoad(neptuneMap);
    },

    setShownLayerLegendImg: function (url) {
        jQuery("#shownLayerLegendImg").attr("src", url);
    }


});

function toggleCurrentOrNetChangeLoad(neptuneMap) {
    var currentOrNetChangeLoadingBool = jQuery("input[name='CurrentOrNetChangeLoadingBool']");

    currentOrNetChangeLoadingBool.change(function () {
        var currentOrNetChangeLoadingBoolChecked = jQuery("input[name='CurrentOrNetChangeLoadingBool']:checked");

        var displayCurrent = currentOrNetChangeLoadingBoolChecked.val();

        if (displayCurrent === "true") {
            neptuneMap.map.removeLayer(neptuneMap.deltaLoadLayer);
            neptuneMap.map.addLayer(neptuneMap.currentLoadLayer);
            jQuery("#shownLayerLegendImg").attr("src", neptuneMap.currentLoadLegendUrl);
        } else {
            neptuneMap.map.removeLayer(neptuneMap.currentLoadLayer);
            neptuneMap.map.addLayer(neptuneMap.deltaLoadLayer);
            jQuery("#shownLayerLegendImg").attr("src", neptuneMap.deltaLoadLegendUrl);

        }
    });
}

function changeLayerLegendImg(url) {

    var span = L.DomUtil.create('span');
    var img = L.DomUtil.create('img');

    img.src = url;
    img.style.width = '200px';
    span.innerHTML = img;

    return span.outerHTML;
}

L.control.loadBasedCurrentOrNetChangeControl = function (opts) {
    return new L.Control.LoadBasedCurrentOrNetChangeControl(opts);
};