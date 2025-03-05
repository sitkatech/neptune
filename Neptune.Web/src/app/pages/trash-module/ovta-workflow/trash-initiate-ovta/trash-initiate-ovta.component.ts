import { Component } from "@angular/core";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormFieldComponent, FormFieldType, FormInputOption } from "src/app/shared/components/form-field/form-field.component";
import { SelectDropDownModule } from "ngx-select-dropdown";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { Observable, map } from "rxjs";
import { AsyncPipe, NgIf } from "@angular/common";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import * as L from "leaflet";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentSimpleDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-simple-dto";
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
    imports: [
        PageHeaderComponent,
        ReactiveFormsModule,
        FormsModule,
        FormFieldComponent,
        SelectDropDownModule,
        AsyncPipe,
        FieldDefinitionComponent,
        NeptuneMapComponent,
        LandUseBlockLayerComponent,
        NgIf,
        ParcelLayerComponent,
    ],
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
    public selectedJurisdiction: FormControl = new FormControl();
    public selectedAreaType: FormControl = new FormControl(false);

    public jurisdictionDropdownConfig = {
        search: true,
        height: "320px",
        placeholder: "Select an jurisdiction",
        searchFn: (option: SelectDropdownOption) => option.Label,
        displayFn: (option: SelectDropdownOption) => option.Label,
    };

    public layerIsOnByDefaultOptions: FormInputOption[] = [
        { Value: false, Label: "Reassess existing area", Disabled: false },
        { Value: true, Label: "Assess new area", Disabled: false },
    ];

    private styleDictionary = {
        "A": {
            color: "#00FF00",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
            graphicFill: "Slash",
        },
        "B": {
            color: "#ebc400",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        "C": {
            color: "#FF7F7F",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        "D": {
            color: "#c500ff",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        "Not Assessed": {
            color: "#808080",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
        "null": {
            color: "#808080",
            weight: 2,
            opacity: 0.65,
            fillOpacity: 0.1,
        },
    };

    private highlightStyle = {
        color: "#fcfc12",
        weight: 2,
        opacity: 0.65,
        fillOpacity: 0.1,
    };

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
        this.stormwaterJurisdictionOptions$ = this.stormwaterJurisdictionService.jurisdictionsGet().pipe(
            map((list) => {
                if (list.length == 1) {
                    return [{ Value: list[0].StormwaterJurisdictionID, Label: list[0].Organization.OrganizationName, Disabled: false }];
                }

                let options = list.map((x) => ({ Value: x.StormwaterJurisdictionID, Label: x.Organization.OrganizationName } as SelectDropdownOption));
                options = [{ Value: null, Label: "- Select -", Disabled: true }, ...options]; // insert an empty option at the front
                return options;
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.addOVTAAreasToLayer();
    }

    public save(andContinue: boolean = false) {
        this.isLoadingSubmit = true;
        var dto = new OnlandVisualTrashAssessmentSimpleDto();
        dto.AssessingNewArea = this.selectedAreaType.getRawValue();
        dto.OnlandVisualTrashAssessmentAreaID = 1;
        dto.StormwaterJurisdictionID = this.selectedJurisdiction.getRawValue().Value;
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsPost(dto).subscribe((response) => {
            this.isLoadingSubmit = false;
            this.alertService.clearAlerts();
            this.alertService.pushAlert(new Alert("Your ovta was successfully created.", AlertContext.Success));
            this.ovtaWorkflowProgressService.updateProgress(response.OnlandVisualTrashAssessmentID);
            if (andContinue) {
                this.router.navigate([`../../edit/${response.OnlandVisualTrashAssessmentID}/record-observations`], { relativeTo: this.route });
            }
        });
    }

    private addOVTAAreasToLayer() {
        let cql_filter = ``;

        this.wfsService.getGeoserverWFSLayer("OCStormwater:AssessmentAreaExport", cql_filter, "OVTAAreaID").subscribe((response) => {
            if (response.length == 0) return;

            const featuresGroupedByOVTAAreaID = this.groupByPipe.transform(response, "properties.OVTAAreaID");
            Object.keys(featuresGroupedByOVTAAreaID).forEach((ovtaAreaID) => {
                const geoJson = L.geoJSON(featuresGroupedByOVTAAreaID[ovtaAreaID], {
                    style: this.styleDictionary[featuresGroupedByOVTAAreaID[ovtaAreaID][0].properties.Score],
                });
                geoJson.on("mouseover", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.5 });
                });
                geoJson.on("mouseout", (e) => {
                    geoJson.setStyle({ fillOpacity: 0.1 });
                });

                // geoJson.on("click", (e) => {
                //     this.onOVTAAreaSelected(Number(ovtaAreaID));
                // });

                geoJson.addTo(this.layer);
            });
        });
    }

    private getStormwaterJurisdictionBounds(jurisdictionID) {
        this.wfsService.getGeoserverWFSLayer("OCStormwater:Jurisdictions", `StormwaterJurisdictionID = ${jurisdictionID}`, "StormwaterJurisdictionID").subscribe((response) => {
            this.map.fitBounds(L.geoJson(response).getBounds());
        });
    }

    public onJurisdictionSelected(event: any) {
        this.getStormwaterJurisdictionBounds(event.Value);
    }

    // private onOVTAAreaSelected(ovtaAreaID: number) {
    //     this.selectedOVTAAreaID = ovtaAreaID;
    //     this.highlightSelectedOVTAArea();

    //     this.ovtaAreaSelected.emit(ovtaAreaID);
    // }

    // private highlightSelectedOVTAArea() {
    //     this.layer.eachLayer((layer) => {
    //         // skip if well layer
    //         if (layer.options?.icon) return;

    //         const geoJsonLayers = layer.getLayers();
    //         if (geoJsonLayers[0].feature.properties.OVTAAreaID == this.selectedOVTAAreaID) {
    //             layer.setStyle(this.highlightStyle);
    //             this.map.fitBounds(layer.getBounds());
    //         } else {
    //             layer.setStyle(this.styleDictionary[geoJsonLayers[0].feature.properties.Score]);
    //         }
    //     });
    // }
}
