import { ApplicationRef, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { forkJoin } from 'rxjs';
import * as L from 'leaflet';
import 'leaflet-gesture-handling';
import 'leaflet.fullscreen';
import 'leaflet.marker.highlight';
import 'leaflet-loading';
import 'leaflet.markercluster';
import * as esri from 'esri-leaflet';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BoundingBoxDto, DelineationDto, PersonDto, ProjectDto, TreatmentBMPDisplayDto } from 'src/app/shared/generated/model/models';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';
import { MarkerHelper } from 'src/app/shared/helpers/marker-helper';
import { PrioritizationMetric } from 'src/app/shared/models/prioritization-metric';
import { WfsService } from 'src/app/shared/services/wfs.service';
import { OctaPrioritizationDetailPopupComponent } from 'src/app/shared/components/octa-prioritization-detail-popup/octa-prioritization-detail-popup.component';
import { ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { AgGridAngular } from 'ag-grid-angular';
import { FieldDefinitionGridHeaderComponent } from 'src/app/shared/components/field-definition-grid-header/field-definition-grid-header.component';
import { DelineationService } from 'src/app/shared/generated/api/delineation.service';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { StormwaterJurisdictionService } from 'src/app/shared/generated/api/stormwater-jurisdiction.service';
import { TreatmentBMPService } from 'src/app/shared/generated/api/treatment-bmp.service';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';

declare var $: any;

@Component({
  selector: 'hippocamp-planning-map',
  templateUrl: './planning-map.component.html',
  styleUrls: ['./planning-map.component.scss']
})
export class PlanningMapComponent implements OnInit {
  @ViewChild("projectsGrid") projectsGrid: AgGridAngular;

  private currentUser: PersonDto;
  public richTextTypeID = NeptunePageTypeEnum.HippocampPlanningMap;

  public projects: Array<ProjectDto>;
  private treatmentBMPs: Array<TreatmentBMPDisplayDto>;
  private delineations: Array<DelineationDto>;
  public selectedTreatmentBMP: TreatmentBMPDisplayDto;
  public relatedTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
  public selectedDelineation: DelineationDto;
  public selectedProject: ProjectDto;

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

  private delineationDefaultStyle = {
    color: '#51F6F8',
    fillOpacity: 0.2,
    opacity: 1
  }
  private delineationSelectedStyle = {
    color: 'yellow',
    fillOpacity: 0.2,
    opacity: 1
  }
  private plannedTreatmentBMPOverlayName = "<img src='./assets/main/map-icons/marker-icon-violet.png' style='height:17px'> Project BMPs";
  private inventoriedTreatmentBMPOverlayName = "<span>Inventoried BMP Locations<br /> <img src='./assets/main/map-icons/marker-icon-orange.png' style='height:17px; margin:3px'> BMP (Verified)</span>";
  private inventoriedTreatmentBMPsLayer: L.GeoJSON<any>;

  private viewInitialized: boolean = false;

  public columnDefs: ColDef[];
  public defaultColDef: ColDef;
  public paginationPageSize: number = 100;

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
      this.stormwaterJurisdictionService.jurisdictionsBoundingBoxGet().subscribe(result => {
        this.boundingBox = result;
        forkJoin({
          projects: this.projectService.projectsGet(),
          treatmentBMPs: this.treatmentBMPService.treatmentBMPsGet(),
          delineations: this.delineationService.delineationsGet()
        }).subscribe(({ projects, treatmentBMPs, delineations }) => {
          this.projects = projects;

          this.treatmentBMPs = treatmentBMPs;
          this.addTreatmentBMPLayersToMap();

          this.delineations = delineations;
        });
        this.initMap();
        this.map.fireEvent('dataloading');
      });

      this.columnDefs = [
        {
          headerName: 'Project ID', valueGetter: (params: any) => {
            return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectID };
          }, cellRenderer: LinkRendererComponent,
          cellRendererParams: { inRouterLink: "/projects/" },
          filterValueGetter: (params: any) => {
            return params.data.ProjectID;
          },
          comparator: this.utilityFunctionsService.linkRendererComparator
        },
        {
          headerName: 'Project Name', valueGetter: (params: any) => {
            return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectName };
          }, cellRenderer: LinkRendererComponent,
          cellRendererParams: { inRouterLink: "/projects/" },
          filterValueGetter: (params: any) => {
            return params.data.ProjectID;
          },
          comparator: this.utilityFunctionsService.linkRendererComparator
        },
        { 
          headerComponent: FieldDefinitionGridHeaderComponent,
          headerComponentParams: { fieldDefinitionType: 'Organization' },
          field: 'Organization.OrganizationName' 
        },
        { 
          headerComponent: FieldDefinitionGridHeaderComponent,
          headerComponentParams: { fieldDefinitionType: 'Jurisdiction'},
          field: 'StormwaterJurisdiction.Organization.OrganizationName' 
        },
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
      "<span>Inventoried BMP Delineations</br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", verifiedDelineationsWMSOptions)
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
  
    this.map.on('overlayadd overlayremove', e => {
      if (e.name != this.plannedTreatmentBMPOverlayName) {
        return;
      }
      if (e.type == 'overlayremove') {
        this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
          layer.disablePermanentHighlight();
        });
      }
    });
  }

  public initializePanes(): void {
    let hippocampChoroplethPane = this.map.createPane("hippocampChoroplethPane");
    hippocampChoroplethPane.style.zIndex = 300;
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

  public addTreatmentBMPLayersToMap(): void {
    //If you were called and there is no map, try again in a little bit
    if (!this.map) {
      setTimeout(() => { this.addTreatmentBMPLayersToMap() }, 500);
    }

    if (this.plannedProjectTreatmentBMPsLayer) {
      this.map.removeLayer(this.plannedProjectTreatmentBMPsLayer);
      this.layerControl.removeLayer(this.plannedProjectTreatmentBMPsLayer);
    }
    if (this.inventoriedTreatmentBMPsLayer) {
      this.map.removeLayer(this.inventoriedTreatmentBMPsLayer);
      this.layerControl.removeLayer(this.inventoriedTreatmentBMPsLayer);
    }

    // add inventoried BMPs layer
    this.addInventoriedBMPsLayer();

    // add planned project BMPs layer
    const projectTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter(x => x.ProjectID != null));
    this.plannedProjectTreatmentBMPsLayer = new L.GeoJSON(projectTreatmentBMPGeoJSON, {
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
    this.layerControl.addOverlay(this.plannedProjectTreatmentBMPsLayer, this.plannedTreatmentBMPOverlayName)

    this.map.fireEvent('dataload');
  }

  private addInventoriedBMPsLayer() {
    const inventoriedTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter(x => x.ProjectID == null && x.InventoryIsVerified));
    this.inventoriedTreatmentBMPsLayer = new L.GeoJSON(inventoriedTreatmentBMPGeoJSON, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: MarkerHelper.inventoriedTreatmentBMPMarker });
      },
      onEachFeature: (feature, layer) => {
        layer.bindPopup(
          `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${feature.properties.TreatmentBMPName}</a><br>`
          + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`

        );
      },
    });

    var clusteredInventoriedBMPLayer = L.markerClusterGroup({
      iconCreateFunction: function (cluster) {
        var childCount = cluster.getChildCount();

        return new L.DivIcon({
          html: '<div><span>' + childCount + '</span></div>',
          className: 'marker-cluster', iconSize: new L.Point(40, 40)
        });
      }
    });
    clusteredInventoriedBMPLayer.addLayer(this.inventoriedTreatmentBMPsLayer);
    this.layerControl.addOverlay(clusteredInventoriedBMPLayer, this.inventoriedTreatmentBMPOverlayName);
  }

  private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
    return {
      type: "FeatureCollection",
      features: treatmentBMPs.map(x => {
        let treatmentBMPGeoJson =
        {
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
        return treatmentBMPGeoJson;
      })
    }
  }

  private mapDelineationsToGeoJson(delineations: DelineationDto[]) {
    return delineations.map(x => JSON.parse(x.Geometry));
  }

  public selectTreatmentBMPImpl(treatmentBMPID: number) {
    if (!this.map.hasLayer(this.plannedProjectTreatmentBMPsLayer)) {
      this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => layer.disablePermanentHighlight())
      this.plannedProjectTreatmentBMPsLayer.addTo(this.map);
    }

    let selectedTreatmentBMP = this.treatmentBMPs.find(x => x.TreatmentBMPID == treatmentBMPID);
    this.selectedTreatmentBMP = selectedTreatmentBMP;
    this.selectProjectImpl(selectedTreatmentBMP.ProjectID);
    this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
      
      if (!layer.feature.properties.DefaultZIndexOffset) {
        layer.feature.properties.DefaultZIndexOffset = layer._zIndex;
      }
      //Doing this here as well feels redundant, but if we dont
      //whenever we set the icon it puts the highlight in a weird state.
      //So just disable and enable as needed
      layer.disablePermanentHighlight();
      if (this.selectedTreatmentBMP == null || treatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(MarkerHelper.treatmentBMPMarker);
        layer.setZIndexOffset(layer.feature.properties.DefaultZIndexOffset); 
        if (this.relatedTreatmentBMPs.some(x => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID)) {
          layer.enablePermanentHighlight();
        }
        return;
      }
      layer.setZIndexOffset(10000);
      layer.setIcon(MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath('/assets/main/map-icons/marker-icon-red.png'));
      // if (!this.map.getBounds().contains(layer.getLatLng())) {
      //   this.map.flyTo(layer.getLatLng());
      // }
    });
    this.selectedDelineation = this.delineations.find(x => x.TreatmentBMPID == treatmentBMPID);
  }

  public selectProjectImpl(projectID: number) {
    if (this.selectedProjectDelineationsLayer) {
      this.map.removeLayer(this.selectedProjectDelineationsLayer);
      this.selectedProjectDelineationsLayer = null;
    }

    this.selectedDelineation = null;
    //this.selectedTreatmentBMP = null;

    this.selectedProject = this.projects.find(x => x.ProjectID == projectID);
    this.relatedTreatmentBMPs = this.treatmentBMPs.filter(x => x.ProjectID == projectID);
    let relatedTreatmentBMPIDs = this.relatedTreatmentBMPs.map(x => x.TreatmentBMPID);
    let featureGroupForZoom = new L.featureGroup();

    let relatedDelineations = this.delineations.filter(x => relatedTreatmentBMPIDs.includes(x.TreatmentBMPID));
    if (relatedDelineations != null && relatedDelineations.length > 0) {
      this.selectedProjectDelineationsLayer = new L.GeoJSON(this.mapDelineationsToGeoJson(relatedDelineations), {
        style: (feature) => {
          if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != feature.properties.TreatmentBMPID) {
            return this.delineationDefaultStyle;
          }
          return this.delineationSelectedStyle;
        }
      });
      this.selectedProjectDelineationsLayer.addTo(this.map);
      this.selectedProjectDelineationsLayer.addTo(featureGroupForZoom);
    }

    this.map.fitBounds(featureGroupForZoom.getBounds(), {padding: new L.Point(50,50)});
    this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
      layer.disablePermanentHighlight();
      layer.setIcon(MarkerHelper.treatmentBMPMarker);
      if (relatedTreatmentBMPIDs.includes(layer.feature.properties.TreatmentBMPID)) {
          layer.addTo(featureGroupForZoom);
          layer.enablePermanentHighlight();
        }
        if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID){
          layer.bringToFront();
        }
      });

    this.projectsGrid.api.forEachNode(node => {
      if (node.data.ProjectID === projectID) {
        node.setSelected(true);
        var rowIndex = node.rowIndex;
        //I am honestly kind of flabbergasted that ag-grid doesn't tell me what page the node is on
        this.projectsGrid.api.paginationGoToPage(Math.floor(rowIndex / this.paginationPageSize));
        this.projectsGrid.api.ensureIndexVisible(node.rowIndex)
      }
    });
    
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
  }

  public onSelectionChanged(event: Event) {
    const selectedNode = this.projectsGrid.api.getSelectedNodes()[0];
    // If we have no selected node or our node has already been selected so we can stop infinite looping
    if (!selectedNode || (this.selectedProject != null && this.selectedProject.ProjectID == selectedNode.data.ProjectID)) {
      return;
    }
    let treatmentBMPID = this.treatmentBMPs.filter(x => x.ProjectID == selectedNode.data.ProjectID).map(x => x.TreatmentBMPID)[0];
    if (treatmentBMPID != null) {
      this.selectTreatmentBMPImpl(treatmentBMPID);
      return;
    }

    this.selectProjectImpl(selectedNode.data.ProjectID);
  }

  public getRelatedBMPsToShow() {
    let selectedTreatmentBMPID = this.selectedTreatmentBMP?.TreatmentBMPID;
    return this.relatedTreatmentBMPs.filter(x => x.TreatmentBMPID != selectedTreatmentBMPID);
  }
  
  public ocstBaseUrl(): string {
    return environment.ocStormwaterToolsBaseUrl
  }
}
