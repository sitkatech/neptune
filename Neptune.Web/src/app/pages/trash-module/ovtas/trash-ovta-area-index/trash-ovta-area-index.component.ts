import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ColDef } from "ag-grid-community";
import { Observable, tap } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-grid-dto";
import { AsyncPipe, NgIf } from "@angular/common";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { environment } from "src/environments/environment";
import { IconComponent } from "../../../../shared/components/icon/icon.component";
import { HybridMapGridComponent } from "../../../../shared/components/hybrid-map-grid/hybrid-map-grid.component";
import { NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { Map, layerControl } from "leaflet";
import { SelectedOvtaAreaLayerComponent } from "src/app/shared/components/leaflet/layers/selected-ovta-area-layer/selected-ovta-area-layer.component";

@Component({
    selector: "trash-ovta-area-index",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NgIf, AsyncPipe, LoadingDirective, IconComponent, HybridMapGridComponent, SelectedOvtaAreaLayerComponent],
    templateUrl: "./trash-ovta-area-index.component.html",
    styleUrl: "./trash-ovta-area-index.component.scss",
})
export class TrashOvtaAreaIndexComponent {
    public onlandVisualTrashAssessmentAreas$: Observable<OnlandVisualTrashAssessmentAreaGridDto[]>;
    public ovtaAreaColumnDefs: ColDef[];
    public isLoading: boolean = true;
    public url = environment.ocStormwaterToolsBaseUrl;
    public ovtaAreaID: number;

    public map: Map;
    public layerControl: layerControl;
    public bounds: any;
    public mapIsReady: boolean = false;

    constructor(private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.ovtaAreaColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Assessment Area Name", "OnlandVisualTrashAssessmentAreaName", "OnlandVisualTrashAssessmentAreaID"),
            this.utilityFunctionsService.createBasicColumnDef("Baseline Score", "OnlandVisualTrashAssessmentBaselineScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentBaselineScoreName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Progress Score", "OnlandVisualTrashAssessmentProgressScoreName", {
                CustomDropdownFilterField: "OnlandVisualTrashAssessmentProgressScoreName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Number of Assessments Completed", "NumberOfAssessmentsCompleted"),
            this.utilityFunctionsService.createDateColumnDef("Last Assessment Date", "LastAssessmentDate", "short"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName", {
                CustomDropdownFilterField: "StormwaterJurisdictionName",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Description", "AssessmentAreaDescription"),
        ];
        this.onlandVisualTrashAssessmentAreas$ = this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasGet().pipe(tap((x) => (this.isLoading = false)));
    }

    public handleMapReady(event: NeptuneMapInitEvent) {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    public onSelectedOVTAAreaChanged(selectedOVTAAreaID) {
        if (this.ovtaAreaID == selectedOVTAAreaID) return;

        this.ovtaAreaID = selectedOVTAAreaID;
        return this.ovtaAreaID;
    }

    public handleLayerBoundsCalculated(bounds: any) {
        this.bounds = bounds;
    }
}
