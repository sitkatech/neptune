import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { DecimalPipe } from "@angular/common";

@Component({
    selector: "octa-prioritization-detail-popup",
    templateUrl: "./octa-prioritization-detail-popup.component.html",
    styleUrls: ["./octa-prioritization-detail-popup.component.scss"],
    standalone: true,
    imports: [DecimalPipe],
})
export class OctaPrioritizationDetailPopupComponent implements OnInit {
    constructor(private cdr: ChangeDetectorRef) {}

    public feature: any;

    ngOnInit() {}

    public detectChanges(): void {
        this.cdr.detectChanges();
    }
}
