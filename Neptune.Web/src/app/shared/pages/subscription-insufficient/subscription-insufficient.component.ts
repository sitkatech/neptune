import { Component, OnInit } from "@angular/core";
import { PlanningSiteLayoutComponent } from "../../../pages/planning-module/planning-site-layout/planning-site-layout.component";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";

@Component({
    selector: "subscription-insufficient",
    templateUrl: "./subscription-insufficient.component.html",
    styleUrls: ["./subscription-insufficient.component.scss"],
    standalone: true,
    imports: [PlanningSiteLayoutComponent, PageHeaderComponent],
})
export class SubscriptionInsufficientComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
