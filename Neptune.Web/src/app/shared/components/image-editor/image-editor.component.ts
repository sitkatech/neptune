import { JsonPipe } from "@angular/common";
import { Component, Input } from "@angular/core";

@Component({
    selector: "image-editor",
    imports: [JsonPipe],
    templateUrl: "./image-editor.component.html",
    styleUrl: "./image-editor.component.scss",
})
export class ImageEditorComponent {
    @Input() images: ImageEditorItem[] = [];
}

export interface ImageEditorItem {
    FileResourceGUID?: string;
    Caption?: string;
}
