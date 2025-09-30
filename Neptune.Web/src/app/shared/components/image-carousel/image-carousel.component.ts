import { Component, Input } from "@angular/core";

export interface ImageCarouselItem {
    FileResourceGUID: string;
    Caption?: string | null;
}
import { environment } from "src/environments/environment";

@Component({
    selector: "image-carousel",
    templateUrl: "./image-carousel.component.html",
    styleUrls: ["./image-carousel.component.scss"],
    standalone: true,
})
export class ImageCarouselComponent {
    @Input() images: ImageCarouselItem[] = [];
    @Input() noImagesMessage: string = "No images available.";
    currentImageIndex = 0;

    setImageIndex(idx: number): void {
        if (this.images && this.images.length > 0) {
            this.currentImageIndex = (idx + this.images.length) % this.images.length;
        }
    }

    getFileResourceUrl(fileResourceGUID: string): string {
        return environment.ocStormwaterToolsBaseUrl + "/FileResource/DisplayResource/" + fileResourceGUID;
    }
}
