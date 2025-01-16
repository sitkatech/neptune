import { Component, OnInit } from "@angular/core";
import { SiteLayoutComponent } from "../../../pages/planning-module/site-layout/site-layout.component";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";

@Component({
    selector: "subscription-insufficient",
    templateUrl: "./subscription-insufficient.component.html",
    styleUrls: ["./subscription-insufficient.component.scss"],
    standalone: true,
    imports: [SiteLayoutComponent, PageHeaderComponent],
})
export class SubscriptionInsufficientComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
