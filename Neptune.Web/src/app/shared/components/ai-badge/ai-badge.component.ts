import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";

@Component({
    selector: "ai-badge",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./ai-badge.component.html",
    styleUrls: ["./ai-badge.component.scss"],
})
export class AiBadgeComponent {
    @Input() type: typeof AiBadgeInterface;
}

export var AiBadgeInterface: "ai" | "human" | "human-verified" | "ai-suggested";
