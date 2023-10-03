/*
 * L.Control.WMSLegend is used to add a WMS Legend to the map
 */

L.Control.WMSLegend = L.Control.extend({
    onAdd: function () {
        var controlClassName = "leaflet-control-wms-legend leaflet-bar",
            legendClassName = "wms-legend",
            stop = L.DomEvent.stopPropagation;
        this.container = L.DomUtil.create("div", controlClassName);
        if (this.options.title) {
            this.title = L.DomUtil.create("label", "leaflet-wms-legend-title", this.container);
            this.title.innerHTML = this.options.title;
        }
        this.br = L.DomUtil.create("br", "", this.container);
        this.img = L.DomUtil.create("img", legendClassName, this.container);
        this.toggle = L.DomUtil.create("span", "glyphicon glyphicon-menu-hamburger", this.container);
        this.toggle.style.display = "none";
        this.toggle.style.textalign = "center";
        this.img.src = this.options.uri;
        this.img.alt = "Legend";

        L.DomEvent
            .on(this.img, "click", this._click, this)
            .on(this.container, "click", this._click, this)
            .on(this.img, "mousedown", stop)
            .on(this.img, "dblclick", stop)
            .on(this.img, "click", L.DomEvent.preventDefault)
            .on(this.img, "click", stop);
        this.height = null;
        this.width = null;
        return this.container;
    },
    _click: function (e) {
        L.DomEvent.stopPropagation(e);
        L.DomEvent.preventDefault(e);
        // toggle legend visibility
        var style = window.getComputedStyle(this.img);
        if (style.display === "none") {
            this.container.style.height = this.height + "px";
            this.container.style.width = this.width + "px";
            this.img.style.display = this.displayStyle;
            this.toggle.style.display = "none";
            if (this.options.title) {
                this.title.style.display = this.displayStyle;
            }
            this.br.style.display = this.displayStyle;
        }
        else {
            if (this.width === null && this.height === null) {
                // Only do inside the above check to prevent the container
                // growing on successive uses
                this.height = this.container.offsetHeight;
                this.width = this.container.offsetWidth;
            }
            this.displayStyle = this.img.style.display;
            this.img.style.display = "none";
            this.toggle.style.display = this.displayStyle;
            if (this.options.title) {
                this.title.style.display = "none";
            }
            this.br.style.display = "none";
            this.container.style.height = "20px";
            this.container.style.width = "20px";
        }
    },
});

L.wmsLegend = function (title, serviceUrl, legendOptions, map) {
    var baseLegendOptions = {
        service: "WMS",
        request: "GetLegendGraphic",
        version: "1.0.0"
    };

    var fullLegendOptions = L.Util.extend(baseLegendOptions, legendOptions);

    var queryString = L.Util.getParamString(fullLegendOptions);
    var uri = serviceUrl + queryString;

    var options = {
        position: "topright",
        uri: uri,
        title: title
    };

    var wmsLegendControl = new L.Control.WMSLegend(options);
    map.addControl(wmsLegendControl);
    return wmsLegendControl;
};
