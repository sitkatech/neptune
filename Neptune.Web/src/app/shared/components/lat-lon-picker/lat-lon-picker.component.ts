import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, TemplateRef } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormControl, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType } from "src/app/shared/components/forms/form-field/form-field.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { IconComponent } from "src/app/shared/components/icon/icon.component";
import * as L from "leaflet";

@Component({
    selector: "lat-lon-picker",
    standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule, FormFieldComponent, NeptuneMapComponent, IconComponent],
    templateUrl: "./lat-lon-picker.component.html",
    styleUrls: ["./lat-lon-picker.component.scss"],
})
export class LatLonPickerComponent implements OnInit, OnDestroy {
    public FormFieldType = FormFieldType;
    @Input() latControl: FormControl;
    @Input() lonControl: FormControl;
    /** Optional template to render custom instructions or help UI. If not provided, projected content with attribute [lat-lon-instructions] will be rendered. */
    @Input() instructionsTemplate?: TemplateRef<any>;

    // emits when user selects a location (either from map or geolocation)
    @Output() locationSelected = new EventEmitter<{ lat: number; lon: number }>();

    private map: L.Map | null = null;
    private clickHandler: any;
    private marker: L.Marker | null = null;
    public mapHeight: string = "400px";

    ngOnInit(): void {}

    onMapLoad(event: NeptuneMapInitEvent) {
        this.map = event.map as L.Map;

        // attach click handler
        this.clickHandler = (e: any) => {
            const lat = e.latlng?.lat ?? e.latitude ?? null;
            const lon = e.latlng?.lng ?? e.longitude ?? null;
            if (lat != null && lon != null) {
                this.setLatLon(lat, lon);
            }
        };

        if (this.map) {
            this.map.on("click", this.clickHandler);

            // if controls already have values, show marker
            const latVal = this.latControl?.value;
            const lonVal = this.lonControl?.value;
            if (latVal != null && lonVal != null) {
                this.updateMarker(latVal, lonVal);
                try {
                    this.map.setView([latVal, lonVal], 15);
                } catch {}
            }
        }
    }

    useCurrentLocation() {
        if (!navigator || !navigator.geolocation) {
            return;
        }

        navigator.geolocation.getCurrentPosition(
            (pos) => {
                const lat = pos.coords.latitude;
                const lon = pos.coords.longitude;
                this.setLatLon(lat, lon);

                // center map if available
                if (this.map) {
                    this.map.setView([lat, lon], 15);
                }
            },
            (err) => {
                console.warn("Geolocation error", err);
            },
            { enableHighAccuracy: true }
        );
    }

    private setLatLon(lat: number, lon: number) {
        if (this.latControl) {
            this.latControl.setValue(lat);
            try {
                this.latControl.markAsDirty();
                this.latControl.markAsTouched();
            } catch {}
        }
        if (this.lonControl) {
            this.lonControl.setValue(lon);
            try {
                this.lonControl.markAsDirty();
                this.lonControl.markAsTouched();
            } catch {}
        }
        this.locationSelected.emit({ lat, lon });

        // update marker on the map
        this.updateMarker(lat, lon);
    }

    private updateMarker(lat: number, lon: number) {
        if (!this.map) return;
        if (this.marker) {
            this.marker.setLatLng([lat, lon]);
        } else {
            this.marker = L.marker([lat, lon], {
                icon: L.icon({
                    iconUrl: "assets/main/map-icons/marker-icon-blue.png",
                    iconSize: [28, 40],
                    iconAnchor: [14, 40],
                    shadowUrl: "",
                }),
            });
            this.marker.addTo(this.map);
        }
    }

    public clearLocation() {
        if (this.latControl) this.latControl.reset();
        if (this.lonControl) this.lonControl.reset();

        if (this.marker && this.map) {
            try {
                this.map.removeLayer(this.marker);
            } catch {}
            this.marker = null;
        }
    }

    ngOnDestroy(): void {
        if (this.map && this.clickHandler) {
            this.map.off("click", this.clickHandler);
        }

        if (this.marker && this.map) {
            try {
                this.map.removeLayer(this.marker);
            } catch {}
            this.marker = null;
        }
    }
}
