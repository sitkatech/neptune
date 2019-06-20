

L.Control.LoadBasedCurrentOrNetChangeControl = L.Control.extend({
    onAdd: function (map) {
        this.map = map;

        var img = changeLayer();

        var div = L.DomUtil.create('div', 'command');
        div.innerHTML = "<form>" +
                            "<div class='radio-inline'>" +
                                "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='true'>Current Loading</label>" +
                            "</div>" +
                            "<div class='radio-inline'>" +
                                "<label><input type='radio' name='CurrentOrNetChangeLoadingBool' value='false'> Net Change in Loading</label>" +
                            "</div>" +
                        "</form>" +
                        img;

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

function changeLayer(displayCurrent) {
    var img = L.DomUtil.create('img');
    var url = "";

    if (displayCurrent) {
        url = "";
    } else {
        url = "";
    }

    jQuery.ajax({
        url: url,
        method: "GET"
    }).then(function (response) {
        img.src = response;
    });

    img.style.width = '200px';

    return img;
}

L.control.loadBasedCurrentOrNetChangeControl = function (opts) {
    return new L.Control.LoadBasedCurrentOrNetChangeControl(opts);
};