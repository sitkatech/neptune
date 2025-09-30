import { Component, OnInit, ViewChild, TemplateRef, Input } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { DatePipe, AsyncPipe, CommonModule } from "@angular/common";
import { Observable } from "rxjs";
import { map, switchMap } from "rxjs/operators";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
// TODO: Import the correct services and models for Treatment BMP
// import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
// import { TreatmentBMPUpsertDto, ProjectDocumentDto } from "src/app/shared/generated/model/models";

@Component({
    selector: "treatment-bmp-detail",
    templateUrl: "./treatment-bmp-detail.component.html",
    styleUrls: ["./treatment-bmp-detail.component.scss"],
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        DatePipe,
        AsyncPipe,
        PageHeaderComponent,
        AlertDisplayComponent,
        // TODO: Add shared components as needed
    ],
})
export class TreatmentBmpDetailComponent implements OnInit {
    @ViewChild("templateRight", { static: true }) templateRight!: TemplateRef<any>;
    @ViewChild("templateAbove", { static: true }) templateAbove!: TemplateRef<any>;
    @Input() treatmentBMPID!: number;

    // Observables for async pipe
    treatmentBMP$!: Observable<any>; // TODO: Replace 'any' with correct model
    attachments$!: Observable<any[]>; // TODO: Replace 'any' with ProjectDocumentDto

    // Placeholder properties for template bindings
    isAnonymousOrUnassigned = false;
    delineationErrors: string[] = [];
    parameterizationErrors: string[] = [];
    openRevisionRequest: any = null;
    openRevisionRequestDetailUrl = "";
    upstreamestBMP: any = null;
    isUpstreamestBMPAnalyzedInModelingModule = false;
    currentPersonCanManage = false;
    isAnalyzedInModelingModule = false;
    isSitkaAdmin = false;
    // TODO: Add more properties as needed

    constructor(private router: Router) {} // private treatmentBMPService: TreatmentBMPService

    ngOnInit(): void {
        // treatmentBMPID will be set via input binding from the route param (withComponentInputBinding)
        // Example: Fetch detail data using the treatmentBMPID
        // this.treatmentBMP$ = this.treatmentBMPService.treatmentBmpsTreatmentBMPIDGet(this.treatmentBMPID);
        // this.attachments$ = this.treatmentBMP$.pipe(
        //     switchMap(bmp => this.treatmentBMPService.treatmentBmpsTreatmentBMPIDAttachmentsGet(bmp.TreatmentBMPID))
        // );
    }

    getEditLink(treatmentBMP: any): string {
        // TODO: Return the correct edit route for this BMP
        return `/inventory/treatment-bmps/edit/${treatmentBMP.TreatmentBMPID}`;
    }
}
