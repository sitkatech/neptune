import { Component, inject, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { FormFieldComponent, FormFieldType, SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { OrganizationService } from "src/app/shared/generated/api/organization.service";
import { DialogRef } from "@ngneat/dialog";
import { OrganizationUpsertDto, OrganizationUpsertDtoForm, OrganizationUpsertDtoFormControls } from "src/app/shared/generated/model/organization-upsert-dto";
import { UserService } from "src/app/shared/generated/api/user.service";
import { map, Observable, tap } from "rxjs";
import { AsyncPipe } from "@angular/common";
import { OrganizationTypeService } from "src/app/shared/generated/api/organization-type.service";

@Component({
    selector: "organization-modal",
    standalone: true,
    imports: [ReactiveFormsModule, FormFieldComponent, AlertDisplayComponent, AsyncPipe],
    templateUrl: "./organization-modal.component.html",
    styleUrl: "./organization-modal.component.scss",
})
export class OrganizationModalComponent implements OnInit {
    public ref: DialogRef<{ mode: "add" | "edit"; organization: any }, boolean> = inject(DialogRef);
    public FormFieldType = FormFieldType;
    public formGroup = new FormGroup<OrganizationUpsertDtoForm>({
        OrganizationName: OrganizationUpsertDtoFormControls.OrganizationName(undefined, { validators: [Validators.required] }),
        OrganizationShortName: OrganizationUpsertDtoFormControls.OrganizationShortName(),
        OrganizationTypeID: OrganizationUpsertDtoFormControls.OrganizationTypeID(undefined, { validators: [Validators.required] }),
        IsActive: OrganizationUpsertDtoFormControls.IsActive(false),
        OrganizationUrl: OrganizationUpsertDtoFormControls.OrganizationUrl(),
        PrimaryContactPersonID: OrganizationUpsertDtoFormControls.PrimaryContactPersonID(),
        LogoFileResourceID: OrganizationUpsertDtoFormControls.LogoFileResourceID(),
    });
    public mode: "add" | "edit";

    public organizationTypeOptions$: Observable<SelectDropdownOption[]>;
    public peopleOptions$: Observable<SelectDropdownOption[]>;

    constructor(
        private alertService: AlertService,
        private organizationService: OrganizationService,
        private organizationTypeService: OrganizationTypeService,
        private userService: UserService
    ) {}

    ngOnInit(): void {
        this.alertService.clearAlerts();
        this.mode = this.ref.data.mode;

        if (this.mode === "edit" && this.ref.data.organization) {
            this.formGroup.patchValue({
                OrganizationName: this.ref.data.organization.OrganizationName,
                OrganizationShortName: this.ref.data.organization.OrganizationShortName,
                OrganizationTypeID: this.ref.data.organization.OrganizationType.OrganizationTypeID,
                IsActive: this.ref.data.organization.IsActive,
                OrganizationUrl: this.ref.data.organization.OrganizationUrl,
                PrimaryContactPersonID: this.ref.data.organization.PrimaryContactPerson?.PersonID,
                LogoFileResourceID: this.ref.data.organization.LogoFileResourceID,
            });
        }

        this.organizationTypeOptions$ = this.organizationTypeService
            .listSimpleOrganizationType()
            .pipe(map((types) => types.map((type) => ({ Label: type.OrganizationTypeName, Value: type.OrganizationTypeID, disabled: false }) as SelectDropdownOption)));

        this.peopleOptions$ = this.userService
            .listUser()
            .pipe(map((people) => people.map((person) => ({ Label: person.FullName!, Value: person.PersonID, disabled: false }) as SelectDropdownOption)));
    }

    save(): void {
        if (this.formGroup.invalid) return;
        const dto = new OrganizationUpsertDto(this.formGroup.value);
        if (this.mode === "add") {
            this.organizationService.createOrganization(dto).subscribe(() => {
                this.ref.close(true);
            });
        } else {
            const organizationID = this.ref.data.organization?.OrganizationID;
            this.organizationService.updateOrganization(organizationID, dto).subscribe(() => {
                this.ref.close(true);
            });
        }
    }

    cancel(): void {
        this.ref.close(null);
    }
}
