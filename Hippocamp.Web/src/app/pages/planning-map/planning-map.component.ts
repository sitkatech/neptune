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

@Component({
  selector: 'hippocamp-planning-map',
  templateUrl: './planning-map.component.html',
  styleUrls: ['./planning-map.component.scss']
})
export class PlanningMapComponent implements OnInit {

  private currentUser : PersonDto;

  private treatmentBMPs : Array<TreatmentBMPDisplayDto>;
  private delineations: Array<DelineationSimpleDto>;

  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;

  constructor(
    private authenticationService: AuthenticationService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      this.currentUser = result;
      forkJoin({
        treatmentBMPs : this.treatmentBMPService.getTreatmentBMPs(),
        delineations: this.delineationService.getDelineations()
      }).subscribe(({treatmentBMPs, delineations}) => {
        this.treatmentBMPs = treatmentBMPs;
        this.delineations = delineations;
      });
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

    this.overlayLayers = Object.assign({
      "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", regionalSubbasinsWMSOptions),
      "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(environment.geoserverMapServiceUrl + "/wms?", jurisdictionsWMSOptions),
      "<span>Stormwater Network <br/> <img src='./assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({ url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/" })
    }, this.overlayLayers);

    this.compileService.configure(this.appRef);
  }

  initMap() {

  }

}
