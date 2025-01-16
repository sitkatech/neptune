import { Component, OnInit } from "@angular/core";
import { SiteLayoutComponent } from "../../../pages/planning-module/site-layout/site-layout.component";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";

@Component({
    selector: "unauthenticated",
    templateUrl: "./unauthenticated.component.html",
    styleUrls: ["./unauthenticated.component.scss"],
    standalone: true,
    imports: [SiteLayoutComponent, PageHeaderComponent],
})
export class UnauthenticatedComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
