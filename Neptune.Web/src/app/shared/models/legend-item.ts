import { SafeHtml } from "@angular/platform-browser";

export class LegendItem {
    Group: string;
    Title: string;
    WmsUrl: string;
    WmsLayerName: string;
    Color: string;
    WmsLayerStyle: string;
    LengendHtml: SafeHtml;
}
