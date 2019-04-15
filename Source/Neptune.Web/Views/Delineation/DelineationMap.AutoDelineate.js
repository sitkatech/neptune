/* Wraps calls to the OC GIS auto-delineation service for consumption by UI code */

NeptuneMaps.DelineationMap.AutoDelineate = function (delineationServiceUrl, successCallback, errorCallback) {
    this.DelineationServiceUrl = delineationServiceUrl;
    this.SuccessCallback = successCallback;
    this.ErrorCallback = errorCallback;
};

NeptuneMaps.DelineationMap.AutoDelineate.prototype.MakeDelineationRequest = function (latLng) {
    var url = this.DelineationServiceUrl + "submitJob?" + buildAutoDelineateParameterString(latLng);
    
    var self = this;
    jQuery.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            self.PollDelineationStatus(data.jobId);
        },
        error: this.ErrorCallback
    });
};

NeptuneMaps.DelineationMap.AutoDelineate.prototype.PollDelineationStatus = function (jobID) {
    var url = this.DelineationServiceUrl + "jobs/" + jobID + "?f=json&returnMessages=true"; // todo: dojo.preventCache?

    var self = this;
    jQuery.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            if (data.jobStatus === "esriJobSubmitted" || data.jobStatus === "esriJobExecuting") {
                window.setTimeout(function() {
                        self.PollDelineationStatus(jobID);
                    },
                    1000);
            } else if (data.jobStatus === "esriJobSucceeded") {
                self.FetchDelineationResult(jobID);
            } else if (data.jobStatus === "esriJobFailed") {
                // this is a failure condition
                self.ErrorCallback(data);
            }
        },
        error: this.ErrorCallback
    });
};

NeptuneMaps.DelineationMap.AutoDelineate.prototype.FetchDelineationResult = function (jobID) {
    var self = this;
    jQuery.ajax({
        url: this.DelineationServiceUrl + "jobs/" + jobID + "/results/Output_Watershed?f=geojson&returnType=data", // important to get results in geojson 
        type: "GET",
        success: function (data) {
            if (data.value && data.value.type === "FeatureCollection") {
                self.SuccessCallback(data.value);
            } else {
               //failure condition
                self.ErrorCallback();
            }
        },
        error: this.ErrorCallback
    });
};

var buildAutoDelineateParameterString = function (latLng) {
    return jQuery.param(buildAutoDelineateParameters(latLng));
};

var buildAutoDelineateParameters = function (latLng) {
    return {
        f: "json",
        "env:outSR": NeptuneMaps.Constants.spatialReference,
        Input_Batch_Point: JSON.stringify(buildInputBatchPointParameter(latLng)),
        Snap_Distance: JSON.stringify({ "distance": 10, "units": "esriFeet" })
    };
};

var buildInputBatchPointParameter = function (latLng) {
    return {
        "geometryType": "esriGeometryPoint",
        "features": [
            {
                "geometry": {
                    "x": latLng.lng, //fixme: what"s the actual property name here?
                    "y": latLng.lat, //fixme too probably
                    "spatialReference": { "wkid": NeptuneMaps.Constants.spatialReference }
                }
            }
        ],
        "sr": { "wkid": NeptuneMaps.Constants.spatialReference },
        "Input_Catalog_Layer": "CatalogLayer",
        "Input_Delineation_Type": "Direct Surface Contribution Only"
    };
};
