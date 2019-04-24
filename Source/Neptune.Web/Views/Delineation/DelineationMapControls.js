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
        this.registerDelineationButtonHandler(treatmentBMPFeature);
        this.registerDeleteButtonHandler(treatmentBMPFeature);

        var detailLink = this.getTrackedElement("selectedBMPDetailLink");
        var href =
            "/TreatmentBMP/Detail/" + treatmentBMPFeature.properties.TreatmentBMPID;
        detailLink.href = href;
        detailLink.innerHTML = treatmentBMPFeature.properties.Name;// + "<span class='glyphicon glyphicon-new-window></span>";

        var newWindowIcon = L.DomUtil.create("span", "glyphicon glyphicon-new-window");
        detailLink.append(newWindowIcon);

        this.getTrackedElement("selectedBMPType").innerHTML = treatmentBMPFeature.properties.TreatmentBMPType;

        this.getTrackedElement("delineationArea").innerHTML = "-";
        this.getTrackedElement("delineationButton").innerHTML = "Edit";

        if (treatmentBMPFeature.properties.DelineationURL) {
            this.getTrackedElement("delineationType").innerHTML = treatmentBMPFeature.properties.DelineationType;
            this.getTrackedElement("delineationStatus").style.display = "initial";
        } else {
            this.getTrackedElement("delineationType").innerHTML = "No delineation provided";
            this.getTrackedElement("delineationStatus").style.display = "none";
        }

        this.getTrackedElement("selectedBmpInfo").classList.remove("hiddenControlElement");
        this.getTrackedElement("noAssetSelected").classList.add("hiddenControlElement");

        $('#verifyDelineationButton').bootstrapToggle({
            // note that bootstrapToggle is broken and you have supply class names that don't quite make sense to make it work
            onstyle: 'btn btn-neptune btn-sm',
            offstyle: 'sm btn-default',
            style: "neptuneToggle"
        });

        var self = this;

        // hook up event handler on button
        jQuery("#verifyDelineationButton").off("change");
        jQuery("#verifyDelineationButton").change(function(e) {
            self.changeDelineationStatus(jQuery(this).prop("checked"));
        });
    },

    registerDelineationButtonHandler: function(treatmentBMPFeature) {
        var self = this;
        var delineationButton = this.getTrackedElement("delineationButton");
        if (this._beginDelineationHandler) {
            L.DomEvent.off(delineationButton, "click", this._beginDelineationHandler);
            this._beginDelineationHandler = null;
        }

        this._beginDelineationHandler = function(e) {
            window.delineationMap.addBeginDelineationControl(treatmentBMPFeature);
            self.disableDelineationButton();
            e.stopPropagation();
        };

        L.DomEvent.on(delineationButton,
            "click", this._beginDelineationHandler
        );
    },

    registerDeleteButtonHandler: function(treatmentBMPFeature) {

        var deleteButton = this.getTrackedElement("deleteDelineationButton");
        if (this._deleteDelineationHandler) {
            L.DomEvent.off(deleteButton, "click", this._deleteDelineationHandler);
            this._deleteDelineationHandler = null;
        }

        this._deleteDelineationHandler = function(e) {
            window.delineationMap.deleteDelineation(treatmentBMPFeature);
        };

        L.DomEvent.on(deleteButton,
            "click", this._deleteDelineationHandler
        );
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
        this.getTrackedElement("selectedBmpInfo").classList.add("hiddenControlElement");
        this.getTrackedElement("noAssetSelected").classList.remove("hiddenControlElement");
    },

    reportUpstreamCatchments: function (count) {
        this._upstreamCatchmentReportContainer.classList.remove("hiddenControlElement");
        this._upstreamCatchmentReport.innerHTML = "Found " + count + " upstream catchment(s)";
    },

    reportDelineationArea: function(properties) {
        this.getTrackedElement("delineationArea").innerHTML = properties.Area + " ac";
        this.getTrackedElement("delineationType").innerHTML = properties.DelineationType;
    },

    disableDelineationButton: function() {
        if (!this.getTrackedElement("delineationButton")) {
            return; //misplaced call
        }
        this.getTrackedElement("delineationButton").disabled = "disabled";
    },

    enableDelineationButton: function() {
        if (!this.getTrackedElement("delineationButton")) {
            return; //misplaced call
        }
        this.getTrackedElement("delineationButton").removeAttribute("disabled");
    },

    changeDelineationStatus:function(verified) {
        window.delineationMap.changeDelineationStatus(verified);
    },

    flipVerifyButton:function(verified) {
        if (!verified) {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').off(true);
        } else {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').on(true);
        }
    },

    showVerifyButton:function() {
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
            this.getTrackedElement("delineationOptionDraw").hidden = false;

            this.getTrackedElement("delineationOptionAuto").hidden = true;
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
            } else if (delineationOption === "drawDelineate") {
                window.delineationMap.launchDrawCatchmentMode();
            }
        }
    },

    enableDelineationButton() {
        this.getTrackedElement("continueDelineationButton").removeAttribute("disabled");
    },

    preselectDelineationType(type) {
        jQuery("input[name='flowOption'][value='" + type + "']").prop("checked", true);
    }
});

L.control.beginDelineation = function (opts, treatmentBMPFeature) {
    return new L.Control.BeginDelineation(opts, treatmentBMPFeature);
};
