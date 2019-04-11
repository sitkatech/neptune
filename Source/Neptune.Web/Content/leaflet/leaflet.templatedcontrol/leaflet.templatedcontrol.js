L.Control.TemplatedControl = L.Control.extend({
    templateID: null, // must set this value when extending; else onAdd will not work.

    initializeControlInstance: function () {
        // override this method to perform additional initialization during onAdd
    },

    getTrackedElement: function (id) {
        // todo: might not be a bad idea to memoize
        return this.parentElement.querySelector("#" + id);
    },

    onAdd: function (map) {
        var template = document.querySelector("#" + this.templateID);
        this.parentElement = document.importNode(template.content, true).firstElementChild;

        L.DomEvent.on(this.parentElement,
            'mouseover',
            function() {
                map.dragging.disable();
            });

        L.DomEvent.on(this.parentElement,
            'mouseout',
            function() {
                map.dragging.enable();
            });

        this.initializeControlInstance(map);
        return this.parentElement;
    },

    onRemove: function (map) {
        jQuery(this.parentElement).remove();
    }
});
