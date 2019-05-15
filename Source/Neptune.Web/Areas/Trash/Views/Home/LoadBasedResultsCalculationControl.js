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
        debugger;
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
        jQuery("#PLUIsA").text(response.PLUSumAcresWhereLoadIsA);
        jQuery("#PLUIsB").text(response.PLUSumAcresWhereLoadIsB);
        jQuery("#PLUIsC").text(response.PLUSumAcresWhereLoadIsC);
        jQuery("#PLUIsD").text(response.PLUSumAcresWhereLoadIsD);
        jQuery("#ALUIsA").text(response.ALUSumAcresWhereLoadIsA);
        jQuery("#ALUIsB").text(response.ALUSumAcresWhereLoadIsB);
        jQuery("#ALUIsC").text(response.ALUSumAcresWhereLoadIsC);
        jQuery("#ALUIsD").text(response.ALUSumAcresWhereLoadIsD);
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


L.control.loadBasedResultsControl = function (opts) {
    return new L.Control.LoadBasedResultsControl(opts);
};