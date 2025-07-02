import { SafeHtml } from "@angular/platform-browser";
import { IconInterface } from "../components/icon/icon.component";
export class LegendItem {
    Group: string;
    Title: string;
    Icon: typeof IconInterface;
    IconColor: string;
    IconFillOpacity: number;
    Text: string;
    WmsUrl: string;
    WmsLayerName: string;
    Color: string;
    WmsLayerStyle: string;
    LegendHtml: SafeHtml;

    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}
