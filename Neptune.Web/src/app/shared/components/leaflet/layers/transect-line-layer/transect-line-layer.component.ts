import { Component, Input, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { Observable, tap } from "rxjs";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { IFeature } from "src/app/shared/generated/model/i-feature";
import { AsyncPipe, NgIf } from "@angular/common";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";

@Component({
    selector: "transect-line-layer",
    standalone: true,
    imports: [NgIf, AsyncPipe],
    templateUrl: "./transect-line-layer.component.html",
    styleUrl: "./transect-line-layer.component.scss",
})
export class TransectLineLayerComponent extends MapLayerBase implements OnChanges {
    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService
    ) {
        super();
    }

    @Input() ovtaID: number;
    @Input() ovtaAreaID: number;
    public layer;

    private transectLineStyle = {
        color: "#ff42ff",
        weight: 2,
    };

    public featureCollection$: Observable<IFeature[]>;

    ngAfterViewInit(): void {
        if (this.ovtaID) {
            this.featureCollection$ = this.onlandVisualTrashAssessmentService
                .onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDTransectLineAsFeatureCollectionGet(this.ovtaID)
                .pipe(
                    tap((transectLineFeatureCollection) => {
                        this.layer = new L.GeoJSON(transectLineFeatureCollection, {
                            style: this.transectLineStyle,
                        });
                        this.initLayer();
                    })
                );
        } else if (this.ovtaAreaID) {
            this.featureCollection$ = this.onlandVisualTrashAssessmentAreaService
                .onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDTransectLineAsFeatureCollectionGet(this.ovtaAreaID)
                .pipe(
                    tap((transectLineFeatureCollection) => {
                        console.log(transectLineFeatureCollection);
                        this.layer = new L.GeoJSON(transectLineFeatureCollection, {
                            style: this.transectLineStyle,
                        });
                        this.initLayer();
                    })
                );
        }
    }
}
