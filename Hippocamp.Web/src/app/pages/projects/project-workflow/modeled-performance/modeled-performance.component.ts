import { ApplicationRef, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { BoundingBoxDto, DelineationUpsertDto, PersonDto, ProjectNetworkSolveHistorySimpleDto, TreatmentBMPModeledResultSimpleDto, TreatmentBMPUpsertDto, TreatmentBMPHRUCharacteristicsSummarySimpleDto } from 'src/app/shared/generated/model/models';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import * as L from 'leaflet';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet';
import { GestureHandling } from "leaflet-gesture-handling";
import { forkJoin } from 'rxjs';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { environment } from 'src/environments/environment';
import { ProjectNetworkSolveHistoryStatusTypeEnum } from 'src/app/shared/models/enums/project-network-solve-history-status-type.enum';

declare var $: any

@Component({
  selector: 'hippocamp-modeled-performance',
  templateUrl: './modeled-performance.component.html',
  styleUrls: ['./modeled-performance.component.scss']
})
export class ModeledPerformanceComponent implements OnInit {
  private currentUser: PersonDto;
  public ProjectNetworkHistoryStatusTypeEnum = ProjectNetworkSolveHistoryStatusTypeEnum;
  public projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[];
  
  @ViewChild('mapDiv') mapDiv: ElementRef;
  public mapID: string = 'modeledPerformanceMap';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public delineations: DelineationUpsertDto[];
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '400px';
  public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
  public onEachFeatureCallback?: (feature, layer) => void;
  public map: L.Map;
  public layerControl: L.Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  public selectedTreatmentBMP: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: L.GeoJSON<any>;
  public delineationsLayer: L.GeoJSON<any>;
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

  public projectID: number;
  public customRichTextTypeID = CustomRichTextType.ModeledPerformance;

  constructor(
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private treatmentBMPService: TreatmentBMPService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private route: ActivatedRoute,
    private alertService: AlertService,
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        forkJoin({
          treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
          delineations: this.projectService.getDelineationsByProjectID(this.projectID),
          boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
          projectNetworkSolveHistories:  this.projectService.getNetworkSolveHistoriesByProjectID(this.projectID)
        }).subscribe(({ treatmentBMPs, delineations, boundingBox, projectNetworkSolveHistories }) => {
          this.treatmentBMPs = treatmentBMPs;
          this.delineations = delineations;
          this.boundingBox = boundingBox;
          this.projectNetworkSolveHistories = projectNetworkSolveHistories;
          this.initializeMap();
        });
      }
    });

    this.tileLayers = Object.assign({}, {
      "Aerial": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Street": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Terrain": L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain',
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

    let verifiedDelineationsWMSOptions = ({
      layers: "OCStormwater:Delineations",
      transparent: true,
      format: "image/png",
      tiled: true,
      cql_filter: "DelineationStatus = 'Verified'"
    } as L.WMSOptions);

    this.overlayLayers = Object.assign({
      "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions),
      "<span>Stormwater Network <br/> <img src='../../assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" }),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<span>Delineations (Verified) </br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", verifiedDelineationsWMSOptions)
    }, this.overlayLayers);

    this.compileService.configure(this.appRef);
  }

  public getAboutModelingPerformanceURL(): string {
    return `${environment.ocStormwaterToolsBaseUrl}/Home/AboutModelingBMPPerformance`;
  }

  public initializeMap(): void {

    const mapOptions: L.MapOptions = {
      minZoom: 9,
      maxZoom: 18,
      layers: [
        this.tileLayers["Terrain"],
      ],
      fullscreenControl: true,
      gestureHandling: true
    } as L.MapOptions;
    this.map = L.map(this.mapID, mapOptions);
    L.Map.addInitHook("addHandler", "gestureHandling", GestureHandling);

    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);

    const delineationGeoJson = this.mapDelineationsToGeoJson(this.delineations);
    this.delineationsLayer = new L.GeoJSON(delineationGeoJson, {
      onEachFeature: (feature, layer) => {
        layer.setStyle(this.delineationDefaultStyle);
        layer.on('click', (e) => {
          this.selectFeatureImpl(feature.properties.TreatmentBMPID);
        })
      }
    })
    this.delineationsLayer.addTo(this.map);

    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: this.markerIcon })
      },
      onEachFeature: (feature, layer) => {
        layer.on('click', (e) => {
          this.selectFeatureImpl(feature.properties.TreatmentBMPID);
        })
      }
    });
    this.treatmentBMPsLayer.addTo(this.map);
    this.setControl();
    this.registerClickEvents();
  }

  public setControl(): void {
    this.layerControl = new L.Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false })
      .addTo(this.map);
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
    $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");

    this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
      this.selectFeatureImpl(event.propagatedFrom.feature.properties.TreatmentBMPID);
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

  private selectFeatureImpl(treatmentBMPID: number) {
    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.treatmentBMPsLayer.eachLayer(layer => {
      if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(this.markerIcon).setZIndexOffset(1000);
        return;
      }
      layer.setIcon(this.markerIconSelected);
      layer.setZIndexOffset(10000);
      this.map.panTo(layer.getLatLng());
    })
    this.delineationsLayer?.eachLayer(layer => {
      if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setStyle(this.delineationDefaultStyle);
        return;
      }
      layer.setStyle(this.delineationSelectedStyle);
    })

    //this.selectedListItem = treatmentBMPID;
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

  getDelineationAcreageForTreatmentBMP(treatmentBMPID: number) : string {
    let delineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    if (delineation == null) {
      return "Not provided";
    }

    return `${delineation.DelineationArea} ac`;
  }

  triggerModelRunForProject(): void {
    this.projectService.triggerNetworkSolveByProjectID(this.projectID).subscribe(results => {
      this.alertService.pushAlert(new Alert('Model run was successfully started and will run in the background.', AlertContext.Success, true));
      window.scroll(0, 0);
      this.projectService.getNetworkSolveHistoriesByProjectID(this.projectID).subscribe(result => {
        this.projectNetworkSolveHistories = result;
      })
    },
    error => {
      window.scroll(0,0);
      this.cdr.detectChanges();
    })
  }

  showModelResultsPanel(): boolean {
    return this.projectID &&
      this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories.length > 0 && 
      this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded).length > 0;
  }

  getModelResultsLastCalculatedText(): string {
    if (this.projectNetworkSolveHistories == null || this.projectNetworkSolveHistories == undefined || this.projectNetworkSolveHistories.length == 0) {
      return "";
    }

    //These will be ordered by date by the api
    var successfulResults = this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded);

    if (successfulResults == null || successfulResults.length == 0) {
      return "";
    }

    return `Results last calculated at ${new Date(successfulResults[0].LastUpdated).toLocaleString()}`
  }

  isMostRecentHistoryOfType(type: ProjectNetworkSolveHistoryStatusTypeEnum): boolean {
    return this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories.length > 0 && this.projectNetworkSolveHistories[0].ProjectNetworkSolveHistoryStatusTypeID == type;
  }
}
