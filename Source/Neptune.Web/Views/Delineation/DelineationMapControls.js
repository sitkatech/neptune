// page-specific leaflet controls.
// todo: the use of window.delineationMap throughout to back-reference the map object is a little brittle

// WIP: base class for the html-template driven control pattern
L.Control.TemplatedControl = L.Control.extend({
    templateID: null,

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

var stopClickPropagation = function (el) {
    L.DomEvent.on(el, "click", function (e) { e.stopPropagation(); });
};

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
}

L.Control.DelineationMapSelectedAsset = L.Control.extend({
    onAdd: function (map) {
        var t = document.querySelector("#selectedAssetControlTemplate");
        this.parentElement = document.importNode(t.content, true).firstElementChild;

        stopClickPropagation(this.parentElement);

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

        L.DomEvent.on(this._delineationButton,
            "click",
            function (e) {
                window.delineationMap.addBeginDelineationControl();
                this.disableDelineationButton();
                e.stopPropagation();
            }.bind(this));
        
        return this.parentElement;
    },

    onRemove: function (map) {
        jQuery(this.parentElement).remove();
    },

    treatmentBMP: function (treatmentBMPFeature) {
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

    networkCatchment: function (networkCatchmentFeature) {
        // todo: I'm not sure I like this add/remove pattern but I'm not sure I can get around it. closures for the win?
        if (this._traverseCatchmentsHandler) {
            L.DomEvent.off(this._traverseCatchmentsButton, "click", this._traverseCatchmentsHandler);
            this._traverseCatchmentsHandler = null;
        }
        this._traverseCatchmentsHandler = this.makeNetworkCatchmentHandler(networkCatchmentFeature);
        L.DomEvent.on(this._traverseCatchmentsButton,
            "click",
            this._traverseCatchmentsHandler);

        this._selectedCatchmentDetails.innerHTML = "Selected Catchment ID: " + networkCatchmentFeature.properties["NetworkCatchmentID"];

        this._selectedCatchmentInfo.classList.remove("hiddenControlElement");
        this._selectedBmpInfo.classList.add("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");
        this._upstreamCatchmentReportContainer.classList.add("hiddenControlElement");
    },

    makeNetworkCatchmentHandler: function(networkCatchmentFeature) {
        return function(e) {
            window.delineationMap.retrieveAndShowUpstreamCatchments(networkCatchmentFeature);
        };
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
}

L.Control.BeginDelineation = L.Control.extend({
    onAdd: function (map) {

        var t = document.querySelector("#beginDelineationControlTemplate");
        this.parentElement = document.importNode(t.content, true).firstElementChild;

        stopClickPropagation(this.parentElement);
        var stopBtn = this.parentElement.querySelector("#cancelDelineationButton");
        L.DomEvent.on(stopBtn,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });

        return this.parentElement;
    },
    onRemove: function () {
        jQuery(this.parentElement).remove();
    }
});

L.control.beginDelineation = function(opts) {
    return new L.Control.BeginDelineation(opts);
};
