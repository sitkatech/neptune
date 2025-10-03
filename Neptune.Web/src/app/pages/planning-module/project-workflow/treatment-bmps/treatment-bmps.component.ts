import { ApplicationRef, ChangeDetectorRef, Component, ComponentRef, OnInit, ViewChild } from "@angular/core";
import * as L from "leaflet";
import { Router } from "@angular/router";
import { Input } from "@angular/core";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { forkJoin, Observable } from "rxjs";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { TreatmentBMPModelingTypeEnum } from "src/app/shared/generated/enum/treatment-b-m-p-modeling-type-enum";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { TreatmentBMPTypeWithModelingAttributesDto } from "src/app/shared/generated/model/treatment-bmp-type-with-modeling-attributes-dto";
import { TreatmentBMPUpsertDto } from "src/app/shared/generated/model/treatment-bmp-upsert-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { ProjectDto, TreatmentBMPDisplayDto } from "src/app/shared/generated/model/models";
import { FieldDefinitionComponent } from "src/app/shared/components/field-definition/field-definition.component";
import { FormsModule } from "@angular/forms";
import { CustomRichTextComponent } from "src/app/shared/components/custom-rich-text/custom-rich-text.component";
import { AsyncPipe, CommonModule } from "@angular/common";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "src/app/shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { ModalService, ModalSizeEnum, ModalThemeEnum } from "src/app/shared/services/modal/modal.service";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { ConfirmOptions } from "src/app/shared/services/confirm/confirm-options";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { DelineationsLayerComponent } from "src/app/shared/components/leaflet/layers/delineations-layer/delineations-layer.component";
import { JurisdictionsLayerComponent } from "src/app/shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { RegionalSubbasinsLayerComponent } from "src/app/shared/components/leaflet/layers/regional-subbasins-layer/regional-subbasins-layer.component";
import { StormwaterNetworkLayerComponent } from "src/app/shared/components/leaflet/layers/stormwater-network-layer/stormwater-network-layer.component";
import { WqmpsLayerComponent } from "src/app/shared/components/leaflet/layers/wqmps-layer/wqmps-layer.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "src/app/shared/components/leaflet/neptune-map/neptune-map.component";
import { ProjectWorkflowProgressService } from "src/app/shared/services/project-workflow-progress.service";
import { InventoriedBMPsLayerComponent } from "src/app/shared/components/leaflet/layers/inventoried-bmps-layer/inventoried-bmps-layer.component";
import { TreatmentBMPTypeService } from "src/app/shared/generated/api/treatment-bmp-type.service";
import { CustomAttributeTypePurposeEnum } from "src/app/shared/generated/enum/custom-attribute-type-purpose-enum";
import { CustomAttributeTypeDto } from "src/app/shared/generated/model/custom-attribute-type-dto";
import { CustomAttributeDataTypeEnum } from "src/app/shared/generated/enum/custom-attribute-data-type-enum";
import { CustomAttributeUpsertDto } from "src/app/shared/generated/model/custom-attribute-upsert-dto";
import { TreatmentBMPTypeCustomAttributeTypeDto } from "src/app/shared/generated/model/treatment-bmp-type-custom-attribute-type-dto";
import { TreatmentBMPTypeCustomAttributeTypeService } from "src/app/shared/generated/api/treatment-bmp-type-custom-attribute-type.service";
import { PopperDirective } from "src/app/shared/directives/popper.directive";

@Component({
    selector: "treatment-bmps",
    templateUrl: "./treatment-bmps.component.html",
    styleUrls: ["./treatment-bmps.component.scss"],
    imports: [
        AsyncPipe,
        CustomRichTextComponent,
        FormsModule,
        FieldDefinitionComponent,
        PageHeaderComponent,
        WorkflowBodyComponent,
        AlertDisplayComponent,
        NeptuneMapComponent,
        RegionalSubbasinsLayerComponent,
        DelineationsLayerComponent,
        JurisdictionsLayerComponent,
        WqmpsLayerComponent,
        StormwaterNetworkLayerComponent,
        InventoriedBMPsLayerComponent,
        PopperDirective,
        CommonModule,
    ],
})
export class TreatmentBmpsComponent implements OnInit {
    @Input() projectID!: number;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampTreatmentBMPs;

    public mapIsReady: boolean = false;

    @ViewChild("editTreatmentBMPTypeModal") editTreatmentBMPTypeModal;
    private editTreatmentBMPTypeModalComponent: ComponentRef<ModalComponent>;

    private zoomOnSelection: boolean = false;

    public mapID: string = "treatmentBMPMap";
    public visibleTreatmentBMPStyle: string = "treatmentBMP_purple_outline_only";
    public projectTreatmentBMPs: Array<TreatmentBMPUpsertDto>;
    public project: ProjectUpsertDto = new ProjectUpsertDto();
    private originalTreatmentBMPs: string;
    private originalDoesNotIncludeTreatmentBMPs: boolean;
    public selectedTreatmentBMPStyle: string = "treatmentBMP_yellow";
    public zoomMapToDefaultExtent: boolean = true;
    public mapHeight: string = "750px";
    public defaultFitBoundsOptions?: L.FitBoundsOptions = null;
    public onEachFeatureCallback?: (feature, layer) => void;

    public component: any;
    public map: L.Map;
    public featureLayer: any;
    public layerControl: L.Control.Layers;
    public boundingBox$: Observable<BoundingBoxDto>;
    public selectedListItem: number;
    public selectedListItemDetails: { [key: string]: any } = {};
    public selectedObjectMarker: L.Layer;
    public selectedTreatmentBMP: TreatmentBMPUpsertDto;
    public selectedTreatmentBMPType: number;
    public treatmentBMPsLayer: L.GeoJSON<any>;
    public newTreatmentBMPType: TreatmentBMPTypeWithModelingAttributesDto;

    public treatmentBMPModelingTypeEnum = TreatmentBMPModelingTypeEnum;
    public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
    public treatmentBMPTypes: Array<TreatmentBMPTypeWithModelingAttributesDto>;
    public newTreatmentBMPIndex = -1;
    public isLoadingSubmit = false;
    public isEditingLocation = false;

    public delineations: DelineationUpsertDto[];
    public treatmentBMPTypeCustomAttributeTypes: TreatmentBMPTypeCustomAttributeTypeDto[];

    constructor(
        private cdr: ChangeDetectorRef,
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private treatmentBMPTypeService: TreatmentBMPTypeService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private modalService: ModalService,
        private alertService: AlertService,
        private projectWorkflowProgressService: ProjectWorkflowProgressService,
        private router: Router,
        private confirmService: ConfirmService,
        private treatmentBMPTypeCustomAttributeTypeService: TreatmentBMPTypeCustomAttributeTypeService
    ) {}

    canExit() {
        if (this.projectID) {
            return this.originalDoesNotIncludeTreatmentBMPs == this.project.DoesNotIncludeTreatmentBMPs && this.originalTreatmentBMPs == JSON.stringify(this.projectTreatmentBMPs);
        }
        return true;
    }

    public ngOnInit(): void {
        this.boundingBox$ = this.projectService.getBoundingBoxByProjectIDProject(this.projectID);
        this.compileService.configure(this.appRef);
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    public handleMapReady(event: NeptuneMapInitEvent): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;

        if (this.projectID) {
            this.projectService.getProject(this.projectID).subscribe((project) => {
                // redirect to review step if project is shared with OCTA grant program
                if (project.ShareOCTAM2Tier2Scores) {
                    this.router.navigateByUrl(`/planning/projects/edit/${this.projectID}/review-and-share`);
                } else {
                    this.mapProjectDtoToProject(project);
                    forkJoin({
                        projectTreatmentBMPs: this.projectService.listTreatmentBMPsAsUpsertDtosByProjectIDProject(this.projectID),
                        delineations: this.projectService.listDelineationsByProjectIDProject(this.projectID),
                        treatmentBMPTypes: this.treatmentBMPTypeService.listTreatmentBMPType(),
                        treatmentBMPTypeCustomAttributeTypes:
                            this.treatmentBMPTypeCustomAttributeTypeService.getTreatmentBMPTypeCustomAttributeTypeByCustomAttributePurposeIDTreatmentBMPTypeCustomAttributeType(
                                CustomAttributeTypePurposeEnum.Modeling
                            ),
                    }).subscribe(({ projectTreatmentBMPs, delineations, treatmentBMPTypes, treatmentBMPTypeCustomAttributeTypes: treatmentBMPTypeCustomAttributeTypes }) => {
                        this.originalDoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
                        this.projectTreatmentBMPs = projectTreatmentBMPs;
                        this.originalTreatmentBMPs = JSON.stringify(projectTreatmentBMPs);
                        this.delineations = delineations;
                        this.treatmentBMPTypes = treatmentBMPTypes;
                        this.treatmentBMPTypeCustomAttributeTypes = treatmentBMPTypeCustomAttributeTypes;
                        this.updateMapLayers();
                    });
                }
            });
        }

        this.registerClickEvents();
    }

    private mapProjectDtoToProject(project: ProjectDto) {
        this.project.ProjectName = project.ProjectName;
        this.project.OrganizationID = project.OrganizationID;
        this.project.StormwaterJurisdictionID = project.StormwaterJurisdictionID;
        this.project.PrimaryContactPersonID = project.PrimaryContactPersonID;
        this.project.ProjectDescription = project.ProjectDescription;
        this.project.AdditionalContactInformation = project.AdditionalContactInformation;
        this.project.DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
        this.project.CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores;
        this.project.ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores;
    }

    public updateMapLayers(): void {
        this.updateTreatmentBMPsLayer();

        if (this.projectTreatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        }
    }

    public updateTreatmentBMPsLayer() {
        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }

        let hasFlownToSelectedObject = false;

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson as any, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            filter: (feature) => {
                return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID;
            },
            onEachFeature: (feature, layer: L.Marker) => {
                if (this.selectedTreatmentBMP != null && this.zoomOnSelection && !hasFlownToSelectedObject) {
                    if (layer.feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID) {
                        return;
                    }
                    this.map.flyTo(layer.getLatLng(), 18);
                }
            },
        });
        this.treatmentBMPsLayer.addTo(this.map);

        this.treatmentBMPsLayer.on("click", (event: L.LeafletEvent) => {
            if (this.isEditingLocation) {
                return;
            }
            this.selectTreatmentBMP(event.propagatedFrom.feature.properties.TreatmentBMPID);
        });
    }

    private mapTreatmentBMPsToGeoJson(treatmentBMPs: TreatmentBMPDisplayDto[]) {
        return {
            type: "FeatureCollection",
            features: treatmentBMPs.map((x) => {
                let treatmentBMPGeoJson = {
                    type: "Feature",
                    geometry: {
                        type: "Point",
                        coordinates: [x.Longitude ?? 0, x.Latitude ?? 0],
                    },
                    properties: {
                        TreatmentBMPID: x.TreatmentBMPID,
                        TreatmentBMPName: x.TreatmentBMPName,
                        TreatmentBMPTypeName: x.TreatmentBMPTypeName,
                        Latitude: x.Latitude,
                        Longitude: x.Longitude,
                    },
                };
                return treatmentBMPGeoJson;
            }),
        };
    }

    public registerClickEvents(): void {
        this.map.on("click", (event: L.LeafletMouseEvent) => {
            if (!this.isEditingLocation) {
                return;
            }
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = L.marker(event.latlng, { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

            this.selectedObjectMarker.addTo(this.map);

            this.selectedTreatmentBMP.Latitude = event.latlng.lat;
            this.selectedTreatmentBMP.Longitude = event.latlng.lng;
            this.updateTreatmentBMPsLayer();
        });
    }

    public selectTreatmentBMP(treatmentBMPID: number) {
        this.isEditingLocation = false;
        this.selectTreatmentBMPImpl(treatmentBMPID);
        this.updateTreatmentBMPsLayer();
    }

    private selectTreatmentBMPImpl(treatmentBMPID: number) {
        this.clearSelectedItem();

        this.selectedListItem = treatmentBMPID;
        let selectedNumber = null;
        let selectedAttributes = null;
        this.selectedTreatmentBMP = this.projectTreatmentBMPs.find((x) => x.TreatmentBMPID == treatmentBMPID);
        selectedAttributes = [
            `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
            `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
            `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`,
        ];

        if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
            this.selectedObjectMarker = L.marker(
                { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
                { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 }
            );

            this.selectedObjectMarker.addTo(this.map);
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }

        if (this.selectedTreatmentBMP.TreatmentBMPTypeID != null) {
            this.updateModelingAttributesForSelectedTreatmentBMP();
        }
    }

    public updateModelingAttributesForSelectedTreatmentBMP(): void {
        if (!this.selectedTreatmentBMP || this.selectedTreatmentBMP.TreatmentBMPTypeID == null) return;
        if (this.selectedTreatmentBMP.ModelingAttributes == null) {
            this.selectedTreatmentBMP.ModelingAttributes = new Array<CustomAttributeUpsertDto>();
        }
        let treatmentBMPTypeCustomAttributeTypes = this.getTreatmentBMPTypeCustomAttributeTypesForTreatmentBMPType(this.selectedTreatmentBMP.TreatmentBMPTypeID);
        for (let treatmentBMPTypeCustomAttributeType of treatmentBMPTypeCustomAttributeTypes) {
            let index = this.selectedTreatmentBMP.ModelingAttributes.findIndex((x) => x.CustomAttributeTypeID == treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
            if (index == -1) {
                this.selectedTreatmentBMP.ModelingAttributes.push(
                    new CustomAttributeUpsertDto({
                        TreatmentBMPID: this.selectedTreatmentBMP.TreatmentBMPID,
                        TreatmentBMPTypeID: this.selectedTreatmentBMP.TreatmentBMPTypeID,
                        TreatmentBMPTypeCustomAttributeTypeID: treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                        CustomAttributeTypeID: treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID,
                        CustomAttributeValues: new Array(treatmentBMPTypeCustomAttributeType.CustomAttributeType.CustomAttributeTypeDefaultValue),
                    })
                );
            } else if (
                treatmentBMPTypeCustomAttributeType.CustomAttributeType.CustomAttributeTypeDefaultValue != null &&
                (this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues == null ||
                    this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues.length == 0 ||
                    this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues[0] == null ||
                    this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues[0] == "")
            ) {
                this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues = new Array(
                    treatmentBMPTypeCustomAttributeType.CustomAttributeType.CustomAttributeTypeDefaultValue
                );
            }
        }
    }

    public getTreatmentBMPTypeCustomAttributeTypesForTreatmentBMPType(treatmentBMPTypeID: number) {
        return this.treatmentBMPTypeCustomAttributeTypes.filter((x) => x.TreatmentBMPTypeID == treatmentBMPTypeID);
    }

    public getCustomAttributeFieldsToDisplay(treatmentBMPTypeID: number): Array<CustomAttributeTypeDto> {
        return this.getTreatmentBMPTypeCustomAttributeTypesForTreatmentBMPType(treatmentBMPTypeID)
            .sort((a, b) => a.SortOrder - b.SortOrder)
            .map((x) => x.CustomAttributeType);
    }

    public getTypeNameByTypeID(typeID: number) {
        return this.treatmentBMPTypes.find((x) => x.TreatmentBMPTypeID == typeID).TreatmentBMPTypeName ?? -1;
    }

    public isFieldWithDropdown(customAttributeDataTypeID: number): boolean {
        return customAttributeDataTypeID == CustomAttributeDataTypeEnum.PickFromList;
    }

    public getFieldName(customAttributeDataTypeID: number): string {
        return `selectedTreatmentBMP.ModelingAttributes[${this.getIndexOfCustomAttribute(customAttributeDataTypeID)}].CustomAttributeValues[0]`;
    }

    public getDropdownItemsForCustomAttributeType(optionsSchema: string): string[] {
        return optionsSchema
            .replaceAll("[", "")
            .replaceAll("]", "")
            .replaceAll('"', "")
            .split(",")
            .map((x) => x.trim());
    }

    public getIndexOfCustomAttribute(customAttributeTypeID: number): number {
        let value = this.selectedTreatmentBMP.ModelingAttributes.findIndex((x) => x.CustomAttributeTypeID == customAttributeTypeID);
        return value;
    }

    public convertSelectedBMPNumericFieldToString(enteredVal: number, customAttributeTypeID: number) {
        let index = this.getIndexOfCustomAttribute(customAttributeTypeID);
        this.selectedTreatmentBMP.ModelingAttributes[index].CustomAttributeValues[0] = enteredVal.toString();
    }

    public toggleIsEditingLocation() {
        this.isEditingLocation = !this.isEditingLocation;
        this.updateTreatmentBMPsLayer();
    }

    openEditTreatmentBMPTypeModal(): void {
        this.selectedTreatmentBMPType = this.selectedTreatmentBMP.TreatmentBMPTypeID;
        this.editTreatmentBMPTypeModalComponent = this.modalService.open(this.editTreatmentBMPTypeModal, null, {
            ModalTheme: ModalThemeEnum.Light,
            ModalSize: ModalSizeEnum.Medium,
        });
    }

    closeEditTreatmentBMPTypeModal(): void {
        if (!this.editTreatmentBMPTypeModalComponent) return;
        this.modalService.close(this.editTreatmentBMPTypeModalComponent);
    }

    public onDelete() {
        const options = {
            title: "Confirm: Delete Treatment BMP",
            message: `<p>You are about to delete ${this.selectedTreatmentBMP.TreatmentBMPName}.</p><p>Are you sure you wish to proceed?</p>`,
            buttonClassYes: "btn-danger",
            buttonTextYes: "Confirm",
            buttonTextNo: "Cancel",
        } as ConfirmOptions;
        this.confirmService.confirm(options).then((confirmed) => {
            if (confirmed) {
                const index = this.projectTreatmentBMPs.indexOf(this.selectedTreatmentBMP);
                this.projectTreatmentBMPs.splice(index, 1);
                this.selectedTreatmentBMP = null;
                this.clearSelectedItem();
                if (this.projectTreatmentBMPs.length > 0) {
                    this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
                }
            }
        });
    }

    public changeTreatmentBMPType(treatmentBMPType: number) {
        this.treatmentBMPService.changeTreatmentBMPTypeTreatmentBMP(this.selectedTreatmentBMP.TreatmentBMPID, treatmentBMPType).subscribe((temp) => {
            this.closeEditTreatmentBMPTypeModal();
            this.selectedTreatmentBMP.TreatmentBMPTypeID = treatmentBMPType;
            this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = temp;
            this.updateModelingAttributesForSelectedTreatmentBMP();
            this.originalTreatmentBMPs = JSON.stringify(this.projectTreatmentBMPs);
        });
    }

    private clearSelectedItem() {
        if (this.selectedListItem) {
            this.selectedListItem = null;
            this.selectedListItemDetails = {};
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = null;
        }
    }

    public addTreatmentBMP() {
        var newTreatmentBMP = new TreatmentBMPUpsertDto();
        newTreatmentBMP.TreatmentBMPID = this.newTreatmentBMPIndex;
        this.newTreatmentBMPIndex--;

        this.projectTreatmentBMPs.push(newTreatmentBMP);
        this.selectTreatmentBMP(newTreatmentBMP.TreatmentBMPID);

        this.isEditingLocation = true;
    }

    public treatmentBMPHasDelineation(treatmentBMPID: number) {
        return this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID) != null;
    }

    public getDelineationAreaForTreatmentBMP(treatmentBMPID: number) {
        let delineation = this.delineations?.find((x) => x.TreatmentBMPID == treatmentBMPID);

        if (delineation?.DelineationArea == null) {
            return "Not provided yet";
        }

        return `${delineation.DelineationArea} ac`;
    }

    public clearNonApplicableModelingAttributesForTreatmentBMPs() {
        this.projectTreatmentBMPs.forEach((treatmentBMP) => {
            let applicableCustomAttributeTypes = this.treatmentBMPTypeCustomAttributeTypes
                .filter((x) => x.TreatmentBMPTypeID == treatmentBMP.TreatmentBMPTypeID)
                .map((x) => x.CustomAttributeTypeID);
            treatmentBMP.ModelingAttributes = treatmentBMP.ModelingAttributes.filter((attribute) => applicableCustomAttributeTypes.includes(attribute.CustomAttributeTypeID));
        });
    }

    public save(continueToNextStep?: boolean) {
        this.isLoadingSubmit = true;
        this.alertService.clearAlerts();

        this.project.DoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs && (this.projectTreatmentBMPs == null || this.projectTreatmentBMPs.length == 0);
        this.clearNonApplicableModelingAttributesForTreatmentBMPs();
        this.projectService.updateProject(this.projectID, this.project).subscribe(
            () => {
                this.projectService.mergeTreatmentBMPsProject(this.projectID, this.projectTreatmentBMPs).subscribe(
                    () => {
                        this.isLoadingSubmit = false;
                        this.projectWorkflowProgressService.updateProgress(this.projectID);
                        this.projectService.listTreatmentBMPsAsUpsertDtosByProjectIDProject(this.projectID).subscribe(
                            (treatmentBMPs) => {
                                this.projectTreatmentBMPs = treatmentBMPs;
                                this.originalTreatmentBMPs = JSON.stringify(treatmentBMPs);
                                this.originalDoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs;
                                if (this.projectTreatmentBMPs.length > 0) {
                                    this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
                                }

                                if (continueToNextStep) {
                                    const rerouteURL =
                                        this.projectTreatmentBMPs.length > 0
                                            ? `/planning/projects/edit/${this.projectID}/stormwater-treatments/delineations`
                                            : `/planning/projects/edit/${this.projectID}/attachments`;
                                    this.router.navigateByUrl(rerouteURL).then((x) => {
                                        this.alertService.pushAlert(new Alert("Your Treatment BMP changes have been saved.", AlertContext.Success));
                                    });
                                    return;
                                }
                                this.alertService.pushAlert(new Alert("Your Treatment BMP changes have been saved.", AlertContext.Success));
                                window.scroll(0, 0);
                            },
                            (error) => {
                                this.isLoadingSubmit = false;
                                window.scroll(0, 0);
                                this.cdr.detectChanges();
                            }
                        );
                    },
                    (error) => {
                        this.isLoadingSubmit = false;
                        window.scroll(0, 0);
                        this.cdr.detectChanges();
                    }
                );
            },
            (error) => {
                this.isLoadingSubmit = false;
                window.scroll(0, 0);
                this.cdr.detectChanges();
            }
        );
    }
}
