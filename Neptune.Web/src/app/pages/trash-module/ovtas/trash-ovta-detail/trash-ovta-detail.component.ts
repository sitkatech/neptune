import { AsyncPipe, DatePipe, KeyValuePipe, NgFor, NgIf } from "@angular/common";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import * as L from "leaflet";
import "leaflet-draw";
import "leaflet.fullscreen";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { environment } from "src/environments/environment";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { OvtaObservationLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-observation-layer/ovta-observation-layer.component";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";

@Component({
    selector: "trash-ovta-detail",
    standalone: true,
    imports: [
        NgIf,
        AsyncPipe,
        AlertDisplayComponent,
        PageHeaderComponent,
        FieldDefinitionComponent,
        DatePipe,
        NgFor,
        KeyValuePipe,
        NeptuneMapComponent,
        OvtaObservationLayerComponent,
        OvtaAreaLayerComponent,
        TransectLineLayerComponent,
    ],
    templateUrl: "./trash-ovta-detail.component.html",
    styleUrl: "./trash-ovta-detail.component.scss",
})
export class TrashOvtaDetailComponent {
    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }

    getUrl(fileResourceGUID) {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }
}
