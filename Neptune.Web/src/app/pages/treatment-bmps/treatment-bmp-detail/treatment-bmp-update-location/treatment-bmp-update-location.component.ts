import { Component, inject, Input, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Router, RouterModule } from "@angular/router";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { LatLonPickerComponent } from "src/app/shared/components/lat-lon-picker/lat-lon-picker.component";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { TreatmentBMPLocationUpdate, TreatmentBMPLocationUpdateForm, TreatmentBMPLocationUpdateFormControls } from "src/app/shared/generated/model/treatment-bmp-location-update";
import { TreatmentBMPDto } from "src/app/shared/generated/model/treatment-bmp-dto";
import { AsyncPipe } from "@angular/common";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";

@Component({
    selector: "treatment-bmp-update-location",
    standalone: true,
    imports: [PageHeaderComponent, RouterModule, ReactiveFormsModule, AlertDisplayComponent, LatLonPickerComponent, AsyncPipe],
    templateUrl: "./treatment-bmp-update-location.component.html",
    styleUrls: ["./treatment-bmp-update-location.component.scss"],
})
export class TreatmentBmpUpdateLocationComponent implements OnInit {
    private treatmentBMPService = inject(TreatmentBMPService);
    private router = inject(Router);

    @Input() treatmentBMPID?: number;

    public formGroup: FormGroup<TreatmentBMPLocationUpdateForm> = new FormGroup<TreatmentBMPLocationUpdateForm>({
        Latitude: TreatmentBMPLocationUpdateFormControls.Latitude(undefined),
        Longitude: TreatmentBMPLocationUpdateFormControls.Longitude(undefined),
    });

    public treatmentBMP$: Observable<TreatmentBMPDto>;

    public isLoadingSubmit = false;

    ngOnInit(): void {
        this.treatmentBMP$ = this.treatmentBMPService.getByIDTreatmentBMP(this.treatmentBMPID!).pipe(
            tap((bmp) => {
                if (bmp.Latitude != null && bmp.Longitude != null) {
                    this.formGroup.controls.Latitude.setValue(bmp.Latitude);
                    this.formGroup.controls.Longitude.setValue(bmp.Longitude);
                    this.formGroup.markAsPristine();
                }
            })
        );
    }

    public save(): void {
        this.isLoadingSubmit = true;
        const updateLocationDto = this.formGroup.value as TreatmentBMPLocationUpdate;
        this.treatmentBMPService.updateLocationTreatmentBMP(this.treatmentBMPID, updateLocationDto).subscribe({
            next: (bmp: TreatmentBMPDto) => {
                this.isLoadingSubmit = false;
                this.formGroup.markAsPristine();
                this.router.navigate(["/treatment-bmps", bmp.TreatmentBMPID]);
            },
            error: () => {
                this.isLoadingSubmit = false;
            },
        });
    }

    public cancel(): void {
        if (this.treatmentBMPID) {
            this.router.navigate(["/treatment-bmps", this.treatmentBMPID]);
        }
    }
}
