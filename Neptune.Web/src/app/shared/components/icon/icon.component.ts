import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";

@Component({
    selector: "icon",
    standalone: true,
    imports: [CommonModule],
    templateUrl: "./icon.component.html",
    styleUrls: ["./icon.component.scss"],
})
export class IconComponent {
    @Input() icon: typeof IconInterface;
    @Input() enableFontSize: boolean = false;
}

export var IconInterface:
    | "ActivityCenter"
    | "Allocations"
    | "AngleUp"
    | "AngleDown"
    | "ArrowLeft"
    | "Budget"
    | "BulletedList"
    | "Calendar"
    | "CaretDown"
    | "CaretUp"
    | "ChatBubble"
    | "CircleCheckmark"
    | "CircleX"
    | "Configure"
    | "Data"
    | "DataLayers"
    | "Delete"
    | "Download"
    | "Drag"
    | "EnterFullScreen"
    | "ExitFullScreen"
    | "ExternalLink"
    | "Geography"
    | "Geography-Alt"
    | "Guide"
    | "Inbox"
    | "Info"
    | "Layout"
    | "LineChart"
    | "Logo"
    | "Manage"
    | "Map"
    | "MapLegend"
    | "Measurements"
    | "Model"
    | "Parcels"
    | "Question"
    | "Resend"
    | "Review"
    | "ReviewApprove"
    | "ReviewReturn"
    | "Satellite"
    | "ScenarioPlanner"
    | "ScenarioRun"
    | "Statistics"
    | "StepComplete"
    | "StepIncomplete"
    | "SupportLogo"
    | "Transactions"
    | "Upload"
    | "User"
    | "Users"
    | "VerticalMap"
    | "Warning"
    | "WaterAccounts"
    | "WaterDrop"
    | "WaterDropFilled"
    | "WaterSupply"
    | "Wells"
    | "Zones";
