/* Leaflet controls for the Delineation Workflow mpa.
 * Main map code in DelineationMap.js
 * HTML templates in DelineationMap.cshtml (TODO: move to DelineationMapTemplate)
 */

// WIP: base class for the html-template driven control pattern
// todo: sufficiently general as to belong to its own js file
L.Control.TemplatedControl = L.Control.extend({
    templateID: null, // must set this value when extending; else onAdd will not work.

    initializeControlInstance: function() {
        // override this method to perform additional initialization during onAdd
    },

    getTrackedElement: function(id) {
        // todo: might not be a bad idea to memoize
        return this.parentElement.querySelector("#" + id);
    },

    onAdd: function(map) {
        var template = document.querySelector("#" + this.templateID);
        this.parentElement = document.importNode(template.content, true).firstElementChild;

        this.initializeControlInstance(map);
        return this.parentElement;
    },

    onRemove: function(map) {
        jQuery(this.parentElement).remove();
    }
});

// todo: watermark is sufficiently general as to belong to its own js file
L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {
        
        var img = L.DomUtil.create("img");

        img.src = "/Content/img/OCStormwater/banner_logo.png";
        img.style.width = "200px";

        return img;
    },

    onRemove: function (map) {
        jQuery(this.parentElement).remove();
    }
});

L.control.watermark = function(opts) {
    return new L.Control.Watermark(opts);
};

var stopClickPropagation = function (el) {
    L.DomEvent.on(el, "click", function (e) { e.stopPropagation(); });
};

L.Control.DelineationMapSelectedAsset = L.Control.TemplatedControl.extend({
    templateID: "selectedAssetControlTemplate",

    initializeControlInstance: function (map) {
        stopClickPropagation(this.parentElement);

        // todo: there's no reason to set all of these to member variables; just use getTrackedElement()
        this._noAssetSelected = this.parentElement.querySelector("#noAssetSelected");

        this._selectedBmpInfo = this.parentElement.querySelector("#selectedBmpInfo");
        this._selectedBmpName = this.parentElement.querySelector("#selectedBmpName");
        this._delineationStatus = this.parentElement.querySelector("#delineationStatus");
        this._delineationButton = this.parentElement.querySelector("#delineationButton");

        this._selectedCatchmentInfo = this.parentElement.querySelector("#selectedCatchmentInfo");
        this._selectedCatchmentDetails = this.parentElement.querySelector("#selectedCatchmentDetails");
        this._traverseCatchmentsButton = this.parentElement.querySelector("#traverseCatchmentsButton");
        this._upstreamCatchmentReportContainer = this.parentElement.querySelector("#upstreamCatchmentReportContainer");
        this._upstreamCatchmentReport = this.parentElement.querySelector("#upstreamCatchmentReport");

        L.DomEvent.on(this.getTrackedElement("cancelDelineationButton"),
            "click",
            function(e) {
                this.exitDrawCatchmentMode();
            }.bind(this));
    },

    treatmentBMP: function (treatmentBMPFeature) {
        if (this._beginDelineationHandler) {
            L.DomEvent.off(this._delineationButton, "click", this._beginDelineationHandler);
            this._beginDelineationHandler = null;
        }

        this._beginDelineationHandler = function (e) {
            // todo: the use of window.delineationMap throughout to back-reference the map object is a little brittle
            window.delineationMap.addBeginDelineationControl(treatmentBMPFeature);
            this.disableDelineationButton();
            e.stopPropagation();
        }.bind(this);

        L.DomEvent.on(this._delineationButton,
            "click", this._beginDelineationHandler
        );

        this._selectedBmpName.innerHTML = "BMP: " +
            treatmentBMPFeature.properties["Name"];

        if (treatmentBMPFeature.properties["DelineationURL"]) {
            this._delineationStatus.innerHTML = "This BMP's current recorded delineation is displayed in yellow on the map.";
            this._delineationButton.innerHTML = "Redelineate Drainage Area";
        } else {
            this._delineationStatus.innerHTML = "No catchment delineation has been performed for this BMP";
            this._delineationButton.innerHTML = "Delineate Drainage Area";
        }

        // todo: lines like these are going to proliferate and it should be possible to DRY it up
        this._selectedBmpInfo.classList.remove("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");
        this._selectedCatchmentInfo.classList.add("hiddenControlElement");
    },

    launchDrawCatchmentMode: function () {
        this.getTrackedElement("saveAndCancelButtonsWrapper").classList.remove("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.add("hiddenControlElement");
    },

    exitDrawCatchmentMode: function () {
        this.getTrackedElement("saveAndCancelButtonsWrapper").classList.add("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.remove("hiddenControlElement");
        this.enableDelineationButton();

        window.delineationMap.exitDrawCatchmentMode();
    },

    networkCatchment: function (networkCatchmentFeature) {
        // todo: I'm not sure I like this add/remove pattern but I'm not sure I can get around it. closures for the win?
        if (this._traverseCatchmentsHandler) {
            L.DomEvent.off(this._traverseCatchmentsButton, "click", this._traverseCatchmentsHandler);
            this._traverseCatchmentsHandler = null;
        }
        this._traverseCatchmentsHandler = function (e) {
            window.delineationMap.retrieveAndShowUpstreamCatchments(networkCatchmentFeature);
        };
        L.DomEvent.on(this._traverseCatchmentsButton,
            "click",
            this._traverseCatchmentsHandler);

        this._selectedCatchmentDetails.innerHTML = "Selected Catchment ID: " + networkCatchmentFeature.properties["NetworkCatchmentID"];

        this._selectedCatchmentInfo.classList.remove("hiddenControlElement");

        this._selectedBmpInfo.classList.add("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");
        this._upstreamCatchmentReportContainer.classList.add("hiddenControlElement");
    },

    reset: function () {
        this._selectedBmpInfo.classList.add("hiddenControlElement");
        this._selectedCatchmentInfo.classList.add("hiddenControlElement");
        this._noAssetSelected.classList.remove("hiddenControlElement");
    },

    reportUpstreamCatchments: function (count) {
        this._upstreamCatchmentReportContainer.classList.remove("hiddenControlElement");
        this._upstreamCatchmentReport.innerHTML = "Found " + count + " upstream catchment(s)";
    },

    disableDelineationButton() {
        if (!this._delineationButton) {
            return; //misplaced call
        }
        this._delineationButton.disabled = "disabled";
    },

    enableDelineationButton() {
        if (!this._delineationButton) {
            return; //misplaced call
        }
        this._delineationButton.removeAttribute("disabled");
    },

    disableUpstreamCatchmentsButton() {
        if (!this._traverseCatchmentsButton) {
            return; //misplaced call
        }
        this._traverseCatchmentsButton.innerHTML = "Loading...";
        this._traverseCatchmentsButton.disabled = "disabled";
    },

    enableUpstreamCatchmentsButton() {
        if (!this._traverseCatchmentsButton) {
            return; //misplaced call
        }
        this._traverseCatchmentsButton.innerHTML = "Trace Upstream Catchments";
        this._traverseCatchmentsButton.removeAttribute("disabled");
    }
});

L.control.delineationSelectedAsset = function(opts) {
    return new L.Control.DelineationMapSelectedAsset(opts);
};

L.Control.BeginDelineation = L.Control.TemplatedControl.extend({
    templateID: "beginDelineationControlTemplate",

    initializeControlInstance: function () {
        stopClickPropagation(this.parentElement);

        var drawOptionText = this.treatmentBMPFeature.properties.HasDelineation
            ? "Revise the Catchment Area"
            : "Draw the Catchment Area";
        this.getTrackedElement("delineationOptionDrawText").innerHTML = drawOptionText;

        var stopBtn = this.getTrackedElement("cancelDelineationButton");
        L.DomEvent.on(stopBtn,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });

        var goBtn = this.getTrackedElement("continueDelineationButton");
        L.DomEvent.on(goBtn,
            "click",
            function(e) {
                this.delineate();
            }.bind(this));
    },

    initialize: function(options, treatmentBMPFeature) {
        this.treatmentBMPFeature = treatmentBMPFeature;
        L.setOptions(this, options);
        console.log(this.treatmentBMPFeature.properties);
    },

    delineate: function() {
        var flowOption = jQuery("input[name='flowOption']").val();
        var delineationOption = jQuery("input[name='delineationOption']").val();

        // todo (future story): condition on flowOption and delineationOption
        // for now, just go to draw mode because that's the only thing we've built
        window.delineationMap.launchDrawCatchmentMode();
    }
});

L.control.beginDelineation = function(opts, treatmentBMPFeature) {
    return new L.Control.BeginDelineation(opts, treatmentBMPFeature);
};
