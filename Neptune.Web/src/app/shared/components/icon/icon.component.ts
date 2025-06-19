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
    @Input() color: string = "black";
    @Input() fillOpacity: number;
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
    | "FlowArrow"
    | "Inbox"
    | "Info"
    | "Layout"
    | "LineChart"
    | "Logo"
    | "Manage"
    | "Map"
    | "MapLegend"
    | "MapMarker"
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
    | "Square"
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
