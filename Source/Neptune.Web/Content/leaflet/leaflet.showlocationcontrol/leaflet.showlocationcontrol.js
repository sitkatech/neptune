L.Control.ShowLocationControl = L.Control.extend({
    onAdd: function (map) {
        var button = L.DomUtil.create("button", "btn btn-neptune btn-sm");
        button.innerHTML = "<span class='glyphicon glyphicon-globe'></span> Show My Location";



        L.DomEvent.on(button,
            "click",
            function () {
                map.locate({ setView: true, enableHighAccuracy: true });
            });

        return button;
    }
});

L.control.showLocationControl = function (opts) {
    return new L.Control.ShowLocationControl(opts)
};