import { Component } from "@angular/core";
import { RouterOutlet, RouterLink } from "@angular/router";
import { HeaderNavComponent } from "../../shared/components/header-nav/header-nav.component";
import { AsyncPipe } from "@angular/common";
import { PersonDto } from "src/app/shared/generated/model/person-dto";
import { AuthenticationService } from "src/app/services/authentication.service";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { DropdownToggleDirective } from "../../shared/directives/dropdown-toggle.directive";
import { IconComponent } from "../../shared/components/icon/icon.component";

@Component({
    selector: "inventory-site-layout",
    templateUrl: "./inventory-site-layout.component.html",
    styleUrls: ["./inventory-site-layout.component.scss"],
    imports: [RouterOutlet, RouterLink, HeaderNavComponent, AsyncPipe, DropdownToggleDirective, IconComponent],
    standalone: true,
})
export class InventorySiteLayoutComponent {
    public siteUrl = environment.ocStormwaterToolsBaseUrl;
    public currentUser$: Observable<PersonDto>;
    constructor(private authenticationService: AuthenticationService) {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }
}
