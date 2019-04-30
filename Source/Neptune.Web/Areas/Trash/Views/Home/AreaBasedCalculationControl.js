L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function (map) {
        this.map = map;
        // must register the event handler before moving the element
        this.registerHandlerOnDropdown(map);

        this.getTrackedElement("jurisdictionDropdownContainer").append(jQuery("select[name='jurisdictionDropdown']").get(0));
        this.areaCalculationsUrlTemplate = this.options.areaCalculationsUrlTemplate;
        $(document).ready(function() {

        });
    },

    registerHandlerOnDropdown: function (map) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var areaCalculationsUrl =
                new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(this.value);

            //todo: move this ajax call to its own function so it can be used to select the first jurisdiction when the user loads the page
            populateTGUValues(areaCalculationsUrl);
        });
    },

    registerZoomToJurisdictionHandler: function (jurisdictionFeatures) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
            self.map.fitBounds(bounds);
        });
    }
});

function populateTGUValues(areaCalculationsUrl) {
    jQuery.ajax({
        url: areaCalculationsUrl,
        method: "GET"
    }).then(function (response) {
        console.log(response);

        jQuery("#fullTrashCapture").text(response.FullTrashCaptureAcreage);
        jQuery("#equivalentArea").text(response.EquivalentAreaAcreage);
        jQuery("#totalAcresCaptured").text(response.TotalAcresCaptured);
        jQuery("#totalPLUAcres").text(response.TotalPLUAcres);
    });
}

function zoom(jurisdictionFeatures, selectedJurisdictionID) {
    var geoJson = L
        .geoJson(_.find(jurisdictionFeatures,
            function (f) {
                return f.properties.StormwaterJurisdictionID ===
                    Number(selectedJurisdictionID);
            }));
    return geoJson.getBounds();
};

L.control.areaBasedCalculationControl = function (opts) {
    return new L.Control.AreaBasedCalculationControl(opts);
};