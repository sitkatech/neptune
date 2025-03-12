import { CommonModule } from "@angular/common";
import { Component, Input, OnChanges } from "@angular/core";
import { environment } from "src/environments/environment";
import * as L from "leaflet";
import "leaflet.markercluster";
import { MapLayerBase } from "../map-layer-base.component";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { Observable, tap } from "rxjs";
import { IFeature } from "src/app/shared/generated/model/i-feature";
import { TrashCaptureStatusTypeEnum } from "src/app/shared/generated/enum/trash-capture-status-type-enum";
@Component({
    selector: "inventoried-bmps-trash-capture-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./inventoried-bmps-trash-capture-layer.component.html",
    styleUrls: ["./inventoried-bmps-trash-capture-layer.component.scss"],
})
export class InventoriedBMPsTrashCaptureLayerComponent extends MapLayerBase implements OnChanges {
    public iconSrc: string;
    public legendName: string;

    public layer: L.markerClusterGroup = L.markerClusterGroup({
        iconCreateFunction: function (cluster) {
            var childCount = cluster.getChildCount();

            return new L.DivIcon({
                html: "<div><span>" + childCount + "</span></div>",
                className: "treatment-bmp-cluster",
                iconSize: new L.Point(40, 40),
            });
        },
    });

    public treatmentBMPs$: Observable<IFeature[]>;

    constructor(private treatmentBMPService: TreatmentBMPService) {
        super();
        
    }

    ngAfterViewInit(): void {
        this.treatmentBMPs$ = this.treatmentBMPService.treatmentBmpsVerifiedFeatureCollectionGet().pipe(
            tap((treatmentBMPs) => {
                const inventoriedTreatmentBMPsLayer = new L.GeoJSON(treatmentBMPs, {
                    pointToLayer: (feature, latlng) => {
                        var iconSrc ="./assets/main/map-icons/marker-icon-orange.png";
                        switch(feature.properties["TrashCaptureStatusTypeID"]){
                            case TrashCaptureStatusTypeEnum.Full:
                                iconSrc = "./assets/main/map-icons/marker-icon-red.png";
                                break;
                            case TrashCaptureStatusTypeEnum.Partial: 
                                iconSrc = "./assets/main/map-icons/marker-icon-blue.png";
                                break;
                            case TrashCaptureStatusTypeEnum.None: 
                                iconSrc = "./assets/main/map-icons/marker-icon-black.png";
                                break;
                            case TrashCaptureStatusTypeEnum.NotProvided: 
                                iconSrc = "./assets/main/map-icons/marker-icon-gray.png";
                                break;
                            default:
                                iconSrc = "./assets/main/map-icons/marker-icon-orange.png";
                                break;
                        }        
                        return L.marker(latlng, { icon: MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath(iconSrc) });
                    },
                    onEachFeature: (feature, layer) => {
                        layer.bindPopup(
                            `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${
                                feature.properties.TreatmentBMPName
                            }</a><br>` + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
                        );
                    },
                });
                this.layer.addLayer(inventoriedTreatmentBMPsLayer);
                this.initLayer();
            })
        );
    }

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
