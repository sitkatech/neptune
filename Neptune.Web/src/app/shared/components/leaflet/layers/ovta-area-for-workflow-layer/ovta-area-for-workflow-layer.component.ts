import { AfterViewInit, Component, Input, OnChanges } from "@angular/core";
import { MapLayerBase } from "../map-layer-base.component";
import * as L from "leaflet";
import { CommonModule } from "@angular/common";
import { WfsService } from "src/app/shared/services/wfs.service";

@Component({
    selector: "ovta-area-for-workflow-layer",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./ovta-area-for-workflow-layer.component.html",
    styleUrl: "./ovta-area-for-workflow-layer.component.scss",
})
export class OvtaAreaLayerForWorkflowComponent extends MapLayerBase implements OnChanges, AfterViewInit {
    @Input() ovtaAreaID: number;
    public layer;

    private defaultStyle = {
        color: "blue",
        fillOpacity: 0.2,
        opacity: 0,
    };

    constructor(private wfsService: WfsService) {
        super();
    }

    ngAfterViewInit(): void {
        let cql_filter = `OnlandVisualTrashAssessmentAreaID = ${this.ovtaAreaID}`;

        this.wfsService
            .getGeoserverWFSLayerWithCQLFilter("OCStormwater:OnlandVisualTrashAssessmentAreas", cql_filter, "OnlandVisualTrashAssessmentAreaID")
            .subscribe((response) => {
                if (response.length == 0) return;
                this.layer = new L.GeoJSON(response, {
                    style: this.defaultStyle,
                });
                this.initLayer();
            });
    }
}
