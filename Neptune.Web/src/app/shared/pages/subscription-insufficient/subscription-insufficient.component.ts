import { Component, OnInit } from "@angular/core";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";
import { SiteLayoutComponent } from "src/app/pages/site-layout/site-layout.component";

@Component({
    selector: "subscription-insufficient",
    templateUrl: "./subscription-insufficient.component.html",
    styleUrls: ["./subscription-insufficient.component.scss"],
    imports: [PageHeaderComponent, SiteLayoutComponent],
})
export class SubscriptionInsufficientComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
