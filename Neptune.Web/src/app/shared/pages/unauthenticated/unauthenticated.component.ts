import { Component, OnInit } from "@angular/core";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";
import { SiteLayoutComponent } from "../../../pages/site-layout/site-layout.component";

@Component({
    selector: "unauthenticated",
    templateUrl: "./unauthenticated.component.html",
    styleUrls: ["./unauthenticated.component.scss"],
    standalone: true,
    imports: [PageHeaderComponent, SiteLayoutComponent],
})
export class UnauthenticatedComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
