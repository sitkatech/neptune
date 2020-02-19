/* Leaflet controls for the Delineation Workflow mpa.
 * Main map code in DelineationMap.js
 * HTML templates in DelineationMapTemplates.cshtml 
 */

var stopClickPropagation = function (parentElement) {
    L.DomEvent.on(parentElement, "mouseover", function (e) {
        // todo: still not the best way to handle this event-pausing stuff
        window.freeze = true;
    });

    L.DomEvent.on(parentElement,
        "mouseout",
        function (e) {
            window.freeze = false;
        });
};

L.Control.DelineationMapSelectedAsset = L.Control.extend({
    templateID: "selectedAssetControlTemplate",

    onAdd: function (map) {
        var parentElement = L.DomUtil.create('div');
        this.parentElement = parentElement;
        parentElement.id = "selectedAssetControlContainer";
        return parentElement;
    },

    getTrackedElement: function (id) {
        // todo: might not be a bad idea to memoize
        return this.parentElement.querySelector("#" + id);
    },

    initializeControlInstance: function (map) {
        stopClickPropagation(this.parentElement);

        //L.DomEvent.on(this.getTrackedElement("cancelDelineationButton"),
        //    "click",
        //    function (e) {
        //        this.exitDrawCatchmentMode(false);
        //    }.bind(this));

        //L.DomEvent.on(this.getTrackedElement("saveDelineationButton"),
        //    "click",
        //    function (e) {
        //        this.exitDrawCatchmentMode(true);
        //    }.bind(this));

        L.DomEvent.on(this.getTrackedElement("delineationVertexThinningButton"),
            "click",
            function (e) {
                this.thinButtonHandler();
            }.bind(this));
    },

    treatmentBMP: function (treatmentBMPFeature, delineationStatus) {
        this.treatmentBMPFeature = treatmentBMPFeature;

        $('#verifyDelineationButton').bootstrapToggle({
            // note that bootstrapToggle is broken and you have supply class names that don't quite make sense to make it work
            onstyle: 'btn btn-neptune btn-sm',
            offstyle: 'sm btn-default',
            style: "neptuneToggle"
        });

        var self = this;

        // preset and hook up event handler on button
        jQuery("#verifyDelineationButton").off("change");
        if (delineationStatus === "Verified") {
            $('#verifyDelineationButton').bootstrapToggle("on");
        } else {
            $('#verifyDelineationButton').bootstrapToggle("off");
        }
        jQuery("#verifyDelineationButton").change(function (e) {
            if (window.freeze) {
                self.changeDelineationStatus(jQuery(this).prop("checked"));
            }
        });
    },

    launchDrawCatchmentMode: function (drawModeOptions) {
        // okay to persist state because this control mode is ephemeral: see below for where it dies
        this.drawModeOptions = drawModeOptions;

        this.getTrackedElement("saveCancelAndThinButtonsWrapper").classList.remove("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.add("hiddenControlElement");


        this.getTrackedElement("vertexControlContainer").style.display = "none";
        this.getTrackedElement("delineationVertexThinningButton").innerHTML = "Thin";

        if (drawModeOptions.delineationStrategy === STRATEGY_MANUAL && drawModeOptions.newDelineation) {
            this.getTrackedElement("delineationVertexThinningButton").style.display = "none";
        } else {
            this.getTrackedElement("delineationVertexThinningButton").style.display = "initial";
        }

        this.thinButtonHandler = function () {
            this.enableThinning();
        };

        if (!this.slider) {
            this.slider = new Slider('#vertexControl', {
                formatter: function (value) {
                    return 'Current value: ' + value;
                },
                tooltip: "hide",
                ticks: [TOLERANCE_DISTRIBUTED, TOLERANCE_CENTRALIZED],
                ticks_positions: [0, 100],
                ticks_labels: ["More", "Less"],
                min: TOLERANCE_DISTRIBUTED,
                max: TOLERANCE_CENTRALIZED,
                step: TOLERANCE_STEP_INCREMENT,
                value: TOLERANCE_DISTRIBUTED,
            });
            this.slider.on("slideStop",
                function (sliderValue) {
                    this.thin(sliderValue);
                }.bind(this));
        }

        this.slider.setValue(TOLERANCE_DISTRIBUTED);
    },

    exitDrawCatchmentMode: function (save) {
        //// see above
        //this.drawModeOptions = null;

        //this.getTrackedElement("saveCancelAndThinButtonsWrapper").classList.add("hiddenControlElement");
        //this.getTrackedElement("delineationButton").classList.remove("hiddenControlElement");
        //this.enableDelineationButton();

        //// handle the centralized case separately since it doesn't involve drawing anymore
        //if (this.isEditingCentralizedDelineation) {
        //    window.delineationMap.exitTraceMode(save);
        //    this.getTrackedElement("editLocationButtonWrapper").classList.remove("hiddenControlElement");
        //    this.getTrackedElement("editOrDeleteDelineationButtonsWrapper").classList.remove("hiddenControlElement");
        //    this.isEditingCentralizedDelineation = false;
        //} else {
        //    window.delineationMap.exitDrawCatchmentMode(save);
        //}
        console.log("noop");
        console.trace();
    },

    showCentralizedDelineationControls: function () {
        //this.isEditingCentralizedDelineation = true;
        //this.getTrackedElement("saveCancelAndThinButtonsWrapper").classList.remove("hiddenControlElement");
        //this.getTrackedElement("delineationVertexThinningButton").classList.add("hiddenControlElement");
        //this.getTrackedElement("editLocationButtonWrapper").classList.add("hiddenControlElement");
        //this.getTrackedElement("editOrDeleteDelineationButtonsWrapper").classList.add("hiddenControlElement");
        console.log("noop");
        console.trace();
    },

    disableDelineationButton: function () {
        if (!this.getTrackedElement("delineationButton")) {
            return; //misplaced call
        }
        this.getTrackedElement("delineationButton").disabled = "disabled";
    },

    enableDelineationButton: function () {
        if (!this.getTrackedElement("delineationButton")) {
            return; //misplaced call
        }
        this.getTrackedElement("delineationButton").removeAttribute("disabled");
    },

    showDelineationButtons: function () {
        this.getTrackedElement("editOrDeleteDelineationButtonsWrapper").classList.remove("hiddenControlElement");
    },

    thin: function (tolerance) {
        window.delineationMap.thinDelineationVertices(this.drawModeOptions, tolerance);
    },

    acceptThinning: function () {
        this.getTrackedElement("vertexControlContainer").style.display = "none";
        this.getTrackedElement("delineationVertexThinningButton").innerHTML = "Reset";

        this.thinButtonHandler = function () {
            this.unthin();
        };
    },

    unthin: function () {
        window.delineationMap.unthinDelineationVertices();

        this.getTrackedElement("delineationVertexThinningButton").innerHTML = "Thin";
        this.thinButtonHandler = function () {
            this.enableThinning();
        };
    },

    enableThinning: function () {
        this.getTrackedElement("vertexControlContainer").style.display = "initial";
        this.getTrackedElement("delineationVertexThinningButton").innerHTML = "Accept";

        this.slider.resize();

        this.thinButtonHandler = function () {
            this.acceptThinning();
        };
    },

    changeDelineationStatus: function (verified) {
        window.delineationMap.changeDelineationStatus(verified);
    },

    flipVerifyButton: function (verified) {
        if (!verified) {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').off(true);
        } else {
            jQuery(this.getTrackedElement("verifyDelineationButton")).data('bs.toggle').on(true);
        }
    },

    showVerifyButton: function () {
        this.getTrackedElement("delineationStatus").style.display = "initial";
    },
    showDeleteButton: function () {
        this.getTrackedElement("deleteDelineationButton").style.display = "initial";
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
            L.DomEvent.on(el, 'click', function () {
                // deselect any previouslyly selected delineation option and disable the delineation button so they have to select again to proceed.
                self.parentElement.querySelectorAll("[name='delineationOption']").forEach(function (el) { el.checked = false; });
                self.disableDelineationButton();
                self.displayDelineationOptionsForFlowOption(this.value);            // will enable the delineation button if they selected centralized
            });
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
            this.enableDelineationButton();

            this.getTrackedElement("noFlowOptionSelectedText").hidden = true;
            this.getTrackedElement("delineationTypeOptions").hidden = false;

            this.getTrackedElement("delineateOptionTrace").hidden = false;
            this.getTrackedElement("delineationOptionDraw").hidden = true;

            this.getTrackedElement("delineationOptionAuto").hidden = true;
        }
    },

    delineate: function () {
        var flowOption = jQuery("input[name='flowOption']:checked").val();

        window.delineationMap.delineationType = flowOption;

        var delineationOption = jQuery("input[name='delineationOption']:checked").val();

        var drawModeOptions = { delineationType: flowOption, delineationStrategy: STRATEGY_MANUAL };

        if (flowOption === "Distributed") {
            if (delineationOption === "drawDelineate") {
                window.delineationMap.launchDrawCatchmentMode(drawModeOptions);
            } else if (delineationOption === "autoDelineate") {
                window.delineationMap.launchAutoDelineateMode();
            }
        } else if (flowOption === "Centralized") {
            // 2/13/2020 Centralized BMPs' catchments are no longer editable by the user. They must conform to Regional Subbasins.
            window.delineationMap.launchTraceDelineateMode();
        }
    },

    enableDelineationButton: function () {
        this.getTrackedElement("continueDelineationButton").removeAttribute("disabled");
    },

    disableDelineationButton: function () {
        this.getTrackedElement("continueDelineationButton").setAttribute("disabled", "disabled");
    },

    preselectDelineationType: function (type) {
        jQuery("input[name='flowOption'][value='" + type + "']").prop("checked", true);
    }
});

L.control.beginDelineation = function (opts, treatmentBMPFeature) {
    return new L.Control.BeginDelineation(opts, treatmentBMPFeature);
};
