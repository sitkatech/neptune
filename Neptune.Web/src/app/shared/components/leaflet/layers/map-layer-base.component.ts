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

    @ViewChild("layerName") layerTemplate!: TemplateRef<any>;
    @ViewChild("legend") lengendTemplate!: TemplateRef<any>;
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
            const layerHtml = viewRef.rootNodes[0].innerText;
            if (this.sortOrder) {
                this.layer.sortOrder = this.sortOrder;
            }
            if(this.lengendTemplate){
                const legendViewRef = this.lengendTemplate.createEmbeddedView(null);
                if(legendViewRef){
                    legendViewRef.detectChanges();
                    const legendHtml = legendViewRef.rootNodes[0].outerHTML;
                    this.layer["legendHtml"] = legendHtml;
                }
            }

            this.layerControl.addOverlay(this.layer, layerHtml);
            if (this.displayOnLoad) {
                this.map.addLayer(this.layer);
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
}

export interface IMapLayer {
    initLayer(): void;
    layer: any;
}
