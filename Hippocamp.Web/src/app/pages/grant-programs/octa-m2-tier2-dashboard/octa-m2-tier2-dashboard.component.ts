import { ApplicationRef, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { forkJoin } from 'rxjs';
import * as L from 'leaflet';
import 'leaflet-gesture-handling';
import 'leaflet.fullscreen';
import 'leaflet.marker.highlight';
import 'leaflet-loading';
import * as esri from 'esri-leaflet';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BoundingBoxDto, DelineationSimpleDto, ProjectSimpleDto, TreatmentBMPDisplayDto } from 'src/app/shared/generated/model/models';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { environment } from 'src/environments/environment';
import { MarkerHelper } from 'src/app/shared/helpers/marker-helper';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { PrioritizationMetric } from 'src/app/shared/models/prioritization-metric';
import { WfsService } from 'src/app/shared/services/wfs.service';
import { OctaPrioritizationDetailPopupComponent } from 'src/app/shared/components/octa-prioritization-detail-popup/octa-prioritization-detail-popup.component';
import { ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { AgGridAngular } from 'ag-grid-angular';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { FieldDefinitionGridHeaderComponent } from 'src/app/shared/components/field-definition-grid-header/field-definition-grid-header.component';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { StormwaterJurisdictionService } from 'src/app/shared/generated/api/stormwater-jurisdiction.service';
import { TreatmentBMPService } from 'src/app/shared/generated/api/treatment-bmp.service';

declare var $: any;

@Component({
  selector: 'hippocamp-octa-m2-tier2-dashboard',
  templateUrl: './octa-m2-tier2-dashboard.component.html',
  styleUrls: ['./octa-m2-tier2-dashboard.component.scss']
})
export class OCTAM2Tier2DashboardComponent implements OnInit {
  @ViewChild("projectsGrid") projectsGrid: AgGridAngular;

  private currentUser: PersonDto;
  public richTextTypeID = CustomRichTextType.OCTAM2Tier2GrantProgramDashboard;

  public projects: Array<ProjectSimpleDto>;
  private treatmentBMPs: Array<TreatmentBMPDisplayDto>;
  private verifiedTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
  private delineations: Array<DelineationSimpleDto>;
  public selectedTreatmentBMP: TreatmentBMPDisplayDto;
  public relatedTreatmentBMPs: Array<TreatmentBMPDisplayDto>;
  public relateedTreatmentBMPsToDisplay: Array<TreatmentBMPDisplayDto>;
  public selectedDelineation: DelineationSimpleDto;
  public selectedProject: ProjectSimpleDto;

  public mapID: string = 'planningMap';
  public mapHeight = (window.innerHeight - (window.innerHeight * 0.2)) + "px";
  public map: L.Map;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  public plannedProjectTreatmentBMPsLayer: L.GeoJSON<any>;
  public inventoriedTreatmentBMPsLayer: L.GeoJSON<any>;
  public selectedProjectDelineationsLayer: L.GeoJSON<any>;
  private boundingBox: BoundingBoxDto;
  public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
  public layerControl: L.Control.Layers;

  //this is needed to allow binding to the static class
  public PrioritizationMetric = PrioritizationMetric;
  public prioritizationMetrics = Object.values(PrioritizationMetric);
  public selectedPrioritizationMetric = PrioritizationMetric.NoMetric;
  public prioritizationMetricOverlayLayer: L.Layers;

  private plannedTreatmentBMPOverlayName = "<img src='./assets/main/map-icons/marker-icon-violet.png' style='height:17px; margin-bottom:3px'> Project BMPs";
  private inventoriedTreatmentBMPOverlayName = "<img src='./assets/main/map-icons/marker-icon-orange.png' style='height:17px; margin-bottom:3px'> Inventoried BMPs (Verified)";
  private delineationDefaultStyle = {
    color: '#51F6F8',
    fillOpacity: 0.2,
    opacity: 1
  }

  private viewInitialized: boolean = false;

  public columnDefs: ColDef[];
  public defaultColDef: ColDef;
  public paginationPageSize: number = 100;

  constructor(
    private authenticationService: AuthenticationService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService,
    private treatmentBMPService: TreatmentBMPService,
    private wfsService: WfsService,
    private utilityFunctionsService: UtilityFunctionsService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      this.currentUser = result;
      this.stormwaterJurisdictionService.jurisdictionsBoundingBoxGet().subscribe(result => {
        this.boundingBox = result;
        forkJoin({
          projects: this.projectService.projectsOCTAM2Tier2GrantProgramGet(),
          treatmentBMPs: this.projectService.projectsOCTAM2Tier2GrantProgramTreatmentBMPsGet(),
          verifiedTreatmentBMPs: this.treatmentBMPService.treatmentBMPsVerifiedGet(),
          delineations: this.projectService.projectsDelineationsGet(),
        }).subscribe(({ projects, treatmentBMPs, verifiedTreatmentBMPs, delineations}) => {
          this.projects = projects;
          this.treatmentBMPs = treatmentBMPs;
          this.verifiedTreatmentBMPs = verifiedTreatmentBMPs;
          this.addTreatmentBMPLayersToMap();
          this.delineations = delineations;
        });
        this.initMap();
        this.map.fireEvent('dataloading');
      });

      this.columnDefs = [
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
        { headerName: 'Project Description', field: 'ProjectDescription' },
        { 
          headerComponentFramework: FieldDefinitionGridHeaderComponent,
          headerComponentParams: { fieldDefinitionType: 'Organization' },
          field: 'Organization.OrganizationName' 
        },
        { 
          headerComponentFramework: FieldDefinitionGridHeaderComponent,
          headerComponentParams: { fieldDefinitionType: 'Jurisdiction'},
          field: 'StormwaterJurisdiction.Organization.OrganizationName' 
        },
        this.utilityFunctionsService.createDateColumnDef('Last Shared On', 'OCTAM2Tier2ScoresLastSharedDate', 'short', 140),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('AreaTreatedAcres', 'AreaTreatedAcres', 'Area', 'Area Treated (ac)', null, 2),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('ImperviousAreaTreatedAcres', 'ImperviousAreaTreatedAcres', 'ImperviousArea', 'Impervious Area Treated (ac)', 220, 2),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('SEAScore', 'SEA', 'SEAScore', 'SEA Score', 90, 2),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('TPIScore', 'TPI', 'TPIScore', 'TPI Score', 90, 2),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('WQLRI', 'DryWeatherWQLRI', 'WQLRI', 'Dry Weather WQLRI', null, 2),
        this.utilityFunctionsService.createDecimalColumnDefWithFieldDefinition('WQLRI', 'WetWeatherWQLRI', 'WQLRI', 'Wet Weather WQLRI', null, 2),
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
    
    // add inventoried BMPs layer
    const inventoriedTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.verifiedTreatmentBMPs);
    this.inventoriedTreatmentBMPsLayer = new L.GeoJSON(inventoriedTreatmentBMPGeoJSON, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: MarkerHelper.inventoriedTreatmentBMPMarker })
      },
      onEachFeature: (feature, layer) => {
        layer.bindPopup(
          `<b>Name:</b> ${feature.properties.TreatmentBMPName} <br>`
          + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
        );
      },
    });

    var clusteredInventoriedBMPLayer = L.markerClusterGroup({
      iconCreateFunction: function(cluster) {
        var childCount = cluster.getChildCount();

          return new L.DivIcon({ html: '<div><span>' + childCount + '</span></div>', 
            className: 'marker-cluster', iconSize: new L.Point(40, 40) });
      }
    });
    clusteredInventoriedBMPLayer.addLayer(this.inventoriedTreatmentBMPsLayer);
    this.layerControl.addOverlay(clusteredInventoriedBMPLayer, this.inventoriedTreatmentBMPOverlayName);
    
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
        TreatmentBMPID: x.TreatmentBMPID,
        TreatmentBMPName: x.TreatmentBMPName,
        TreatmentBMPTypeName: x.TreatmentBMPTypeName
      }
    };
  }

  private mapDelineationsToGeoJson(delineations: DelineationSimpleDto[]) {
    return delineations.map(x => JSON.parse(x.Geometry));
  }

  public selectTreatmentBMPImpl(treatmentBMPID: number) {
    let selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.selectProjectImpl(selectedTreatmentBMP.ProjectID);
    this.selectedTreatmentBMP = selectedTreatmentBMP;
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
    })
    this.selectedDelineation = this.delineations.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
  }

  public selectProjectImpl(projectID: number) {
    if (this.selectedProjectDelineationsLayer) {
      this.map.removeLayer(this.selectedProjectDelineationsLayer);
      this.selectedProjectDelineationsLayer = null;
    }

    this.selectedDelineation = null;
    this.selectedTreatmentBMP = null;

    this.selectedProject = this.projects.filter(x => x.ProjectID == projectID)[0];
    this.relatedTreatmentBMPs = this.treatmentBMPs.filter(x => x.ProjectID == projectID);
    let relatedTreatmentBMPIDs = this.relatedTreatmentBMPs.map(x => x.TreatmentBMPID);
    let featureGroupForZoom = new L.featureGroup();

    let relatedDelineations = this.delineations.filter(x => relatedTreatmentBMPIDs.includes(x.TreatmentBMPID));
    if (relatedDelineations != null && relatedDelineations.length > 0) {
      this.selectedProjectDelineationsLayer = new L.GeoJSON(this.mapDelineationsToGeoJson(relatedDelineations), {
        style: (feature) => {
          return this.delineationDefaultStyle;
        }
      });
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

  public exportProjectGridToCsv() {   
    let columnIDs = this.projectsGrid.columnApi.getAllGridColumns().map(x => x.getColId());
    this.utilityFunctionsService.exportGridToCsv(this.projectsGrid, 'OCTA-M2-Tier2-projects' + '.csv', columnIDs);
  } 

  public downloadProjectModelResults() {
    this.projectService.projectsOCTAM2Tier2GrantProgramDownloadGet().subscribe(csv => {
      //Create a fake object for us to click and download
      var a = document.createElement('a');
      a.href = URL.createObjectURL(csv);
      a.download = `OCTA-M2-Tier2-project-modeled-results.csv`;
      document.body.appendChild(a);
      a.click();
      //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
      URL.revokeObjectURL(a.href);
      document.body.removeChild(a);
    }, (() => {
      this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
    }))
  }

  public downloadTreatmentBMPModelResults() {
    this.projectService.projectsOCTAM2Tier2GrantProgramTreatmentBMPsDownloadGet().subscribe(csv => {
      //Create a fake object for us to click and download
      var a = document.createElement('a');
      a.href = URL.createObjectURL(csv);
      a.download = `OCTA-M2-Tier2-BMP-modeled-results.csv`;
      document.body.appendChild(a);
      a.click();
      //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
      URL.revokeObjectURL(a.href);
      document.body.removeChild(a);
    }, (() => {
      this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
    }))
  }
}
