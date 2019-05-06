L.Control.OVTABasedResultsControl = L.Control.TemplatedControl.extend({
    templateID: "OVTABasedResultsControlTemplate",

    initializeControlInstance: function(map) {
        this.map = map;

        if (this.options.showDropdown) {

            // must register the event handler before moving the element
            this.registerHandlerOnDropdown(map);
            this.getTrackedElement("jurisdictionResultsDropdownContainer")
                .append(jQuery("select[name='jurisdictionResultsDropdown']").get(0));
        }
        this.OVTABasedResultsUrlTemplate = this.options.OVTABasedResultsUrlTemplate;
    },

    zoomToJurisdictionOnLoad: function (jurisdictionFeatures, callback) {
        debugger;
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionResultsDropdown']").val();
        var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
        self.map.fitBounds(bounds);
        callback(selectedJurisdictionID);
    },

    loadAreaBasedCalculationOnLoad: function () {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionResultsDropdown']").val();
        var resultsCalculationsUrl = new Sitka.UrlTemplate(self.OVTABasedResultsUrlTemplate).ParameterReplace(selectedJurisdictionID);
        populateTGUValues(resultsCalculationsUrl);
    },

    registerHandlerOnDropdown: function (map) {
        var self = this;
        jQuery("select[name='jurisdictionResultsDropdown']").change(function () {
            var resultsCalculationsUrl =
                new Sitka.UrlTemplate(self.OVTABasedResultsUrlTemplate).ParameterReplace(this.value);
            populateTGUValues(resultsCalculationsUrl);
        });
    },

    registerZoomToJurisdictionHandler: function (jurisdictionFeatures) {
        var self = this;
        jQuery("select[name='jurisdictionResultsDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
            self.map.fitBounds(bounds);
        });
    },

    registerAdditionalHandler: function (callback) {
        var self = this;
        jQuery("select[name='jurisdictionResultsDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            callback(selectedJurisdictionID);
        });
    }
});

function populateTGUValues(areaCalculationsUrl) {
    jQuery.ajax({
        url: areaCalculationsUrl,
        method: "GET"
    }).then(function (response) {
        jQuery("#PLUIsA").text(response.PLUSumAcresWhereOVTAIsA);
        jQuery("#PLUIsB").text(response.PLUSumAcresWhereOVTAIsB);
        jQuery("#PLUIsC").text(response.PLUSumAcresWhereOVTAIsC);
        jQuery("#PLUIsD").text(response.PLUSumAcresWhereOVTAIsD);
        jQuery("#ALUIsA").text(response.ALUSumAcresWhereOVTAIsA);
        jQuery("#ALUIsB").text(response.ALUSumAcresWhereOVTAIsB);
        jQuery("#ALUIsC").text(response.ALUSumAcresWhereOVTAIsC);
        jQuery("#ALUIsD").text(response.ALUSumAcresWhereOVTAIsD);
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


L.control.ovtaBasedResultsControl = function (opts) {
    return new L.Control.OVTABasedResultsControl(opts);
};