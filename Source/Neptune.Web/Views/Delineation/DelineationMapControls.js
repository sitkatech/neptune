// page-specific leaflet controls.
// todo: the use of window.delineationMap throughout to back-reference the map object is a little brittle

var stopClickPropagation = function (el) {
    L.DomEvent.on(el, 'click', function (e) { e.stopPropagation(); });
};

L.Control.Watermark = L.Control.extend({
    onAdd: function (map) {
        var img = L.DomUtil.create('img');

        img.src = '/Content/img/OCStormwater/banner_logo.png';
        img.style.width = '200px';

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
        //this._div = L.DomUtil.create("div", "selectedAssetControl leaflet-bar");
        //stopClickPropagation(this._div);
        //this._div.innerHTML = "<h4>Selected Asset</h4>";

        //this.reset();

        //this._div.append(this._innerDiv);

        //return this._div;

        var t = document.querySelector('#selectedAssetControlTemplate');
        var clone = document.importNode(t.content, true).firstElementChild;

        debugger;
        this._noAssetSelected = clone.querySelector("#noAssetSelected");

        this._selectedBmpInfo = clone.querySelector("#selectedBmpInfo");
        this._selectedBmpName = clone.querySelector("#selectedBmpName");
        this._delineationStatus = clone.querySelector("#delineationStatus");
        this._delineationButton = clone.querySelector("#delineationButton");

        this._selectedCatchmentInfo = clone.querySelector("#selectedCatchmentInfo");

        return clone;
    },

    onRemove: function (map) {
        //perfunctory
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

        this._selectedBmpInfo.classList.remove("hiddenControlElement");
        this._noAssetSelected.classList.add("hiddenControlElement");
        


        L.DomEvent.on(this._delineationButton,
            "click",
            function (e) {
                window.delineationMap.addBeginDelineationControl();
                this.disableDelineationButton();
                e.stopPropagation();
            }.bind(this));
    },

    networkCatchment: function (networkCatchmentFeature) {
        this._innerDiv.innerHTML = "";

        this._catchmentInfoDiv = L.DomUtil.create("div");

        this._catchmentInfoDiv.innerHTML = "Selected Catchment ID: " +
            networkCatchmentFeature.properties["NetworkCatchmentID"] + "<hr/>";

        this._upstreamCatchmentInfoDiv = L.DomUtil.create("div");
        this._catchmentInfoDiv.append(this._upstreamCatchmentInfoDiv);
        //this._catchmentInfoDiv.append(L.DomUtil.create("br"));

        this._traverseBtn = L.DomUtil.create("button", "traverseBtn btn btn-sm btn-neptune");
        this._traverseBtn.type = "button";
        this._traverseBtn.innerHTML = "Trace Upstream Catchments";

        L.DomEvent.on(this._traverseBtn,
            "click",
            function (e) {
                window.delineationMap.retrieveAndShowUpstreamCatchments(networkCatchmentFeature);
            });

        this._innerDiv.append(this._catchmentInfoDiv);
        this._innerDiv.append(this._traverseBtn);
    },

    reportUpstreamCatchments: function (count) {
        this._upstreamCatchmentInfoDiv.innerHTML = "<p>Found " + count + " upstream catchment(s)</p> <br/>";
    },

    reset: function () {
        if (!this._innerDiv) {
            this._innerDiv = L.DomUtil.create("div", "selectedAssetInfo");
        }
        this._innerDiv.innerHTML = "<p>Select a BMP on the map to see options.</p>";
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
        if (!this._traverseBtn) {
            return; //misplaced call
        }
        this._traverseBtn.innerHTML = "Loading...";
        this._traverseBtn.disabled = "disabled";
    },

    enableUpstreamCatchmentsButton() {
        if (!this._traverseBtn) {
            return; //misplaced call
        }
        this._traverseBtn.innerHTML = "Trace Upstream Catchments";
        this._traverseBtn.removeAttribute("disabled");
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
