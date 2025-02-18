import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType, FormInputOption } from "src/app/shared/components/form-field/form-field.component";
import { SelectDropDownModule } from "ngx-select-dropdown";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { Observable, map } from "rxjs";
import { AsyncPipe } from "@angular/common";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentSimpleDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-simple-dto";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: "trash-initiate-ovta",
    standalone: true,
    imports: [PageHeaderComponent, ReactiveFormsModule, FormsModule, FormFieldComponent, SelectDropDownModule, AsyncPipe, FieldDefinitionComponent, NeptuneMapComponent],
    templateUrl: "./trash-initiate-ovta.component.html",
    styleUrl: "./trash-initiate-ovta.component.scss",
})
export class TrashInitiateOvtaComponent {
    public FormFieldType = FormFieldType;

    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public isLoadingSubmit = false;

    public stormwaterJurisdictionOptions$: Observable<SelectDropdownOption[]>;
    public selectedJurisdiction: FormControl = new FormControl();
    public selectedAreaType: FormControl = new FormControl();

    public jurisdictionDropdownConfig = {
        search: true,
        height: "320px",
        placeholder: "Select an jurisdiction",
        searchFn: (option: SelectDropdownOption) => option.Label,
        displayFn: (option: SelectDropdownOption) => option.Label,
    };

    public layerIsOnByDefaultOptions: FormInputOption[] = [
        { Value: false, Label: "Reassess existing area", Disabled: false },
        { Value: true, Label: "Assess new area", Disabled: false },
    ];

    constructor(
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private router: Router,
        private route: ActivatedRoute
    ) {}

    ngOnInit() {
        this.stormwaterJurisdictionOptions$ = this.stormwaterJurisdictionService.jurisdictionsGet().pipe(
            map((list) => {
                if (list.length == 1) {
                    return [{ Value: list[0].StormwaterJurisdictionID, Label: list[0].Organization.OrganizationName, Disabled: false }];
                }

                let options = list.map((x) => ({ Value: x.StormwaterJurisdictionID, Label: x.Organization.OrganizationName } as SelectDropdownOption));
                options = [{ Value: null, Label: "- Select -", Disabled: true }, ...options]; // insert an empty option at the front
                return options;
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
    }

    public save(andContinue: boolean = false) {
        this.isLoadingSubmit = true;
        // if (this.projectID) {
        //     this.projectService.projectsProjectIDUpdatePost(this.projectID, this.formGroup.getRawValue()).subscribe((response) => {
        //         this.isLoadingSubmit = false;
        //         this.alertService.clearAlerts();
        //         this.alertService.pushAlert(new Alert("Your project was successfully updated.", AlertContext.Success));
        //         this.projectWorkflowProgressService.updateProgress(this.projectID);
        //         //this.formGroup.patchValue(response);
        //         this.formGroup.markAsPristine();
        //         if (andContinue) {
        //             this.router.navigate(["../stormwater-treatments/treatment-bmps"], { relativeTo: this.route });
        //         }
        //     });
        // } else {
        var dto = new OnlandVisualTrashAssessmentSimpleDto();
        dto.AssessingNewArea = this.selectedAreaType.getRawValue();
        dto.OnlandVisualTrashAssessmentAreaID = 1;
        dto.StormwaterJurisdictionID = this.selectedJurisdiction.getRawValue().Value;
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsPost(dto).subscribe((response) => {
            this.isLoadingSubmit = false;
            console.log(response);
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your ovta was successfully created.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(response.OnlandVisualTrashAssessmentID);
            // this.formGroup.patchValue(response);
            // this.formGroup.markAsPristine();
            if (andContinue) {
                this.router.navigate([`../../edit/${response.OnlandVisualTrashAssessmentID}/record-observations`], { relativeTo: this.route });
            }
        });
        // }
        // }
    }
}
