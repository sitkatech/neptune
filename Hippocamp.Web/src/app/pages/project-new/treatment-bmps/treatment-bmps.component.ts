import { ApplicationRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import * as L from 'leaflet';
import {
  Control, FitBoundsOptions,
  GeoJSON,
  marker,
  map,
  Map,
  MapOptions,
  tileLayer,
  geoJSON,
  icon,
  latLng,
  Layer,
  DomEvent,
  DomUtil,
  LeafletEvent} from 'leaflet';
import 'leaflet.fullscreen';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { BoundingBoxDto } from 'src/app/shared/models/bounding-box-dto';
import { Feature } from 'geojson';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { ActivatedRoute } from '@angular/router';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';

declare var $: any

@Component({
  selector: 'hippocamp-treatment-bmps',
  templateUrl: './treatment-bmps.component.html',
  styleUrls: ['./treatment-bmps.component.scss']
})
export class TreatmentBmpsComponent implements OnInit {

  public mapID: string = 'poolDetailMap';
  public visibleTreatmentBMPStyle: string = 'treatmentBMP_purple_outline_only';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public selectedTreatmentBMPStyle: string = 'treatmentBMP_yellow';
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '400px';
  public defaultFitBoundsOptions?: FitBoundsOptions = null;
  public onEachFeatureCallback?: (feature, layer) => void;

  @Output()
  public afterSetControl: EventEmitter<Control.Layers> = new EventEmitter();

  @Output()
  public afterLoadMap: EventEmitter<LeafletEvent> = new EventEmitter();

  @Output()
  public onMapMoveEnd: EventEmitter<LeafletEvent> = new EventEmitter();

  public component: any;
  public map: Map;
  public featureLayer: any;
  public layerControl: Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  public selectedListItem: number;
  public selectedListItemDetails: { [key: string]: any } = {};
  public selectedObjectLayer: Layer;
  public selectedDto: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: GeoJSON<any>;
  private markerIcon = this.buildMarker('/assets/main/map-icons/marker-icon-violet.png', '/assets/main/map-icons/marker-icon-2x-violet.png');
  private markerIconSelected = this.buildMarker('/assets/main/map-icons/marker-icon-selected.png', '/assets/main/map-icons/marker-icon-2x-selected.png');

  public projectID: number;

  constructor(
    private treatmentBMPService: TreatmentBMPService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private route: ActivatedRoute,
  ) {
  }

  public ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);
      const bmps = this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID).subscribe(treatmentBMPs => {
        this.treatmentBMPs = treatmentBMPs;
        this.updateMapLayers();
      })
    }

    // Default bounding box
    this.boundingBox = new BoundingBoxDto();
    this.boundingBox.Left = -117.96363830566408;
    this.boundingBox.Bottom = 33.444047234512354;
    this.boundingBox.Right = -117.23030090332033;
    this.boundingBox.Top = 33.73005042840439;

    this.tileLayers = Object.assign({}, {
      "Aerial": tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Street": tileLayer('https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Aerial',
      }),
      "Terrain": tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Terrain',
      }),
    }, this.tileLayers);

    this.overlayLayers = [];

    this.compileService.configure(this.appRef);
  }

  public updateMapLayers(): void {

    const mapOptions: MapOptions = {
      // center: [46.8797, -110],
      // zoom: 6,
      minZoom: 9,
      maxZoom: 17,
      layers: [
        this.tileLayers["Aerial"],
      ],
      fullscreenControl: true
    } as MapOptions;
    this.map = map(this.mapID, mapOptions);

    this.map.on('load', (event: LeafletEvent) => {
      this.afterLoadMap.emit(event);
    });
    this.map.on("moveend", (event: LeafletEvent) => {
      this.onMapMoveEnd.emit(event);
    });
    this.map.fitBounds([[this.boundingBox.Bottom, this.boundingBox.Left], [this.boundingBox.Top, this.boundingBox.Right]], this.defaultFitBoundsOptions);
    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: function (feature, latlng) {
        return marker(latlng, { icon: this.markerIcon})
      }.bind(this)
    });
    this.treatmentBMPsLayer.addTo(this.map);
    this.setControl();
    this.registerClickEvents()
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

  public setControl(): void {
    this.layerControl = new Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false })
      .addTo(this.map);
    this.afterSetControl.emit(this.layerControl);
  }

  public registerClickEvents(): void {
    var leafletControlLayersSelector = ".leaflet-control-layers";
    var closeButtonClass = "leaflet-control-layers-close";

    var closem = DomUtil.create("a", closeButtonClass);
    closem.innerHTML = "Close";
    DomEvent.on(closem,
      "click",
      function () {
        $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
      });

    $(leafletControlLayersSelector).append(closem);

    this.treatmentBMPsLayer.on("click", (event: LeafletEvent) => {
      this.selectTreatmentBMPImpl(event.propagatedFrom.feature);
    })
  }

  public selectTreatmentBMP(treatmentBMPID: number) {
    const feature = this.mapTreatmentBMPToFeature(this.treatmentBMPs.find(x => x.TreatmentBMPID === treatmentBMPID));
    this.selectTreatmentBMPImpl(feature);
  }

  private selectTreatmentBMPImpl(feature: any) {
    const treatmentBMPID = feature.properties.TreatmentBMPID;
    if (this.selectedListItem) {
      this.selectedListItem = null;
      this.selectedListItemDetails = {};
      this.map.removeLayer(this.selectedObjectLayer);
      this.selectedObjectLayer = null;
    }
    this.selectedListItem = treatmentBMPID;
    let selectedNumber = null;
    let selectedAttributes = null;
    this.selectedDto = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    selectedAttributes = [
      `<strong>Type:</strong> ${this.selectedDto.TreatmentBMPTypeName}`,
      `<strong>Latitude:</strong> ${this.selectedDto.Latitude}`,
      `<strong>Longitude:</strong> ${this.selectedDto.Longitude}`
    ];

    if (this.selectedDto) {
      this.selectedObjectLayer = new L.GeoJSON(feature, {
        pointToLayer: (feature, latlng) => {
          return L.marker(latlng, { icon: this.markerIconSelected, zIndexOffset: 1000 });
        }
      });;
      this.selectedObjectLayer.addTo(this.map).bringToFront();
      this.selectedListItemDetails.title = `${selectedNumber}`;
      this.selectedListItemDetails.attributes = selectedAttributes;
    }
  }
}