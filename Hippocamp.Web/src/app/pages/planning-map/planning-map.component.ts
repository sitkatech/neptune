import { ApplicationRef, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import * as L from 'leaflet';
import 'leaflet-gesture-handling';
import 'leaflet.fullscreen';
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

@Component({
  selector: 'hippocamp-planning-map',
  templateUrl: './planning-map.component.html',
  styleUrls: ['./planning-map.component.scss']
})
export class PlanningMapComponent implements OnInit {

  private currentUser : PersonDto;
  public richTextTypeID = CustomRichTextType.PlanningMap;

  private projects : Array<ProjectSimpleDto>;
  private treatmentBMPs : Array<TreatmentBMPDisplayDto>;
  private delineations: Array<DelineationSimpleDto>;
  public selectedTreatmentBMP : TreatmentBMPDisplayDto;
  public selectedDelineation : DelineationSimpleDto;
  public selectedProject : ProjectSimpleDto;

  public mapID: string = 'planningMap';
  public mapHeight = (window.innerHeight - (window.innerHeight * 0.2)) + "px";
  public map: L.Map;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  public plannedProjectTreatmentBMPsLayer: L.GeoJSON<any>;
  public selectedDelineationLayer: L.GeoJSON<any>;
  private boundingBox: BoundingBoxDto;
  public defaultFitBoundsOptions?: L.FitBoundsOptions = null;

  constructor(
    private authenticationService: AuthenticationService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      this.currentUser = result;
      forkJoin({
        projects : this.projectService.getProjectsByPersonID(),
        treatmentBMPs : this.treatmentBMPService.getTreatmentBMPs(),
        delineations: this.delineationService.getDelineations(),
        boundingBox : this.stormwaterJurisdictionService.getBoundingBoxByLoggedInPerson()
      }).subscribe(({projects, treatmentBMPs, delineations, boundingBox}) => {
        this.projects = projects;
        this.treatmentBMPs = treatmentBMPs;
        this.delineations = delineations;
        this.boundingBox = boundingBox;

        this.initMap();
      });
    });

    this.tileLayers = Object.assign({}, {
      "Aerial": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial', maxZoom:22, maxNativeZoom:18
      }),
      "Street": L.tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Street', maxZoom:22, maxNativeZoom:18
      }),
      "Terrain": L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain', maxZoom:22, maxNativeZoom:18
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

  initMap() {
    debugger;
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
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);
    
    const treatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter(x => x.ProjectID != null));
    this.plannedProjectTreatmentBMPsLayer = new L.GeoJSON(treatmentBMPGeoJSON, {
      pointToLayer: (feature, latlng) => {
        return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker })
      },
      onEachFeature: (feature, layer) => {
        layer.on('click', (e) => {
          this.selectFeatureImpl(feature.properties.TreatmentBMPID);
        })
      },
    });

    this.plannedProjectTreatmentBMPsLayer.addTo(this.map);
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

  private mapDelineationToFeature(x: DelineationSimpleDto) {
    return {
      "type": "Feature",
      "geometry": x.Geometry != null && x.Geometry != undefined ? JSON.parse(x.Geometry) : null
    };
  }

  public selectFeatureImpl(treatmentBMPID : number) {
    if (this.selectedDelineation) {
      this.map.removeLayer(this.selectedDelineationLayer);
      this.selectedDelineationLayer = null;
      this.selectedDelineation = null;
    }

    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.plannedProjectTreatmentBMPsLayer.eachLayer(layer => {
      if (this.selectedTreatmentBMP == null || treatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(MarkerHelper.treatmentBMPMarker);
        return;
      }
      layer.setIcon(MarkerHelper.selectedMarker);
    })
    this.selectedProject = this.projects.filter(x => x.ProjectID == this.selectedTreatmentBMP.ProjectID)[0];
    this.selectedDelineation = this.delineations.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    if (this.selectedDelineation != null) {
      this.selectedDelineationLayer = new L.GeoJSON(this.mapDelineationToFeature(this.selectedDelineation));
      this.selectedDelineationLayer.addTo(this.map);
    }
  }

}
