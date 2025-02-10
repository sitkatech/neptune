import { Component } from "@angular/core";
import { ActivatedRoute, RouterLink } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { AsyncPipe, DatePipe, NgIf } from "@angular/common";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NeptuneGridComponent } from "../../../../shared/components/neptune-grid/neptune-grid.component";
import { ColDef } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import "leaflet-draw";
import "leaflet.fullscreen";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";

@Component({
    selector: "trash-ovta-area-detail",
    standalone: true,
    imports: [
        PageHeaderComponent,
        AlertDisplayComponent,
        NgIf,
        AsyncPipe,
        DatePipe,
        FieldDefinitionComponent,
        NeptuneGridComponent,
        NeptuneMapComponent,
        TransectLineLayerComponent,
        OvtaAreaLayerComponent,
        RouterLink,
    ],
    templateUrl: "./trash-ovta-area-detail.component.html",
    styleUrl: "./trash-ovta-area-detail.component.scss",
})
export class TrashOvtaAreaDetailComponent {
    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public ovtaColumnDefs: ColDef[];

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    constructor(
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private utilityFunctionsService: UtilityFunctionsService,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.ovtaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment ID", "OnlandVisualTrashAssessmentID", "OnlandVisualTrashAssessmentID", {
                InRouterLink: "../../onland-visual-trash-assessment/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Score", "OnlandVisualTrashAssessmentScoreName"),
            this.utilityFunctionsService.createBasicColumnDef("Assessment Type", "IsProgressAssessment", { CustomDropdownFilterField: "IsProgressAssessment" }),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "CompletedDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Status", "OnlandVisualTrashAssessmentStatusName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentStatusName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Created By", "CreatedByPersonFullName"),
            this.utilityFunctionsService.createDateColumnDef("Created On", "CreatedDate", "short"),
        ];
        this.onlandVisualTrashAssessmentArea$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(
                    params[routeParams.onlandVisualTrashAssessmentAreaID]
                );
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }
}
