import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import "leaflet-gesture-handling";
import "leaflet.fullscreen";
import "leaflet-loading";
import "leaflet.markercluster";
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { ActivatedRoute } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentAreaService } from "src/app/shared/generated/api/onland-visual-trash-assessment-area.service";
import { OnlandVisualTrashAssessmentAreaDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-area-detail-dto";
import { TransectLineLayerComponent } from "../../../../shared/components/leaflet/layers/transect-line-layer/transect-line-layer.component";
import { OvtaAreaLayerComponent } from "src/app/shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { SelectParcelLayerComponent } from "../../../../shared/components/leaflet/layers/select-parcel-layer/select-parcel-layer.component";

@Component({
    selector: "trash-ovta-area-edit-location",
    standalone: true,
    imports: [
        PageHeaderComponent,
        NeptuneMapComponent,
        LandUseBlockLayerComponent,
        NgIf,
        AsyncPipe,
        OvtaAreaLayerComponent,
        TransectLineLayerComponent,
        SelectParcelLayerComponent,
    ],
    templateUrl: "./trash-ovta-area-edit-location.component.html",
    styleUrl: "./trash-ovta-area-edit-location.component.scss",
})
export class TrashOvtaAreaEditLocationComponent {
    public customRichTextTypeID = NeptunePageTypeEnum.EditOVTAArea;

    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public mapHeight = window.innerHeight - window.innerHeight * 0.4 + "px";
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;

    constructor(private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService, private route: ActivatedRoute) {}

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    ngOnInit(): void {
        this.onlandVisualTrashAssessmentArea$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(
                    params[routeParams.onlandVisualTrashAssessmentAreaID]
                );
            })
        );
    }
}
