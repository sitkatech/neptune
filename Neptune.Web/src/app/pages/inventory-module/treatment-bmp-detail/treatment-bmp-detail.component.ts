import { Component, OnInit, ViewChild, TemplateRef, Input } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { DatePipe, AsyncPipe, CommonModule } from "@angular/common";
import { Observable } from "rxjs";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
// TODO: Import the correct services and models for Treatment BMP
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPDto } from "src/app/shared/generated/model/treatment-bmp-dto";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { TreatmentBMPLifespanTypeEnum } from "src/app/shared/generated/enum/treatment-b-m-p-lifespan-type-enum";

@Component({
    selector: "treatment-bmp-detail",
    templateUrl: "./treatment-bmp-detail.component.html",
    styleUrls: ["./treatment-bmp-detail.component.scss"],
    standalone: true,
    imports: [CommonModule, RouterLink, DatePipe, AsyncPipe, PageHeaderComponent, AlertDisplayComponent, FieldDefinitionComponent],
})
export class TreatmentBmpDetailComponent implements OnInit {
    @ViewChild("templateRight", { static: true }) templateRight!: TemplateRef<any>;
    @ViewChild("templateAbove", { static: true }) templateAbove!: TemplateRef<any>;
    @Input() treatmentBMPID!: number;

    // Observables for async pipe
    treatmentBMP$!: Observable<TreatmentBMPDto>;
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

    public TreatmentBMPLifespanTypeEnum = TreatmentBMPLifespanTypeEnum;

    constructor(private router: Router, private treatmentBMPService: TreatmentBMPService) {}

    ngOnInit(): void {
        // treatmentBMPID will be set via input binding from the route param (withComponentInputBinding)
        // Example: Fetch detail data using the treatmentBMPID
        this.treatmentBMP$ = this.treatmentBMPService.treatmentBmpsTreatmentBMPIDGet(this.treatmentBMPID);
        // this.attachments$ = this.treatmentBMP$.pipe(
        //     switchMap(bmp => this.treatmentBMPService.treatmentBmpsTreatmentBMPIDAttachmentsGet(bmp.TreatmentBMPID))
        // );
    }

    getEditLink(treatmentBMP: any): string {
        // TODO: Return the correct edit route for this BMP
        return `/inventory/treatment-bmps/edit/${treatmentBMP.TreatmentBMPID}`;
    }
}
