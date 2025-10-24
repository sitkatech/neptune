import { Component, Input } from "@angular/core";

@Component({
    selector: "icon",
    imports: [],
    templateUrl: "./icon.component.html",
    styleUrls: ["./icon.component.scss"],
})
export class IconComponent {
    @Input() icon: typeof IconInterface;
    @Input() enableFontSize: boolean = false;
    @Input() color: string = "inherit";
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
    | "DownCarrot"
    | "Download"
    | "Drag"
    | "EnterFullScreen"
    | "ExitFullScreen"
    | "ExternalLink"
    | "File"
    | "Geography"
    | "Geography-Alt"
    | "GeomanDelete"
    | "GeomanEdit"
    | "GeomanPolygon"
    | "Guide"
    | "FlowArrow"
    | "Inbox"
    | "Info"
    | "Layout"
    | "Leaf"
    | "LineChart"
    | "Logo"
    | "Manage"
    | "Map"
    | "MapLegend"
    | "MapMarker"
    | "Measurements"
    | "Model"
    | "Parcels"
    | "PDF"
    | "Question"
    | "Resend"
    | "Review"
    | "ReviewApprove"
    | "ReviewReturn"
    | "Satellite"
    | "Square"
    | "Stars"
    | "Statistics"
    | "StepComplete"
    | "StepIncomplete"
    | "SupportLogo"
    | "Transactions"
    | "UpArrow"
    | "Upload"
    | "User"
    | "Users"
    | "VerticalMap"
    | "Warning"
    | "WaterDrop"
    | "WaterDropFilled"
    | "WaterSupply";
