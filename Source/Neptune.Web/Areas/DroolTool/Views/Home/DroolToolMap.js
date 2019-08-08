var resizeHandler = function (mapInitJson) {
    var totalHeaderHeight = (jQuery("header").height());

    if (jQuery("header").is(":hidden")){
        jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height());
    } else {
        jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
            totalHeaderHeight);
    }

    window.totalHeaderHeight = totalHeaderHeight;
};

function initResizeHandler(mapInitJson) {
    $(window).on("load",
        function () { resizeHandler(mapInitJson); }
    );

    $(window).on("resize",
        function () { resizeHandler(mapInitJson); });
}

var NEIGHBORHOOD_NOT_FOUND =
    "This neighborhood is not within the Urban Drool Tool reporting area. If you wish to be included in the Urban Drool Tool, please contact your local water district.";
var NOMINATIM_ERROR =
    "There was an error retrieving the address. The address location service may be unavailable. If the issue persists, please contact support.";

L.Control.DroolToolWatermark = L.Control.extend({
    onAdd: function (map) {

        var div = L.DomUtil.create("div");
        div.innerHTML =
            "<img src='/Areas/DroolTool/Content/udt_mchr_logo.png' height=70 /><img src='/Areas/DroolTool/Content/h2oc_color_logo.png' height=70/><img src='/Areas/DroolTool/Content/mnwd_color_logo.png' height=70/>";

        return div;
    }
});

L.control.droolToolWatermark = function(options) {
    return new L.Control.DroolToolWatermark(options);
}

L.Control.NeighborhoodDetailControl = L.Control.extend({
    onAdd: function (map) {
        this.parentElement = L.DomUtil.create("div", "leaflet-bar leaflet-control neptune-leaflet-control neighborhood-detail-control neighborhood-detail-hidden-mobile ");
        this.neptuneMap = this.options.neptuneMap;

        // boilerplate-ish code to prevent clicks on this element from registering as clicks on the map proper
        window.stopClickPropagation(this.parentElement);

        L.DomEvent.on(this.parentElement,
            "mouseover",
            function () {
                map.dragging.disable();
                map.touchZoom.disable();
                map.doubleClickZoom.disable();
                map.scrollWheelZoom.disable();
            });

        L.DomEvent.on(this.parentElement,
            "mouseout",
            function () {
                map.dragging.enable();
                map.touchZoom.enable();
                map.doubleClickZoom.enable();
                map.scrollWheelZoom.enable();
            });

        var currentMonthName = new Date().toLocaleString('default', { month: 'long' });

        this.parentElement.innerHTML = "<div>" +
            "<div class='row'><div class='col-xs-8'><h4 class=''>Selected Neighborhood</h4></div><div class='col-xs-2 text-right'><button class='btn btn-sm btn-neptune neighborhod-detail-hide-button-mobile' style=' outline: currentcolor none medium;position: fixed;right: 5px;'><span class='glyphicon glyphicon-remove'></span></button></div></div>" + 
            "" +
            "<span>Neighborhood Area: </span><span id='Area'></span> acres<br/>" +
            "<span>Drains to </span><span id='DrainsTo'></span><span> Watershed</span><br/><br/>" +
            "<div class='neighborhoodSection sectionAboutYourNeighborhood' style='height:30%'>" +
            "<h6>About Your Neighborhood</h6>" +
            "<strong>Number of Accounts: </strong><span class='about-datum' id='NumberOfReshoaAccounts'></span><br/>" +
            "<strong>Irrigated Area: </strong><span class='about-datum' id='TotalReshoaIrrigatedArea'></span><br/>" +
            "<strong>" +
            currentMonthName +
            " Water Budget: </strong><span class='about-datum' id='TotalBudget'></span><br/>" +
            "<strong>" +
            currentMonthName +
            " Outdoor Budget: </strong><span class='about-datum' id='TotalOutdoorBudget'></span><br/>" +
            "</div>" +
            "<div class='neighborhoodSection sectionWaterUsage'>" +
            "<h6>Water Usage</h6>" +
            "<div class='row'><div class='col-xs-6 text-center'><strong>Neighborhood Drool Rating</strong></div><div class='col-xs-6 text-center'><strong>Neighborhood Drool Trend</strong></div></div>" +
            "<div class='row'><div class='col-xs-6 text-center'><img src='/Areas/DroolTool/Content/mock/wuplace_score.png'/></div><div class='col-xs-6 text-center'><img src='/Areas/DroolTool/Content/mock/wuplace_arr.png'/></div></div>" +
            "<div class='row'><div class='col-xs-6 text-center'> " +
            currentMonthName +
            " range: XX to YY. Lower is better.</div><div class='col-xs-6 text-center'>Improving</div></div>" +
            "</div>" +
            "<div class='neighborhoodSection sectionConservationActions'>" +
            "<h6>Conservation Actions</h6>" +
            "<div class='row'><div class='col-xs-6 text-center'><strong>Neighborhood Action Score</strong></div><div class='col-xs-6 text-center'><strong>Neighborhood Action Trend</strong></div></div>" +
            "<div class='row'><div class='col-xs-6 text-center'><img src='/Areas/DroolTool/Content/mock/caplace_score.png'/></div><div class='col-xs-6 text-center'><img src='/Areas/DroolTool/Content/mock/caplace_arr.png'/></div></div>" +
            "<div class='row'><div class='col-xs-6 text-center'> Number of Water Conservation Actions by your Neighbors</div><div class='col-xs-6 text-center'>Improving</div></div>" +
            "</div>" +
            "<button class='btn btn-neptune btn-sm' id='highlightFlowButton'>Where does my runoff go?</button>" +
            "</div>";

        this.hide();

        return this.parentElement;
    },

    wireHighlightFlowButton: function() {
        var highlightFlowButton = document.getElementById("highlightFlowButton");

        var self = this;
        L.DomEvent.on(highlightFlowButton,
            "click",
            function () {
                self.neptuneMap.highlightFlow();
            });
    },

    // neutered under #430; may bring back the heart and soul of this piece as part of a "Neighborhood Detail Page" later
    //wireMonthPicker: function () {

    //    jQuery(".metricMonthPicker").on("click",
    //        function() {
    //            jQuery("#MonthPicker_Button_").click();
    //        });

    //    var getNeighborhoodID = function () {
    //        return this.NeighborhoodID;
    //    }.bind(this);
        
    //    jQuery(".metricMonthPicker").MonthPicker({
    //            OnAfterChooseMonth: function() {
    //                var month = Number(this.value.split("/")[0]);
    //                var year = Number(this.value.split("/")[1]);
                    
    //                RemoteService.getMetrics(getNeighborhoodID(), year, month).then(function(metricResponse) {
    //                    jQuery("#NumberOfReshoaAccounts").text(metricResponse.NumberOfReshoaAccounts);
    //                    jQuery("#TotalReshoaIrrigatedArea").text(metricResponse.TotalReshoaIrrigatedArea);
    //                    jQuery("#AverageIrrigatedArea").text(metricResponse.AverageIrrigatedArea);
    //                    jQuery("#TotalEstimatedReshoaUsers").text(metricResponse.TotalEstimatedReshoaUsers);
    //                    jQuery("#TotalBudget").text(metricResponse.TotalBudget);
    //                    jQuery("#TotalOutdoorBudget").text(metricResponse.TotalOutdoorBudget);
    //                    jQuery("#AverageTotalUsage").text(metricResponse.AverageTotalUsage);
    //                    jQuery("#AverageEstimatedIrrigationUsage").text(metricResponse.AverageEstimatedIrrigationUsage);
    //                    jQuery("#NumberOfAccountsOverBudget").text(metricResponse.NumberOfAccountsOverBudget);
    //                    jQuery("#PercentOfAccountsOverBudget").text(metricResponse.PercentOfAccountsOverBudget);
    //                    jQuery("#AverageOverBudgetUsage").text(metricResponse.AverageOverBudgetUsage);
    //                    jQuery("#AverageOverBudgetUsageRolling").text(metricResponse.AverageOverBudgetUsageRolling);
    //                    jQuery("#AverageOverBudgetUsageSlope").text(metricResponse.AverageOverBudgetUsageSlope);
    //                    jQuery("#TotalOverBudgetUsage").text(metricResponse.TotalOverBudgetUsage);
    //                    jQuery("#RebateParticipationPercentage").text(metricResponse.RebateParticipationPercentage);
    //                    jQuery("#RebateParticipationPercentageRolling").text(metricResponse.RebateParticipationPercentageRolling);
    //                    jQuery("#RebateParticipationPercentageSlope").text(metricResponse.RebateParticipationPercentageSlope);
    //                    jQuery("#TotalTurfReplacementArea").text(metricResponse.TotalTurfReplacementArea);
    //                });
    //            },
    //        ButtonIcon: "glyphicon glyphicon-calendar monthPickerButtonIcon"
    //        }
    //    );

    //    jQuery(".monthPickerButtonIcon").removeClass("ui-button-icon-primary ui-icon");
    //},

    hide: function () {
        this.parentElement.style.display = "none";
    },

    show: function () {
        this.parentElement.style.display = "block";
    },

    selectNeighborhood: function (properties) {
        this.NeighborhoodID = properties.NetworkCatchmentID;
        this.neptuneMap.neighborhoodDetailShowMobileControl.button.disabled = false;
        
        var now = new Date();
        var year = now.getFullYear();
        // js months are zero-indexed, one of two things in the whole wide world that should not be zero-indexed
        var month = now.getMonth() + 1;

        jQuery("#Area").text(properties.Area.toLocaleString(undefined, { minimumFractionDigits: 1, maximumFractionDigits: 1 }));
        jQuery("#DrainsTo").text(properties.Watershed);

        RemoteService.getMetrics(this.NeighborhoodID, year, month).then(function (metricResponse) {
                        jQuery("#NumberOfReshoaAccounts").text(metricResponse.NumberOfReshoaAccounts);
                        jQuery("#TotalReshoaIrrigatedArea").text(metricResponse.TotalReshoaIrrigatedArea);
                        jQuery("#AverageIrrigatedArea").text(metricResponse.AverageIrrigatedArea);
                        jQuery("#TotalEstimatedReshoaUsers").text(metricResponse.TotalEstimatedReshoaUsers);
                        jQuery("#TotalBudget").text(metricResponse.TotalBudget);
                        jQuery("#TotalOutdoorBudget").text(metricResponse.TotalOutdoorBudget);
                        jQuery("#AverageTotalUsage").text(metricResponse.AverageTotalUsage);
                        jQuery("#AverageEstimatedIrrigationUsage").text(metricResponse.AverageEstimatedIrrigationUsage);
                        jQuery("#NumberOfAccountsOverBudget").text(metricResponse.NumberOfAccountsOverBudget);
                        jQuery("#PercentOfAccountsOverBudget").text(metricResponse.PercentOfAccountsOverBudget);
                        jQuery("#AverageOverBudgetUsage").text(metricResponse.AverageOverBudgetUsage);
                        jQuery("#AverageOverBudgetUsageRolling").text(metricResponse.AverageOverBudgetUsageRolling);
                        jQuery("#AverageOverBudgetUsageSlope").text(metricResponse.AverageOverBudgetUsageSlope);
                        jQuery("#TotalOverBudgetUsage").text(metricResponse.TotalOverBudgetUsage);
                        jQuery("#RebateParticipationPercentage").text(metricResponse.RebateParticipationPercentage);
                        jQuery("#RebateParticipationPercentageRolling").text(metricResponse.RebateParticipationPercentageRolling);
                        jQuery("#RebateParticipationPercentageSlope").text(metricResponse.RebateParticipationPercentageSlope);
                        jQuery("#TotalTurfReplacementArea").text(metricResponse.TotalTurfReplacementArea);
                    });

        this.show();
    }
});

L.Control.NeighborhoodDetailShowMobile = L.Control.extend({
    onAdd: function (map) {
        var button = L.DomUtil.create("button", "btn btn-neptune btn-sm neighborhood-detail-show-mobile");
        button.innerHTML = "<span class='glyphicon glyphicon-info-sign'></span>";
        button.disabled = true;

        L.DomEvent.on(button,
            "click",
            function () {
                jQuery(".neighborhood-detail-control").removeClass("neighborhood-detail-hidden-mobile");
                jQuery(button).addClass("neighborhood-detail-show-hidden-mobile");
            });

        this.button = button;
        window.stopClickPropagation(this.button);
        L.DomEvent.on(this.button,
            "mouseover",
            function () {
                map.dragging.disable();
                map.touchZoom.disable();
                map.doubleClickZoom.disable();
                map.scrollWheelZoom.disable();
            });

        L.DomEvent.on(this.button,
            "mouseout",
            function () {
                map.dragging.enable();
                map.touchZoom.enable();
                map.doubleClickZoom.enable();
                map.scrollWheelZoom.enable();
            });

        jQuery('.neighborhod-detail-hide-button-mobile').on('click',
            function () {
                jQuery(".neighborhood-detail-control").addClass("neighborhood-detail-hidden-mobile");
                jQuery(button).removeClass("neighborhood-detail-show-hidden-mobile");
            });

        return button;
    }
});

L.Control.NominatimSearchControl = L.Control.extend({
    onAdd: function (map) {
        var input = jQuery(".nominatimSearchInput").get(0);
        var searchButton = jQuery(".nominatimSearchButton").get(0);

        this.neptuneMap = this.options.neptuneMap;
        this.parentElement = L.DomUtil.create("div",
            "neighborhood-search-control");
        this.parentElement.append(jQuery("#nominatimSearchWrapper").get(0));
        jQuery("#nominatimSearchWrapper").css("display", "block");

        L.DomEvent.on(searchButton, "click", function () {
            var q = jQuery(".nominatimSearchInput").val();

            RemoteService.nominatimLookup(q);
        });
        L.DomEvent.on(input, "keyup", function (event) {
            if (event.keyCode === 13) {
                var q = jQuery(".nominatimSearchInput").val();

                RemoteService.nominatimLookup(q);
            }
        });
        
        window.stopClickPropagation(this.parentElement);
        return this.parentElement;
    },
});

L.Control.ExplorerTrayControl = L.Control.extend({
    onAdd: function(map) {
        this.parentElement = L.DomUtil.create("div", "explorerTray");

        this.parentElement.innerHTML = "<div class='row'>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'>" +
                            "<a id='animateButton'>" +
                            "<img src='/Areas/DroolTool/Content/chevvy.png' class='img-circle' />" +
                            "</a>" +
                        "</div>" +
                        "<div class='col-sm-8'>Where does my irrigation runoff go? (Start animation)</div>" +
                    "</div>" +
                "</div>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'>" +
                            "<a href='https://www.mnwd.com/rebates/'>" +
                            "<img src='/Areas/DroolTool/Content/piggy.png' class='img-circle' />" +
                            "</a>" +
                        "</div>" +
                        "<div class='col-sm-8'>View rebates and find out about water efficiency</div>" +
                    "</div>" +
                "</div>" +
                "<div class='col-sm-4'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-4'>" +
                            "<a href='https://www.mnwd.com/payment/'>" +
                            "<img src='/Areas/DroolTool/Content/moneywater.png' class='img-circle' />" +
                            "</a>" +
                        "</div>" +
                        "<div class='col-sm-8'>Access my Water Bill (via Moulton Niguel Water District)</div>" +
                    "</div>" +
                "</div>" +
            "</div>";

        window.stopClickPropagation(this.parentElement);
        return this.parentElement;
    },

    getAnimateButtonJ: function() {
        return jQuery('#animateButton');
    }

});

L.control.nominatimSearchControl = function (options) { return new L.Control.NominatimSearchControl(options); };
L.control.neighborhoodDetailControl = function (options) { return new L.Control.NeighborhoodDetailControl(options); };
L.control.explorerTrayControl = function (options) { return new L.Control.ExplorerTrayControl(options); };
L.control.neighborhoodDetailShowMobile = function (options) { return new L.Control.NeighborhoodDetailShowMobile(options); };

NeptuneMaps.DroolToolMap = function (mapInitJson, initialBaseLayerShown, geoServerUrl, config) {
    this.config = config;
    NeptuneMaps.GeoServerMap.call(this, mapInitJson, initialBaseLayerShown, geoServerUrl, { collapseLayerControl: true });
    
    this.neighborhoodLayerWfsParams = this.createWfsParamsWithLayerName("OCStormwater:NetworkCatchments");
    this.backboneLayerWmsParams = this.createWmsParamsWithLayerName("OCStormwater:Backbone");
    this.configureRemoteService();
    this.initializeOverlays();
    this.initializeControls();
    this.initializeMask(mapInitJson.WatershedCoverage);
    
    var self = this;
    this.map.on("click",
        function (evt) {
            if (window.freeze) {
                return;
            }

            var customParams = {
                cql_filter: 'intersects(CatchmentGeometry, POINT(' + evt.latlng.lat + ' ' + evt.latlng.lng + '))'
            };

            L.Util.extend(customParams, self.neighborhoodLayerWfsParams);

            RemoteService.geoserverLookup(self.config.GeoServerUrl, customParams).then(function (geoJsonResponse) {
                if (geoJsonResponse.totalFeatures === 0) {
                    return null;
                }
                self.SelectNeighborhood(geoJsonResponse, evt);
            });

        });
};

NeptuneMaps.DroolToolMap.prototype = Sitka.Methods.clonePrototype(NeptuneMaps.GeoServerMap.prototype);

NeptuneMaps.DroolToolMap.prototype.DisplayStormshedAndBackboneDetail = function (selectedNeighborhoodID) {
    var self = this;

    return RemoteService.getStormshed(selectedNeighborhoodID).then(function (response) {
        var geoJsonResponse = JSON.parse(response);
        
        if (geoJsonResponse.totalFeatures === 0) {
            return null;
        }

        if (self.stormshedLayer) {
            self.map.removeLayer(self.stormshedLayer);
        }

        self.stormshedLayer = L.geoJson(geoJsonResponse,
            {
                style: function(feature) {
                    return {
                        fillColor: "#ffbd38",
                        fill: true,
                        fillOpacity: 0.4,
                        color: "#ffaf0f",
                        weight: 5,
                        stroke: true
                    };
                }
            });
        self.stormshedLayer.addTo(self.map);
        self.stormshedLayer.bringToBack();

        if (self.backboneDetailLayer) {
            self.map.removeLayer(self.backboneDetailLayer);
        }

        var ids = _.map(geoJsonResponse.features,
            function(f) {
                return f.properties.NetworkCatchmentID
            });

        var cql_filter = "NetworkCatchmentID in (" + ids.join(",") + ")";

        var wmsParams = { styles: "backbone_narrow", wmsParameterThatDoesNotExist: Date.now(), pane: "droolToolOverlayPane", cql_filter: cql_filter };

        L.Util.extend(wmsParams, self.backboneLayerWmsParams);

        self.backboneDetailLayer = L.tileLayer.wms(self.config.GeoServerUrl, wmsParams).addTo(self.map);

    });
};

NeptuneMaps.DroolToolMap.prototype.zoomToStormshed = function() {
    if (!this.stormshedLayer) {
        return;
    }

    this.map.fitBounds(Object.values(this.lastSelected._layers)[0].getBounds());
};

NeptuneMaps.DroolToolMap.prototype.setSelectedNeighborhood = function (feature) {
    if (!Sitka.Methods.isUndefinedNullOrEmpty(this.lastSelected)) {
        this.map.removeLayer(this.lastSelected);
    }

    this.lastSelected = L.geoJson(feature,
        {
            style: function (feature) {
                return {
                    fillColor: "#ffad0a",
                    fill: true,
                    fillOpacity: 0.4,
                    color: "#ffff00",
                    weight: 5,
                    stroke: true
                };
            }
        });

    this.lastSelected.addTo(this.map);
    this.lastSelected.bringToFront();
};

NeptuneMaps.DroolToolMap.prototype.SetClickMarker = function(lat, lon) {
    if (this.clickMarker) {
        this.map.removeLayer(this.clickMarker);
    }

    var icon = L.MakiMarkers.icon({
        icon: "marker",
        color: "#FFFF00",
        size: "m"
    });

    this.clickMarker = L.marker({ lat: lat, lon: lon }, {icon:icon});
    this.clickMarker.addTo(this.map);
}

NeptuneMaps.DroolToolMap.prototype.SelectNeighborhood = function (geoJson, evt) {
    this.selectedNeighborhoodID = geoJson.features[0].properties.NetworkCatchmentID;
    if (this.config.NetworkCatchmentsWhereItIsOkayToClickIDs.includes(this.selectedNeighborhoodID)) {
        this.setSelectedNeighborhood(geoJson);
        this.neighborhoodDetailControl.selectNeighborhood(geoJson.features[0].properties);

        if (this.traceLayer) {
            this.traceLayer.remove();
            this.traceLayer = null;
        }

        this.SetClickMarker(evt.latlng.lat, evt.latlng.lng);
        return this.DisplayStormshedAndBackboneDetail(this.selectedNeighborhoodID);
    } else {
        toast(NEIGHBORHOOD_NOT_FOUND);
        return jQuery.Deferred();
    }
};

NeptuneMaps.DroolToolMap.prototype.highlightFlow = function () {
    var self = this;
    RemoteService.getTrace(this.selectedNeighborhoodID, this.config.BackboneTraceUrlTemplate).then(function (response) {
        var backboneFeatureCollection = JSON.parse(response);
        if (self.traceLayer) {
            self.traceLayer.remove();
            self.traceLayer = null;
        }

        self.traceLayer = L.geoJSON(backboneFeatureCollection,
            {
                style: function (feature) {
                    return {
                        color: "#FFFF00",
                        weight: 8,
                        stroke: true
                    };
                }
            });
        self.traceLayer.addTo(self.map);
    });
};

var RemoteService = {
    options: {},

    geoserverLookup: function (geoServerUrl, params) {
        
        return jQuery.ajax({
            url: geoServerUrl + L.Util.getParamString(params),
            method: 'GET'
        });
    },

    getTrace: function(neighborhoodID, urlTemplate) {
        var backboneUrl = new Sitka.UrlTemplate(urlTemplate).ParameterReplace(neighborhoodID);

        return jQuery.ajax({
            url: backboneUrl
        });
    },

    getStormshed: function(neighborhoodID) {
        var stormshedUrl = new Sitka.UrlTemplate(this.options.stormshedUrlTemplate).ParameterReplace(neighborhoodID);

        return jQuery.ajax({
            url: stormshedUrl
        });
    },

    makeNominatimRequestUrl: function (q) {
        var base = "https://open.mapquestapi.com/nominatim/v1/search.php?key=";

        return base +
            this.options.nominatimApiKey +
            "&format=json&q=" +
            q + "&viewbox=-117.82019474260474,33.440338462792681,-117.61081200648763,33.670204787351004" + "&bounded=1";
    },

    nominatimLookup: function(q) {
        var self = this;
        var neptuneMap = self.options.neptuneMap;

        return jQuery.ajax({
            url: RemoteService.makeNominatimRequestUrl(q),
            jsonp: false,
            method: 'GET'
        }).then(function(response) {

            if (response.length === 0) {
                return null;
            }

            // NP/JHB June 2019 deliberate decision not to invest in deciding between multiple results
            var lat = response[0].lat;
            var lon = response[0].lon;
            neptuneMap.SetClickMarker(lat, lon);

            var customParams = {
                cql_filter: 'intersects(CatchmentGeometry, POINT(' + lat + ' ' + lon + '))'
            };

            L.Util.extend(customParams, self.options.neighborhoodLayerWfsParams);

            return RemoteService.geoserverLookup(self.options.geoserverUrl, customParams);
        }).then(function(responseGeoJson) {
            if (!responseGeoJson || responseGeoJson.totalFeatures === 0) {
                toast(NEIGHBORHOOD_NOT_FOUND);
                return;
            }
            neptuneMap.SelectNeighborhood(responseGeoJson).then(function() {
                neptuneMap.zoomToStormshed();
            });
        }).fail(function() {
            toast(NOMINATIM_ERROR);
        });
    },

    getMetrics: function(neighborhoodID, year, month) {
        var metricUrl = new Sitka.UrlTemplate(this.options.metricUrlTemplate).ParameterReplace(neighborhoodID, year, month);
        return jQuery.ajax({
            url: metricUrl,
            method: 'GET'
        });
    }
};

/* Initializers--relatively boring and static*/

NeptuneMaps.DroolToolMap.prototype.configureRemoteService = function () {

    RemoteService.options.nominatimApiKey = this.config.NominatimApiKey;
    RemoteService.options.neighborhoodLayerWfsParams = this.neighborhoodLayerWfsParams;
    RemoteService.options.neptuneMap = this;
    RemoteService.options.geoserverUrl = this.config.GeoServerUrl;
    RemoteService.options.stormshedUrlTemplate = this.config.StormshedUrlTemplate;
    RemoteService.options.metricUrlTemplate = this.config.MetricUrlTemplate;
};

NeptuneMaps.DroolToolMap.prototype.initializeOverlays = function () {
    var droolToolOverlayPane = this.map.createPane("droolToolOverlayPane");
    droolToolOverlayPane.style.zIndex = 10000;
    this.map.getPane("markerPane").style.zIndex = 10001;

    this.neighborhoodLayer =
        this.addWmsLayer("OCStormwater:NetworkCatchments",
            "<span><img src='/Content/img/legendImages/networkCatchment.png' height='12px' style='margin-bottom:3px;' /> Neighborhoods</span>",
            { pane: "droolToolOverlayPane", styles: "neighborhood" },
            true);

    this.backboneLayer =
        this.addWmsLayer("OCStormwater:Backbone",
            "<span><img src='/Content/img/legendImages/backbone.png' height='12px' style='margin-bottom:3px;' /> Streams</span>",
            { pane: "droolToolOverlayPane", styles: "backbone_wide" },
            false);

    this.watershedLayer =
        this.addWmsLayer("OCStormwater:Watersheds",
            "<span><img src='/Content/img/legendImages/backbone.png' height='12px' style='margin-bottom:3px;' /> Watersheds</span>",
            { pane: "droolToolOverlayPane", styles: "watershed" },
            false);

    this.addEsriDynamicLayer("https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
        "<span>Stormwater Network <br/> <img src='/Content/img/legendImages/stormwaterNetwork.png' height='50'/> </span>",
        false);
};

NeptuneMaps.DroolToolMap.prototype.intializeTray = function() {
    this.explorerTrayControl = L.control.explorerTrayControl({
        position: "bottomright",
        neptuneMap: this
    });

    this.explorerTrayControl.addTo(this.map);

    var self = this;
    this.explorerTrayControl.getAnimateButtonJ().on("click",
        function() {
            self.highlightFlow();
        });
};



NeptuneMaps.DroolToolMap.prototype.initializeControls = function () {
    var config = this.config;
    this.nominatimSearchControl = L.control.nominatimSearchControl({
        position: "topleft",
        nominatimApiKey: config.NominatimApiKey,
        geoserverUrl: config.GeoServerUrl,
        wfsParams: this.neighborhoodLayerWfsParams,
        neptuneMap: this
    });

    window.nominatimSearchControl = this.nominatimSearchControl;
    this.nominatimSearchControl.addTo(this.map);

    this.neighborhoodDetailControl = L.control.neighborhoodDetailControl({
        position: "topleft",
        neptuneMap: this
    });

    var watermark = L.control.droolToolWatermark({ position: 'bottomleft' });
    watermark.addTo(this.map);

    this.neighborhoodDetailControl.addTo(this.map);
    //this.neighborhoodDetailControl.wireMonthPicker();
    this.neighborhoodDetailControl.wireHighlightFlowButton();

    this.neighborhoodDetailShowMobileControl = L.control.neighborhoodDetailShowMobile({
        position: "topleft"
    });
    this.neighborhoodDetailShowMobileControl.addTo(this.map);
    

    this.showLocationControl = L.control.showLocationControl({
        position: "topleft"
    });
    this.showLocationControl.addTo(this.map);

    // rearrange controls because leaflet provides the second-coarsest positioning system imaginable.
    jQuery(".leaflet-top.leaflet-right")
        .append(jQuery(".leaflet-control-zoom"));
    jQuery(".leaflet-top.leaflet-right")
        .append(jQuery(".leaflet-control-fullscreen"));

    jQuery(".showLocationWrapper").append(jQuery(".showLocation"));
};

NeptuneMaps.DroolToolMap.prototype.initializeMask = function(watershedCoverageLayer) {
    L.geoJson(watershedCoverageLayer.GeoJsonFeatureCollection,
        {
            invert: true,
            style: function (feature) {
                return {
                    fillColor: "#323232",
                    fill: true,
                    fillOpacity: 0.4,
                    color: "#3388ff",
                    weight: 5,
                    stroke: true
                };
            }
        }).addTo(this.map);
};

/* Utility functions */

function toast(toastText) {
    jQuery.toast({
        top: window.totalHeaderHeight + 8,
        text: toastText,
        hideAfter: 20000,
        stack: 1,
        icon: 'error',
        bgColor: "#707070",
        loaderBg: "#77cfdc"
    });
}

window.stopClickPropagation = function (parentElement) {
    L.DomEvent.on(parentElement, "mouseover", function (e) {
        window.freeze = true;
    });

    L.DomEvent.on(parentElement,
        "mouseout",
        function (e) {
            window.freeze = false;
        });
};