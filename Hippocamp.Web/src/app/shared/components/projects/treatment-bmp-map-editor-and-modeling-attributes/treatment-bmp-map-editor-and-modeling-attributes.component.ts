import { ApplicationRef, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as L from 'leaflet';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet';
import { forkJoin } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DelineationService } from 'src/app/services/delineation.service';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { TreatmentBMPTypeSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-type-simple-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { FieldDefinitionTypeEnum } from 'src/app/shared/models/enums/field-definition-type.enum';
import { TimeOfConcentrationEnum } from 'src/app/shared/models/enums/time-of-concentration.enum';
import { TreatmentBMPModelingType } from 'src/app/shared/models/enums/treatment-bmp-modeling-type.enum';
import { UnderlyingHydrologicSoilGroupEnum } from 'src/app/shared/models/enums/underlying-hydrologic-soil-group.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';

declare var $: any

//This component could use a fair amount of cleanup. It should likely be sent in the treatment bmps and delineations instead of grabbing them itself
@Component({
  selector: 'hippocamp-treatment-bmp-map-editor-and-modeling-attributes',
  templateUrl: './treatment-bmp-map-editor-and-modeling-attributes.component.html',
  styleUrls: ['./treatment-bmp-map-editor-and-modeling-attributes.component.scss']
})
export class TreatmentBmpMapEditorAndModelingAttributesComponent implements OnInit {

  @ViewChild('deleteTreatmentBMPModal') deleteTreatmentBMPModal

  @Input('readOnly') readOnly: boolean = true;
  @Input('includeDelineations') includeDelineations: boolean = false;
  @Input('projectID') projectID: number;

  public mapID: string = 'treatmentBMPMap';
  public visibleTreatmentBMPStyle: string = 'treatmentBMP_purple_outline_only';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public project: ProjectUpsertDto = new ProjectUpsertDto();
  private originalTreatmentBMPs: string;
  private originalDoesNotIncludeTreatmentBMPs: boolean;
  public selectedTreatmentBMPStyle: string = 'treatmentBMP_yellow';
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '400px';
  public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
  public onEachFeatureCallback?: (feature, layer) => void;

  @Output()
  public afterSetControl: EventEmitter<L.Control.Layers> = new EventEmitter();

  @Output()
  public afterLoadMap: EventEmitter<L.LeafletEvent> = new EventEmitter();

  @Output()
  public onMapMoveEnd: EventEmitter<L.LeafletEvent> = new EventEmitter();

  public component: any;
  public map: L.Map;
  public featureLayer: any;
  public layerControl: L.Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  public selectedListItem: number;
  public selectedListItemDetails: { [key: string]: any } = {};
  public selectedObjectMarker: L.Layer;
  public selectedTreatmentBMP: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: L.GeoJSON<any>;
  public delineationsLayer: L.GeoJson<any>;
  private markerIcon = this.buildMarker('/assets/main/map-icons/marker-icon-violet.png', '/assets/main/map-icons/marker-icon-2x-violet.png');
  private markerIconSelected = this.buildMarker('/assets/main/map-icons/marker-icon-selected.png', '/assets/main/map-icons/marker-icon-2x-selected.png');
  private delineationDefaultStyle = {
    color: 'blue',
    fillOpacity: 0.2,
    opacity: 1
  }
  private delineationSelectedStyle = {
    color: 'yellow',
    fillOpacity: 0.2,
    opacity: 1
  }
  public treatmentBMPModelingTypeEnum = TreatmentBMPModelingType;
  public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
  public modelingAttributeDropdownItems: Array<TreatmentBMPModelingAttributeDropdownItemDto>;
  public treatmentBMPTypes: Array<TreatmentBMPTypeSimpleDto>;
  public newTreatmentBMPIndex = -1;
  private modalReference: NgbModalRef;
  public isLoadingSubmit = false;
  public isEditingLocation = false;

  public static modelingAttributesByModelingType = {
    [TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain]:
      [
        'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
        'MediaBedFootprint', 'DesignMediaFiltrationRate', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.BioretentionWithNoUnderdrain]:
      [
        'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
      ],
    [TreatmentBMPModelingType.BioretentionWithUnderdrainAndImperviousLiner]:
      [
        'TotalEffectiveBMPVolume', 'MediaBedFootprint', 'DesignMediaFiltrationRate',
      ],
    [TreatmentBMPModelingType.CisternsForHarvestAndUse]:
      [
        'TotalEffectiveBMPVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
      ],
    [TreatmentBMPModelingType.ConstructedWetland]:
      [
        'PermanentPoolorWetlandVolume', 'DesignResidenceTimeforPermanentPool',
        'WaterQualityDetentionVolume', 'DrawdownTimeforWQDetentionVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
      ],
    [TreatmentBMPModelingType.DryExtendedDetentionBasin]:
      [
        'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
        'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.DryWeatherTreatmentSystems]:
      ['DesignDryWeatherTreatmentCapacity', 'AverageTreatmentFlowrate', 'MonthsOfOperationID'],
    [TreatmentBMPModelingType.Drywell]:
      ['TotalEffectiveDrywellBMPVolume', 'InfiltrationDischargeRate'],
    [TreatmentBMPModelingType.FlowDurationControlBasin]:
      [
        'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
        'EffectiveFootprint', 'DrawdownTimeforWQDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.FlowDurationControlTank]:
      [
        'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
        'EffectiveFootprint', 'DrawdownTimeforWQDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.HydrodynamicSeparator]:
      ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.InfiltrationBasin]:
      [
        'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
      ],
    [TreatmentBMPModelingType.InfiltrationTrench]:
      [
        'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
      ],
    [TreatmentBMPModelingType.LowFlowDiversions]:
      ['DesignLowFlowDiversionCapacity', 'AverageDivertedFlowrate', 'MonthsOfOperationID'],
    [TreatmentBMPModelingType.PermeablePavement]:
      [
        'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
      ],
    [TreatmentBMPModelingType.ProprietaryBiotreatment]:
      ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.ProprietaryTreatmentControl]:
      ['TreatmentRate', 'TimeOfConcentrationID'],
    [TreatmentBMPModelingType.SandFilters]:
      [
        'TotalEffectiveBMPVolume', 'StorageVolumeBelowLowestOutletElevation',
        'EffectiveFootprint', 'DrawdownTimeForDetentionVolume', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.UndergroundInfiltration]:
      [
        'TotalEffectiveBMPVolume', 'InfiltrationSurfaceArea', 'UnderlyingInfiltrationRate'
      ],
    [TreatmentBMPModelingType.VegetatedFilterStrip]:
      [
        'TimeOfConcentrationID', 'TreatmentRate', 'WettedFootprint',
        'EffectiveRetentionDepth', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.VegetatedSwale]:
      [
        'TimeOfConcentrationID', 'TreatmentRate', 'WettedFootprint',
        'EffectiveRetentionDepth', 'UnderlyingHydrologicSoilGroupID'
      ],
    [TreatmentBMPModelingType.WetDetentionBasin]:
      [
        'PermanentPoolorWetlandVolume', 'DesignResidenceTimeforPermanentPool',
        'WaterQualityDetentionVolume', 'DrawdownTimeforWQDetentionVolume', 'WinterHarvestedWaterDemand', 'SummerHarvestedWaterDemand'
      ]
  };

  public static modelingAttributeDisplayUnitsByField = {
    AverageDivertedFlowrate: 'gpd',
    AverageTreatmentFlowrate: 'cfs',
    DesignDryWeatherTreatmentCapacity: 'cfs',
    DesignLowFlowDiversionCapacity: 'gpd',
    DesignMediaFiltrationRate: 'in/hr',
    DesignResidenceTimeforPermanentPool: 'days',
    DiversionRate: 'cfs',
    DrawdownTimeForDetentionVolume: 'hrs',
    DrawdownTimeforWQDetentionVolume: 'hrs',
    EffectiveFootprint: 'sq ft',
    EffectiveRetentionDepth: 'ft',
    InfiltrationDischargeRate: 'cfs',
    InfiltrationSurfaceArea: 'sq ft',
    MediaBedFootprint: 'sq ft',
    PermanentPoolorWetlandVolume: 'cu ft',
    StorageVolumeBelowLowestOutletElevation: 'cu ft',
    SummerHarvestedWaterDemand: 'gpd',
    TotalEffectiveBMPVolume: 'cu ft',
    TotalEffectiveDrywellBMPVolume: 'cu ft',
    TreatmentRate: 'cfs',
    UnderlyingInfiltrationRate: 'in/hr',
    WaterQualityDetentionVolume: 'cu ft',
    WettedFootprint: 'sq ft',
    WinterHarvestedWaterDemand: 'gpd',
    TimeOfConcentrationID: 'mins'
  }

  public static modelingAttributeFieldsWithDropdown = ["TimeOfConcentrationID", "MonthsOfOperationID", "UnderlyingHydrologicSoilGroupID", "DryWeatherFlowOverrideID"];
  public delineations: DelineationUpsertDto[];

  constructor(
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private alertService: AlertService,
    private projectWorkflowService: ProjectWorkflowService
  ) {
  }

  public unsavedChangesCheck() {
    return this.originalDoesNotIncludeTreatmentBMPs == this.project.DoesNotIncludeTreatmentBMPs && this.originalTreatmentBMPs == JSON.stringify(this.treatmentBMPs);
  };

  public ngOnInit(): void {
    if (this.projectID) {

      forkJoin({
        project: this.projectService.getByID(this.projectID),
        treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
        delineations: this.delineationService.getDelineationsByProjectID(this.projectID),
        boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
        treatmentBMPTypes: this.treatmentBMPService.getTypes(),
        modelingAttributeDropdownItems: this.treatmentBMPService.getModelingAttributesDropdownitems()
      }).subscribe(({ project, treatmentBMPs, delineations, boundingBox, treatmentBMPTypes, modelingAttributeDropdownItems }) => {
        this.mapProjectSimpleDtoToProject(project);
        this.originalDoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
        this.treatmentBMPs = treatmentBMPs;
        this.originalTreatmentBMPs = JSON.stringify(treatmentBMPs);
        this.delineations = delineations
        this.boundingBox = boundingBox;
        this.treatmentBMPTypes = treatmentBMPTypes;
        this.modelingAttributeDropdownItems = modelingAttributeDropdownItems;

        if (!this.readOnly || this.readOnly && treatmentBMPs != null && treatmentBMPs.length > 0) {
          this.cdr.detectChanges();
          this.updateMapLayers();
        }
      });
    }

    this.tileLayers = Object.assign({}, {
      "Aerial": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial', maxZoom: 22, maxNativeZoom:18
      }),
      "Street": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Street', maxZoom: 22, maxNativeZoom:18
      }),
      "Terrain": L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain', maxZoom: 22, maxNativeZoom:18
      }),
    }, this.tileLayers);


    let regionalSubbasinsWMSOptions = ({
      layers: "OCStormwater:RegionalSubbasins",
      transparent: true,
      format: "image/png",
      tiled: true
    } as L.WMSOptions);

    let jurisdictionsWMSOptions = ({
      layers: "OCStormwater:Jurisdictions",
      transparent: true,
      format: "image/png",
      tiled: true,
      styles: "jurisdiction_orange"
    } as L.WMSOptions);

    this.overlayLayers = Object.assign({
      "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<span>Stormwater Network <br/> <img src='./assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" })
    }, this.overlayLayers);

    this.compileService.configure(this.appRef);
  }

  ngOnDestroy() {
    this.cdr.detach();
  }

  private mapProjectSimpleDtoToProject(project: ProjectSimpleDto) {
    this.project.ProjectName = project.ProjectName;
    this.project.OrganizationID = project.OrganizationID;
    this.project.StormwaterJurisdictionID = project.StormwaterJurisdictionID;
    this.project.PrimaryContactPersonID = project.PrimaryContactPersonID;
    this.project.ProjectDescription = project.ProjectDescription;
    this.project.AdditionalContactInformation = project.AdditionalContactInformation;
    this.project.DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs
  }

  public updateMapLayers(): void {
    const mapOptions: L.MapOptions = {
      // center: [46.8797, -110],
      // zoom: 6,
      minZoom: 9,
      maxZoom: 22,
      layers: [
        this.tileLayers["Terrain"],
      ],
      fullscreenControl: true
    } as L.MapOptions;
    this.map = L.map(this.mapID, mapOptions);

    this.map.on('load', (event: L.LeafletEvent) => {
      this.afterLoadMap.emit(event);
    });
    this.map.on("moveend", (event: L.LeafletEvent) => {
      this.onMapMoveEnd.emit(event);
    });
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);
    this.updateTreatmentBMPsLayer();
    this.setControl();
    this.registerClickEvents();

    if (this.treatmentBMPs.length > 0) {
      this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
    }
  }

  public updateTreatmentBMPsLayer() {
    if (this.treatmentBMPsLayer) {
      this.map.removeLayer(this.treatmentBMPsLayer);
      this.treatmentBMPsLayer = null;
    }

    if (this.delineationsLayer) {
      this.map.removeLayer(this.delineationsLayer);
      this.delineationsLayer = null;
    }

    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: this.markerIcon })
      },
      filter: (feature) => {
        return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID
      }
    });
    this.treatmentBMPsLayer.addTo(this.map);

    if (this.includeDelineations) {
      const delineationGeoJson = this.mapDelineationsToGeoJson(this.delineations);
      this.delineationsLayer = new L.GeoJSON(delineationGeoJson, {
        style: (feature) => {
          if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != feature.properties.TreatmentBMPID) {
            return this.delineationDefaultStyle;
          }
          return this.delineationSelectedStyle;
        }
      });
      this.delineationsLayer.addTo(this.map);
      if (this.selectedTreatmentBMP != null) {
        this.delineationsLayer.eachLayer(layer => {
          if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
            return;
          }
          layer.bringToFront();
        })
      }

      this.delineationsLayer.on("click", (event: L.LeafletEvent) => {
        if (this.isEditingLocation) {
          return;
        }
        this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
      });
    }

    this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
      if (this.isEditingLocation) {
        return;
      }
      this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
    });
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
        "coordinates": [x.Longitude ?? 0, x.Latitude ?? 0]
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

  private mapDelineationsToGeoJson(delineations: DelineationUpsertDto[]) {
    return {
      type: "FeatureCollection",
      features: delineations.map(x => {
        let delineationGeoJson =
          this.mapDelineationToFeature(x);
        return delineationGeoJson;
      })
    }
  }

  private mapDelineationToFeature(x: DelineationUpsertDto) {
    return {
      "type": "Feature",
      "geometry": x.Geometry != null && x.Geometry != undefined ? JSON.parse(x.Geometry) : null,
      "properties": {
        DelineationID: x.DelineationID,
        TreatmentBMPID: x.TreatmentBMPID
      }
    };
  }

  public setControl(): void {
    this.layerControl = new L.Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false })
      .addTo(this.map);
    this.afterSetControl.emit(this.layerControl);
  }

  public registerClickEvents(): void {
    var leafletControlLayersSelector = ".leaflet-control-layers";
    var closeButtonClass = "leaflet-control-layers-close";

    var closem = L.DomUtil.create("a", closeButtonClass);
    closem.innerHTML = "Close";
    L.DomEvent.on(closem,
      "click",
      function () {
        $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
      });

    $(leafletControlLayersSelector).append(closem);

    this.map.on("click", (event: L.LeafletEvent) => {
      if (!this.isEditingLocation) {
        return;
      }
      if (this.selectedObjectMarker) {
        this.map.removeLayer(this.selectedObjectMarker);
      }
      this.selectedObjectMarker = new L.marker(
        event.latlng,
        { icon: this.markerIconSelected, zIndexOffset: 1000 });

      this.selectedObjectMarker.addTo(this.map);

      this.selectedTreatmentBMP.Latitude = event.latlng.lat;
      this.selectedTreatmentBMP.Longitude = event.latlng.lng;
      this.updateTreatmentBMPsLayer();
    });
  }

  public selectTreatmentBMP(treatmentBMPID: number) {
    this.isEditingLocation = false;
    this.selectTreatmentBMPImpl(treatmentBMPID);
    this.updateTreatmentBMPsLayer();
  }

  private selectTreatmentBMPImpl(treatmentBMPID: number) {
    this.clearSelectedItem();

    this.selectedListItem = treatmentBMPID;
    let selectedNumber = null;
    let selectedAttributes = null;
    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    selectedAttributes = [
      `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
      `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
      `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`
    ];

    if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
      this.selectedObjectMarker = new L.marker(
        { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
        { icon: this.markerIconSelected, zIndexOffset: 1000 });

      this.selectedObjectMarker.addTo(this.map);
      this.selectedListItemDetails.title = `${selectedNumber}`;
      this.selectedListItemDetails.attributes = selectedAttributes;
    }
  }

  public getModelingAttributeFieldsToDisplay(treatmentBMPModelingTypeID: number): Array<string> {
    return TreatmentBmpMapEditorAndModelingAttributesComponent.modelingAttributesByModelingType[treatmentBMPModelingTypeID] ?? [];
  }

  public getModelingAttributeDisplayUnitsByField(fieldName: string): string {
    return TreatmentBmpMapEditorAndModelingAttributesComponent.modelingAttributeDisplayUnitsByField[fieldName] ?? '';
  }

  public getTypeNameByTypeID(typeID: number) {
    return (this.treatmentBMPTypes.find(x => x.TreatmentBMPTypeID == typeID).TreatmentBMPTypeName) ?? -1;
  }

  public getModelingAttributeDropdownItemsByFieldName(fieldName: string): Array<TreatmentBMPModelingAttributeDropdownItemDto> {
    return this.modelingAttributeDropdownItems.filter(x => x.FieldName == fieldName);
  }

  public getDropdownItemNameByFieldNameAndItemID(fieldName: string, itemID: number): string {
    const dropdownItem = this.modelingAttributeDropdownItems.find(x => x.FieldName == fieldName && x.ItemID == itemID);
    return dropdownItem ? dropdownItem.ItemName : '';
  }

  public isFieldWithDropdown(fieldName: string): boolean {
    return TreatmentBmpMapEditorAndModelingAttributesComponent.modelingAttributeFieldsWithDropdown.indexOf(fieldName) > -1;
  }

  public updateModelingTypeOnTypeChange(selectedType: TreatmentBMPTypeSimpleDto) {
    if (selectedType) {
      this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = selectedType.TreatmentBMPModelingTypeID;
    }
  }

  public toggleIsEditingLocation() {
    this.isEditingLocation = !this.isEditingLocation;
    this.updateTreatmentBMPsLayer();
  }

  private launchModal(modalContent: any, modalTitle: string): void {
    this.modalReference = this.modalService.open(
      modalContent,
      { ariaLabelledBy: modalTitle, backdrop: 'static', keyboard: false }
    );
  }

  public onDelete() {
    this.launchModal(this.deleteTreatmentBMPModal, 'deleteTreatmentBMPModalTitle');
  }

  public deleteTreatmentBMP() {
    const index = this.treatmentBMPs.indexOf(this.selectedTreatmentBMP);
    this.treatmentBMPs.splice(index, 1);
    this.modalReference.close();

    this.selectedTreatmentBMP = null;
    this.clearSelectedItem();
    if (this.treatmentBMPs.length > 0) {
      this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
    }
  }

  private clearSelectedItem() {
    if (this.selectedListItem) {
      this.selectedListItem = null;
      this.selectedListItemDetails = {};
      if (this.selectedObjectMarker) {
        this.map.removeLayer(this.selectedObjectMarker);
      }
      this.selectedObjectMarker = null;
    }
  }

  public addTreatmentBMP() {
    var newTreatmentBMP = new TreatmentBMPUpsertDto;
    newTreatmentBMP.TreatmentBMPID = this.newTreatmentBMPIndex;
    this.newTreatmentBMPIndex--;

    newTreatmentBMP.TimeOfConcentrationID = TimeOfConcentrationEnum.FiveMinutes;
    newTreatmentBMP.UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroupEnum.D;

    this.treatmentBMPs.push(newTreatmentBMP);
    this.selectTreatmentBMP(newTreatmentBMP.TreatmentBMPID);
    document.getElementById(this.mapID).scrollIntoView();

    this.isEditingLocation = true;
  }

  public treatmentBMPHasDelineation(treatmentBMPID: number) {
    return this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0] != null;
  }

  public getDelineationAreaForTreatmentBMP(treatmentBMPID: number) {
    let delineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];

    if (delineation?.DelineationArea == null) {
      return "Not provided yet"
    }

    return `${delineation.DelineationArea} ac`;
  }

  public onSubmit() {
    this.isLoadingSubmit = true;
    this.alertService.clearAlerts();
    this.project.DoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs && (this.treatmentBMPs == null || this.treatmentBMPs.length == 0);

    this.projectService.updateProject(this.projectID, this.project).subscribe(() => {
      this.treatmentBMPService.mergeTreatmentBMPs(this.treatmentBMPs, this.projectID).subscribe(() => {
        this.isLoadingSubmit = false;
        this.alertService.pushAlert(new Alert('Your Treatment BMP changes have been saved.', AlertContext.Success, true));
        this.projectWorkflowService.emitWorkflowUpdate();
        window.scroll(0, 0);
        this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID).subscribe(treatmentBMPs => {
          this.treatmentBMPs = treatmentBMPs;
          this.originalTreatmentBMPs = JSON.stringify(treatmentBMPs);
          this.originalDoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs;
          if (this.treatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
          }
        })
      }, error => {
        this.isLoadingSubmit = false;
        window.scroll(0, 0);
        this.cdr.detectChanges();
      });
    }, error => {
      this.isLoadingSubmit = false;
      window.scroll(0, 0);
      this.cdr.detectChanges();
    });
  }

}
