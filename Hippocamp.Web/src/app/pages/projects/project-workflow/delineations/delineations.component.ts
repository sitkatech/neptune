import { ApplicationRef, ChangeDetectorRef, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as L from 'leaflet';
import 'leaflet-draw';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet'
import { GestureHandling } from "leaflet-gesture-handling";
import { forkJoin } from 'rxjs';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';
import { DelineationTypeEnum } from 'src/app/shared/models/enums/delineation-type.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { MarkerHelper } from 'src/app/shared/helpers/marker-helper';
import { WfsService } from 'src/app/shared/services/wfs.service';

declare var $: any

@Component({
  selector: 'hippocamp-delineations',
  templateUrl: './delineations.component.html',
  styleUrls: ['./delineations.component.scss']
})
export class DelineationsComponent implements OnInit {

  @ViewChild('mapDiv') mapDiv: ElementRef;
  public mapID: string = 'delineationsMap';
  public drawMapClicked: boolean = false;
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public delineations: DelineationUpsertDto[];
  private originalDelineations: string;
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '500px';
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
  public mapInitComplete: boolean = false;
  public featureLayer: any;
  public delineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();
  public editableDelineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();
  private preStartEditingEditableDelineationFeatureGroup: string;
  public layerControl: L.Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  private squareMetersToAcreDivisor: number = 4047;
  public selectedListItem: number;
  public selectedObjectMarker: L.Layer;
  public selectedDelineation: DelineationUpsertDto;
  public selectedTreatmentBMP: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: L.GeoJSON<any>;
  public selectedListItemDetails: { [key: string]: any } = {};
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
  private delineationTransparentStyle = {
    fillOpacity: 0,
    opacity: 0
  }
  public projectID: number;
  public customRichTextTypeID = CustomRichTextType.Delineations;

  private isPerformingDrawAction: boolean = false;
  private defaultDrawControlSpec: L.Control.DrawConstructorOptions = {
    polyline: false,
    rectangle: false,
    circle: false,
    marker: false,
    circlemarker: false,
    polygon: {
      allowIntersection: false, // Restricts shapes to simple polygons
      drawError: {
        color: '#E1E100', // Color the shape will turn when intersects
        message: 'Self-intersecting polygons are not allowed.' // Message that will show when intersect
      }
    }
  }
  private defaultEditControlSpec: L.Control.DrawConstructorOptions = {
    featureGroup: this.editableDelineationFeatureGroup,
    remove: true,
    poly: {
      allowIntersection: false, // Restricts shapes to simple polygons
      drawError: {
        color: '#E1E100', // Color the shape will turn when intersects
        message: 'Self-intersecting polygons are not allowed.' // Message that will show when intersect
      }
    }
  }
  private drawControl: L.Control.Draw;
  private newDelineationID: number = -1;
  public isLoadingSubmit: boolean = false;
  public isEditingLocation = false;

  constructor(
    private treatmentBMPService: TreatmentBMPService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private cdr: ChangeDetectorRef,
    private projectWorkflowService: ProjectWorkflowService,
    private projectService: ProjectService,
    private wfsService: WfsService
  ) {
  }

  canExit(){
    let currentDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
    if (this.isPerformingDrawAction) {
      return this.originalDelineations == currentDelineations && this.preStartEditingEditableDelineationFeatureGroup == JSON.stringify(this.editableDelineationFeatureGroup.getLayers().map(x => x.getLatLngs));
    }

    return this.originalDelineations == currentDelineations;
  };

  public ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);

      forkJoin({
        project: this.projectService.getByID(this.projectID),
        treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
        delineations: this.projectService.getDelineationsByProjectID(this.projectID),
        boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
      }).subscribe(({ project, treatmentBMPs, delineations, boundingBox }) => {
        // redirect to review step if project is shared with OCTA grant program
        if (project.ShareOCTAM2Tier2Scores) {
          this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
        }
        if (treatmentBMPs.length == 0) {
          this.router.navigateByUrl(`/projects/edit/${this.projectID}`);
        }

        this.treatmentBMPs = treatmentBMPs;
        this.delineations = delineations;
        this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
        this.boundingBox = boundingBox;

        this.initializeMap();
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
      cql_filter: "DelineationStatus = 'Verified'"
    } as L.WMSOptions);

    this.overlayLayers = Object.assign({
      "<span>Stormwater Network <br/> <img src='../../assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" }),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<img src='./assets/main/map-legend-images/wqmpBoundary.png' style='height:12px; margin-bottom:4px'> WQMPs": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", WQMPsWMSOptions),
      "<span>Delineations (Verified) </br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", verifiedDelineationsWMSOptions)
    }, this.overlayLayers);

    this.wfsService.getRegionalSubbasins().subscribe(response => {
      let layer = L.geoJson(response, {
          pointToLayer: function (feature, latlng) {
              return L.circleMarker(latlng, {});
          },
          style: function (feature) {
              return {
                  stroke: true,
                  color: "#000",
                  radius: 5,
                  weight: 1,
                  opacity: 1,
                  fillOpacity: 0.0
              }
          },
          onEachFeature: (feature, layer) => {
              var popupContent = `RSB ID: <a href = "${this.ocstBaseUrl()}/RegionalSubbasin/Detail/${feature.properties.RegionalSubbasinID.toString()}">${feature.properties.RegionalSubbasinID}</a>`
              layer.bindPopup(popupContent);
              
          }
      })
      this.layerControl.addOverlay(layer, "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins")
    });

    this.compileService.configure(this.appRef);
  }

  ngOnDestroy() {
    this.cdr.detach();
  }

  public initializeMap(): void {
    this.mapInitComplete = false;
    const mapOptions: L.MapOptions = {
      minZoom: 9,
      maxZoom: 22,
      layers: [
        this.tileLayers["Terrain"],
      ],
      fullscreenControl: true
    } as L.MapOptions;
    this.map = L.map(this.mapID, mapOptions);
    L.Map.addInitHook("addHandler", "gestureHandling", GestureHandling);

    this.map.on('load', (event: L.LeafletEvent) => {
      this.afterLoadMap.emit(event);
    });
    this.map.on("moveend", (event: L.LeafletEvent) => {
      this.onMapMoveEnd.emit(event);
    });
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);

    this.delineationFeatureGroup.addTo(this.map);
    this.editableDelineationFeatureGroup.addTo(this.map);
    this.resetDelineationFeatureGroups();

    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker })
      }
    });
    this.selectTreatmentBMP(this.treatmentBMPs[0].TreatmentBMPID);
    this.treatmentBMPsLayer.addTo(this.map);
    this.setControl();
    this.registerClickEvents();

    if (this.treatmentBMPs.length > 0) {
      this.selectFeatureImpl(this.treatmentBMPs[0].TreatmentBMPID);
    }

    this.mapInitComplete = true;
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

  public addFeatureCollectionToFeatureGroup(featureJsons: any, featureGroup: L.FeatureGroup) {
    L.geoJson(featureJsons, {
      onEachFeature: (feature, layer) => {
        this.addLayersToFeatureGroup(layer, featureGroup);
        layer.on('click', (e) => {
          if (this.isEditingLocation) {
            return;
          }
          this.selectFeatureImpl(feature.properties.TreatmentBMPID);
        })
      }
    });
  };

  private addLayersToFeatureGroup(layer: any, featureGroup: L.FeatureGroup) {
    if (layer.getLayers) {
      layer.getLayers().forEach((l) => {
        featureGroup.addLayer(l);
      });
    } else {
      featureGroup.addLayer(layer);
    }
  }

  public addOrRemoveDrawControl(turnOn: boolean) {
    if (turnOn) {
      var drawOptions = {
        draw: Object.assign({}, this.defaultDrawControlSpec),
        edit: Object.assign({}, this.defaultEditControlSpec)
      };
      if (this.selectedDelineation?.Geometry == null || this.drawMapClicked) {
        drawOptions.edit = false;
      }
      else {
        drawOptions.draw = false;
        if (this.selectedDelineation.DelineationTypeID == DelineationTypeEnum.Centralized) {
          drawOptions.edit.edit = false;
        }
      }
      this.drawControl = new L.Control.Draw(drawOptions);
      this.drawControl.addTo(this.map);
      return;
    }
    this.drawControl.remove();
  }

  public setControl(): void {
    this.layerControl = new L.Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false })
      .addTo(this.map);

    L.EditToolbar.Delete.include({
      removeAllLayers: false
    });

    this.map
      .on(L.Draw.Event.CREATED, (event) => {
        this.isPerformingDrawAction = false;
        const layer = (event as L.DrawEvents.Created).layer;
        var delineationUpsertDto = this.delineations.filter(x => this.selectedTreatmentBMP.TreatmentBMPID == x.TreatmentBMPID)[0];
        if (delineationUpsertDto == null) {
          delineationUpsertDto = new DelineationUpsertDto({
            DelineationID: this.newDelineationID,
            TreatmentBMPID: this.selectedTreatmentBMP.TreatmentBMPID
          });
          this.delineations = this.delineations.concat(delineationUpsertDto);
          this.newDelineationID--;
        }
        delineationUpsertDto.DelineationTypeID = DelineationTypeEnum.Distributed;
        delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON().geometry);
        delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / this.squareMetersToAcreDivisor).toFixed(2);
        this.resetDelineationFeatureGroups();
        this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
      })
      .on(L.Draw.Event.EDITED, (event) => {
        this.isPerformingDrawAction = false;
        const layers = (event as L.DrawEvents.Edited).layers;
        layers.eachLayer((layer) => {
          var delineationUpsertDto = this.delineations.filter(x => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID)[0];
          delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON().geometry);
          delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / this.squareMetersToAcreDivisor).toFixed(2);
        });
        this.resetDelineationFeatureGroups();
        this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
      })
      .on(L.Draw.Event.DELETED, (event) => {
        this.isPerformingDrawAction = false;
        const layers = (event as L.DrawEvents.Deleted).layers;
        layers.eachLayer((layer) => {
          var delineationUpsertDto = this.delineations.filter(x => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID)[0];
          delineationUpsertDto.Geometry = null;
          delineationUpsertDto.DelineationArea = null;
        });
        this.resetDelineationFeatureGroups();
        this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
      })
      .on(L.Draw.Event.DRAWSTART, () => {
        if (this.selectedDelineation != null && this.selectedDelineation.DelineationTypeID == DelineationTypeEnum.Centralized) {
          this.editableDelineationFeatureGroup.clearLayers();
        };
      })
      .on(L.Draw.Event.TOOLBAROPENED, () => {
        this.isPerformingDrawAction = true;
        this.preStartEditingEditableDelineationFeatureGroup = JSON.stringify(this.editableDelineationFeatureGroup.getLayers().map(x => x.getLatLngs()));
      })
      .on(L.Draw.Event.TOOLBARCLOSED, () => {
        this.isPerformingDrawAction = false;
        this.preStartEditingEditableDelineationFeatureGroup = "";
      });
    this.addOrRemoveDrawControl(true);
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

    this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
      this.selectFeatureImpl(event.propagatedFrom.feature.properties.TreatmentBMPID);
    });
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

  private selectFeatureImpl(treatmentBMPID: number) {
    if (this.isPerformingDrawAction) {
      return;
    }

    let hasFlownToSelectedObject = false;

    this.drawControl.remove();
    if (this.selectedListItem) {
      this.selectedListItem = null;
    }

    if (this.selectedDelineation) {
      this.editableDelineationFeatureGroup.clearLayers();
      this.selectedDelineation = null;
    }

    this.selectedDelineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.delineationFeatureGroup.eachLayer(layer => {
      if (this.selectedDelineation == null || this.selectedDelineation.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setStyle(this.delineationDefaultStyle);
        return;
      }
      this.addFeatureCollectionToFeatureGroup(layer.toGeoJSON(), this.editableDelineationFeatureGroup);
      this.editableDelineationFeatureGroup.eachLayer(l => {
        l.setStyle(this.delineationSelectedStyle).bringToFront();
      });
      layer.setStyle(this.delineationTransparentStyle);
      if (!this.mapInitComplete) {
        this.map.fitBounds(layer.getBounds(), {padding: new L.Point(50,50)})
      }
      else {
        this.map.flyToBounds(layer.getBounds(),{padding: new L.Point(50,50)});
      }
      hasFlownToSelectedObject = true;
    })

    this.selectedListItem = treatmentBMPID;
    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];

    this.treatmentBMPsLayer?.eachLayer(layer => {
      if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(MarkerHelper.treatmentBMPMarker).setZIndexOffset(1000);
        return;
      }
      layer.setIcon(MarkerHelper.selectedMarker);
      layer.setZIndexOffset(10000);
      if (!hasFlownToSelectedObject) {
        if (!this.mapInitComplete) {
          this.map.setView(layer.getLatLng(), 18);
        }
        else {
          this.map.flyTo(layer.getLatLng(), 18);
        }
      }
    })
    this.addOrRemoveDrawControl(true);

    if (this.drawMapClicked) {
      if (this.isEditingLocation) {
        return;
      }
      if (this.selectedDelineation?.Geometry != null && this.selectedDelineation?.DelineationTypeID != DelineationTypeEnum.Centralized) {
        $('.leaflet-draw-edit-edit').get(0).click();
      } else {
        $('.leaflet-draw-draw-polygon').get(0).click();
      }
    }
    this.drawMapClicked = false;
  }

  public treatmentBMPHasDelineationGeometry(treatmentBMPID: number) {
    return this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0]?.Geometry;
  }

  public getDelineationAreaForTreatmentBMP(treatmentBMPID: number) {
    let delineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];

    if (delineation?.DelineationArea == null) {
      return "Not provided yet"
    }

    return `${delineation.DelineationArea} ac`;
  }

  public drawDelineationForTreatmentBMP(treatmentBMPID: number) {
    this.drawMapClicked = true;
    this.scrollToMapIfNecessary();

    if (this.delineations.some(x => x.TreatmentBMPID == treatmentBMPID)) {
      return;
    }

    var newDelineation = new DelineationUpsertDto({
      DelineationTypeID: DelineationTypeEnum.Distributed,
      TreatmentBMPID: treatmentBMPID
    });

    this.delineations = this.delineations.concat(newDelineation);
  }

  private scrollToMapIfNecessary() {
    const rect = this.mapDiv.nativeElement.getBoundingClientRect();
    if (rect.top < 0 || rect.bottom > window.innerHeight) {
      this.mapDiv.nativeElement.scrollIntoView();
    }
  }

  public onSubmit(continueToNextStep?: boolean) {
    this.isLoadingSubmit = true;
    this.alertService.clearAlerts();
    this.getFullyQualifiedJSONGeometryForDelineations(this.delineations);
    var updatedDelineations = this.delineations.filter(x => x.Geometry != null);
    forkJoin({
      delineations: this.projectService.mergeDelineationsByProjectID(updatedDelineations, this.projectID),
      treatmentBMPs: this.treatmentBMPService.mergeTreatmentBMPs(this.treatmentBMPs, this.projectID)
    }).subscribe(({delineations, treatmentBMPs}) => {
      window.scroll(0, 0); 
      this.isLoadingSubmit = false;
      this.projectWorkflowService.emitWorkflowUpdate();
      this.projectService.getDelineationsByProjectID(this.projectID).subscribe(delineations => {
        this.delineations = delineations;
        this.originalDelineations = JSON.stringify(this.mapDelineationsToGeoJson(this.delineations));
        this.resetDelineationFeatureGroups();
        this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);

        if (continueToNextStep) {
          this.router.navigateByUrl(`/projects/edit/${this.projectID}/stormwater-treatments/modeled-performance-and-metrics`).then(x => {
            this.alertService.pushAlert(new Alert('Your Delineation changes have been saved.', AlertContext.Success, true));
          });
          return;
        }
  
        this.alertService.pushAlert(new Alert('Your Delineation changes have been saved.', AlertContext.Success, true));
      });
    }, error => {
      this.isLoadingSubmit = false;
      window.scroll(0,0);
      this.cdr.detectChanges();
    });
  }

  public getFullyQualifiedJSONGeometryForDelineations(delineations: DelineationUpsertDto[]) {
    //We need a fully qualified geojson string and above we are just getting the geometry
    //Possible can remove the update above if we are always going to do it here
    this.delineationFeatureGroup.eachLayer((layer) => {
      var delineationUpsertDto = delineations.filter(x => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID)[0];
      delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON());
    });
  }

  private resetDelineationFeatureGroups() {
    this.editableDelineationFeatureGroup.clearLayers();
    this.preStartEditingEditableDelineationFeatureGroup = "";
    this.delineationFeatureGroup.clearLayers();
    this.addFeatureCollectionToFeatureGroup(this.mapDelineationsToGeoJson(this.delineations), this.delineationFeatureGroup);
  }

  public getUpstreamRSBCatchmentForTreatmentBMP(treatmentBMPID: number) {
    this.treatmentBMPService.getUpstreamRSBCatchmentGeoJSON(treatmentBMPID).subscribe(result => {
      let currentDelineationForTreatmentBMP = this.delineations.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
      if (currentDelineationForTreatmentBMP == null) {
        currentDelineationForTreatmentBMP = new DelineationUpsertDto({
          TreatmentBMPID: treatmentBMPID
        });
        this.delineations = this.delineations.concat(currentDelineationForTreatmentBMP);
      }
      currentDelineationForTreatmentBMP.DelineationTypeID = DelineationTypeEnum.Centralized;
      currentDelineationForTreatmentBMP.Geometry = result.GeometryGeoJSON;
      currentDelineationForTreatmentBMP.DelineationArea = result.Area;
      this.resetDelineationFeatureGroups();
      this.scrollToMapIfNecessary();
      this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
    })
  }
  public toggleIsEditingLocation() {
    if (this.isEditingLocation){
      this.onSubmit();
    }
    this.isEditingLocation = !this.isEditingLocation;
    this.updateTreatmentBMPsLayer();
  }
  public treatmentBMPHasDelineation(treatmentBMPID: number) {
    return this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0] != null;
  }
  public updateTreatmentBMPsLayer() {
    if (this.treatmentBMPsLayer) {
      this.map.removeLayer(this.treatmentBMPsLayer);
      this.treatmentBMPsLayer = null;
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
          if (this.selectedTreatmentBMP != null) {
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
        { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

      this.selectedObjectMarker.addTo(this.map);
      this.selectedListItemDetails.title = `${selectedNumber}`;
      this.selectedListItemDetails.attributes = selectedAttributes;
    }
  }
  public ocstBaseUrl(): string {
    return environment.ocStormwaterToolsBaseUrl
  }
}
