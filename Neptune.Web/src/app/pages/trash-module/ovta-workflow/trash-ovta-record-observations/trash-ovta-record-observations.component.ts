import { Component } from "@angular/core";
import * as L from "leaflet";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { NeptuneMapComponent, NeptuneMapInitEvent } from "../../../../shared/components/leaflet/neptune-map/neptune-map.component";
import { JurisdictionsLayerComponent } from "../../../../shared/components/leaflet/layers/jurisdictions-layer/jurisdictions-layer.component";
import { OvtaAreaLayerComponent } from "../../../../shared/components/leaflet/layers/ovta-area-layer/ovta-area-layer.component";
import { AsyncPipe, NgIf } from "@angular/common";
import { Observable, switchMap } from "rxjs";
import { ActivatedRoute } from "@angular/router";
import { routeParams } from "src/app/app.routes";
import { OnlandVisualTrashAssessmentDetailDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-detail-dto";
import { OnlandVisualTrashAssessmentService } from "src/app/shared/generated/api/onland-visual-trash-assessment.service";
import { OnlandVisualTrashAssessmentObservationWithPhotoDto } from "src/app/shared/generated/model/onland-visual-trash-assessment-observation-with-photo-dto";

@Component({
    selector: "trash-ovta-record-observations",
    standalone: true,
    imports: [PageHeaderComponent, NeptuneMapComponent, JurisdictionsLayerComponent, OvtaAreaLayerComponent, NgIf, AsyncPipe],
    templateUrl: "./trash-ovta-record-observations.component.html",
    styleUrl: "./trash-ovta-record-observations.component.scss",
})
export class TrashOvtaRecordObservationsComponent {
    public map: L.Map;
    public layerControl: L.Control.Layers;
    public mapIsReady = false;
    public isLoadingSubmit = false;
    public ovtaAreaID: number;
    public layer: L.layerGroup = new L.layerGroup();
    public observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[] = [];

    public onlandVisualTrashAssessment$: Observable<OnlandVisualTrashAssessmentDetailDto>;

    constructor(private onlandVisualTrashAssessmentService: OnlandVisualTrashAssessmentService, private route: ActivatedRoute) {}

    ngOnInit() {
        this.onlandVisualTrashAssessment$ = this.route.params.pipe(
            switchMap((params) => {
                this.ovtaAreaID = params[routeParams.onlandVisualTrashAssessmentID];
                return this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDGet(params[routeParams.onlandVisualTrashAssessmentID]);
            })
        );
    }

    public handleMapReady(event: NeptuneMapInitEvent, observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]): void {
        this.map = event.map;
        this.layerControl = event.layerControl;
        this.mapIsReady = true;
        this.map.addLayer(this.layer);
        console.log(observations);
        this.addCurrentObservationsToMap(observations);
    }

    addCurrentObservationsToMap(observations: OnlandVisualTrashAssessmentObservationWithPhotoDto[]) {
        observations.forEach((observation) => {
            var lat = observation.Latitude;
            var long = observation.Longitude;
            L.marker({ lat: lat, lng: long }).addTo(this.layer);
        });
    }

    public addObservationMarker() {
        this.map.on("click", (e: L.LeafletMouseEvent) => {
            L.marker(e.latlng).addTo(this.layer);
            console.log(e.latlng);
            this.addObservation(e.latlng);
            this.map.off("click");
        });
    }

    public addObservation(latlng) {
        var observation = new OnlandVisualTrashAssessmentObservationWithPhotoDto();
        observation.OnlandVisualTrashAssessmentID = this.ovtaAreaID;
        observation.Latitude = latlng.lat;
        observation.Longitude = latlng.lng;
        this.observations = this.observations.concat(observation);
        console.log(this.observations);
        //this.map.off("click", (e: L.LeafletMouseEvent) => {});
    }

    save(andContinue: boolean = false) {
        this.onlandVisualTrashAssessmentService.onlandVisualTrashAssessmentsOnlandVisualTrashAssessmentIDObservationsPost(this.ovtaAreaID, this.observations).subscribe(() => {
            console.log("hello");
        });
    }
}
