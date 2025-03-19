import { Component } from "@angular/core";
import { ActivatedRoute, Router, RouterLink } from "@angular/router";
import { BehaviorSubject, Observable, switchMap, tap } from "rxjs";
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
import { OvtaAreaLayerComponent } from "src/app/shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { ModalService, ModalSizeEnum, ModalThemeEnum } from "src/app/shared/services/modal/modal.service";
import { UpdateOvtaAreaDetailsModalComponent, UpdateOvtaAreaModalContext } from "../update-ovta-area-details-modal/update-ovta-area-details-modal.component";
import { OnlandVisualTrashAssessmentGridDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-grid-dto";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { OnlandVisualTrashAssessmentSimpleDto } from "src/app/shared/generated/model/models";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";

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
        LoadingDirective,
    ],
    templateUrl: "./trash-ovta-area-detail.component.html",
    styleUrl: "./trash-ovta-area-detail.component.scss",
})
export class TrashOvtaAreaDetailComponent {
    public onlandVisualTrashAssessmentArea$: Observable<OnlandVisualTrashAssessmentAreaDetailDto>;
    public ovtaColumnDefs: ColDef[];
    public onlandVisualTrashAssessments$: Observable<OnlandVisualTrashAssessmentGridDto[]>;

    public refreshOVTAAreasTrigger: BehaviorSubject<void> = new BehaviorSubject(null);
    public refreshOVTAAreasTrigger$: Observable<void> = this.refreshOVTAAreasTrigger.asObservable();

    public map: L.Map;
    public mapIsReady: boolean = false;
    public layerControl: L.Control.Layers;

    public isLoading: boolean;
    public isLoadingGrid: boolean = true;

    constructor(
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private onlandVisualTrashAssessmentAreaService: OnlandVisualTrashAssessmentAreaService,
        private utilityFunctionsService: UtilityFunctionsService,
        private route: ActivatedRoute,
        private alertService: AlertService,
        private modalService: ModalService,
        private confirmService: ConfirmService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.ovtaColumnDefs = [
            this.utilityFunctionsService.createActionsColumnDef((params: any) => {
                return [
                    { ActionName: "View", ActionHandler: () => this.router.navigate(["trash", "onland-visual-trash-assessments", params.data.OnlandVisualTrashAssessmentID]) },
                    {
                        ActionName: "Edit",
                        ActionIcon: "fas fa-edit",
                        ActionHandler: () =>
                            this.router.navigateByUrl(`/trash/onland-visual-trash-assessments/edit/${params.data.OnlandVisualTrashAssessmentID}/record-observations`),
                    },
                    //{ ActionName: "Delete", ActionIcon: "fa fa-trash text-danger", ActionHandler: () => this.deleteModal(params) },
                ];
            }),
            this.utilityFunctionsService.createLinkColumnDef("Assessment ID", "OnlandVisualTrashAssessmentID", "OnlandVisualTrashAssessmentID", {
                InRouterLink: "../../onland-visual-trash-assessments/",
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

        this.onlandVisualTrashAssessmentArea$ = this.refreshOVTAAreasTrigger$.pipe(
            tap(() => {
                this.isLoading = true;
            }),
            switchMap(() => {
                return this.route.params.pipe(
                    switchMap((params) => {
                        return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDGet(
                            params[routeParams.onlandVisualTrashAssessmentAreaID]
                        );
                    })
                );
            }),
            tap(() => {
                this.isLoading = false;
            })
        );

        this.onlandVisualTrashAssessments$ = this.route.params.pipe(
            switchMap((params) => {
                return this.onlandVisualTrashAssessmentAreaService.onlandVisualTrashAssessmentAreasOnlandVisualTrashAssessmentAreaIDOnlandVisualTrashAssessmentsGet(
                    params[routeParams.onlandVisualTrashAssessmentAreaID]
                );
            }),
            tap(() => (this.isLoadingGrid = false))
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    public addNewOVTA(onlandVisualTrashAssessmentAreaID: number, onlandVisualTrashAssessmentAreaName: string, stormwaterJurisdictionID: number) {
        const modalContents = `<p>You are about to begin a new OVTA for Assessment Area: ${onlandVisualTrashAssessmentAreaName}. 
        This will create a new Assessment record and allow you to start entering Assessment Observations on the next page.</p>`;
        this.confirmService
            .confirm({ buttonClassYes: "btn-primary", buttonTextYes: "Continue", buttonTextNo: "Cancel", title: "Add New OVTA", message: modalContents })
            .then((confirmed) => {
                if (confirmed) {
                    var onlandVisualTrashAssessmentSimpleDto = new OnlandVisualTrashAssessmentSimpleDto();
                    onlandVisualTrashAssessmentSimpleDto.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
                    onlandVisualTrashAssessmentSimpleDto.StormwaterJurisdictionID = stormwaterJurisdictionID;
                    onlandVisualTrashAssessmentSimpleDto.AssessingNewArea = false;
                    this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsPost(onlandVisualTrashAssessmentSimpleDto).subscribe((response) => {
                        this.alertService.clearAlerts();
                        this.alertService.pushAlert(new Alert("Your OVTA was successfully created.", AlertContext.Success));
                        this.router.navigate([`/trash/onland-visual-trash-assessments/edit/${response.OnlandVisualTrashAssessmentID}/record-observations`]);
                    });
                }
            });
    }

    public editLocation(assessmentsCount: number) {
        const modalContents =
            assessmentsCount < 2
                ? `<p>Any changes you make to the Assessment Area will apply to all future assessments.</p>`
                : `<p>Any changes you make to the Assessment Area will apply to the ${assessmentsCount} Assessments associated with this area. Proceed?</p>`;
        this.confirmService
            .confirm({ buttonClassYes: "btn-primary", buttonTextYes: "Continue", buttonTextNo: "Cancel", title: "Edit Location", message: modalContents })
            .then((confirmed) => {
                if (confirmed) {
                    this.router.navigate(["edit-location"], { relativeTo: this.route });
                }
            });
    }

    updateOVTAAreaDetails(ovtaAreaDto: OnlandVisualTrashAssessmentAreaDetailDto) {
        this.modalService
            .open(UpdateOvtaAreaDetailsModalComponent, null, { CloseOnClickOut: false, TopLayer: false, ModalSize: ModalSizeEnum.Medium, ModalTheme: ModalThemeEnum.Light }, {
                OnlandVisualTrashAssessmentAreaID: ovtaAreaDto.OnlandVisualTrashAssessmentAreaID,
                OnlandVisualTrashAssessmentAreaName: ovtaAreaDto.OnlandVisualTrashAssessmentAreaName,
                AssessmentAreaDescription: ovtaAreaDto.AssessmentAreaDescription,
            } as UpdateOvtaAreaModalContext)
            .instance.result.then((result) => {
                if (result) {
                    this.alertService.clearAlerts();
                    this.alertService.pushAlert(new Alert("Successfully updated Assessment Area.", AlertContext.Success));
                    this.refreshOVTAAreasTrigger.next();
                }
            });
    }
}
