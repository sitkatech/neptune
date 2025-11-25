import { Component, inject, Input, OnInit } from "@angular/core";
import { BehaviorSubject, combineLatest, map, Observable, switchMap } from "rxjs";
import { TreatmentBMPImageByTreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp-image-by-treatment-bmp.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPImageDto } from "src/app/shared/generated/model/models";
import { TreatmentBMPDto } from "src/app/shared/generated/model/treatment-bmp-dto";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { CanDeactivate, Router, RouterModule } from "@angular/router";
import { AsyncPipe, JsonPipe } from "@angular/common";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { LoadingDirective } from "src/app/shared/directives/loading.directive";
import { ImageEditorComponent, ImageEditorItem } from "src/app/shared/components/image-editor/image-editor.component";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { IDeactivateComponent } from "src/app/shared/guards/unsaved-changes.guard";
import { Form, FormGroup } from "@angular/forms";

@Component({
    selector: "treatment-bmp-update-images",
    imports: [PageHeaderComponent, RouterModule, AsyncPipe, AlertDisplayComponent, LoadingDirective, ImageEditorComponent],
    templateUrl: "./treatment-bmp-update-images.component.html",
    styleUrls: ["./treatment-bmp-update-images.component.scss"],
})
export class TreatmentBmpUpdateImagesComponent implements OnInit, IDeactivateComponent {
    private treatementBMPService = inject(TreatmentBMPService);
    private treatmentBMPImageService = inject(TreatmentBMPImageByTreatmentBMPService);
    private alertService = inject(AlertService);
    private router = inject(Router);

    @Input() treatmentBMPID?: number;

    public treatmentBMP$: Observable<TreatmentBMPDto>;
    public treatmentBMPImages$: Observable<TreatmentBMPImageDto[]>;
    public reloadTreatmentBMPImagesTrigger$: BehaviorSubject<void> = new BehaviorSubject<void>(undefined);
    public imageEditorItems$: Observable<ImageEditorItem[]>;
    public captionControlForm: FormGroup = new FormGroup({});
    public isLoadingSubmit = false;

    ngOnInit(): void {
        this.treatmentBMP$ = this.treatementBMPService.getByIDTreatmentBMP(this.treatmentBMPID!);

        this.treatmentBMPImages$ = combineLatest({
            treatmentBMP: this.treatmentBMP$,
            _: this.reloadTreatmentBMPImagesTrigger$,
        }).pipe(switchMap((result) => this.treatmentBMPImageService.listTreatmentBMPImageByTreatmentBMP(result.treatmentBMP.TreatmentBMPID!)));

        this.imageEditorItems$ = this.treatmentBMPImages$.pipe(
            map((images) =>
                images.map((bmpImage) => ({
                    PrimaryKey: bmpImage.TreatmentBMPImageID,
                    FileResourceGUID: bmpImage.FileResourceGUID,
                    Caption: bmpImage.Caption,
                }))
            )
        );
    }

    canExit(): boolean {
        return !this.captionControlForm.dirty;
    }

    public onNewImageAdded(event: { file: File; caption: string }): void {
        this.isLoadingSubmit = true;
        this.treatmentBMPImageService.createTreatmentBMPImageByTreatmentBMP(this.treatmentBMPID!, event.file, event.caption).subscribe({
            next: () => {
                this.reloadTreatmentBMPImagesTrigger$.next();
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Image added successfully.", AlertContext.Success));
                this.isLoadingSubmit = false;
            },
            error: () => {
                this.isLoadingSubmit = false;
            },
        });
    }

    public onImageDeleted(imageItem: ImageEditorItem): void {
        this.isLoadingSubmit = true;
        this.treatmentBMPImageService.deleteTreatmentBMPImageByTreatmentBMP(this.treatmentBMPID!, imageItem.PrimaryKey).subscribe({
            next: () => {
                this.reloadTreatmentBMPImagesTrigger$.next();
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Image deleted successfully.", AlertContext.Success));
                this.isLoadingSubmit = false;
            },
            error: () => {
                this.isLoadingSubmit = false;
            },
        });
    }

    public onSaveClicked(images: ImageEditorItem[]): void {
        this.isLoadingSubmit = true;

        let treatmentBMPImageUpdates: { TreatmentBMPImageID: number; Caption: string }[] = images.map((img) => ({
            TreatmentBMPImageID: img.PrimaryKey,
            Caption: img.Caption || "",
        }));

        this.treatmentBMPImageService.updateTreatmentBMPImageByTreatmentBMP(this.treatmentBMPID!, treatmentBMPImageUpdates).subscribe({
            next: () => {
                this.reloadTreatmentBMPImagesTrigger$.next();
                this.alertService.clearAlerts();
                this.alertService.pushAlert(new Alert("Images updated successfully.", AlertContext.Success));
                this.isLoadingSubmit = false;
            },
            error: () => {
                this.isLoadingSubmit = false;
            },
        });
    }

    public onCancelClicked(): void {
        this.router.navigate(["/treatment-bmps", this.treatmentBMPID]);
    }
}
