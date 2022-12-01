/* Wraps calls to the OC GIS auto-delineation service for consumption by UI code */

// todo: need not use object instantiation
NeptuneMaps.DelineationMap.AutoDelineate = function (delineationServiceUrl) {
    this.DelineationServiceUrl = delineationServiceUrl;
};

NeptuneMaps.DelineationMap.AutoDelineate.prototype.MakeDelineationRequestNew = function (latLng) {
    var submitUrl = this.DelineationServiceUrl + "submitJob?" + buildAutoDelineateParameterString(latLng);
    var self = this;
    
    return jQuery.ajax({
        url: submitUrl,
        type: "GET"
    }).fail(function(data) {
        // this fail is only hit if the initial request fails, in which case we assume the service is probably unavailable.
        return jQuery.Deferred().reject({ serviceUnavailable: true });
    }).then(function (data) {
        var statusUrl = self.DelineationServiceUrl + "jobs/" + data.jobId + "?f=json&returnMessages=true";
        return pollUntilDone(statusUrl);
    }).then(function (data) {
        return jQuery.ajax({
            url: self.DelineationServiceUrl +
                "jobs/" +
                data.jobId +
                "/results/Output_Watershed?f=geojson&returnType=data", // important to get results in geojson 
            type: "GET"
        });
    }).then(function (data) {
        return jQuery.Deferred(function (deferred) {
            if (data.value && data.value.type === "FeatureCollection") {
                deferred.resolve(data.value);
            } else {
                //failure condition
                return deferred.reject(/*what to put here*/);
            }
        });
    });
};

// halpers

var buildAutoDelineateParameterString = function (latLng) {
    return jQuery.param(buildAutoDelineateParameters(latLng));
};

var buildAutoDelineateParameters = function (latLng) {
    return {
        f: "json",
        Input_Batch_Point: JSON.stringify(buildInputBatchPointParameter(latLng)),
        Get_Watershed_Characteristics: "No",
        Input_Delineation_Type: "Direct Surface Only",
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
        "sr": { "wkid": NeptuneMaps.Constants.spatialReference }
    };
};

// see https://stackoverflow.com/a/46208449
var delay = function (t) {
    return jQuery.Deferred(function () {
        setTimeout(this.resolve, t);
    });
};

function pollUntilDone(url) {
    function run() {
        return jQuery.ajax({
            url: url,
            type: "GET"
        }).then(function (data) {
            if (data.jobStatus === "esriJobSubmitted" || data.jobStatus === "esriJobExecuting") {
                return delay(1000).then(run);
            } else if (data.jobStatus === "esriJobSucceeded") {
                return data;
            } else {
                return jQuery.Deferred().reject(data);
            }
        });
    }
    return run();
}
