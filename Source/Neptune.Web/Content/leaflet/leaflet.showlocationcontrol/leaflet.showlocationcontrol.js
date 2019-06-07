L.Control.ShowLocationControl = L.Control.extend({
    onAdd: function (map) {
        var button = L.DomUtil.create("button", "btn btn-neptune btn-sm");
        button.innerHTML = "<span class='glyphicon glyphicon-globe'></span> Show My Location";

        var locateOptions = {
            setView: true,
            enableHighAccuracy: true
        };

        L.Util.extend(locateOptions, this.options);

        L.DomEvent.on(button,
            "click",
            function () {
                map.locate(
                    locateOptions);
            });

        if (window.stopClickPropagation) {


            window.stopClickPropagation(button);
        }

        return button;
    }
});

// options may include any valid options for L.Map.locate. Default options are setView and enableHighAccuracy
L.control.showLocationControl = function (options) {
    return new L.Control.ShowLocationControl(options)
};