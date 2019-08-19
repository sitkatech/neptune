angular.module("NeptuneApp").controller("EditWqmpParcelsController", function ($scope, angularModelAndViewData) {
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.getParcelNumber = function (parcelId) {
        return $scope.AngularViewData.ParcelNumberByID[parcelId];
    };

    $scope.getParcelAddress = function (parcelId) {
        var parcelAddress = $scope.AngularViewData.ParcelAddressByID[parcelId];
        parcelAddress = parcelAddress == null ? "Address is unavailable" : parcelAddress;
        return parcelAddress;
    };

    var typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, findParcelByAddressUrl, findParcelByApnUrl) {
        var finder = jQuery(typeaheadSelector);
        finder.typeahead({
            highlight: true,
            minLength: 3
        },
            $scope.makeTypeaheadObject('Parcels', findParcelByApnUrl, 'Parcels'),
            $scope.makeTypeaheadObject('Addresses', findParcelByAddressUrl, 'Addresses')
        );

        finder.bind("typeahead:select",
            function (event, suggestion) {
                $scope.toggleParcel(suggestion.ParcelID, suggestion.ParcelNumber, suggestion.ParcelAddress, function () {
                    $scope.$apply();
                });
                $('.typeahead').typeahead('val', '');
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

    $scope.makeTypeaheadObject = function (name, url, displayName) {
        var bloodhound = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: {
                url: url + '?term=%QUERY',
                wildcard: '%QUERY'
            }
        });

        var displayText = displayName === 'Parcels' ? 'ParcelNumber' : 'ParcelAddress';
        displayName = displayName === 'Parcels' ? 'Parcel Numbers' : 'Addresses';

        return {
            name: name,
            source: bloodhound,
            display: displayText,
            limit: Number.MAX_VALUE,
            templates: {
                header: '<p class="findResultsHeader">' + displayName + '</p>',
                empty: function (context) {
                    return '<p class="findResultsHeader">' + displayName + '</p>' + '<div class="tt-dataset" style="padding: 3px 40px;">No parcels matching search criteria</div>';
                }
            }
        }
    }

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

        $scope.neptuneMap.getFeatureInfo("OCStormwater:Parcels", [latlng.lng, latlng.lat]).then(function(response) {
            if (response.features.length === 0)
                return;

            var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

            $scope.toggleParcel(mergedProperties.ParcelID, mergedProperties.ParcelNumber, mergedProperties.ParcelAddress, function () {
                $scope.$apply();
            });
        }).fail(function(response) {
            console.error("There was an error selecting the " + $scope.AngularViewData.ParcelFieldDefinitionLabel + " from list");
        });
    }

    $scope.toggleParcel = function (parcelId, parcelNumber, parcelAddress, callback) {
        if (parcelNumber && typeof $scope.AngularViewData.ParcelNumberByID[parcelId] === "undefined") {
            $scope.AngularViewData.ParcelNumberByID[parcelId] = parcelNumber;
        }

        if (parcelNumber && typeof $scope.AngularViewData.ParcelAddressByID[parcelId] === "undefined") {
            $scope.AngularViewData.ParcelAddressByID[parcelId] = parcelAddress;
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
        $scope.calculatedParcelArea = null;

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
                    if (response.features.length > 0) {
                        var calculatedParcelArea = 0;
                        for (var i = 0; i < response.features.length; ++i) {
                            calculatedParcelArea += response.features[i].properties.ParcelArea;
                        }
                        $scope.calculatedParcelArea = calculatedParcelArea.toFixed(1);
                    }
                    $scope.$apply();
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

        //typeaheadSearch("#" + $scope.AngularViewData.TypeAheadInputId,
        //    "#" + $scope.AngularViewData.TypeAheadInputId + "Button",
        //    $scope.AngularViewData.FindParcelByNameUrl, "ParcelNumber");

        typeaheadSearch("#" + $scope.AngularViewData.TypeAheadInputId,
            "#" + $scope.AngularViewData.TypeAheadInputId + "Button",
            $scope.AngularViewData.FindParcelByAddress,
            $scope.AngularViewData.FindParcelByNameUrl);

        updateSelectedParcelLayer();
    }

    initializeMap();
});
