import { AfterViewInit, Component, EventEmitter, Input, OnChanges, Output, SimpleChange } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";

import { WfsService } from "src/app/shared/services/wfs.service";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { PriorityLandUseTypeEnum, PriorityLandUseTypes } from "src/app/shared/generated/enum/priority-land-use-type-enum";

@Component({
    selector: "selected-land-use-block-layer",
    imports: [],
    templateUrl: "./selected-land-use-block-layer.component.html",
    styleUrl: "./selected-land-use-block-layer.component.scss"
})
export class SelectedLandUseBlockLayerComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() selectedLandUseBlockID: number;

    @Output() layerBoundsCalculated = new EventEmitter();
    @Output() landUseBlockSelected = new EventEmitter<number>();

    public isLoading: boolean = false;
    public landUseBlockSelectedWithinLayer: boolean = false;
    public layer: L.featureGroup;

    private styleDictionary = {
        [PriorityLandUseTypeEnum.Commercial]: {
            color: "#c2fbfc",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.HighDensityResidential]: {
            color: "#c0d6fc",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.Industrial]: {
            color: "#b4fcb3",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.MixedUrban]: {
            color: "#fcb6b9",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.CommercialRetail]: {
            color: "#f2cafc",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.PublicTransportationStations]: {
            color: "#fcd6b6",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
        [PriorityLandUseTypeEnum.ALU]: {
            color: "#ffffed",
            weight: 2,
            opacity: 1,
            fillOpacity: 0.5,
        },
    };

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 1,
        fillOpacity: 0.5,
    };

    constructor(private wfsService: WfsService, private groupByPipe: GroupByPipe) {
        super();
    }

    ngOnChanges(changes: any): void {
        if (changes.selectedLandUseBlockID) {
            if (this.landUseBlockSelectedWithinLayer) {
                this.landUseBlockSelectedWithinLayer = false;
                return;
            }
            if (changes.selectedLandUseBlockID.previousValue == changes.selectedLandUseBlockID.currentValue) return;
            this.selectedLandUseBlockID = changes.selectedLandUseBlockID.currentValue;
            this.highlightSelectedLandUseBlock(true);
        } else if (Object.values(changes).some((x: SimpleChange) => x.firstChange === false)) {
            this.updateLayer();
        }
    }

    ngAfterViewInit(): void {
        this.setupLayer();
        this.updateLayer();
    }

    private updateLayer() {
        this.isLoading = true;
        this.layer.clearLayers();

        this.addLandUseBlocksToLayer();

        this.layer.addTo(this.map);
        this.isLoading = false;
    }

    private addLandUseBlocksToLayer() {
        let cql_filter = ``;

        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:LandUseBlocks", cql_filter, "LandUseBlockID").subscribe((response) => {
            if (response.length == 0) return;

            const featuresGroupedByLandUseBlockID = this.groupByPipe.transform(response, "properties.LandUseBlockID");

            Object.keys(featuresGroupedByLandUseBlockID).forEach((landUseBlockID) => {
                const geoJson = L.geoJSON(featuresGroupedByLandUseBlockID[landUseBlockID], {
                    style: this.styleDictionary[featuresGroupedByLandUseBlockID[landUseBlockID][0].properties.PriorityLandUseTypeID],
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.75 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });

                geoJson.on("click", (e) => {
                    this.onLandUseBlockSelected(Number(landUseBlockID));
                });

                geoJson.addTo(this.layer);
            });
        });
    }

    private onLandUseBlockSelected(landUseBlockID: number) {
        if (landUseBlockID == this.selectedLandUseBlockID) {
            return;
        }
        this.selectedLandUseBlockID = landUseBlockID;
        this.landUseBlockSelectedWithinLayer = true;
        this.highlightSelectedLandUseBlock();

        this.landUseBlockSelected.emit(landUseBlockID);
    }

    private highlightSelectedLandUseBlock(zoomToFeature: boolean = false) {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.LandUseBlockID == this.selectedLandUseBlockID) {
                layer.setStyle(this.highlightStyle);
                if (zoomToFeature) {
                    this.map.fitBounds(layer.getBounds());
                }
            } else {
                layer.setStyle(this.styleDictionary[geoJsonLayers[0].feature.properties.PriorityLandUseTypeID]);
            }
        });
    }

    private setupLayer() {
        this.layer = L.geoJSON();
        this.initLayer();
    }
}
