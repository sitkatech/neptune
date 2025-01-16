import { Component, OnInit } from "@angular/core";
import { AlertDisplayComponent } from "../../components/alert-display/alert-display.component";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";
import { SiteLayoutComponent } from "../../../pages/planning-module/site-layout/site-layout.component";

@Component({
    selector: "not-found",
    templateUrl: "./not-found.component.html",
    styleUrls: ["./not-found.component.scss"],
    standalone: true,
    imports: [AlertDisplayComponent, PageHeaderComponent, SiteLayoutComponent],
})
export class NotFoundComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
