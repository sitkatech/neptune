import { Component, OnInit, OnChanges, SimpleChanges, ViewChild, TemplateRef, Input } from "@angular/core";
import { RouterLink } from "@angular/router";
import { AsyncPipe, CommonModule } from "@angular/common";
import { Observable } from "rxjs";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { ColDef } from "ag-grid-community";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { NeptuneGridComponent } from "src/app/shared/components/neptune-grid/neptune-grid.component";
import { PersonDisplayDto, StormwaterJurisdictionGridDto, TreatmentBMPGridDto } from "src/app/shared/generated/model/models";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { tap } from "rxjs";

@Component({
    selector: "jurisdiction-detail",
    templateUrl: "./jurisdiction-detail.component.html",
    styleUrls: ["./jurisdiction-detail.component.scss"],
    standalone: true,
    imports: [CommonModule, RouterLink, AsyncPipe, PageHeaderComponent, AlertDisplayComponent, NeptuneGridComponent],
})
export class JurisdictionDetailComponent implements OnInit, OnChanges {
    ngOnChanges(changes: SimpleChanges): void {
        if (changes["jurisdictionID"] && !changes["jurisdictionID"].firstChange) {
            this.loadData();
        }
    }

    @ViewChild("templateAbove", { static: true }) templateAbove!: TemplateRef<any>;
    @Input() jurisdictionID!: number;

    // Observables for async pipe
    jurisdiction$!: Observable<StormwaterJurisdictionGridDto>;
    users$: Observable<PersonDisplayDto[]>;
    treatmentBMPs$: Observable<TreatmentBMPGridDto[]>;
    treatmentBMPColumnDefs: Array<ColDef>;

    usersCol1: PersonDisplayDto[] = [];
    usersCol2: PersonDisplayDto[] = [];
    usersCol3: PersonDisplayDto[] = [];

    constructor(private stormwaterJurisdictionService: StormwaterJurisdictionService, private utilityFunctionsService: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.treatmentBMPColumnDefs = [
            this.utilityFunctionsService.createLinkColumnDef("Name", "TreatmentBMPName", "TreatmentBMPID", {
                InRouterLink: "/inventory/treatment-bmp-detail/",
            }),
            this.utilityFunctionsService.createBasicColumnDef("Jurisdiction", "StormwaterJurisdictionName"),
            this.utilityFunctionsService.createBasicColumnDef("Owner Organization", "OwnerOrganizationName"),
            this.utilityFunctionsService.createBasicColumnDef("Type", "TreatmentBMPTypeName"),
            this.utilityFunctionsService.createBasicColumnDef("Year Built", "YearBuilt"),
            this.utilityFunctionsService.createBasicColumnDef("Notes", "Notes"),
            this.utilityFunctionsService.createBasicColumnDef("Last Assessment Date", "LatestAssessmentDate"),
            this.utilityFunctionsService.createBasicColumnDef("Last Assessed Score", "LatestAssessmentScore"),
            this.utilityFunctionsService.createBasicColumnDef("# of Assessments", "NumberOfAssessments"),
            this.utilityFunctionsService.createBasicColumnDef("Last Maintenance Date", "LatestMaintenanceDate"),
            this.utilityFunctionsService.createBasicColumnDef("# of Maintenance Events", "NumberOfMaintenanceRecords"),
            this.utilityFunctionsService.createBasicColumnDef("Benchmark and Threshold Set?", "BenchmarkAndThresholdSet"),
            this.utilityFunctionsService.createBasicColumnDef("Required Lifespan of Installation", "TreatmentBMPLifespanTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Lifespan End Date (if Fixed End Date)", "TreatmentBMPLifespanEndDate"),
            this.utilityFunctionsService.createBasicColumnDef("Required Field Visits/Year", "RequiredFieldVisitsPerYear"),
            this.utilityFunctionsService.createBasicColumnDef("Required Post-Storm Field Visits/Year", "RequiredPostStormFieldVisitsPerYear"),
            this.utilityFunctionsService.createBasicColumnDef("Sizing Basis", "SizingBasisTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Status", "TrashCaptureStatusTypeDisplayName"),
            this.utilityFunctionsService.createBasicColumnDef("Trash Capture Effectiveness (%)", "TrashCaptureEffectiveness"),
            this.utilityFunctionsService.createBasicColumnDef("Delineation Type", "DelineationTypeDisplayName"),
        ];
        this.loadData();
    }

    private loadData(): void {
        this.jurisdiction$ = this.stormwaterJurisdictionService.getStormwaterJurisdiction(this.jurisdictionID);
        this.users$ = this.stormwaterJurisdictionService.listUsersStormwaterJurisdiction(this.jurisdictionID).pipe(
            tap((users) => {
                const third = Math.ceil(users.length / 3);
                this.usersCol1 = users.slice(0, third);
                this.usersCol2 = users.slice(third, third * 2);
                this.usersCol3 = users.slice(third * 2);
            })
        );
        this.treatmentBMPs$ = this.stormwaterJurisdictionService.listTreatmentBMPsStormwaterJurisdiction(this.jurisdictionID);
    }
}
