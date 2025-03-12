import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType, FormInputOption } from "src/app/shared/components/form-field/form-field.component";
import { SelectDropdownOption } from "src/app/shared/components//form-field/form-field.component";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { Observable, map } from "rxjs";
import { AsyncPipe, NgIf } from "@angular/common";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import {
    OnlandVisualTrashAssessmentSimpleDto,
    OnlandVisualTrashAssessmentSimpleDtoForm,
    OnlandVisualTrashAssessmentSimpleDtoFormControls,
} from "src/app/shared/generated/model/onland-visual-trash-assessment-simple-dto";
import { AlertService } from "src/app/shared/services/alert.service";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { OvtaWorkflowProgressService } from "src/app/shared/services/ovta-workflow-progress.service";
import { ActivatedRoute, Router } from "@angular/router";
import { GroupByPipe } from "src/app/shared/pipes/group-by.pipe";
import { WfsService } from "src/app/shared/services/wfs.service";
import { LandUseBlockLayerComponent } from "../../../../shared/components/leaflet/layers/land-use-block-layer/land-use-block-layer.component";
import { ParcelLayerComponent } from "../../../../shared/components/leaflet/layers/parcel-layer/parcel-layer.component";

@Component({
    selector: "trash-initiate-ovta",
    standalone: true,
    imports: [PageHeaderComponent, ReactiveFormsModule, FormsModule, FormFieldComponent, AsyncPipe, NeptuneMapComponent, LandUseBlockLayerComponent, NgIf, ParcelLayerComponent],
    templateUrl: "./trash-initiate-ovta.component.html",
    styleUrl: "./trash-initiate-ovta.component.scss",
})
export class TrashInitiateOvtaComponent {
    public FormFieldType = FormFieldType;

    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public isLoadingSubmit = false;
    public layer: L.featureGroup = new L.featureGroup();

    public stormwaterJurisdictionOptions$: Observable<SelectDropdownOption[]>;
    public selectedOVTAArea: FormControl = new FormControl("");
    public selectedOVTAAreaID: number;
    public selectedOVTAAreaName: string = "";

    public layerIsOnByDefaultOptions: FormInputOption[] = [
        { Value: false, Label: "Reassess existing area", Disabled: false },
        { Value: true, Label: "Assess new area", Disabled: false },
    ];

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

    public formGroup: FormGroup<OnlandVisualTrashAssessmentSimpleDtoForm> = new FormGroup<any>({
        AssessingNewArea: OnlandVisualTrashAssessmentSimpleDtoFormControls.AssessingNewArea(),
        OnlandVisualTrashAssessmentAreaID: OnlandVisualTrashAssessmentSimpleDtoFormControls.OnlandVisualTrashAssessmentAreaID(),
        StormwaterJurisdictionID: OnlandVisualTrashAssessmentSimpleDtoFormControls.StormwaterJurisdictionID(),
    });

    constructor(
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService,
        private alertService: AlertService,
        private ovtaWorkflowProgressService: OvtaWorkflowProgressService,
        private router: Router,
        private route: ActivatedRoute,
        private wfsService: WfsService,
        private groupByPipe: GroupByPipe
    ) {}

    ngOnInit() {
        this.formGroup.controls.AssessingNewArea.patchValue(false);
        this.stormwaterJurisdictionOptions$ = this.stormwaterJurisdictionService.jurisdictionsGet().pipe(
            map((list) => {
                if (list.length == 1) {
                    return [{ Value: list[0].StormwaterJurisdictionID, Label: list[0].Organization.OrganizationName, Disabled: false }];
                }

                let options = list.map((x) => ({ Value: x.StormwaterJurisdictionID, Label: x.Organization.OrganizationName } as SelectDropdownOption));
                return options;
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.layer.addTo(this.map);
        this.addOVTAAreasToLayer();
    }

    public save(andContinue: boolean = false) {
        this.isLoadingSubmit = true;
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsPost(this.formGroup.getRawValue()).subscribe((response) => {
            this.isLoadingSubmit = false;
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your ovta was successfully created.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(response.OnlandVisualTrashAssessmentID);
            if (andContinue) {
                this.router.navigate([`../../${response.OnlandVisualTrashAssessmentID}/record-observations`], { relativeTo: this.route });
            }
        });
    }

    private addOVTAAreasToLayer() {
        let cql_filter = ``;

        this.wfsService.getGeoserverWFSLayerWithCQLFilter("OCStormwater:AssessmentAreaExport", cql_filter, "OVTAAreaID").subscribe((response) => {
            if (response.length == 0) return;
            const featuresGroupedByOVTAAreaID = this.groupByPipe.transform(response, "properties.OVTAAreaID");
            Object.keys(featuresGroupedByOVTAAreaID).forEach((ovtaAreaID) => {
                const geoJson = L.geoJSON(featuresGroupedByOVTAAreaID[ovtaAreaID], {
                    style: this.defaultStyle,
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });

                geoJson.on("click", (e) => {
                    this.onOVTAAreaSelected(Number(ovtaAreaID), featuresGroupedByOVTAAreaID[ovtaAreaID][0].properties.OVTAAreaName);
                });

                geoJson.addTo(this.layer);
            });
        });
    }

    private getStormwaterJurisdictionBounds(jurisdictionID: number) {
        this.wfsService
            .getGeoserverWFSLayerWithCQLFilter("OCStormwater:Jurisdictions", `StormwaterJurisdictionID = ${jurisdictionID}`, "StormwaterJurisdictionID")
            .subscribe((response) => {
                this.map.fitBounds(L.geoJson(response).getBounds());
            });
    }

    public onJurisdictionSelected(event: any) {
        this.getStormwaterJurisdictionBounds(event.Value);
    }

    private onOVTAAreaSelected(ovtaAreaID: number, ovtaAreaName: string) {
        this.selectedOVTAAreaID = ovtaAreaID;
        this.selectedOVTAAreaName = ovtaAreaName;
        this.selectedOVTAArea.setValue(this.selectedOVTAAreaName);
        this.formGroup.controls.OnlandVisualTrashAssessmentAreaID.patchValue(ovtaAreaID);
        this.highlightSelectedOVTAArea();
    }

    private highlightSelectedOVTAArea() {
        this.layer.eachLayer((layer) => {
            // skip if well layer
            if (layer.options?.icon) return;

            const geoJsonLayers = layer.getLayers();
            if (geoJsonLayers[0].feature.properties.OVTAAreaID == this.selectedOVTAAreaID) {
                layer.setStyle(this.highlightStyle);
                this.map.fitBounds(layer.getBounds());
            } else {
                layer.setStyle(this.defaultStyle);
            }
        });
    }
}
