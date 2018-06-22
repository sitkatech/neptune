angular.module("NeptuneApp").controller("EditWqmpParcelsController", function ($scope, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.getParcelNumber = function (parcelId) {
        return $scope.AngularViewData.ParcelNumberByID[parcelId];
    };

    var typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, summaryUrl) {
        var finder = jQuery(typeaheadSelector);
        finder.typeahead({
            highlight: true,
            minLength: 1
        },
            {
                source: new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.whitespace,
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: {
                        url: summaryUrl +
                            "?term=%QUERY",
                        wildcard: "%QUERY"
                    }
                }),
                display: "ParcelNumber",
                limit: Number.MAX_VALUE
            });

        finder.bind("typeahead:select",
            function (event, suggestion) {
                $scope.toggleParcel(suggestion.ParcelID, suggestion.ParcelNumber, function () {
                    $scope.$apply();
                });
            });

        jQuery(typeaheadSelectorButton).click(function () { $scope.selectFirstSuggestionFunction(finder); });

        finder.keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                $scope.selectFirstSuggestionFunction(this);
                $scope.$apply();
            }
        });
    };

    $scope.selectFirstSuggestionFunction = function () {
        var selectables = jQuery($scope.typeaheadSelectorButton).siblings(".tt-menu").find(".tt-selectable");
        if (selectables.length > 0) {
            jQuery(selectables[0]).trigger("click");
        }
    };

    function onMapClick(event) {
        var parcelMapSericeLayerName = $scope.AngularViewData.ParcelMapServiceLayerName,
            mapServiceUrl = $scope.AngularViewData.MapServiceUrl;

        if (!parcelMapSericeLayerName || !mapServiceUrl)
            return;

        var latlng = event.latlng;
        var latlngWrapped = latlng.wrap();
        var parameters = L.Util.extend($scope.neptuneMap.wfsParams,
            {
                typeName: parcelMapSericeLayerName,
                cql_filter: "intersects(ParcelGeometry, POINT(" + latlngWrapped.lat + " " + latlngWrapped.lng + "))"
            });
        SitkaAjax.ajax({
            url: mapServiceUrl + L.Util.getParamString(parameters),
            dataType: "json",
            jsonpCallback: "getJson"
        },
            function (response) {
                if (response.features.length === 0)
                    return;

                var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

                $scope.toggleParcel(mergedProperties.ParcelID, mergedProperties.ParcelNumber, function () {
                    $scope.$apply();
                });

            },
            function () {
                console.error("There was an error selecting the " + $scope.AngularViewData.ParcelFieldDefinitionLabel + " from list");
            });
    }

    $scope.toggleParcel = function (parcelId, parcelNumber, callback) {
        if (parcelNumber && typeof $scope.AngularViewData.ParcelNumberByID[parcelId] === "undefined") {
            $scope.AngularViewData.ParcelNumberByID[parcelId] = parcelNumber;
        }

        if (_.includes($scope.AngularModel.ParcelIDs, parcelId)) {
            _.pull($scope.AngularModel.ParcelIDs, parcelId);
        } else {
            $scope.AngularModel.ParcelIDs.push(parcelId);
        }

        updateSelectedParcelLayer();

        if (typeof callback === "function") {
            callback.call();
        }
    }

    function updateSelectedParcelLayer() {
        if ($scope.AngularModel.ParcelIDs == null) {
            $scope.AngularModel.ParcelIDs = [];
        }

        if ($scope.neptuneMap.selectedParcelLayer) {
            $scope.neptuneMap.layerControl.removeLayer($scope.neptuneMap.selectedParcelLayer);
            $scope.neptuneMap.map.removeLayer($scope.neptuneMap.selectedParcelLayer);
        }

        var wmsParameters = L.Util.extend(
            {
                layers: $scope.AngularViewData.ParcelMapSericeLayerName,
                cql_filter: "ParcelID in (" + $scope.AngularModel.ParcelIDs.join(",") + ")",
                styles: "parcel_yellow"
            },
            $scope.neptuneMap.wmsParams);

        $scope.neptuneMap.selectedParcelLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, wmsParameters);
        $scope.neptuneMap.layerControl.addOverlay($scope.neptuneMap.selectedParcelLayer, "Selected " + $scope.AngularViewData.ParcelFieldDefinitionLabel + "s");
        $scope.neptuneMap.map.addLayer($scope.neptuneMap.selectedParcelLayer);

        // Update map extent to selected parcels
        if (_.any($scope.AngularModel.ParcelIDs)) {
            var wfsParameters = L.Util.extend($scope.neptuneMap.wfsParams,
                {
                    typeName: $scope.AngularViewData.ParcelMapServiceLayerName,
                    cql_filter: "ParcelID in (" + $scope.AngularModel.ParcelIDs.join(",") + ")"
                });
            SitkaAjax.ajax({
                url: $scope.AngularViewData.MapServiceUrl + L.Util.getParamString(wfsParameters),
                dataType: "json",
                jsonpCallback: "getJson"
            },
                function (response) {
                    if (response.features.length === 0)
                        return;

                    $scope.neptuneMap.map.fitBounds(new L.geoJSON(response).getBounds());
                },
                function () {
                    console.error("There was an error setting map extent to the selected " + $scope.AngularViewData.ParcelFieldDefinitionLabel + "s");
                });
        }
    }

    NeptuneMaps.Map.prototype.handleWmsPopupClickEventWithCurrentLayer = function () {
        // Override parent to do nothing
        return function () { };
    };

    function initializeMap() {
        $scope.neptuneMap = new NeptuneMaps.Map($scope.AngularViewData.MapInitJson, "Hybrid");
        $scope.neptuneMap.map.on("click", onMapClick);
        $scope.neptuneMap.map.scrollWheelZoom.enable();

        typeaheadSearch("#" + $scope.AngularViewData.TypeAheadInputId,
            "#" + $scope.AngularViewData.TypeAheadInputId + "Button",
            $scope.AngularViewData.FindParcelByNameUrl);

        updateSelectedParcelLayer();
    }

    initializeMap();
});
