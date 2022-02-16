import { ApplicationRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as L from 'leaflet';
import 'leaflet-draw';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet'
import { GestureHandling } from "leaflet-gesture-handling";
import { forkJoin } from 'rxjs';
import { DelineationService } from 'src/app/services/delineation.service';
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

declare var $:any

@Component({
  selector: 'hippocamp-delineations',
  templateUrl: './delineations.component.html',
  styleUrls: ['./delineations.component.scss']
})
export class DelineationsComponent implements OnInit {

  public mapID: string = 'delineationsMap';
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public delineations: DelineationUpsertDto[];
  public zoomMapToDefaultExtent: boolean = true;
  public mapHeight: string = '400px';
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
  public editableDelineationFeatureGroup: L.FeatureGroup = new L.FeatureGroup();
  public layerControl: L.Control.Layers;
  public tileLayers: { [key: string]: any } = {};
  public overlayLayers: { [key: string]: any } = {};
  private boundingBox: BoundingBoxDto;
  public selectedListItem: number;
  public selectedDelineation: DelineationUpsertDto;
  public selectedTreatmentBMP: TreatmentBMPUpsertDto;
  public treatmentBMPsLayer: L.GeoJSON<any>;
  private markerIcon = this.buildMarker('/assets/main/map-icons/marker-icon-violet.png', '/assets/main/map-icons/marker-icon-2x-violet.png');
  private markerIconSelected = this.buildMarker('/assets/main/map-icons/marker-icon-selected.png', '/assets/main/map-icons/marker-icon-2x-selected.png');
  private delineationDefaultStyle = {
    color: 'blue'
  }
  private delineationSelectedStyle = {
    color: 'yellow'
  }
  public projectID: number;
  public customRichTextTypeID = CustomRichTextType.Delineations;

  private isPerformingDrawAction: boolean = false;
  private defaultDrawControlOption: L.Control.DrawConstructorOptions = {
      draw: {
        polyline: false,
        rectangle : false,
        circle: false,
        marker: false,
        circlemarker: false,
      },
      edit: {
        featureGroup: this.editableDelineationFeatureGroup,
        remove: true
      }
    };
  private drawControl: L.Control.Draw;
  public drawDelineationChosen: boolean = false;
  private newDelineatonID: number = -1;
  public isLoadingSubmit: boolean = false;

  constructor(
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private appRef: ApplicationRef,
    private compileService: CustomCompileService,
    private route: ActivatedRoute,
    private alertService: AlertService
  ) {
  }

  public ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);

      forkJoin({
        treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
        delineations: this.delineationService.getDelineationsByProjectID(this.projectID),
        boundingBox: this.stormwaterJurisdictionService.getBoundingBoxByProjectID(this.projectID),
      }).subscribe(({treatmentBMPs, delineations, boundingBox}) => {
        this.treatmentBMPs = treatmentBMPs;
        this.delineations = delineations;
        this.boundingBox = boundingBox;

        this.updateMapLayers();
      });
    }

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

  public updateMapLayers(): void {

    const mapOptions: L.MapOptions = {
      // center: [46.8797, -110],
      // zoom: 6,
      minZoom: 9,
      maxZoom: 19,
      layers: [
        this.tileLayers["Aerial"],
      ],
      fullscreenControl: true,
      gestureHandling: true
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

    this.editableDelineationFeatureGroup.addTo(this.map);
    this.addFeatureCollectionToEditableFeatureGroup(this.mapDelineationsToGeoJson(this.delineations));
    
    const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs);
    this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
      pointToLayer: function (feature, latlng) {
        return L.marker(latlng, { icon: this.markerIcon})
      }.bind(this)
    });
    this.treatmentBMPsLayer.addTo(this.map);
    this.setControl();
    this.registerClickEvents();
    
    if (this.treatmentBMPs.length > 0) {
      this.selectFeatureImpl(this.treatmentBMPs[0].TreatmentBMPID);
    }
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

  public addFeatureCollectionToEditableFeatureGroup (delineationJsons : any) {
    L.geoJson(delineationJsons, {
        onEachFeature: (feature, layer) => {
            if (layer.getLayers) {
                layer.getLayers().forEach((l) => {
                    this.editableDelineationFeatureGroup.addLayer(l);
                });
            } else {
                this.editableDelineationFeatureGroup.addLayer(layer);
            }
            layer.on('click', (e) => {
              this.selectFeatureImpl(feature.properties.TreatmentBMPID);
            })
        }
    });
  };

  public addOrRemoveDrawControl(turnOn: boolean) {
    if (turnOn) {
      var drawOptions = Object.assign({}, this.defaultDrawControlOption);
      if (this.selectedDelineation == null) {
        drawOptions.edit = false;
      }
      if (this.selectedDelineation != null) {
        drawOptions.draw = false;
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
          DelineationID : this.newDelineatonID,
          TreatmentBMPID : this.selectedTreatmentBMP.TreatmentBMPID,
          DelineationTypeID : DelineationTypeEnum.Distributed
        });
        this.delineations = this.delineations.concat(delineationUpsertDto);
        this.newDelineatonID--;
      }
      delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON().geometry);
      delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / 4047).toFixed(2);
      this.selectedDelineation = delineationUpsertDto;
      layer.feature = this.mapDelineationToFeature(delineationUpsertDto);
      if (layer.getLayers) {
        layer.getLayers().forEach((l) => {
            this.editableDelineationFeatureGroup.addLayer(l);
        });
      } else {
          this.editableDelineationFeatureGroup.addLayer(layer);
      }
      layer.on('click', (e) => {
        this.selectFeatureImpl(layer.feature.properties.TreatmentBMPID);
      })
      this.selectFeatureImpl(this.selectedTreatmentBMP.TreatmentBMPID);
    })
    .on(L.Draw.Event.EDITED, (event) => {
      this.isPerformingDrawAction = false;
        const layers = (event as L.DrawEvents.Edited).layers;
        layers.eachLayer((layer) => {
            var delineationUpsertDto = this.delineations.filter(x => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID)[0];
            delineationUpsertDto.Geometry = layer.toGeoJSON().geometry;
            delineationUpsertDto.DelineationArea = +(L.GeometryUtil.geodesicArea(layer.getLatLngs()[0]) / 4047).toFixed(2);
        });
    })
    .on(L.Draw.Event.DELETED, (event) => {
        const layers = (event as L.DrawEvents.Deleted).layers;
        layers.eachLayer((layer) => {
          var delineationUpsertDto = this.delineations.filter(x => layer.feature.properties.TreatmentBMPID == x.TreatmentBMPID)[0];
          delineationUpsertDto.Geometry = null;
          delineationUpsertDto.DelineationArea = null;
          this.editableDelineationFeatureGroup.removeLayer(layer);
        });
        this.isPerformingDrawAction = false;
    })
    .on(L.Draw.Event.DRAWSTART, () => {
        this.isPerformingDrawAction = true;
    })
    .on(L.Draw.Event.EDITSTART, () => {
      this.isPerformingDrawAction = true;
      // no edit started event, so here's a janky solution...
      setTimeout(() => {
        this.editableDelineationFeatureGroup.eachLayer(layer => {
          if (layer.feature.properties.TreatmentBMPID == this.selectedDelineation.TreatmentBMPID) {
            layer.editing.enable();
            return;
          }
          layer.editing.disable();
        })}, 10
      )
    })
    .on(L.Draw.Event.DELETESTART, () => {
        this.isPerformingDrawAction = true;
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
  }

  private selectFeatureImpl(treatmentBMPID: number) {
    if (this.isPerformingDrawAction) {
      return;
    }

    this.drawControl.remove();
    if (this.selectedListItem) {
      this.selectedListItem = null;
    }

    if (this.selectedDelineation) {
      this.selectedDelineation = null;
    }

    this.selectedDelineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];
    this.editableDelineationFeatureGroup.eachLayer(layer => {
      if (this.selectedDelineation == null || this.selectedDelineation.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setStyle(this.delineationDefaultStyle);
        return;
      }
      layer.setStyle(this.delineationSelectedStyle).bringToFront();
    })

    this.selectedListItem = treatmentBMPID;
    this.selectedTreatmentBMP = this.treatmentBMPs.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];

    this.treatmentBMPsLayer?.eachLayer(layer => {
      if (this.selectedTreatmentBMP == null || this.selectedTreatmentBMP.TreatmentBMPID != layer.feature.properties.TreatmentBMPID) {
        layer.setIcon(this.markerIcon).setZIndexOffset(1000);
        return;
      }
      layer.setIcon(this.markerIconSelected);
      layer.setZIndexOffset(10000);
    })
    this.addOrRemoveDrawControl(true);
  }

  public treatmentBMPHasDelineation (treatmentBMPID: number) {
    return this.delineations?.some(x => x.TreatmentBMPID == treatmentBMPID);
  }

  public getDelineationAreaForTreatmentBMP (treatmentBMPID: number) {
      let delineation = this.delineations?.filter(x => x.TreatmentBMPID == treatmentBMPID)[0];

      if (delineation?.DelineationArea == null ) {
        return "Not provided yet"
      }

      return `${delineation.DelineationArea} ac`;
  }

  public drawDelineationForTreatmentBMP(treatmentBMPID: number) {
    if (this.delineations.some(x => x.TreatmentBMPID == treatmentBMPID)) {
      return;
    }

    var newDelineation = new DelineationUpsertDto({
      DelineationTypeID : DelineationTypeEnum.Distributed,
      TreatmentBMPID : treatmentBMPID
    });

    this.delineations = this.delineations.concat(newDelineation);
  }

  public onSubmit() {
    this.isLoadingSubmit = true;
    
    //We need a fully qualified geojson string and above we are just getting the geometry
    //Possible can remove the update above if we are always going to do it here
    this.editableDelineationFeatureGroup.eachLayer((layer) => {
      var delineationUpsertDto = this.delineations.filter(x => x.TreatmentBMPID == layer.feature.properties.TreatmentBMPID)[0];
      delineationUpsertDto.Geometry = JSON.stringify(layer.toGeoJSON());
    });

    this.delineationService.mergeDelineations(this.delineations.filter(x => x.Geometry != null), this.projectID).subscribe(() => {
      this.isLoadingSubmit = false;
      this.alertService.pushAlert(new Alert('Your Delineation changes have been saved.', AlertContext.Success, true));
    });
  }

}
