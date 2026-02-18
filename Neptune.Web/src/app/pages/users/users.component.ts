import { Component, OnInit } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { AsyncPipe } from "@angular/common";
import { ColDef } from "ag-grid-community";
import { Observable } from "rxjs";
import { UserService } from "src/app/shared/generated/api/user.service";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { AlertService } from "src/app/shared/services/alert.service";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { PersonSimpleDto } from "src/app/shared/generated/model/person-simple-dto";

@Component({
    selector: "users",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent, AsyncPipe],
    templateUrl: "./users.component.html",
    styleUrl: "./users.component.scss",
})
export class UsersComponent implements OnInit {
    public users$: Observable<PersonSimpleDto[]>;
    public columnDefs: ColDef[];

    constructor(
        private utilityFunctions: UtilityFunctionsService,
        private alertService: AlertService,
        private confirmService: ConfirmService,
        private userService: UserService
    ) {}

    ngOnInit(): void {
        this.columnDefs = [
            this.utilityFunctions.createActionsColumnDef((params: any) => [
                {
                    ActionName: "Delete",
                    ActionIcon: "fa fa-trash text-danger",
                    ActionHandler: () => this.deleteUser(params.data),
                },
            ]),
            this.utilityFunctions.createBasicColumnDef("First Name", "FirstName"),
            this.utilityFunctions.createBasicColumnDef("Last Name", "LastName"),
            this.utilityFunctions.createBasicColumnDef("Email", "Email"),
            this.utilityFunctions.createBasicColumnDef("Phone", "Phone"),
            this.utilityFunctions.createBasicColumnDef("Role", "RoleName", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBasicColumnDef("Organization", "OrganizationName", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBooleanColumnDef("Active?", "IsActive", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBooleanColumnDef("Support Emails?", "ReceiveSupportEmails", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBooleanColumnDef("RSB Revision Emails?", "ReceiveRSBRevisionRequestEmails", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBooleanColumnDef("OCTA Grant Reviewer?", "IsOCTAGrantReviewer", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createBooleanColumnDef("Assigned Stormwater Jurisdiction?", "HasAssignedStormwaterJurisdiction", {
                UseCustomDropdownFilter: true,
            }),
            this.utilityFunctions.createDateColumnDef("Created", "CreateDate", "short"),
            this.utilityFunctions.createDateColumnDef("Updated", "UpdateDate", "short"),
            this.utilityFunctions.createDateColumnDef("Last Activity", "LastActivityDate", "short"),
        ];
        this.users$ = this.userService.listUser();
    }

    deleteUser(user: PersonSimpleDto) {
        this.confirmService
            .confirm({
                title: "Delete User",
                message: `Are you sure you want to delete user '<strong>${user.FirstName} ${user.LastName}</strong>'?`,
                buttonTextYes: "Delete",
                buttonTextNo: "Cancel",
                buttonClassYes: "btn-danger",
            })
            .then((confirmed) => {
                if (confirmed) {
                    this.userService.deleteUser(user.PersonID).subscribe(() => {
                        this.alertService.clearAlerts();
                        this.alertService.pushAlert(new Alert("User deleted successfully.", AlertContext.Success));
                        this.users$ = this.userService.listUser();
                    });
                }
            });
    }
}
