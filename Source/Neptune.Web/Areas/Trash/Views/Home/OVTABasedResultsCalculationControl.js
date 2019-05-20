L.Control.OVTABasedResultsControl = L.Control.TemplatedControl.extend({
    templateID: "OVTABasedResultsControlTemplate",

    initializeControlInstance: function(map) {
        window.stopClickPropagation(this.parentElement);
        this.map = map;

        if (this.options.showDropdown) {

            // must register the event handler before moving the element
            this.registerHandlerOnDropdown(map);
            this.getTrackedElement("jurisdictionResultsDropdownContainer")
                .append(jQuery("select[name='jurisdictionResultsDropdown']").get(0));
        }
        this.OVTABasedResultsUrlTemplate = this.options.OVTABasedResultsUrlTemplate;
    },

    selectJurisdiction: function(stormwaterJursidictionID) {
        jQuery("select[name='jurisdictionResultsDropdown']").val(stormwaterJursidictionID);
        var resultsCalculationsUrl = new Sitka.UrlTemplate(this.OVTABasedResultsUrlTemplate).ParameterReplace(stormwaterJursidictionID);
        populateTGUValuesOVTABased(resultsCalculationsUrl);
    },

    zoomToJurisdictionOnLoad: function (jurisdictionFeatures, callback) {
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
        populateTGUValuesOVTABased(resultsCalculationsUrl);
    },

    registerHandlerOnDropdown: function (map) {
        var self = this;
        jQuery("select[name='jurisdictionResultsDropdown']").change(function () {
            var resultsCalculationsUrl =
                new Sitka.UrlTemplate(self.OVTABasedResultsUrlTemplate).ParameterReplace(this.value);
            populateTGUValuesOVTABased(resultsCalculationsUrl);
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

function populateTGUValuesOVTABased(resultsCalculationsUrl) {
    jQuery.ajax({
        url: resultsCalculationsUrl,
        method: "GET"
    }).then(function (response) {
        jQuery("#PLUIsA").text(Math.round(response.PLUSumAcresWhereOVTAIsA));
        jQuery("#PLUIsB").text(Math.round(response.PLUSumAcresWhereOVTAIsB));
        jQuery("#PLUIsC").text(Math.round(response.PLUSumAcresWhereOVTAIsC));
        jQuery("#PLUIsD").text(Math.round(response.PLUSumAcresWhereOVTAIsD));
        jQuery("#ALUIsA").text(Math.round(response.ALUSumAcresWhereOVTAIsA));
        jQuery("#ALUIsB").text(Math.round(response.ALUSumAcresWhereOVTAIsB));
        jQuery("#ALUIsC").text(Math.round(response.ALUSumAcresWhereOVTAIsC));
        jQuery("#ALUIsD").text(Math.round(response.ALUSumAcresWhereOVTAIsD));
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