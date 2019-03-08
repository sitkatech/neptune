// page-specific leaflet controls.
// todo: the use of window.delineationMap throughout to back-reference the map object is a little brittle

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
        // Nothing to do here
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
        
        L.DomEvent.on(this._delineationButton,
            "click",
            function (e) {
                window.delineationMap.addBeginDelineationControl();
                this.disableDelineationButton();
                e.stopPropagation();
            }.bind(this));
    },

    networkCatchment: function (networkCatchmentFeature) {

        this._selectedCatchmentDetails.innerHTML = "Selected Catchment ID: " + networkCatchmentFeature.properties["NetworkCatchmentID"];

        this._selectedCatchmentInfo.classList.remove("hiddenControlElement");
        this._selectedBmpInfo.classList.add("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");
        this._upstreamCatchmentReportContainer.classList.add("hiddenControlElement");

        L.DomEvent.on(this._traverseCatchmentsButton,
            "click",
            function (e) {
                window.delineationMap.retrieveAndShowUpstreamCatchments(networkCatchmentFeature);
            });
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
        // todo: is there a cleaner way to generate this stuff than L.DomUtil
        this._div = L.DomUtil.create("div", "beginDelineationControl leaflet-bar");
        stopClickPropagation(this._div);

        var titleBar = L.DomUtil.create("div", "row");

        var title = L.DomUtil.create("div", "col-sm-10");
        title.innerHTML = "<h4>Delineate Drainage Area</h4>";

        var closeButtonWrapper = L.DomUtil.create("div", "col-sm-2 text-right");

        var closeButton = L.DomUtil.create("button", "btn btn-sm btn-neptune");
        closeButton.innerHTML = "x";
        L.DomEvent.on(closeButton,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });

        closeButtonWrapper.append(closeButton);
        titleBar.append(title);
        titleBar.append(closeButtonWrapper);

        this._div.append(titleBar);

        var main = L.DomUtil.create("div", "beginDelineationOptions");
        // todo: add values to these radios when we start building out this step
        main.innerHTML = "<label>1. Select the type of flow this BMP will receive</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives local surface flow only</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives piped flow only</label><br/>" +
            "<label class='group'><input type='radio' name='typeOfFlow'> Receives both piped flow and surface flow</label><hr/>" +
            "<label>2. Choose a delineation option</label></br>" +
            "<label class='group'><input type='radio' name='delineationOption'> Delineate Automatically from DEM</label><br/>" +

            "<label class='group'><input type='radio' name='delineationOption'> Draw the Catchment Area</label><br/>" +
            "<label class='group'><input type='radio' name='delineationOption'> Upload a GIS file</label></hr>" +
            //todo get rid of this
            "<p>(Content under development)</p>";

        this._div.append(main);

        // todo: add a handler to goBtn and undisable it
        var formBtnWrapper = L.DomUtil.create("div", "text-right");
        var goBtn = L.DomUtil.create("button", "continueDelineate btn btn-sm btn-neptune");
        goBtn.type = "button";
        goBtn.innerHTML = "Delineate";
        goBtn.disabled = "disabled";
        var stopBtn = L.DomUtil.create("button", "cancelDelineate btn btn-sm btn-neptune");
        stopBtn.type = "button";
        stopBtn.innerHTML = "Cancel";
        L.DomEvent.on(stopBtn,
            "click",
            function (e) {
                window.delineationMap.removeBeginDelineationControl();
                e.stopPropagation();
            });
        formBtnWrapper.append(goBtn);
        formBtnWrapper.append(stopBtn);

        this._div.append(formBtnWrapper);


        return this._div;
    },
    onRemove: function () {
    }
});

L.control.beginDelineation = function(opts) {
    return new L.Control.BeginDelineation(opts);
};
