import { Component, Input, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { Observable, tap } from "rxjs";
import { IFeature } from "src/app/shared/generated/model/models";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";

@Component({
    selector: "ovta-area-layer",
    standalone: true,
    imports: [NgIf, AsyncPipe],
    templateUrl: "./ovta-area-layer.component.html",
    styleUrl: "./ovta-area-layer.component.scss",
})
export class OvtaAreaLayerComponent extends MapLayerBase implements OnChanges {
    constructor(private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService) {
        super();
    }

    @Input() ovtaAreaID: number;
    public layer;

    private ovtaAreaStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    private transectLineStyle = {
        color: "#ff42ff",
        weight: 2,
    };

    public featureCollection$: Observable<IFeature[]>;

    ngAfterViewInit(): void {
        this.featureCollection$ = this.onlandVisualTrashAssessmentAreaService
            .onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(this.ovtaAreaID)
            .pipe(
                tap((transectLineFeatureCollection) => {
                    this.layer = new L.GeoJSON(transectLineFeatureCollection, {
                        style: this.ovtaAreaStyle,
                    });
                    this.initLayer();
                })
            );
    }
}
