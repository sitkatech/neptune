import { Directive, Input, HostListener } from "@angular/core";
import { DropdownToggleDirective } from "./dropdown-toggle.directive";

@Directive({
    selector: "[dropdownToggleClose]",
})
export class DropdownToggleCloseDirective {
    @Input() dropdownToggleClose: DropdownToggleDirective;

    @HostListener("click", ["$event"]) onClick(event) {
        this.closeMenu();
    }

    closeMenu() {
        this.dropdownToggleClose.toggleMenu(false);
    }
}
