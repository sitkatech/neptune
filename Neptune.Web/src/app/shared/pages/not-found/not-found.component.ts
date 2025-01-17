import { Component, OnInit } from "@angular/core";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";
import { SiteLayoutComponent } from "src/app/pages/site-layout/site-layout.component";

@Component({
    selector: "not-found",
    templateUrl: "./not-found.component.html",
    styleUrls: ["./not-found.component.scss"],
    standalone: true,
    imports: [PageHeaderComponent, SiteLayoutComponent],
})
export class NotFoundComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
