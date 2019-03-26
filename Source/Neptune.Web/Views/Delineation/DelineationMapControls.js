/* Leaflet controls for the Delineation Workflow mpa.
 * Main map code in DelineationMap.js
 * HTML templates in DelineationMapTemplates.cshtml 
 */

// WIP: base class for the html-template driven control pattern
// todo: sufficiently general as to belong to its own js file
L.Control.TemplatedControl = L.Control.extend({
    templateID: null, // must set this value when extending; else onAdd will not work.

    initializeControlInstance: function () {
        // override this method to perform additional initialization during onAdd
    },

    getTrackedElement: function (id) {
        // todo: might not be a bad idea to memoize
        return this.parentElement.querySelector("#" + id);
    },

    onAdd: function (map) {
        var template = document.querySelector("#" + this.templateID);
        this.parentElement = document.importNode(template.content, true).firstElementChild;

        this.initializeControlInstance(map);
        return this.parentElement;
    },

    onRemove: function (map) {
        jQuery(this.parentElement).remove();
    }
});

// todo: sufficiently general to pull out.
var LeafletShades = L.Layer.extend({
    includes: L.Evented ? L.Evented.prototype : L.Mixin.Events,

    options: {
        bounds: null
    },

    initialize: function (options) {
        L.setOptions(this, options);
    },

    onAdd: function (map) {
        this._map = map;
        this._addEventListeners();

        this._shadesContainer = L.DomUtil.create('div', 'frosted-overlay leaflet-zoom-hide');

        map.getPanes().overlayPane.appendChild(this._shadesContainer);
        if (this.options.bounds) this._updateShades(this.options.bounds);

        var size = this._map.getSize();
        var offset = this._getOffset();

        this.setDimensions(this._shadesContainer,
            {
                width: size.x,
                height: size.y,
                top: offset.y,
                left: offset.x
            });
    },
    _addEventListeners: function () { },



    _getOffset: function () {
        // Getting the transformation value through style attributes
        var transformation = this._map.getPanes().mapPane.style.transform
        var startIndex = transformation.indexOf('(');
        var endIndex = transformation.indexOf(')');
        transformation = transformation.substring(startIndex + 1, endIndex).split(',');
        var offset = {
            x: parseInt(transformation[0], 10) * -1,
            y: parseInt(transformation[1], 10) * -1
        };
        return offset;
    },

    setDimensions: function (element, dimensions) {
        element.style.width = dimensions.width + 'px';
        element.style.height = dimensions.height + 'px';
        element.style.top = dimensions.top + 'px';
        element.style.left = dimensions.left + 'px';
    },

    onRemove: function (map) {
        map.getPanes().overlayPane.removeChild(this._shadesContainer);
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

L.control.watermark = function (opts) {
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

    exitDrawCatchmentMode: function (save) {
        this.getTrackedElement("saveAndCancelButtonsWrapper").classList.add("hiddenControlElement");
        this.getTrackedElement("delineationButton").classList.remove("hiddenControlElement");
        this.enableDelineationButton();

        window.delineationMap.exitDrawCatchmentMode(save);
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

L.control.delineationSelectedAsset = function (opts) {
    return new L.Control.DelineationMapSelectedAsset(opts);
};

L.Control.BeginDelineation = L.Control.TemplatedControl.extend({
    templateID: "beginDelineationControlTemplate",

    initializeControlInstance: function () {
        stopClickPropagation(this.parentElement);

        this.getTrackedElement("delineationTypeOptions").hidden = true;

        var self = this;
        this.parentElement.querySelectorAll("[name='flowOption']").forEach(function(el) {
            L.DomEvent.on(el, 'click', function() { self.displayDelineationOptionsForFlowOption(this.value); });
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
        
        if (flowOption === "distributed") {
            this.getTrackedElement("noFlowOptionSelectedText").hidden = true;
            this.getTrackedElement("delineationTypeOptions").hidden = false;

            this.getTrackedElement("delineationOptionAuto").hidden = false;
            this.getTrackedElement("delineationOptionDraw").hidden = false;

            this.getTrackedElement("delineateOptionTrace").hidden = true;
        } else if (flowOption === "centralized") {
            this.getTrackedElement("noFlowOptionSelectedText").hidden = true;
            this.getTrackedElement("delineationTypeOptions").hidden = false;
            
            this.getTrackedElement("delineateOptionTrace").hidden = false;

            this.getTrackedElement("delineationOptionAuto").hidden = true;
            this.getTrackedElement("delineationOptionDraw").hidden = true;
        }
    },

    delineate: function () {
        var flowOption = jQuery("input[name='flowOption']:checked").val();
        var delineationOption = jQuery("input[name='delineationOption']:checked").val();

        // todo (future story): condition on flowOption. For now only Distributed delineations are supported    
        if (flowOption === "distributed") {
            if (delineationOption === "drawDelineate") {
                window.delineationMap.launchDrawCatchmentMode();
            } else if (delineationOption === "autoDelineate") {
                window.delineationMap.launchAutoDelineateMode();
            }
        } else if (flowOption === "centralized") {
            if (delineationOption === "traceDelineate") {
                window.delineationMap.launchTraceDelineateMode();
            }
        }
    }
});

L.control.beginDelineation = function (opts, treatmentBMPFeature) {
    return new L.Control.BeginDelineation(opts, treatmentBMPFeature);
};
