import { ChangeDetectorRef, Component, OnInit, ViewChild } from "@angular/core";
import { AgGridModule } from "ag-grid-angular";
import { ColDef, GridApi, ValueGetterParams } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AuthenticationService } from "src/app/services/authentication.service";
import { NgbModalRef, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Router } from "@angular/router";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { FontAwesomeIconLinkRendererComponent } from "src/app/shared/components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component";
import { LinkRendererComponent } from "src/app/shared/components/ag-grid/link-renderer/link-renderer.component";
import { CustomDropdownFilterComponent } from "src/app/shared/components/custom-dropdown-filter/custom-dropdown-filter.component";
import { environment } from "src/environments/environment";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { NgIf } from "@angular/common";
import { CustomRichTextComponent } from "../../../shared/components/custom-rich-text/custom-rich-text.component";
import { AlertDisplayComponent } from "../../../shared/components/alert-display/alert-display.component";
import { NeptuneGridComponent } from "../../../shared/components/neptune-grid/neptune-grid.component";
import { ProjectDto } from "src/app/shared/generated/model/project-dto";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { PageHeaderComponent } from "../../../shared/components/page-header/page-header.component";

@Component({
    selector: "project-list",
    templateUrl: "./project-list.component.html",
    styleUrls: ["./project-list.component.scss"],
    standalone: true,
    imports: [AlertDisplayComponent, CustomRichTextComponent, AgGridModule, NgIf, NeptuneGridComponent, PageHeaderComponent],
})
export class ProjectListComponent implements OnInit {
    private currentUser: PersonDto;

    private gridApi: GridApi;
    public richTextTypeID = NeptunePageTypeEnum.HippocampProjectsList;
    public projectColumnDefs: Array<ColDef>;
    public defaultColDef: ColDef;
    private modalReference: NgbModalRef;
    private projectIDToDelete: number;
    public projectNameToDelete: string;
    private deleteColumnID = 1;
    public isLoadingDelete = false;
    public projects: ProjectDto[];

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private cdr: ChangeDetectorRef,
        private alertService: AlertService,
        private projectService: ProjectService,
        private utilityFunctionsService: UtilityFunctionsService,
        private confirmService: ConfirmService
    ) {}

    ngOnInit(): void {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;

            this.createProjectGridColDefs();
            this.updateGridData();

            this.cdr.detectChanges();
        });
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    public onGridReady(params) {
        this.gridApi = params.api;
    }

    private createProjectGridColDefs() {
        this.projectColumnDefs = [
            this.utilityFunctionsService.createActionsColumnDef((params: any) => {
                return [
                    { ActionName: "View", ActionHandler: () => this.router.navigateByUrl(`/projects/${params.data.ProjectID}`) },
                    {
                        ActionName: "Edit",
                        ActionIcon: "fas fa-edit",
                        ActionHandler: () => this.router.navigateByUrl(`/projects/edit/${params.data.ProjectID + (params.data.ShareOCTAM2Tier2Scores ? "/review-and-share" : "")}`),
                    },
                    { ActionName: "Delete", ActionIcon: "fa fa-trash text-danger", ActionHandler: () => this.deleteModal(params) },
                ];
            }),
            this.utilityFunctionsService.createLinkColumnDef("Project ID", "ProjectID", "ProjectID", {
                InRouterLink: "/projects/",
            }),
            this.utilityFunctionsService.createLinkColumnDef("Project Name", "ProjectName", "ProjectID", {
                InRouterLink: "/projects/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Organization", "Organization.OrganizationName"),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdiction.Organization.OrganizationName"),
            this.utilityFunctionsService.createDateColumnDef("Date Created", "DateCreated", "M/d/yyyy", { Width: 120 }),
            this.utilityFunctionsService.createBasicColumnDef("Project Description", "ProjectDescription"),
            {
                headerName: "Shared with OCTA M2 Tier 2 Grant Program",
                valueGetter: (params) => (params.data.ShareOCTAM2Tier2Scores ? "Yes" : "No"),
                filter: CustomDropdownFilterComponent,
                filterParams: { field: "ShareOCTAM2Tier2Scores" },
            },
            this.utilityFunctionsService.createDateColumnDef("Last Shared with OCTA M2 Tier 2 Grant Program", "OCTAM2Tier2ScoresLastSharedDate", "short"),
        ];

        this.defaultColDef = {
            filter: true,
            sortable: true,
            resizable: true,
        };
    }

    private updateGridData() {
        this.projectService.projectsGet().subscribe((projects) => {
            this.projects = projects;
        });
    }

    private deleteModal(params: ValueGetterParams<ProjectDto, any>) {
        const confirmOptions = {
            title: "Delete Project",
            message: `<p>You are about to delete ${params.data.ProjectName}.</p><p>Are you sure you wish to proceed?</p>`,
            buttonClassYes: "btn btn-danger",
            buttonTextYes: "Delete",
            buttonTextNo: "Cancel",
        };
        this.confirmService.confirm(confirmOptions).then((confirmed) => {
            if (confirmed) {
                this.projectService.projectsProjectIDDeleteDelete(params.data.ProjectID).subscribe(() => {
                    this.alertService.pushAlert(new Alert("Successfully deleted project", AlertContext.Success));
                    params.api.applyTransaction({ remove: [params.data] });
                });
            }
        });
    }

    public downloadProjectModelResults() {
        this.projectService.projectsDownloadGet().subscribe(
            (csv) => {
                //Create a fake object for us to click and download
                var a = document.createElement("a");
                a.href = URL.createObjectURL(csv);
                a.download = `project-modeled-results.csv`;
                document.body.appendChild(a);
                a.click();
                //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
                URL.revokeObjectURL(a.href);
                document.body.removeChild(a);
            },
            () => {
                this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
            }
        );
    }

    public downloadTreatmentBMPModelResults() {
        this.projectService.projectsTreatmentBMPsDownloadGet().subscribe(
            (csv) => {
                //Create a fake object for us to click and download
                var a = document.createElement("a");
                a.href = URL.createObjectURL(csv);
                a.download = `project-BMP-modeled-results.csv`;
                document.body.appendChild(a);
                a.click();
                //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
                URL.revokeObjectURL(a.href);
                document.body.removeChild(a);
            },
            () => {
                this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
            }
        );
    }

    public downloadTreatmentBMPDelineationShapefile() {
        return (
            environment.geoserverMapServiceUrl +
            "/ows?service=WFS&version=2.0&request=GetFeature&typeName=OCStormwater:TreatmentBMPDelineation&outputFormat=shape-zip&SrsName=EPSG:4326"
        );
    }

    public downloadTreatmentBMPLocationPointShapefile() {
        return (
            environment.geoserverMapServiceUrl +
            "/ows?service=WFS&version=2.0&request=GetFeature&typeName=OCStormwater:TreatmentBMPPointLocation&outputFormat=shape-zip&SrsName=EPSG:4326"
        );
    }
}
