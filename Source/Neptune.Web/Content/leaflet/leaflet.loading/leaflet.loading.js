L.LeafletLoading = L.Layer.extend({
    includes: L.Evented ? L.Evented.prototype : L.Mixin.Events,

    options: {
        bounds: null
    },

    initialize: function (options) {
        L.setOptions(this, options);
    },

    onAdd: function (map) {
        this._map = map;
        this._addEventListeners();

        this._shadesContainer = L.DomUtil.create('div', 'frosted-overlay leaflet-zoom-hide');

        map.getPanes().overlayPane.appendChild(this._shadesContainer);
        if (this.options.bounds) this._updateShades(this.options.bounds);

        var size = this._map.getSize();
        var offset = this._getOffset();

        this.setDimensions(this._shadesContainer,
            {
                width: size.x,
                height: size.y,
                top: offset.y,
                left: offset.x
            });
    },
    _addEventListeners: function () { },



    _getOffset: function () {
        // Getting the transformation value through style attributes
        var transformation = this._map.getPanes().mapPane.style.transform
        var startIndex = transformation.indexOf('(');
        var endIndex = transformation.indexOf(')');
        transformation = transformation.substring(startIndex + 1, endIndex).split(',');
        var offset = {
            x: parseInt(transformation[0], 10) * -1,
            y: parseInt(transformation[1], 10) * -1
        };
        return offset;
    },

    setDimensions: function (element, dimensions) {
        element.style.width = dimensions.width + 'px';
        element.style.height = dimensions.height + 'px';
        element.style.top = dimensions.top + 'px';
        element.style.left = dimensions.left + 'px';
    },

    onRemove: function (map) {
        map.getPanes().overlayPane.removeChild(this._shadesContainer);
    }
});
