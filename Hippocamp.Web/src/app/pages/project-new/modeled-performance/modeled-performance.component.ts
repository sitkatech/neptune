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
import { DelineationService } from 'src/app/services/delineation.service';
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

  private watchUserChangeSubscription: any;
  private currentUser: PersonDto;
  public ModeledPerformanceDisplayTypeEnum = ModeledPerformanceDisplayTypeEnum;
  public activeID = ModeledPerformanceDisplayTypeEnum.Total;
  public modelingSelectListOptions: {TreatmentBMPID: number, TreatmentBMPName: string}[] = [];
  public treatmentBMPIDForSelectedModelResults = 0;
  public modeledResults: Array<TreatmentBMPModeledResultSimpleDto>;
  public selectedModelResults: TreatmentBMPModeledResultSimpleDto;
  public treatmentBMPHRUCharacteristicSummaries: Array<TreatmentBMPHRUCharacteristicsSummarySimpleDto>;
  public selectedTreatmentBMPHRUCharacteristicSummaries: Array<TreatmentBMPHRUCharacteristicsSummarySimpleDto>;
  public selectedTreatmentBMPHRUCharacteristicSummaryTotal: TreatmentBMPHRUCharacteristicsSummarySimpleDto = {
    LandUse: "Total",
    Area: 0,
    ImperviousCover: 0
  };
  public projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[];
  public ProjectNetworkHistoryStatusTypeEnum = ProjectNetworkSolveHistoryStatusTypeEnum;
  
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
  public customRichTextTypeID = CustomRichTextType.TreatmentBMPs;

  constructor(
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private route: ActivatedRoute,
    private alertService: AlertService,
  ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        forkJoin({
          treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
          delineations: this.delineationService.getDelineationsByProjectID(this.projectID),
          boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
          projectNetworkSolveHistories:  this.projectService.getNetworkSolveHistoriesForProject(this.projectID)
        }).subscribe(({ treatmentBMPs, delineations, boundingBox, projectNetworkSolveHistories }) => {
          this.treatmentBMPs = treatmentBMPs;
          this.delineations = delineations;
          this.boundingBox = boundingBox;
          this.projectNetworkSolveHistories = projectNetworkSolveHistories;
          if (this.projectNetworkSolveHistories != null && 
              this.projectNetworkSolveHistories != undefined && 
              this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded).length > 0) 
              {
                forkJoin({
                  modeledResults : this.projectService.getModeledResultsForProject(this.projectID),
                  treatmentBMPHRUCharacteristicSummaries : this.projectService.getTreatmentBMPHRUCharacteristicSummariesForProject(this.projectID)
                })
                .subscribe(({modeledResults, treatmentBMPHRUCharacteristicSummaries}) => {
                  this.modeledResults = modeledResults;
                  this.treatmentBMPHRUCharacteristicSummaries = treatmentBMPHRUCharacteristicSummaries;
                  this.populateModeledResultsOptions();
                  this.updateSelectedModelResults();
                });
              }

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
    this.projectService.triggerNetworkSolveForProject(this.projectID).subscribe(results => {
      this.alertService.pushAlert(new Alert('Model run was successfully started and will run in the background.', AlertContext.Success, true));
      window.scroll(0, 0);
      this.projectService.getNetworkSolveHistoriesForProject(this.projectID).subscribe(result => {
        this.projectNetworkSolveHistories = result;
      })
    },
    error => {
      window.scroll(0,0);
      this.cdr.detectChanges();
    })
  }

  populateModeledResultsOptions() {
    var tempOptions = [];
    tempOptions.push({TreatmentBMPID: 0, TreatmentBMPName :"All Treatment BMPs"});
    this.modeledResults.forEach(x => {
      var treatmentBMP = this.treatmentBMPs.filter(y => y.TreatmentBMPID == x.TreatmentBMPID)[0];
      tempOptions.push({TreatmentBMPID: treatmentBMP.TreatmentBMPID, TreatmentBMPName : treatmentBMP.TreatmentBMPName});
    });

    this.modelingSelectListOptions = [...this.modelingSelectListOptions, ...tempOptions];
  }

  updateSelectedModelResults() {
    if (this.treatmentBMPIDForSelectedModelResults != 0) {
      this.selectedModelResults = this.modeledResults.filter(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedModelResults)[0];
      this.selectedTreatmentBMPHRUCharacteristicSummaries = this.treatmentBMPHRUCharacteristicSummaries.filter(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedModelResults).sort((a, b) => { if (a.LandUse > b.LandUse) {return 1;} if (b.LandUse > a.LandUse) {return -1;} return 0});
      this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();
      return;
    }

    this.selectedModelResults = new TreatmentBMPModeledResultSimpleDto();
    //We get the property names of the first one so we have a fully populated object because Typescript doesn't always populate the keys which is VERY annoying
    for (let key of Object.getOwnPropertyNames(this.modeledResults[0])) {
      this.selectedModelResults[key] = this.modeledResults.reduce((sum, current) => sum + (current[key] ?? 0), 0);
    }
    
    this.selectedTreatmentBMPHRUCharacteristicSummaries = [...this.treatmentBMPHRUCharacteristicSummaries.reduce((r, o) => {
      const key = o.LandUse;
      
      const item = r.get(key) || Object.assign({}, o, {
        Area: 0,
        ImperviousCover: 0
      });
      
      item.Area += o.Area;
      item.ImperviousCover += o.ImperviousCover;
    
      return r.set(key, item);
    }, new Map).values()].sort((a, b) => { if (a.LandUse > b.LandUse) {return 1;} if (b.LandUse > a.LandUse) {return -1;} return 0});
    this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();
    
  }

  updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal() {
    this.selectedTreatmentBMPHRUCharacteristicSummaryTotal.Area = this.selectedTreatmentBMPHRUCharacteristicSummaries.reduce((sum, current) => sum + current.Area, 0);
    this.selectedTreatmentBMPHRUCharacteristicSummaryTotal.ImperviousCover = this.selectedTreatmentBMPHRUCharacteristicSummaries.reduce((sum, current) => sum + current.ImperviousCover, 0);
  }

  //Helps to prevent keyvalue pipe from trying to do sorting
  returnZero(): number {
    return 0;
  }

  getModelResultsLastCalculatedText() : string {
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

  isMostRecentHistoryOfType(type: ProjectNetworkSolveHistoryStatusTypeEnum) : boolean {
    return this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories[0].ProjectNetworkSolveHistoryStatusTypeID == type;
  }

  getNotFullyParameterizedBMPNames() : string[] {
    return this.treatmentBMPs.filter(x => !x.IsFullyParameterized).map(x => x.TreatmentBMPName);
  }

  getBMPNamesForDelineationsWithDiscrepancies() : string[] {
    if (this.delineations == null || this.delineations.length == 0) {
      return null;
    }

    var treatmentBMPIDsForDelineationsWithDiscrepancies = this.delineations.filter(x => x.HasDiscrepancies).map(x => x.TreatmentBMPID);
    
    if (treatmentBMPIDsForDelineationsWithDiscrepancies == null || treatmentBMPIDsForDelineationsWithDiscrepancies.length == 0) {
      return null;
    }

    return this.treatmentBMPs.filter(x => treatmentBMPIDsForDelineationsWithDiscrepancies.includes(x.TreatmentBMPID)).map(x => x.TreatmentBMPName);
  }

}

export enum ModeledPerformanceDisplayTypeEnum
{
  Total = "Total",
  Dry = "Dry",
  Wet = "Wet"
}
