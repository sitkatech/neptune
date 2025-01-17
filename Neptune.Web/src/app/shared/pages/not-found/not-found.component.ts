import { Component, OnInit } from "@angular/core";
import { PageHeaderComponent } from "../../components/page-header/page-header.component";
import { PlanningSiteLayoutComponent } from "../../../pages/planning-module/planning-site-layout/planning-site-layout.component";

@Component({
    selector: "not-found",
    templateUrl: "./not-found.component.html",
    styleUrls: ["./not-found.component.scss"],
    standalone: true,
    imports: [PageHeaderComponent, PlanningSiteLayoutComponent],
})
export class NotFoundComponent implements OnInit {
    constructor() {}

    ngOnInit() {}
}
