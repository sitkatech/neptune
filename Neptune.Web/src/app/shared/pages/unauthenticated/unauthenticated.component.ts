import { Component, OnInit } from "@angular/core";
import { PlanningSiteLayoutComponent } from "../../../pages/planning-module/planning-site-layout/planning-site-layout.component";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";

@Component({
    selector: "unauthenticated",
    templateUrl: "./unauthenticated.component.html",
    styleUrls: ["./unauthenticated.component.scss"],
    standalone: true,
    imports: [PlanningSiteLayoutComponent, PageHeaderComponent],
})
export class UnauthenticatedComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
