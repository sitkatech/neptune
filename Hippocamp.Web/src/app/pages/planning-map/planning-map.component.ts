import { ApplicationRef, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import * as L from 'leaflet';
import 'leaflet-gesture-handling';
import 'leaflet.fullscreen';
import 'leaflet.marker.highlight';
import 'leaflet-loading';
import * as esri from 'esri-leaflet';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DelineationService } from 'src/app/services/delineation.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { BoundingBoxDto, DelineationSimpleDto, TreatmentBMPDisplayDto } from 'src/app/shared/generated/model/models';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { MarkerHelper } from 'src/app/shared/helpers/marker-helper';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { PrioritizationMetric } from 'src/app/shared/models/prioritization-metric';
import { WfsService } from 'src/app/shared/services/wfs.service';
import { OctaPrioritizationDetailPopupComponent } from 'src/app/shared/components/octa-prioritization-detail-popup/octa-prioritization-detail-popup.component';
import { ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';

declare var $: any;

@Component({
  selector: 'hippocamp-planning-map',
  templateUrl: './planning-map.component.html',
  styleUrls: ['./planning-map.component.scss']
})
export class PlanningMapComponent implements OnInit {

  private currentUser: PersonDto;
  public richTextTypeID = CustomRichTextType.PlanningMap;

  public projects: Array<ProjectSimpleDto>;
  private treatmentBMPs: Array<TreatmentBMPDisplayDto>;
  private delineations: Array<DelineationSimpleDto>;
  public selectedTreatmentBMP: TreatmentBMPDisplayDto;
  public relatedTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
  public selectedDelineation: DelineationSimpleDto;
  public selectedProject: ProjectSimpleDto;

  public mapID: string = 'planningMap';
  public mapHeight = (window.innerHeight - (window.innerHeight * 0.2)) + "px";
  public map: L.Map;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  public plannedProjectTreatmentBMPsLayer: L.GeoJSON<any>;
  public selectedProjectDelineationsLayer: L.GeoJSON<any>;
  private boundingBox: BoundingBoxDto;
  public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
  public layerControl: L.Control.Layers;

  //this is needed to allow binding to the static class
  public PrioritizationMetric = PrioritizationMetric;
  public prioritizationMetrics = Object.values(PrioritizationMetric);
  public selectedPrioritizationMetric = PrioritizationMetric.NoMetric;
  public prioritizationMetricOverlayLayer: L.Layers;

  private delineationSelectedStyle = {
    color: 'yellow',
    fillOpacity: 0.2,
    opacity: 1
  }

  private viewInitialized: boolean = false;

  public columnDefs: ColDef[];
  public defaultColDef: ColDef;

  constructor(
    private authenticationService: AuthenticationService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService,
    private wfsService: WfsService,
    private utilityFunctionsService: UtilityFunctionsService
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      this.currentUser = result;
      this.stormwaterJurisdictionService.getBoundingBoxByLoggedInPerson().subscribe(result => {
        this.boundingBox = result;
        forkJoin({
          projects: this.projectService.getProjectsByPersonID(),
          treatmentBMPs: this.treatmentBMPService.getTreatmentBMPs(),
          delineations: this.delineationService.getDelineations()
        }).subscribe(({ projects, treatmentBMPs, delineations }) => {
          this.projects = projects;
          this.treatmentBMPs = treatmentBMPs;
          this.addPlannedProjectTreatmentBMPLayerToMap();
          this.delineations = delineations;
        });
        this.initMap();
        this.map.fireEvent('dataloading');
      });

      this.columnDefs = [
        {
          headerName: 'Project ID', valueGetter: (params: any) => {
            return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectID };
          }, cellRendererFramework: LinkRendererComponent,
          cellRendererParams: { inRouterLink: "/projects/" },
          filterValueGetter: (params: any) => {
            return params.data.ProjectID;
          },
          comparator: this.utilityFunctionsService.linkRendererComparator
        },
        {
          headerName: 'Project Name', valueGetter: (params: any) => {
            return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectName };
          }, cellRendererFramework: LinkRendererComponent,
          cellRendererParams: { inRouterLink: "/projects/" },
          filterValueGetter: (params: any) => {
            return params.data.ProjectID;
          },
          comparator: this.utilityFunctionsService.linkRendererComparator
        },
        { headerName: 'Organization', field: 'Organization.OrganizationName' },
        { headerName: 'Jurisdiction', field: 'StormwaterJurisdiction.Organization.OrganizationName' },
        { headerName: 'Status', field: 'ProjectStatus.ProjectStatusName', width: 120 },
        this.utilityFunctionsService.createDateColumnDef('Date Created', 'DateCreated', 'M/d/yyyy', 120),
        { headerName: 'Project Description', field: 'ProjectDescription' }
      ];

      this.defaultColDef = {
        filter: true, sortable: true, resizable: true
      };

    });

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
      tiled: true,
      pane: "hippocampOverlayPane"
    } as L.WMSOptions);

    let jurisdictionsWMSOptions = ({
      layers: "OCStormwater:Jurisdictions",
      transparent: true,
      format: "image/png",
      tiled: true,
      styles: "jurisdiction_orange",
      pane: "hippocampOverlayPane"
    } as L.WMSOptions);

    let verifiedDelineationsWMSOptions = ({
      layers: "OCStormwater:Delineations",
      transparent: true,
      format: "image/png",
      tiled: true,
      cql_filter: "DelineationStatus = 'Verified'",
      pane: "hippocampOverlayPane"
    } as L.WMSOptions);

    this.overlayLayers = Object.assign({
      "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions),
      "<span>Stormwater Network <br/> <img src='./assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" }),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<span>Delineations (Verified) </br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", verifiedDelineationsWMSOptions)
    }, this.overlayLayers);

    this.compileService.configure(this.appRef);
  }

  initMap() {
    const mapOptions: L.MapOptions = {
      // center: [46.8797, -110],
      // zoom: 6,
      minZoom: 9,
      maxZoom: 22,
      layers: [
        this.tileLayers["Terrain"],
        this.overlayLayers["<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions"]
      ],
      fullscreenControl: true,
      gestureHandling: true
    } as L.MapOptions;
    this.map = L.map(this.mapID, mapOptions);
    this.initializePanes();
    this.setControl();
    this.registerClickEvents();
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);
  }

  public initializePanes(): void {
    let hippocampOverlayPane = this.map.createPane("hippocampOverlayPane");
    hippocampOverlayPane.style.zIndex = 10000;
    let hippocampChoroplethPane = this.map.createPane("hippocampChoroplethPane");
    hippocampChoroplethPane.style.zIndex = 9999;
    this.map.getPane("markerPane").style.zIndex = 10001;
    this.map.getPane("popupPane").style.zIndex = 10002;
  }

  public setControl(): void {
    var loadingControl = L.Control.loading({
      separate: true
    });
    this.map.addControl(loadingControl);

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

    const wfsService = this.wfsService;
    const self = this;
    this.map.on("click", (event: L.LeafletMouseEvent): void => {
      wfsService.getOCTAPrioritizationMetricsByCoordinate(event.latlng.lng, event.latlng.lat)
        .subscribe((octaPrioritizationFeatureCollection: L.FeatureCollection) => {
          octaPrioritizationFeatureCollection.features
            .forEach((feature: L.Feature) => {
              new L.Popup({
                minWidth: 500,
                autoPanPadding: new L.Point(100, 100)
              })
                .setLatLng(event.latlng)
                .setContent(this.compileService.compile(OctaPrioritizationDetailPopupComponent, (c) => { c.instance.feature = feature; })
                )
                .openOn(self.map);
            });
        });
    });
  }

  public addPlannedProjectTreatmentBMPLayerToMap(): void {
    //If you were called and there is no map, try again in a little bit
    if (!this.map) {
      setTimeout(() => { this.addPlannedProjectTreatmentBMPLayerToMap() }, 500);
    }

    if (this.plannedProjectTreatmentBMPsLayer) {
      this.map.removeLayer(this.plannedProjectTreatmentBMPsLayer);
    }

    const treatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter(x => x.ProjectID != null));
    this.plannedProjectTreatmentBMPsLayer = new L.GeoJSON(treatmentBMPGeoJSON, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker })
      },
      onEachFeature: (feature, layer) => {
        layer.on('click', (e) => {
          this.selectTreatmentBMPImpl(feature.properties.TreatmentBMPID);
        })
      },
    });

    this.plannedProjectTreatmentBMPsLayer.addTo(this.map);
    this.map.fireEvent('dataload');
  }

  private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
    return {
      type: "FeatureCollection",
      features: treatmentBMPs.map(x => {
        let treatmentBMPGeoJson =
          this.mapTreatmentBMPToFeature(x);
        return treatmentBMPGeoJson;
      })
    }
  }

  private mapTreatmentBMPToFeature(x: TreatmentBMPDisplayDto) {
    return {
      "type": "Feature",
      "geometry": {
        "type": "Point",
        "coordinates": [x.Longitude ?? 0, x.Latitude ?? 0]
      },
      "properties": {
        TreatmentBMPID: x.TreatmentBMPID
      }
    };
  }

  private mapDelineationsToGeoJson(delineations: DelineationSimpleDto[]) {
    return {
      type: "FeatureCollection",
      features: delineations.map(x => this.mapDelineationToFeature(x))
    }
  }

  private mapDelineationToFeature(x: DelineationSimpleDto) {
    return {
      "type": "Feature",
      "geometry": x.Geometry != null && x.Geometry != undefined ? JSON.parse(x.Geometry) : null,
      "properties": {
        TreatmentBMPID: x.TreatmentBMPID,
        DelineationID: x.DelineationID
      }
    };
  }

  public selectTreatmentBMPImpl(treatmentBMPID: number) {
    if (this.selectedDelineation) {
      this.selectedDelineation = null;
    }

    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.selectProjectImpl(this.selectedTreatmentBMP.ProjectID);
    this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
      //Doing this here as well feels redundant, but if we dont
      //whenever we set the icon it puts the highlight in a weird state.
      //So just disable and enable as needed
      layer.disablePermanentHighlight();
      if (this.selectedTreatmentBMP == null || treatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(MarkerHelper.treatmentBMPMarker);
        if (this.relatedTreatmentBMPs.some(x => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID)) {
          layer.enablePermanentHighlight();
        }
        return;
      }
      layer.setIcon(MarkerHelper.selectedMarker);
      // if (!this.map.getBounds().contains(layer.getLatLng())) {
      //   this.map.flyTo(layer.getLatLng());
      // }
    })
    this.selectedDelineation = this.delineations.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    if (this.selectedDelineation != null) {
      this.selectedProjectDelineationsLayer.eachLayer(layer => {
        if (layer.feature.properties.DelineationID == this.selectedDelineation.DelineationID) {
          layer.setStyle(this.delineationSelectedStyle);
        }
      });
    }
  }

  public selectProjectImpl(projectID: number) {
    if (this.selectedProjectDelineationsLayer) {
      this.map.removeLayer(this.selectedProjectDelineationsLayer);
      this.selectedProjectDelineationsLayer = null;
    }

    this.selectedProject = this.projects.filter(x => x.ProjectID == this.selectedTreatmentBMP.ProjectID)[0];
    this.relatedTreatmentBMPs = this.treatmentBMPs.filter(x => x.ProjectID == projectID);
    let relatedTreatmentBMPIDs = this.relatedTreatmentBMPs.map(x => x.TreatmentBMPID);
    let featureGroupForZoom = new L.featureGroup();

    let relatedDelineations = this.delineations.filter(x => relatedTreatmentBMPIDs.includes(x.TreatmentBMPID));
    if (relatedDelineations != null && relatedDelineations.length > 0) {
      this.selectedProjectDelineationsLayer = new L.GeoJSON(this.mapDelineationsToGeoJson(relatedDelineations));
      this.selectedProjectDelineationsLayer.addTo(this.map);
      this.selectedProjectDelineationsLayer.addTo(featureGroupForZoom);
    }

    this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
      layer.disablePermanentHighlight();
      layer.setIcon(MarkerHelper.treatmentBMPMarker);
      if (relatedTreatmentBMPIDs.includes(layer.feature.properties.TreatmentBMPID)) {
          layer.addTo(featureGroupForZoom);
          layer.enablePermanentHighlight();
        }
      });
    this.map.fitBounds(featureGroupForZoom.getBounds(), {padding: new L.Point(50,50)});
    
  }

  public applyPrioritizationMetricOverlay(): void {
    if (!this.map) {
      return null;
    }

    if (this.prioritizationMetricOverlayLayer) {
      this.map.removeLayer(this.prioritizationMetricOverlayLayer);
      this.prioritizationMetricOverlayLayer = null;
    }

    if (this.selectedPrioritizationMetric == PrioritizationMetric.NoMetric) {
      return null;
    }

    let prioritizationMetricsWMSOptions = ({
      layers: "Neptune:OCTAPrioritization",
      transparent: true,
      format: "image/png",
      tiled: true,
      styles: this.selectedPrioritizationMetric.geoserverStyle,
      pane: "hippocampChoroplethPane"
    } as L.WMSOptions);

    this.prioritizationMetricOverlayLayer = L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", prioritizationMetricsWMSOptions);
    this.prioritizationMetricOverlayLayer.addTo(this.map);
    this.prioritizationMetricOverlayLayer.bringToFront();
  }

}
