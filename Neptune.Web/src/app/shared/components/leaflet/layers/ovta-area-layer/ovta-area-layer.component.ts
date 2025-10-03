import { Component, Input, OnChanges } from "@angular/core";
import * as L from "leaflet";
import { MapLayerBase } from "../map-layer-base.component";
import { Observable, tap } from "rxjs";
import { IFeature } from "src/app/shared/generated/model/models";
import { AsyncPipe } from "@angular/common";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";

@Component({
    selector: "ovta-area-layer",
    imports: [AsyncPipe],
    templateUrl: "./ovta-area-layer.component.html",
    styleUrl: "./ovta-area-layer.component.scss",
})
export class OvtaAreaLayerComponent extends MapLayerBase implements OnChanges {
    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService
    ) {
        super();
    }

    @Input() ovtaID: number;
    @Input() ovtaAreaID: number;
    public layer;

    private ovtaAreaStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    public featureCollection$: Observable<IFeature[]>;

    ngAfterViewInit(): void {
        if (this.ovtaID) {
            this.featureCollection$ = this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDAreaAsFeatureCollectionGet(this.ovtaID).pipe(
                tap((transectLineFeatureCollection) => {
                    this.layer = new L.GeoJSON(transectLineFeatureCollection as any, {
                        style: this.ovtaAreaStyle,
                    });
                    this.initLayer();
                })
            );
        } else if (this.ovtaAreaID) {
            this.featureCollection$ = this.onlandVisualTrashAssessmentAreaService
                .onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDAreaAsFeatureCollectionGet(this.ovtaAreaID)
                .pipe(
                    tap((transectLineFeatureCollection) => {
                        this.layer = new L.GeoJSON(transectLineFeatureCollection as any, {
                            style: this.ovtaAreaStyle,
                        });
                        this.initLayer();
                    })
                );
        }
    }
}
