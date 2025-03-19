import { Component, Input, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { Observable, tap } from "rxjs";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { IFeature } from "src/app/shared/generated/model/models";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
    selector: "transect-line-layer-by-ovta",
    standalone: true,
    imports: [NgIf, AsyncPipe],
    templateUrl: "./transect-line-layer-by-ovta.component.html",
    styleUrl: "./transect-line-layer-by-ovta.component.scss",
})
export class TransectLineLayerByOvtaComponent extends MapLayerBase implements OnChanges {
    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService) {
        super();
    }

    @Input() ovtaID: number;
    public layer;

    private transectLineStyle = {
        color: "#f70a0a",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    public transectLines$: Observable<IFeature[]>;

    ngAfterViewInit(): void {
        this.transectLines$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDTransectLineAsFeatureCollectionGet(this.ovtaID).pipe(
            tap((transectLineFeatureCollection) => {
                this.layer = new L.GeoJSON(transectLineFeatureCollection, {
                    style: this.transectLineStyle,
                });
                this.initLayer();
            })
        );
    }
}
