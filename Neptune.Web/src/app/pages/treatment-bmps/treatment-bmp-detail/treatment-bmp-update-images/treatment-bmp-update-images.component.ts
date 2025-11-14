import { Component, inject, Input, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { TreatmentBMPImageByTreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp-image-by-treatment-bmp.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPImageDto } from "src/app/shared/generated/model/models";
import { TreatmentBMPDto } from "src/app/shared/generated/model/treatment-bmp-dto";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { RouterModule } from "@angular/router";
import { AsyncPipe, JsonPipe } from "@angular/common";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { ImageEditorComponent } from "src/app/shared/components/image-editor/image-editor.component";

@Component({
    selector: "treatment-bmp-update-images",
    imports: [PageHeaderComponent, RouterModule, AsyncPipe, AlertDisplayComponent, LoadingDirective, JsonPipe, ImageEditorComponent],
    templateUrl: "./treatment-bmp-update-images.component.html",
    styleUrl: "./treatment-bmp-update-images.component.scss",
})
export class TreatmentBmpUpdateImagesComponent implements OnInit {
    private treatementBMPService = inject(TreatmentBMPService);
    private treatmentBMPImageService = inject(TreatmentBMPImageByTreatmentBMPService);

    @Input() treatmentBMPID?: number;

    public treatmentBMP$: Observable<TreatmentBMPDto>;
    public treatmentBMPImages$: Observable<TreatmentBMPImageDto[]>;

    public isLoadingSubmit = false;

    ngOnInit(): void {
        this.treatmentBMP$ = this.treatementBMPService.getByIDTreatmentBMP(this.treatmentBMPID!);
        this.treatmentBMPImages$ = this.treatmentBMPImageService.listTreatmentBMPImageByTreatmentBMP(this.treatmentBMPID!);
    }

    public save(): void {}
    public cancel(): void {}
}
