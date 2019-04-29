L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function (map) {

        //debugger;
        var jurisdictionDropdownJQuery = jQuery("select[name='jurisdictionDropdown']");
        this.getTrackedElement("jurisdictionDropdownContainer").append(jurisdictionDropdownJQuery.get(0));

        jurisdictionDropdownJQuery.change(function() {
            // todo: zoom the map to the selected jurisdiction

            // todo: make an ajax call for the calculations to display
        });
    }
});


L.control.areaBasedCalculationControl = function (opts) {
    return new L.Control.AreaBasedCalculationControl(opts);
};