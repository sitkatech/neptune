﻿L.Control.AreaBasedCalculationControl = L.Control.TemplatedControl.extend({
    templateID: "areaBasedCalculationControlTemplate",

    initializeControlInstance: function (map) {
        window.stopClickPropagation(this.parentElement);

        this.map = map;
        if (this.options.showDropdown) {

            // must register the event handler before moving the element
            this.registerHandlerOnDropdown();

            this.getTrackedElement("jurisdictionDropdownContainer")
                .append(jQuery("select[name='jurisdictionDropdown']").get(0));
        }
        this.areaCalculationsUrlTemplate = this.options.areaCalculationsUrlTemplate;
    },

    selectJurisdiction: function (stormwaterJursidictionID) {
        jQuery("select[name='jurisdictionDropdown']").val(stormwaterJursidictionID);
        var areaCalculationUrl = new Sitka.UrlTemplate(this.areaCalculationsUrlTemplate).ParameterReplace(stormwaterJursidictionID);
        populateTGUValuesAreaBased(areaCalculationUrl);
    },

    zoomToJurisdictionOnLoad: function (jurisdictionFeatures, callback) {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionDropdown']").val();
        var bounds = getBounds(jurisdictionFeatures, selectedJurisdictionID);
        self.map.fitBounds(bounds);
        callback(selectedJurisdictionID);
    },

    loadAreaBasedCalculationOnLoad: function() {
        var self = this;
        var selectedJurisdictionID = jQuery("select[name='jurisdictionDropdown']").val();
        var areaCalculationUrl = new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(selectedJurisdictionID);
        populateTGUValuesAreaBased(areaCalculationUrl);
    },

    registerHandlerOnDropdown: function () {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var areaCalculationsUrl =
                new Sitka.UrlTemplate(self.areaCalculationsUrlTemplate).ParameterReplace(this.value);
            populateTGUValuesAreaBased(areaCalculationsUrl);
        });
    },

    registerZoomToJurisdictionHandler: function (jurisdictionFeatures) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            var bounds = getBounds(jurisdictionFeatures, selectedJurisdictionID);
            self.map.fitBounds(bounds);
        });
    },

    registerAdditionalHandler: function(callback) {
        var self = this;
        jQuery("select[name='jurisdictionDropdown']").change(function () {
            var selectedJurisdictionID = this.value;
            callback(selectedJurisdictionID);
        });
    },
    getSelectedJurisdictionID: function() {
        return jQuery("select[name='jurisdictionDropdown']").val();
    }
});

function populateTGUValuesAreaBased(areaCalculationsUrl) {
    jQuery.ajax({
        url: areaCalculationsUrl,
        method: "GET"
    }).then(function (response) {
        jQuery("#fullTrashCapture").text(Math.round(response.FullTrashCaptureAcreage).toLocaleString());
        jQuery("#equivalentArea").text(Math.round(response.EquivalentAreaAcreage).toLocaleString());
        jQuery("#totalAcresCaptured").text(Math.round(response.TotalAcresCaptured).toLocaleString());
        jQuery("#totalPLUAcres").text(Math.round(response.TotalPLUAcres).toLocaleString());
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