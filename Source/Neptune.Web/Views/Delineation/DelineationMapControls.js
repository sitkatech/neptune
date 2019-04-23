/* Leaflet controls for the Delineation Workflow mpa.
 * Main map code in DelineationMap.js
 * HTML templates in DelineationMapTemplates.cshtml 
 */

var stopClickPropagation = function (parentElement) {
    L.DomEvent.on(parentElement, "mouseover", function (e) {
        // todo: still not the best way to handle this event-pausing stuff
        window.freeze = true;
    });

    L.DomEvent.on(parentElement, "mouseout", function (e) {
        window.freeze = false;
    })
};

L.Control.DelineationMapSelectedAsset = L.Control.TemplatedControl.extend({
    templateID: "selectedAssetControlTemplate",

    initializeControlInstance: function (map) {
        stopClickPropagation(this.parentElement);

        this._noAssetSelected = this.parentElement.querySelector("#noAssetSelected");

        this._selectedBmpInfo = this.parentElement.querySelector("#selectedBmpInfo");
        this._selectedBmpName = this.parentElement.querySelector("#selectedBmpName");
        this._delineationStatus = this.parentElement.querySelector("#delineationStatus");
        this._delineationButton = this.parentElement.querySelector("#delineationButton");

        L.DomEvent.on(this.getTrackedElement("cancelDelineationButton"),
            "click",
            function (e) {
                this.exitDrawCatchmentMode(false);
            }.bind(this));

        L.DomEvent.on(this.getTrackedElement("saveDelineationButton"),
            "click",
            function (e) {
                this.exitDrawCatchmentMode(true);
            }.bind(this));
    },

    treatmentBMP: function (treatmentBMPFeature) {
        if (this._beginDelineationHandler) {
            L.DomEvent.off(this._delineationButton, "click", this._beginDelineationHandler);
            this._beginDelineationHandler = null;
        }

        this._beginDelineationHandler = function (e) {
            window.delineationMap.addBeginDelineationControl(treatmentBMPFeature);
            this.disableDelineationButton();
            e.stopPropagation();
        }.bind(this);

        L.DomEvent.on(this._delineationButton,
            "click", this._beginDelineationHandler
        );

        var detailLink = this.getTrackedElement("selectedBMPDetailLink");
        var href =
            "/TreatmentBMP/Detail/" + treatmentBMPFeature.properties.TreatmentBMPID;
        detailLink.href = href;
        detailLink.innerHTML = treatmentBMPFeature.properties.Name;

        this.getTrackedElement("selectedBMPType").innerHTML = treatmentBMPFeature.properties.TreatmentBMPType;

        this.getTrackedElement("delineationArea").innerHTML = "-";
        this._delineationButton.innerHTML = "Edit Delineation";

        if (treatmentBMPFeature.properties.DelineationURL) {
            this.getTrackedElement("delineationType").innerHTML = treatmentBMPFeature.properties.DelineationType;
            this.getTrackedElement("delineationStatus").style.display = "initial";
        } else {
            this.getTrackedElement("delineationType").innerHTML = "No delineation provided";
            this.getTrackedElement("delineationStatus").style.display = "none";
        }

        this._selectedBmpInfo.classList.remove("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");

        $('#verifyDelineationButton').bootstrapToggle({
            onstyle: 'btn btn-neptune'
        });

        var self = this;

        // hook up event handler on button
        jQuery("#verifyDelineationButton").off("change");
        jQuery("#verifyDelineationButton").change(function(e) {
            self.changeDelineationStatus(jQuery(this).prop("checked"));
        });
    },

    launchDrawCatchmentMode: function () {
        this.getTrackedElement("saveAndCancelButtonsWrapper").classList.remove("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.add("hiddenControlElement");
    },

    exitDrawCatchmentMode: function (save) {
        this.getTrackedElement("saveAndCancelButtonsWrapper").classList.add("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.remove("hiddenControlElement");
        this.enableDelineationButton();

        window.delineationMap.exitDrawCatchmentMode(save);
    },

    reset: function () {
        this._selectedBmpInfo.classList.add("hiddenControlElement");
        this._noAssetSelected.classList.remove("hiddenControlElement");
    },

    reportUpstreamCatchments: function (count) {
        this._upstreamCatchmentReportContainer.classList.remove("hiddenControlElement");
        this._upstreamCatchmentReport.innerHTML = "Found " + count + " upstream catchment(s)";
    },

    reportDelineationArea: function(properties) {
        this.getTrackedElement("delineationArea").innerHTML = properties.Area;
        this.getTrackedElement("delineationType").innerHTML = properties.DelineationType;
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

    changeDelineationStatus(verified) {
        window.delineationMap.changeDelineationStatus(verified);
    },

    flipVerifyButton(verified) {
        if (!verified) {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').off(true);
        } else {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').on(true);
        }
    },

    showVerifyButton() {
        this.getTrackedElement("delineationStatus").style.display = "initial";
    }
});

L.control.delineationSelectedAsset = function (opts) {
    return new L.Control.DelineationMapSelectedAsset(opts);
};

L.Control.BeginDelineation = L.Control.TemplatedControl.extend({
    templateID: "beginDelineationControlTemplate",

    initializeControlInstance: function () {
        stopClickPropagation(this.parentElement);

        this.getTrackedElement("delineationTypeOptions").hidden = true;

        var self = this;
        this.parentElement.querySelectorAll("[name='flowOption']").forEach(function (el) {
            L.DomEvent.on(el, 'click', function () { self.displayDelineationOptionsForFlowOption(this.value); });
        });
        this.parentElement.querySelectorAll("[name='delineationOption']").forEach(function (el) {
            L.DomEvent.on(el, 'click', function () { self.enableDelineationButton(); });
        });

        var drawOptionText = this.treatmentBMPFeature.properties.DelineationURL
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
            function (e) {
                this.delineate();
            }.bind(this));

        var dieBtn = this.getTrackedElement("closeBeginDelineationControlButton");
        L.DomEvent.on(dieBtn,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });
    },

    initialize: function (options, treatmentBMPFeature) {
        this.treatmentBMPFeature = treatmentBMPFeature;
        L.setOptions(this, options);
    },

    displayDelineationOptionsForFlowOption: function (flowOption) {

        if (flowOption === "Distributed") {
            this.getTrackedElement("noFlowOptionSelectedText").hidden = true;
            this.getTrackedElement("delineationTypeOptions").hidden = false;

            this.getTrackedElement("delineationOptionAuto").hidden = false;
            this.getTrackedElement("delineationOptionDraw").hidden = false;

            this.getTrackedElement("delineateOptionTrace").hidden = true;
        } else if (flowOption === "Centralized") {
            this.getTrackedElement("noFlowOptionSelectedText").hidden = true;
            this.getTrackedElement("delineationTypeOptions").hidden = false;

            this.getTrackedElement("delineateOptionTrace").hidden = false;

            this.getTrackedElement("delineationOptionAuto").hidden = true;
            this.getTrackedElement("delineationOptionDraw").hidden = true;
        }
    },

    delineate: function () {
        var flowOption = jQuery("input[name='flowOption']:checked").val();

        window.delineationMap.delineationType = flowOption;

        var delineationOption = jQuery("input[name='delineationOption']:checked").val();

        if (flowOption === "Distributed") {
            if (delineationOption === "drawDelineate") {
                window.delineationMap.launchDrawCatchmentMode();
            } else if (delineationOption === "autoDelineate") {
                window.delineationMap.launchAutoDelineateMode();
            }
        } else if (flowOption === "Centralized") {
            if (delineationOption === "traceDelineate") {
                window.delineationMap.launchTraceDelineateMode();
            }
        }
    },

    enableDelineationButton() {
        this.getTrackedElement("continueDelineationButton").removeAttribute("disabled");
    },
});

L.control.beginDelineation = function (opts, treatmentBMPFeature) {
    return new L.Control.BeginDelineation(opts, treatmentBMPFeature);
};
