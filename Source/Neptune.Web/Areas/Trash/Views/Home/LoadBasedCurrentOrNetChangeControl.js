

L.Control.LoadBasedCurrentOrNetChangeControl = L.Control.extend({
    onAdd: function (map) {
        this.map = map;

        var div = L.DomUtil.create('div', 'command');
        div.innerHTML = "<form>" +
                            "<div class='radio-inline'>" +
                                "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='true'>Current Loading</label>" +
                            "</div>" +
                            "<div class='radio-inline'>" +
                                "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='false'> Net Change in Loading</label>" +
                            "</div>" +
                        "</form>";

        return div;
    },

    onRemove: function (map) {

    }



    //templateID: "LoadBasedCurrentOrNetChangeTemplate",
    //initializeControlInstance: function (map) {
    //    window.stopClickPropagation(this.parentElement);
    //    this.map = map;
    //    // must register the event handler before moving the element
    //    this.registerHandlerOnDropdown(map);
    //    this.getTrackedElement("CurrentOrNetChangeLoadingRadioContainer")
    //        .append(jQuery("#CurrentOrNetChangeLoading").get(0));
    //    this.LoadBasedCurrentOrNetChangeTemplate = this.options.loadBasedCurrentOrNetChangeTemplate;
    //}
});

function toggleCurrentOrNetChangeLoad(event) {
    var loadBasedCurrentOrNetChangeGeoserverUrl = this.loadBasedCurrentOrNetChangeGeoserverUrl;

    if (event.val) {
        changeLayerImg(event.val, loadBasedCurrentOrNetChangeGeoserverUrl);
    } else {
        changeLayerImg(event.val, loadBasedCurrentOrNetChangeGeoserverUrl);
    }
}

function changeLayerImg(displayCurrent) {
    debugger;
    var span = L.DomUtil.create('span');
    var img = L.DomUtil.create('img');

    var url = loadBasedCurrentOrNetChangeGeoserverUrl;
    if (displayCurrent) {
        url += "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnitLoads&style=current_load&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    } else {
        url += "?service=WMS&request=GetLegendGraphic&version=1.0.0&layer=OCStormwater%3ATrashGeneratingUnitLoads&style=delta_load&legend_options=forceLabels%3Aon%3AfontAntiAliasing%3Atrue%3Adpi%3A200&format=image%2Fpng";
    }

    jQuery.ajax({
        url: url,
        method: "GET"
    }).then(function (response) {

     });

    img.src = url;
    img.style.width = '200px';
    span.innerHTML = img;

    return span;
}

//function addWMSLayer(map) {

//    loadLengedLabel = 
//    map.addWmsLayer("OCStormwater:TrashGeneratingUnitLoads", currentLoadLegendlabel, { styles: "current_load", });

//    return map;
//}

L.control.loadBasedCurrentOrNetChangeControl = function (opts) {
    return new L.Control.LoadBasedCurrentOrNetChangeControl(opts);
};