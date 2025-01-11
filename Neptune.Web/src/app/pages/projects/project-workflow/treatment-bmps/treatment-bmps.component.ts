import { ApplicationRef, ChangeDetectorRef, Component, ComponentRef, OnInit, ViewChild } from "@angular/core";
import * as L from "leaflet";
import "leaflet.fullscreen";
import * as esri from "esri-leaflet";
import { ActivatedRoute, Router } from "@angular/router";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { forkJoin } from "rxjs";
import { ProjectWorkflowService } from "src/app/services/project-workflow.service";
import { StormwaterJurisdictionService } from "src/app/shared/generated/api/stormwater-jurisdiction.service";
import { TreatmentBMPService } from "src/app/shared/generated/api/treatment-bmp.service";
import { FieldDefinitionTypeEnum } from "src/app/shared/generated/enum/field-definition-type-enum";
import { TimeOfConcentrationEnum } from "src/app/shared/generated/enum/time-of-concentration-enum";
import { TreatmentBMPModelingTypeEnum } from "src/app/shared/generated/enum/treatment-b-m-p-modeling-type-enum";
import { UnderlyingHydrologicSoilGroupEnum } from "src/app/shared/generated/enum/underlying-hydrologic-soil-group-enum";
import { BoundingBoxDto } from "src/app/shared/generated/model/bounding-box-dto";
import { DelineationUpsertDto } from "src/app/shared/generated/model/delineation-upsert-dto";
import { ProjectUpsertDto } from "src/app/shared/generated/model/project-upsert-dto";
import { TreatmentBMPModelingAttributeDefinitionDto } from "src/app/shared/generated/model/treatment-bmp-modeling-attribute-definition-dto";
import { TreatmentBMPModelingAttributeDropdownItemDto } from "src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto";
import { TreatmentBMPTypeWithModelingAttributesDto } from "src/app/shared/generated/model/treatment-bmp-type-with-modeling-attributes-dto";
import { TreatmentBMPUpsertDto } from "src/app/shared/generated/model/treatment-bmp-upsert-dto";
import { MarkerHelper } from "src/app/shared/helpers/marker-helper";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { CustomCompileService } from "src/app/shared/services/custom-compile.service";
import { environment } from "src/environments/environment";
import { ProjectDto, TreatmentBMPDisplayDto } from "src/app/shared/generated/model/models";
import { FieldDefinitionComponent } from "../../../../shared/components/field-definition/field-definition.component";
import { FormsModule } from "@angular/forms";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { NgIf, NgFor } from "@angular/common";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ModalService, ModalSizeEnum, ModalThemeEnum } from "src/app/shared/services/modal/modal.service";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { ConfirmOptions } from "src/app/shared/services/confirm/confirm-options";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";

declare var $: any;

@Component({
    selector: "treatment-bmps",
    templateUrl: "./treatment-bmps.component.html",
    styleUrls: ["./treatment-bmps.component.scss"],
    standalone: true,
    imports: [NgIf, CustomRichTextComponent, NgFor, FormsModule, FieldDefinitionComponent, PageHeaderComponent, WorkflowBodyComponent, AlertDisplayComponent],
})
export class TreatmentBmpsComponent implements OnInit {
    private currentUser: PersonDto;
    public projectID: number;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampTreatmentBMPs;

    @ViewChild("editTreatmentBMPTypeModal") editTreatmentBMPTypeModal;
    private editTreatmentBMPTypeModalComponent: ComponentRef<ModalComponent>;

    private zoomToProjectExtentOnLoad: boolean = false;
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
    public tileLayers: { [key: string]: any } = {};
    public overlayLayers: { [key: string]: any } = {};
    private boundingBox: BoundingBoxDto;
    public selectedListItem: number;
    public selectedListItemDetails: { [key: string]: any } = {};
    public selectedObjectMarker: L.Layer;
    public selectedTreatmentBMP: TreatmentBMPUpsertDto;
    public selectedTreatmentBMPType: number;
    public treatmentBMPsLayer: L.GeoJSON<any>;
    public newTreatmentBMPType: TreatmentBMPTypeWithModelingAttributesDto;

    public treatmentBMPModelingTypeEnum = TreatmentBMPModelingTypeEnum;
    public fieldDefinitionTypeEnum = FieldDefinitionTypeEnum;
    public modelingAttributeDropdownItems: Array<TreatmentBMPModelingAttributeDropdownItemDto>;
    public treatmentBMPTypes: Array<TreatmentBMPTypeWithModelingAttributesDto>;
    public newTreatmentBMPIndex = -1;
    public isLoadingSubmit = false;
    public isEditingLocation = false;

    public static modelingAttributeFieldsWithDropdown = ["TimeOfConcentrationID", "MonthsOfOperationID", "UnderlyingHydrologicSoilGroupID", "DryWeatherFlowOverrideID"];
    public delineations: DelineationUpsertDto[];

    public treatmentBMPs: Array<TreatmentBMPDisplayDto>;
    private inventoriedTreatmentBMPOverlayName =
        "<span>Inventoried BMP Locations<br /> <img src='./assets/main/map-icons/marker-icon-orange.png' style='height:17px; margin:3px'> BMP (Verified)</span>";
    private inventoriedTreatmentBMPsLayer: L.GeoJSON<any>;

    constructor(
        private authenticationService: AuthenticationService,
        private route: ActivatedRoute,
        private cdr: ChangeDetectorRef,
        private projectService: ProjectService,
        private treatmentBMPService: TreatmentBMPService,
        private stormwaterJurisdictionService: StormwaterJurisdictionService,
        private appRef: ApplicationRef,
        private compileService: CustomCompileService,
        private modalService: ModalService,
        private alertService: AlertService,
        private projectWorkflowService: ProjectWorkflowService,
        private router: Router,
        private confirmService: ConfirmService
    ) {}

    canExit() {
        if (this.projectID) {
            return this.originalDoesNotIncludeTreatmentBMPs == this.project.DoesNotIncludeTreatmentBMPs && this.originalTreatmentBMPs == JSON.stringify(this.projectTreatmentBMPs);
        }
        return true;
    }

    public ngOnInit(): void {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;

            const projectID = this.route.snapshot.paramMap.get("projectID");
            if (projectID) {
                this.projectService.projectsProjectIDGet(parseInt(projectID)).subscribe((project) => {
                    // redirect to review step if project is shared with OCTA grant program
                    if (project.ShareOCTAM2Tier2Scores) {
                        this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
                    } else {
                        this.projectID = parseInt(projectID);
                        this.mapProjectDtoToProject(project);
                        forkJoin({
                            treatmentBMPs: this.treatmentBMPService.treatmentBMPsGet(),
                            projectTreatmentBMPs: this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID),
                            delineations: this.projectService.projectsProjectIDDelineationsGet(this.projectID),
                            boundingBox: this.stormwaterJurisdictionService.jurisdictionsProjectIDGetBoundingBoxByProjectIDGet(this.projectID),
                            treatmentBMPTypes: this.treatmentBMPService.treatmentBMPTypesGet(),
                            modelingAttributeDropdownItems: this.treatmentBMPService.treatmentBMPModelingAttributeDropdownItemsGet(),
                        }).subscribe(({ treatmentBMPs, projectTreatmentBMPs, delineations, boundingBox, treatmentBMPTypes, modelingAttributeDropdownItems }) => {
                            this.treatmentBMPs = treatmentBMPs;
                            this.originalDoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
                            this.projectTreatmentBMPs = projectTreatmentBMPs;
                            this.originalTreatmentBMPs = JSON.stringify(projectTreatmentBMPs);
                            this.delineations = delineations;
                            this.boundingBox = boundingBox;
                            this.treatmentBMPTypes = treatmentBMPTypes;
                            this.modelingAttributeDropdownItems = modelingAttributeDropdownItems;

                            this.cdr.detectChanges();
                            this.updateMapLayers();
                        });
                    }
                });
            }
        });
        this.tileLayers = Object.assign(
            {},
            {
                Aerial: L.tileLayer("https://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Aerial",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
                Street: L.tileLayer("https://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Street",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
                Terrain: L.tileLayer("https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}", {
                    attribution: "Terrain",
                    maxZoom: 22,
                    maxNativeZoom: 18,
                }),
            },
            this.tileLayers
        );

        let regionalSubbasinsWMSOptions = {
            layers: "OCStormwater:RegionalSubbasins",
            transparent: true,
            format: "image/png",
            tiled: true,
        } as L.WMSOptions;

        let jurisdictionsWMSOptions = {
            layers: "OCStormwater:Jurisdictions",
            transparent: true,
            format: "image/png",
            tiled: true,
            styles: "jurisdiction_orange",
        } as L.WMSOptions;

        let WQMPsWMSOptions = {
            layers: "OCStormwater:WaterQualityManagementPlans",
            transparent: true,
            format: "image/png",
            tiled: true,
        } as L.WMSOptions;

        let verifiedDelineationsWMSOptions = {
            layers: "OCStormwater:Delineations",
            transparent: true,
            format: "image/png",
            tiled: true,
            cql_filter: "DelineationStatus = 'Verified' AND IsAnalyzedInModelingModule = 1",
        } as L.WMSOptions;

        this.overlayLayers = Object.assign(
            {
                "<img src='./assets/main/map-legend-images/RegionalSubbasin.png' style='height:12px; margin-bottom:3px'> Regional Subbasins": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    regionalSubbasinsWMSOptions
                ),
                "<span>Stormwater Network <br/> <img src='./assets/main/map-legend-images/stormwaterNetwork.png' height='50'/> </span>": esri.dynamicMapLayer({
                    url: "https://ocgis.com/arcpub/rest/services/Flood/Stormwater_Network/MapServer/",
                }),
                "<img src='./assets/main/map-legend-images/jurisdiction.png' style='height:12px; margin-bottom:3px'> Jurisdictions": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    jurisdictionsWMSOptions
                ),
                "<img src='./assets/main/map-legend-images/wqmpBoundary.png' style='height:12px; margin-bottom:4px'> WQMPs": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    WQMPsWMSOptions
                ),
                "<span>Inventoried BMP Delineations</br><img src='./assets/main/map-legend-images/delineationVerified.png' style='margin-bottom:3px'></span>": L.tileLayer.wms(
                    environment.geoserverMapServiceUrl + "/wms?",
                    verifiedDelineationsWMSOptions
                ),
            },
            this.overlayLayers
        );

        this.compileService.configure(this.appRef);
    }

    ngOnDestroy() {
        this.cdr.detach();
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
        const mapOptions: L.MapOptions = {
            // center: [46.8797, -110],
            // zoom: 6,
            minZoom: 9,
            maxZoom: 22,
            layers: [this.tileLayers["Terrain"]],
            fullscreenControl: true,
        } as L.MapOptions;
        this.map = L.map(this.mapID, mapOptions);

        this.map.fitBounds(
            [
                [this.boundingBox.Bottom, this.boundingBox.Left],
                [this.boundingBox.Top, this.boundingBox.Right],
            ],
            this.defaultFitBoundsOptions
        );
        this.updateTreatmentBMPsLayer();
        this.setControl();
        this.registerClickEvents();

        // add inventoried BMPs layer
        this.addInventoriedBMPsLayer();

        if (this.projectTreatmentBMPs.length > 0) {
            this.selectTreatmentBMP(this.projectTreatmentBMPs[0].TreatmentBMPID);
        }

        if (this.zoomToProjectExtentOnLoad) {
            let tempFeatureGroup = new L.FeatureGroup([this.treatmentBMPsLayer]);
            this.map.fitBounds(tempFeatureGroup.getBounds(), { padding: new L.Point(50, 50) });
        }
    }

    public updateTreatmentBMPsLayer() {
        if (this.treatmentBMPsLayer) {
            this.map.removeLayer(this.treatmentBMPsLayer);
            this.treatmentBMPsLayer = null;
        }

        let hasFlownToSelectedObject = false;

        const treatmentBMPsGeoJson = this.mapTreatmentBMPsToGeoJson(this.projectTreatmentBMPs);
        this.treatmentBMPsLayer = new L.GeoJSON(treatmentBMPsGeoJson, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.treatmentBMPMarker });
            },
            filter: (feature) => {
                return this.selectedTreatmentBMP == null || feature.properties.TreatmentBMPID != this.selectedTreatmentBMP.TreatmentBMPID;
            },
            onEachFeature: (feature, layer) => {
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

    private addInventoriedBMPsLayer() {
        const inventoriedTreatmentBMPGeoJSON = this.mapTreatmentBMPsToGeoJson(this.treatmentBMPs.filter((x) => x.ProjectID == null && x.InventoryIsVerified));
        this.inventoriedTreatmentBMPsLayer = new L.GeoJSON(inventoriedTreatmentBMPGeoJSON, {
            pointToLayer: (feature, latlng) => {
                return L.marker(latlng, { icon: MarkerHelper.inventoriedTreatmentBMPMarker });
            },
            onEachFeature: (feature, layer) => {
                layer.bindPopup(
                    `<b>Name:</b> <a target="_blank" href="${this.ocstBaseUrl()}/TreatmentBMP/Detail/${feature.properties.TreatmentBMPID}">${
                        feature.properties.TreatmentBMPName
                    }</a><br>` + `<b>Type:</b> ${feature.properties.TreatmentBMPTypeName}`
                );
            },
        });

        var clusteredInventoriedBMPLayer = L.markerClusterGroup({
            iconCreateFunction: function (cluster) {
                var childCount = cluster.getChildCount();

                return new L.DivIcon({
                    html: "<div><span>" + childCount + "</span></div>",
                    className: "marker-cluster",
                    iconSize: new L.Point(40, 40),
                });
            },
        });
        clusteredInventoriedBMPLayer.addLayer(this.inventoriedTreatmentBMPsLayer);
        this.layerControl.addOverlay(clusteredInventoriedBMPLayer, this.inventoriedTreatmentBMPOverlayName);
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

    public setControl(): void {
        this.layerControl = new L.Control.Layers(this.tileLayers, this.overlayLayers, { collapsed: false }).addTo(this.map);
    }

    public registerClickEvents(): void {
        var leafletControlLayersSelector = ".leaflet-control-layers";
        var closeButtonClass = "leaflet-control-layers-close";

        var closem = L.DomUtil.create("a", closeButtonClass);
        closem.innerHTML = "Close";
        L.DomEvent.on(closem, "click", function () {
            $(leafletControlLayersSelector).removeClass("leaflet-control-layers-expanded");
        });

        $(leafletControlLayersSelector).append(closem);

        this.map.on("click", (event: L.LeafletEvent) => {
            if (!this.isEditingLocation) {
                return;
            }
            if (this.selectedObjectMarker) {
                this.map.removeLayer(this.selectedObjectMarker);
            }
            this.selectedObjectMarker = new L.marker(event.latlng, { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 });

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
        this.selectedTreatmentBMPType = this.selectedTreatmentBMP.TreatmentBMPTypeID;
        selectedAttributes = [
            `<strong>Type:</strong> ${this.selectedTreatmentBMP.TreatmentBMPTypeName}`,
            `<strong>Latitude:</strong> ${this.selectedTreatmentBMP.Latitude}`,
            `<strong>Longitude:</strong> ${this.selectedTreatmentBMP.Longitude}`,
        ];

        if (this.selectedTreatmentBMP && this.selectedTreatmentBMP.Latitude && this.selectedTreatmentBMP.Longitude) {
            this.selectedObjectMarker = new L.marker(
                { lat: this.selectedTreatmentBMP.Latitude, lng: this.selectedTreatmentBMP.Longitude },
                { icon: MarkerHelper.selectedMarker, zIndexOffset: 1000 }
            );

            this.selectedObjectMarker.addTo(this.map);
            this.selectedListItemDetails.title = `${selectedNumber}`;
            this.selectedListItemDetails.attributes = selectedAttributes;
        }
    }

    public getModelingAttributeFieldsToDisplay(treatmentBMPTypeID: number): Array<TreatmentBMPModelingAttributeDefinitionDto> {
        return this.treatmentBMPTypes.find((x) => x.TreatmentBMPTypeID == treatmentBMPTypeID).TreatmentBMPModelingAttributes ?? [];
    }

    public getTypeNameByTypeID(typeID: number) {
        return this.treatmentBMPTypes.find((x) => x.TreatmentBMPTypeID == typeID).TreatmentBMPTypeName ?? -1;
    }

    public getModelingAttributeDropdownItemsByFieldName(fieldName: string): Array<TreatmentBMPModelingAttributeDropdownItemDto> {
        return this.modelingAttributeDropdownItems.filter((x) => x.FieldName == fieldName);
    }

    public getDropdownItemNameByFieldNameAndItemID(fieldName: string, itemID: number): string {
        const dropdownItem = this.modelingAttributeDropdownItems.find((x) => x.FieldName == fieldName && x.ItemID == itemID);
        return dropdownItem ? dropdownItem.ItemName : "";
    }

    public isFieldWithDropdown(fieldName: string): boolean {
        return TreatmentBmpsComponent.modelingAttributeFieldsWithDropdown.indexOf(fieldName) > -1;
    }

    public updateModelingTypeOnTypeChange(selectedType: any) {
        if (selectedType) {
            this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = selectedType.TreatmentBMPModelingTypeID;
        }
    }

    public toggleIsEditingLocation() {
        this.isEditingLocation = !this.isEditingLocation;
        this.updateTreatmentBMPsLayer();
    }

    openEditTreatmentBMPTypeModal(): void {
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
        this.treatmentBMPService.treatmentBMPsTreatmentBMPIDTreatmentBMPTypeTreatmentBMPTypeIDPut(this.selectedTreatmentBMP.TreatmentBMPID, treatmentBMPType).subscribe((temp) => {
            this.closeEditTreatmentBMPTypeModal();
            this.selectedTreatmentBMP.TreatmentBMPTypeID = treatmentBMPType;
            this.selectedTreatmentBMP.TreatmentBMPModelingTypeID = temp;
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

        newTreatmentBMP.TimeOfConcentrationID = TimeOfConcentrationEnum.FiveMinutes;
        newTreatmentBMP.UnderlyingHydrologicSoilGroupID = UnderlyingHydrologicSoilGroupEnum.D;

        this.projectTreatmentBMPs.push(newTreatmentBMP);
        this.selectTreatmentBMP(newTreatmentBMP.TreatmentBMPID);
        document.getElementById(this.mapID).scrollIntoView();

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

    public save(continueToNextStep?: boolean) {
        this.isLoadingSubmit = true;
        this.alertService.clearAlerts();
        this.project.DoesNotIncludeTreatmentBMPs = this.project.DoesNotIncludeTreatmentBMPs && (this.projectTreatmentBMPs == null || this.projectTreatmentBMPs.length == 0);

        this.projectService.projectsProjectIDUpdatePost(this.projectID, this.project).subscribe(
            () => {
                this.treatmentBMPService.treatmentBMPsProjectIDPut(this.projectID, this.projectTreatmentBMPs).subscribe(
                    () => {
                        this.isLoadingSubmit = false;
                        this.projectWorkflowService.emitWorkflowUpdate();
                        this.treatmentBMPService.treatmentBMPsProjectIDGetByProjectIDGet(this.projectID).subscribe(
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
                                            ? `projects/edit/${this.projectID}/stormwater-treatments/delineations`
                                            : `projects/edit/${this.projectID}/attachments`;
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

    public ocstBaseUrl(): string {
        return environment.ocStormwaterToolsBaseUrl;
    }
}
