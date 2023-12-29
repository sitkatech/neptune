L.Control.LoadBasedResultsControl = L.Control.TemplatedControl.extend({
    templateID: "LoadBasedResultsControlTemplate",

    initializeControlInstance: function(map) {
        window.stopClickPropagation(this.parentElement);
        this.map = map;

        if (this.options.showDropdown) {

            // must register the event handler before moving the element
            this.registerHandlerOnDropdown(map);
            this.getTrackedElement("jurisdictionLoadResultsDropdownContainer")
                .append(jQuery("select[name='jurisdictionLoadResultsDropdown']").get(0));
        }
        this.LoadBasedResultsUrlTemplate = this.options.loadCalculationsUrlTemplate;
    },

    selectJurisdiction: function(stormwaterJursidictionID) {
        jQuery("select[name='jurisdictionLoadResultsDropdown']").val(stormwaterJursidictionID);
        var resultsCalculationsUrl = new Sitka.UrlTemplate(this.LoadBasedResultsUrlTemplate).ParameterReplace(stormwaterJursidictionID);
        populateTGUValuesLoadBased(resultsCalculationsUrl);
    },

    zoomToJurisdictionOnLoad: function (jurisdictionFeatures, callback) {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionLoadResultsDropdown']").val();
        var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
        self.map.fitBounds(bounds);
        callback(selectedJurisdictionID);
    },

    loadAreaBasedCalculationOnLoad: function () {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionLoadResultsDropdown']").val();
        var resultsCalculationsUrl = new Sitka.UrlTemplate(self.LoadBasedResultsUrlTemplate).ParameterReplace(selectedJurisdictionID);
        populateTGUValuesLoadBased(resultsCalculationsUrl);
    },

    registerHandlerOnDropdown: function (map) {
        var self = this;
        jQuery("select[name='jurisdictionLoadResultsDropdown']").change(function () {
            var resultsCalculationsUrl =
                new Sitka.UrlTemplate(self.LoadBasedResultsUrlTemplate).ParameterReplace(this.value);
            populateTGUValuesLoadBased(resultsCalculationsUrl);
        });
    },

    registerZoomToJurisdictionHandler: function (jurisdictionFeatures) {
        var self = this;
        jQuery("select[name='jurisdictionLoadResultsDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            var bounds = zoom(jurisdictionFeatures, selectedJurisdictionID);
            self.map.fitBounds(bounds);
        });
    },

    registerAdditionalHandler: function (callback) {
        var self = this;
        jQuery("select[name='jurisdictionLoadResultsDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            callback(selectedJurisdictionID);
        });
    }
});

function populateTGUValuesLoadBased(resultsCalculationsUrl) {
    jQuery.ajax({
        url: resultsCalculationsUrl,
        method: "GET"
    }).then(function (response) {
        jQuery("#viaFullTrashCapture").text(Math.round(response.LoadFullCapture).toLocaleString());
        jQuery("#viaPartialTrashCapture").text(Math.round(response.LoadPartialCapture).toLocaleString());
        jQuery("#viaOVTAScore").text(Math.round(response.LoadOVTA).toLocaleString());
        jQuery("#totalAchieved").text(Math.round(response.TotalAchieved).toLocaleString());
        jQuery("#targetLoadReduction").text(Math.round(response.TargetLoadReduction).toLocaleString());
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
}


L.control.loadBasedResultsControl = function (opts) {
    return new L.Control.LoadBasedResultsControl(opts);
};