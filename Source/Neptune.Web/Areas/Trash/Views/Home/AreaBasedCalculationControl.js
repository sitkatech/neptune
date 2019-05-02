L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function(map) {
        this.map = map;

        if (this.options.showDropdown) {

            // must register the event handler before moving the element
            this.registerHandlerOnDropdown();

            this.getTrackedElement("jurisdictionDropdownContainer")
                .append(jQuery("select[name='jurisdictionDropdown']").get(0));
        }
        this.areaCalculationsUrlTemplate = this.options.areaCalculationsUrlTemplate;
    },

    zoomToJurisdictionOnLoad: function (jurisdictionFeatures) {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionDropdown']").val();
        var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
        self.map.fitBounds(bounds);
    },

    loadAreaBasedCalculationOnLoad: function() {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionDropdown']").val();
        var areaCalculationUrl = new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(selectedJurisdictionID);
        populateTGUValues(areaCalculationUrl);
    },

    registerHandlerOnDropdown: function () {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var areaCalculationsUrl =
                new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(this.value);
            populateTGUValues(areaCalculationsUrl);
        });
    },

    registerZoomToJurisdictionHandler: function (jurisdictionFeatures) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            var bounds = getBounds(jurisdictionFeatures, selectedJurisdictionID);
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

function getBounds(jurisdictionFeatures, selectedJurisdictionID) {
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