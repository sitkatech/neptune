L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function(map) {

    }
});


L.control.areaBasedCalculationControl = function (opts) {
    return new L.Control.AreaBasedCalculationControl(opts);
};