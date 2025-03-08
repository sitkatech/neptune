import { Component, Input, OnDestroy, TemplateRef, ViewChild } from "@angular/core";

@Component({
    template: "",
    standalone: true,
})
export class MapLayerBase implements IMapLayer, OnDestroy {
    @Input() map: any;
    @Input() layerControl: any;
    @Input() displayOnLoad: boolean = false;
    @Input() sortOrder: number;
    @Input() indexOfDisabledLayerControl: number = null;

    @ViewChild("layerName") layerTemplate!: TemplateRef<any>;
    layer: any;

    constructor() {}

    ngOnDestroy(): void {
        if (this.layer && this.layerControl) {
            this.map.removeLayer(this.layer);
            this.layerControl.removeLayer(this.layer);
        }
    }

    ngOnChanges(changes: any): void {}

    initLayer(): void {
        if (this.checkForMissingInputs()) {
            const viewRef = this.layerTemplate.createEmbeddedView(null);
            viewRef.detectChanges();
            const layerHtml = viewRef.rootNodes[0].outerHTML;
            if (this.sortOrder) {
                this.layer.sortOrder = this.sortOrder;
            }
            this.layerControl.addOverlay(this.layer, layerHtml);
            if (this.displayOnLoad) {
                this.map.addLayer(this.layer);
            }
            if(this.indexOfDisabledLayerControl != null){
                this.disableCheckboxInLayerControl(this.indexOfDisabledLayerControl)
            }
        }
    }

    checkForMissingInputs(): boolean {
        let inputsAreValid = true;
        if (!this.layer) {
            console.error("layer property was not found on the component inheriting from MapLayerBase");
            inputsAreValid = false;
        }
        if (!this.layerControl) {
            console.error("could not find the layerControl to add this layer to");
            inputsAreValid = false;
        }
        if (!this.layerTemplate) {
            console.error(
                "could not find the layerName template within the child class, make sure to implement a <ng-template #layerName></ng-template> that has a single root element."
            );
        }
        return inputsAreValid;
    }

    disableCheckboxInLayerControl(index: number = 0): void{
        document.querySelectorAll('[type = "checkbox"].leaflet-control-layers-selector')[index]["disabled"] = true;
    }
}

export interface IMapLayer {
    initLayer(): void;
    layer: any;
}
