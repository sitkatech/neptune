import { ApplicationRef, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as L from 'leaflet';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet';
import { forkJoin } from 'rxjs';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';
import { MarkerHelper } from 'src/app/shared/helpers/marker-helper';
import { Router } from '@angular/router';
import { WfsService } from 'src/app/shared/services/wfs.service';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { StormwaterJurisdictionService } from 'src/app/shared/generated/api/stormwater-jurisdiction.service';
import { TreatmentBMPService } from 'src/app/shared/generated/api/treatment-bmp.service';
import { FieldDefinitionTypeEnum } from 'src/app/shared/generated/enum/field-definition-type-enum';
import { TimeOfConcentrationEnum } from 'src/app/shared/generated/enum/time-of-concentration-enum';
import { TreatmentBMPModelingTypeEnum } from 'src/app/shared/generated/enum/treatment-b-m-p-modeling-type-enum';
import { UnderlyingHydrologicSoilGroupEnum } from 'src/app/shared/generated/enum/underlying-hydrologic-soil-group-enum';
import { TreatmentBMPTypeWithModelingAttributesDto } from 'src/app/shared/generated/model/treatment-bmp-type-with-modeling-attributes-dto';
import { TreatmentBMPModelingAttributeDefinitionDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-definition-dto';

declare var $: any

//This component could use a fair amount of cleanup. It should likely be sent in the treatment bmps and delineations instead of grabbing them itself
@Component({
  selector: 'hippocamp-treatment-bmp-map-editor-and-modeling-attributes',
  templateUrl: './treatment-bmp-map-editor-and-modeling-attributes.component.html',
  styleUrls: ['./treatment-bmp-map-editor-and-modeling-attributes.component.scss']
})
export class TreatmentBmpMapEditorAndModelingAttributesComponent implements OnInit {

  @ViewChild('deleteTreatmentBMPModal') deleteTreatmentBMPModal
  @ViewChild('editTreatmentBMPTypeModal') editTreatmentBMPTypeModal

  @Input('readOnly') readOnly: boolean = true;
  @Input('includeDelineations') includeDelineations: boolean = false;
  @Input('zoomToProjectExtentOnLoad') zoomToProjectExtentOnLoad: boolean = false;
  @Input('zoomOnSelection') zoomOnSelection: boolean = false;
  @Input('projectID') projectID: number;

  public mapID: string = 'treatmentBMPMap';
  public visibleTreatmentBMPStyle: string = 'treatmentBMP_purple_outline_only';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public project: ProjectUpsertDto = new ProjectUpsertDto();
  private originalTreatmentBMPs: string;
  private originalDoesNotIncludeTreatmentBMPs: boolean;
  public selectedTreatmentBMPStyle: string = 'treatmentBMP_yellow';
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '750px';
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
  public selectedTreatmentBMPType: number;
  public treatmentBMPsLayer: L.GeoJSON<any>;
  public delineationsLayer: L.GeoJson<any>;
  public newTreatmentBMPType: TreatmentBMPTypeWithModelingAttributesDto;
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
  public treatmentBMPModelingTypeEnum = TreatmentBMPModelingTypeEnum;
  public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
  public modelingAttributeDropdownItems: Array<TreatmentBMPModelingAttributeDropdownItemDto>;
  public treatmentBMPTypes: Array<TreatmentBMPTypeWithModelingAttributesDto>;
  public newTreatmentBMPIndex = -1;
  private modalReference: NgbModalRef;
  public isLoadingSubmit = false;
  public isEditingLocation = false;

  public static modelingAttributeFieldsWithDropdown = ["TimeOfConcentrationID", "MonthsOfOperationID", "UnderlyingHydrologicSoilGroupID", "DryWeatherFlowOverrideID"];
  public delineations: DelineationUpsertDto[];

  constructor(
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService,
    private treatmentBMPService: TreatmentBMPService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private modalService: NgbModal,
    private alertService: AlertService,
    private projectWorkflowService: ProjectWorkflowService,
    private router: Router,
    private wfsService: WfsService
  ) {
  }

  public unsavedChangesCheck() {
    return this.originalDoesNotIncludeTreatmentBMPs == this.project.DoesNotIncludeTreatmentBMPs && this.originalTreatmentBMPs == JSON.stringify(this.treatmentBMPs);
  };

  public ngOnInit(): void {
    if (this.projectID) {

      forkJoin({
        project: this.projectService.projectsProjectIDGet(this.projectID),
        treatmentBMPs: this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID),
        delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
        boundingBox: this.stormwaterJurisdictionService.jurisdictionsProjectIDGetBoundingBoxByProjectIDGet(this.projectID),
        treatmentBMPTypes: this.treatmentBMPService.treatmentBMPTypesGet(),
        modelingAttributeDropdownItems: this.treatmentBMPService.treatmentBMPModelingAttributeDropdownItemsGet()
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
        attribution: 'Aerial', maxZoom: 22, maxNativeZoom: 18
      }),
      "Street": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Street', maxZoom: 22, maxNativeZoom: 18
      }),
      "Terrain": L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain', maxZoom: 22, maxNativeZoom: 18
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

    let WQMPsWMSOptions = ({
      layers: "OCStormwater:WaterQualityManagementPlans",
      transparent: true,
      format: "image/png",
      tiled: true
    } as L.WMSOptions);

    let verifiedDelineationsWMSOptions = ({
      layers: "OCStormwater:Delineations",
      transparent: true,
      format: "image/png",
      tiled: true,
      cql_filter: "DelineationStatus = 'Verified' AND IsAnalyzedInModelingModule = 1"
    } as L.WMSOptions);


    this.overlayLayers = Object.assign({
      "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions),
      "<span>Stormwater Network <br/> <img src='./assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" }),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<img src='./assets/main/map-legend-images/wqmpBoundary.png' style='height:12px; margin-bottom:4px'> WQMPs": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", WQMPsWMSOptions),
      "<span>Delineations (Verified) </br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", verifiedDelineationsWMSOptions)
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
    this.project.DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
    this.project.CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores;
    this.project.ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores;
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

    if (this.zoomToProjectExtentOnLoad) {
      let tempFeatureGroup = new L.FeatureGroup([this.treatmentBMPsLayer, this.delineationsLayer]);
      this.map.fitBounds(tempFeatureGroup.getBounds(), { padding: new L.Point(50, 50) });
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
    let hasFlownToSelectedObject = false;

    if (this.includeDelineations) {
      const delineationGeoJson = this.mapDelineationsToGeoJson(this.delineations);
      this.delineationsLayer = new L.GeoJSON(delineationGeoJson, {
        style: (feature) => {
          if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != feature.properties.TreatmentBMPID) {
            return this.delineationDefaultStyle;
          }
          return this.delineationSelectedStyle;
        },
        onEachFeature: (feature, layer) => {
          if (this.selectedTreatmentBMP != null) {
            if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
              return;
            }
            layer.bringToFront();
            if (this.zoomOnSelection) {
              this.map.flyToBounds(layer.getBounds(), { padding: new L.Point(50, 50) });
              hasFlownToSelectedObject = true;
            }
          }
        }
      });
      this.delineationsLayer.addTo(this.map);

      this.delineationsLayer.on("click", (event: L.LeafletEvent) => {
        if (this.isEditingLocation) {
          return;
        }
        this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
      });
    }

    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
      this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
        pointToLayer: (feature, latlng) => {
          return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker })
        },
        filter: (feature) => {
          return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID
        },
        onEachFeature: (feature, layer) => {
          if (this.selectedTreatmentBMP != null && this.zoomOnSelection && !hasFlownToSelectedObject) {
            if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
              return;
            }
            this.map.flyTo(layer.getLatLng(), 18);
          }
        }
      });
      this.treatmentBMPsLayer.addTo(this.map);

    this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
      if (this.isEditingLocation) {
        return;
      }
      this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
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
    return delineations.map(x => JSON.parse(x.Geometry));
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
        { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

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
    this.selectedTreatmentBMPType = this.selectedTreatmentBMP.TreatmentBMPTypeID;
    selectedAttributes = [
      `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
      `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
      `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`
    ];

    if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
      this.selectedObjectMarker = new L.marker(
        { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
        { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

      this.selectedObjectMarker.addTo(this.map);
      this.selectedListItemDetails.title = `${selectedNumber}`;
      this.selectedListItemDetails.attributes = selectedAttributes;
    }
  }

  public getModelingAttributeFieldsToDisplay(treatmentBMPTypeID: number): Array<TreatmentBMPModelingAttributeDefinitionDto> {
    return (this.treatmentBMPTypes.find(x => x.TreatmentBMPTypeID == treatmentBMPTypeID).TreatmentBMPModelingAttributes) ?? [];
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

  public updateModelingTypeOnTypeChange(selectedType: TreatmentBMPTypeWithModelingAttributesDto) {
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

  public onEditTreatmentBMPTypes() {
    this.launchModal(this.editTreatmentBMPTypeModal, 'editTreatmentBMPTypeModalTitle');
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

  public changeTreatmentBMPType(treatmentBMPType: number){
    this.treatmentBMPService.treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(this.selectedTreatmentBMP.TreatmentBMPID, treatmentBMPType).subscribe((temp) => {
      this.modalReference.close();
      this.selectedTreatmentBMP.TreatmentBMPTypeID = treatmentBMPType;
      this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = temp;
      this.originalTreatmentBMPs = JSON.stringify(this.treatmentBMPs);
    })
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

  public onSubmit(continueToNextStep?: boolean) {
    this.isLoadingSubmit = true;
    this.alertService.clearAlerts();
    this.project.DoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs && (this.treatmentBMPs == null || this.treatmentBMPs.length == 0);

    this.projectService.projectsProjectIDUpdatePost(this.projectID, this.project).subscribe(() => {
      this.treatmentBMPService.treatmentBMPsProjectIDPut(this.projectID, this.treatmentBMPs).subscribe(() => {
        this.isLoadingSubmit = false;
        this.projectWorkflowService.emitWorkflowUpdate();
        this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID).subscribe(treatmentBMPs => {
          this.treatmentBMPs = treatmentBMPs;
          this.originalTreatmentBMPs = JSON.stringify(treatmentBMPs);
          this.originalDoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs;
          if (this.treatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
          }

          if (continueToNextStep) {
            const rerouteURL = this.treatmentBMPs.length > 0 ? 
              `projects/edit/${this.projectID}/stormwater-treatments/delineations` : `projects/edit/${this.projectID}/attachments`;
            this.router.navigateByUrl(rerouteURL).then(x => {
              this.alertService.pushAlert(new Alert('Your Treatment BMP changes have been saved.', AlertContext.Success));
            });
            return;
          }
          this.alertService.pushAlert(new Alert('Your Treatment BMP changes have been saved.', AlertContext.Success));
          window.scroll(0, 0);
        }, error => {
          this.isLoadingSubmit = false;
          window.scroll(0, 0);
          this.cdr.detectChanges();
        });
        }, error => {
          this.isLoadingSubmit = false;
          window.scroll(0,0);
          this.cdr.detectChanges();
        });
    }, error => {
      this.isLoadingSubmit = false;
      window.scroll(0, 0);
      this.cdr.detectChanges();
    });
  }

  public ocstBaseUrl(): string {
    return environment.ocStormwaterToolsBaseUrl
  }

}
