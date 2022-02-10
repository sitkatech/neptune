import { ApplicationRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import * as L from 'leaflet';
import {
  Control, FitBoundsOptions,
  GeoJSON,
  marker,
  map,
  Map,
  MapOptions,
  tileLayer,
  geoJSON,
  icon,
  latLng,
  Layer,
  DomEvent,
  DomUtil,
  WMSOptions,
  LeafletEvent} from 'leaflet';
import 'leaflet.fullscreen';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';
import { Feature } from 'geojson';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { ActivatedRoute } from '@angular/router';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { forkJoin } from 'rxjs';
import { TreatmentBMPTypeSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-type-simple-dto';
import { TreatmentBMPModelingType } from 'src/app/shared/models/enums/treatment-bmp-modeling-type.enum';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { environment } from 'src/environments/environment';

declare var $: any

@Component({
  selector: 'hippocamp-treatment-bmps',
  templateUrl: './treatment-bmps.component.html',
  styleUrls: ['./treatment-bmps.component.scss']
})
export class TreatmentBmpsComponent implements OnInit {

  public mapID: string = 'poolDetailMap';
  public visibleTreatmentBMPStyle: string = 'treatmentBMP_purple_outline_only';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public selectedTreatmentBMPStyle: string = 'treatmentBMP_yellow';
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '400px';
  public defaultFitBoundsOptions?: FitBoundsOptions = null;
  public onEachFeatureCallback?: (feature, layer) => void;

  @Output()
  public afterSetControl: EventEmitter<Control.Layers> = new EventEmitter();

  @Output()
  public afterLoadMap: EventEmitter<LeafletEvent> = new EventEmitter();

  @Output()
  public onMapMoveEnd: EventEmitter<LeafletEvent> = new EventEmitter();

  public component: any;
  public map: Map;
  public featureLayer: any;
  public layerControl: Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  public selectedListItem: number;
  public selectedListItemDetails: { [key: string]: any } = {};
  public selectedObjectLayer: Layer;
  public selectedTreatmentBMP: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: GeoJSON<any>;
  private markerIcon = this.buildMarker('/assets/main/map-icons/marker-icon-violet.png', '/assets/main/map-icons/marker-icon-2x-violet.png');
  private markerIconSelected = this.buildMarker('/assets/main/map-icons/marker-icon-selected.png', '/assets/main/map-icons/marker-icon-2x-selected.png');

  public projectID: number;
  public customRichTextTypeID = CustomRichTextType.TreatmentBMPs;
  public treatmentBMPModelingTypeEnum = TreatmentBMPModelingType;
  public modelingAttributeDropdownItems: Array<TreatmentBMPModelingAttributeDropdownItemDto>;
  public treatmentBMPTypes: Array<TreatmentBMPTypeSimpleDto>;
  public treatmentBMPModel: TreatmentBMPUpsertDto;
  public isLoadingSubmit = false;

  public static modelingAttributesByModelingType = {
    [TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
      'MediaBedFootprint', 'DesignMediaFiltrationRate', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.BioretentionWithNoUnderdrain]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
    ],
    [TreatmentBMPModelingType.BioretentionWithUnderdrainAndImperviousLiner]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'MediaBedFootprint', 'DesignMediaFiltrationRate',
    ],
    [TreatmentBMPModelingType.CisternsForHarvestAndUse]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
    ],
    [TreatmentBMPModelingType.ConstructedWetland]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'PermanentPoolorWetlandVolume', 'DesignResidenceTimeforPermanentPool',
      'WaterQualityDetentionVolume', 'DrawdownTimeforWQDetentionVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
    ],
    [TreatmentBMPModelingType.DryExtendedDetentionBasin]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
      'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.DryWeatherTreatmentSystems]: 
    ['DesignDryWeatherTreatmentCapacity', 'AverageTreatmentFlowrate', 'MonthsOfOperationID'],
    [TreatmentBMPModelingType.Drywell]: 
    ['RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveDrywellBMPVolume', 'InfiltrationDischargeRate'],
    [TreatmentBMPModelingType.FlowDurationControlBasin]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
      'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.FlowDurationControlTank]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
      'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.HydrodynamicSeparator]: 
    ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.InfiltrationBasin]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
    ],
    [TreatmentBMPModelingType.InfiltrationTrench]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
    ],
    [TreatmentBMPModelingType.LowFlowDiversions]: 
    ['DesignLowFlowDiversionCapacity', 'AverageDivertedFlowrate', 'MonthsOfOperationID'],
    [TreatmentBMPModelingType.PermeablePavement]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
    ],
    [TreatmentBMPModelingType.ProprietaryBiotreatment]: 
    ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.ProprietaryTreatmentControl]: 
    ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.SandFilters]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
      'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.UndergroundInfiltration]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
    ],
    [TreatmentBMPModelingType.VegetatedFilterStrip]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TreatmentRate', 'WettedFootprint',
      'EffectiveRetentionDepth', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.VegetatedSwale]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'TreatmentRate', 'WettedFootprint',
      'EffectiveRetentionDepth', 'UnderlyingHydrologicSoilGroupID'
    ],
    [TreatmentBMPModelingType.WetDetentionBasin]: 
    [
      'RoutingConfigurationID', 'DiversionRate', 'TimeOfConcentrationID', 'PermanentPoolorWetlandVolume', 'DesignResidenceTimeforPermanentPool',
      'WaterQualityDetentionVolume', 'DrawdownTimeforWQDetentionVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
    ]
  };

  public static modelingAttributeFieldsDisplayNames = {
    AverageDivertedFlowrate: 'Average Diverted Flowrate',
    AverageTreatmentFlowrate: 'Average Treatment Flowrate',
    DesignDryWeatherTreatmentCapacity: 'Design Dry Weather Treatment Capacity',
    DesignLowFlowDiversionCapacity: 'Design Low Flow Diversion Capacity',
    DesignMediaFiltrationRate: 'Design Media Filtration Rate',
    DesignResidenceTimeforPermanentPool: 'Design Residence Time for Permanent Pool',
    DiversionRate: 'Diversion Rate',
    DrawdownTimeforWQDetentionVolume: 'Drawdown Time for WQ Detention Volume',
    EffectiveFootprint: 'Effective Footprint',
    EffectiveRetentionDepth: 'Effective Retention Depth',
    InfiltrationDischargeRate: 'Infiltration Discharge Rate',
    InfiltrationSurfaceArea: 'Infiltration Surface Area',
    MediaBedFootprint: 'Media Bed Footprint',
    PermanentPoolorWetlandVolume: 'Permanent Pool or Wetland Volume',
    StorageVolumeBelowLowestOutletElevation: 'Storage Volume Below Lowest Outlet Elevation',
    SummerHarvestedWaterDemand: 'Summer Harvested Water Demand',
    DrawdownTimeForDetentionVolume: 'Drawdown Time for Detention Volume',
    TotalEffectiveBMPVolume: 'Total Effective BMP Volume',
    TotalEffectiveDrywellBMPVolume: 'Total Effective Drywell BMP Volume',
    TreatmentRate: 'Treatment Rate',
    UnderlyingInfiltrationRate: 'Underlying Infiltration Rate',
    WaterQualityDetentionVolume: 'Water Quality Detention Volume',
    WettedFootprint: 'Wetted Footprint',
    WinterHarvestedWaterDemand: 'Winter Harvested Water Demand',
    TimeOfConcentrationID: "Time of Concentration",
    RoutingConfigurationID: "Routing Configuration",
    MonthsOfOperationID: "Months of Operation",
    UnderlyingHydrologicSoilGroupID: "Underlying Hydrologic Soil Group",
    DryWeatherFlowOverrideID: "Dry Weather Flow Override"
  };

  public static modelingAttributeFieldsWithDropdown = [
    "TimeOfConcentrationID", "RoutingConfigurationID", "MonthsOfOperationID", "UnderlyingHydrologicSoilGroupID", "DryWeatherFlowOverrideID"
  ];

  constructor(
    private treatmentBMPService: TreatmentBMPService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private route: ActivatedRoute,
  ) {
  }

  public ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);

      forkJoin({
        treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
        boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
        treatmentBMPTypes: this.treatmentBMPService.getTypes(),
        modelingAttributeDropdownItems: this.treatmentBMPService.getModelingAttributesDropdownitems()
      }).subscribe(({treatmentBMPs, boundingBox, treatmentBMPTypes, modelingAttributeDropdownItems}) => {
        this.treatmentBMPs = treatmentBMPs;
        this.boundingBox = boundingBox;
        this.treatmentBMPTypes = treatmentBMPTypes;
        this.modelingAttributeDropdownItems = modelingAttributeDropdownItems;

        this.updateMapLayers();
      });
    }

    this.tileLayers = Object.assign({}, {
      "Aerial": tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Street": tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Terrain": tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain',
      }),
    }, this.tileLayers);


    let regionalSubbasinsWMSOptions = ({
      layers: "OCStormwater:RegionalSubbasins",
      transparent: true,
      format: "image/png",
      tiled: true
    } as WMSOptions);

    this.overlayLayers = Object.assign({
        "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions)
    }, this.overlayLayers);

    let jurisdictionsWMSOptions = ({
      layers: "OCStormwater:Jurisdictions",
      transparent: true,
      format: "image/png",
      tiled: true
    } as WMSOptions);

    this.overlayLayers = Object.assign({
        "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions)
    }, this.overlayLayers);

    this.compileService.configure(this.appRef);
  }

  public updateMapLayers(): void {

    const mapOptions: MapOptions = {
      // center: [46.8797, -110],
      // zoom: 6,
      minZoom: 9,
      maxZoom: 17,
      layers: [
        this.tileLayers["Aerial"],
      ],
      fullscreenControl: true
    } as MapOptions;
    this.map = map(this.mapID, mapOptions);

    this.map.on('load', (event: LeafletEvent) => {
      this.afterLoadMap.emit(event);
    });
    this.map.on("moveend", (event: LeafletEvent) => {
      this.onMapMoveEnd.emit(event);
    });
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);
    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: function (feature, latlng) {
        return marker(latlng, { icon: this.markerIcon})
      }.bind(this)
    });
    this.treatmentBMPsLayer.addTo(this.map);
    this.setControl();
    this.registerClickEvents();
    
    if (this.treatmentBMPs.length > 0) {
      this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
    }
  }

  public buildMarker(iconUrl: string, iconRetinaUrl: string): any {
    const shadowUrl = 'assets/marker-shadow.png';
    return L.icon({
      iconRetinaUrl,
      iconUrl,
      shadowUrl,
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });
  }

  private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPUpsertDto[]) {
    return {
      type: "FeatureCollection",
      features: treatmentBMPs.map(x => {
        let treatmentBMPGeoJson = 
        this.mapTreatmentBMPToFeature(x);
        return treatmentBMPGeoJson;
      })  
    }
  }

  private mapTreatmentBMPToFeature(x: TreatmentBMPUpsertDto) {
    return {
      "type": "Feature",
      "geometry": {
        "type": "Point",
        "coordinates": [x.Longitude, x.Latitude]
      },
      "properties": {
        TreatmentBMPID: x.TreatmentBMPID,
        TreatmentBMPName: x.TreatmentBMPName,
        TreatmentBMPTypeName: x.TreatmentBMPTypeName,
        Latitude: x.Latitude,
        Longitude: x.Longitude
      }
    };
  }

  public setControl(): void {
    this.layerControl = new Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false })
      .addTo(this.map);
    this.afterSetControl.emit(this.layerControl);
  }

  public registerClickEvents(): void {
    var leafletControlLayersSelector = ".leaflet-control-layers";
    var closeButtonClass = "leaflet-control-layers-close";

    var closem = DomUtil.create("a", closeButtonClass);
    closem.innerHTML = "Close";
    DomEvent.on(closem,
      "click",
      function () {
        $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
      });

    $(leafletControlLayersSelector).append(closem);

    this.treatmentBMPsLayer.on("click", (event: LeafletEvent) => {
      this.selectTreatmentBMPImpl(event.propagatedFrom.feature);
    })
  }

  public selectTreatmentBMP(treatmentBMPID: number) {
    const feature = this.mapTreatmentBMPToFeature(this.treatmentBMPs.find(x => x.TreatmentBMPID === treatmentBMPID));
    this.selectTreatmentBMPImpl(feature);
  }

  private selectTreatmentBMPImpl(feature: any) {
    const treatmentBMPID = feature.properties.TreatmentBMPID;
    if (this.selectedListItem) {
      this.selectedListItem = null;
      this.selectedListItemDetails = {};
      this.map.removeLayer(this.selectedObjectLayer);
      this.selectedObjectLayer = null;
    }
    this.selectedListItem = treatmentBMPID;
    let selectedNumber = null;
    let selectedAttributes = null;
    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    selectedAttributes = [
      `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
      `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
      `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`
    ];

    if (this.selectedTreatmentBMP) {
      this.selectedObjectLayer = new L.GeoJSON(feature, {
        pointToLayer: (feature, latlng) => {
          return L.marker(latlng, { icon: this.markerIconSelected, zIndexOffset: 1000 });
        }
      });;
      this.selectedObjectLayer.addTo(this.map).bringToFront();
      this.selectedListItemDetails.title = `${selectedNumber}`;
      this.selectedListItemDetails.attributes = selectedAttributes;
    }

    this.treatmentBMPModel = Object.assign({}, this.selectedTreatmentBMP);
  }

  public getModelingAttributeFieldsToDisplay(treatmentBMPModelingTypeID: number): Array<string> {
    return TreatmentBmpsComponent.modelingAttributesByModelingType[treatmentBMPModelingTypeID];
  }

  public getFieldDisplayName(fieldName: string) {
    return TreatmentBmpsComponent.modelingAttributeFieldsDisplayNames[fieldName];
  }

  public getModelingAttributeDropdownItemsByFieldName(fieldName: string): Array<TreatmentBMPModelingAttributeDropdownItemDto> {
    return this.modelingAttributeDropdownItems.filter(x => x.FieldName == fieldName);
  }

  public isFieldWithDropdown(fieldName: string): boolean {
    return TreatmentBmpsComponent.modelingAttributeFieldsWithDropdown.indexOf(fieldName) > -1;
  }

  public updateModelingTypeOnTypeChange(selectedType: TreatmentBMPTypeSimpleDto) {
    this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = selectedType.TreatmentBMPModelingTypeID;
  }

  public onSubmit() {
    this.isLoadingSubmit = true;
    this.treatmentBMPService.mergeTreatmentBMPs(this.treatmentBMPs, this.projectID).subscribe(() => {
      this.isLoadingSubmit = false;
    });
  }
}