L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function (map) {
        // must register the event handler before moving the element
        this.registerHandlerOnDropdown(map);

        this.getTrackedElement("jurisdictionDropdownContainer").append(jQuery("select[name='jurisdictionDropdown']").get(0));
        this.areaCalculationsUrlTemplate = this.options.areaCalculationsUrlTemplate;
    },

    registerHandlerOnDropdown: function (map) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var areaCalculationsUrl =
                new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(this.value);
            jQuery.ajax({
                url: areaCalculationsUrl, 
                method: "GET"
            }).then(function(response) {
                console.log(response);

                // todo: display results of calculation

            });
        });
    }
});


L.control.areaBasedCalculationControl = function (opts) {
    return new L.Control.AreaBasedCalculationControl(opts);
};