import { Directive, Input, ElementRef, HostListener, HostBinding, Renderer2, OnDestroy, TemplateRef, ViewContainerRef } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Overlay, OverlayRef, OverlayPositionBuilder } from "@angular/cdk/overlay";
import { TemplatePortal } from "@angular/cdk/portal";

@Directive({
    selector: "[dropdownToggle]",
    exportAs: "dropdownToggleName",
    standalone: true,
})
export class DropdownToggleDirective implements OnDestroy {
    private routerNavigationEndSubscription = Subscription.EMPTY;
    private classString: string = "active";
    // Accepting a TemplateRef is necessary to support attaching to body overlays
    /**
     * dropdownToggle: in-flow HTMLElement (e.g. div, ul) for in-flow menus, TemplateRef for overlay menus
     * attachToBody: if true, renders dropdown as overlay attached to body
     * dropdownContext: context for overlay template
     */
    @Input() dropdownToggle: HTMLElement | TemplateRef<any>;
    @Input() attachToBody: boolean = false;
    @Input() dropdownContext: any;

    @HostBinding("class.active") showMenu: boolean = false;
    // OverlayRef for overlay dropdown, scrollListener for scroll/visibility tracking
    private overlayRef: OverlayRef | null = null;
    private scrollListener: (() => void) | null = null;

    constructor(
        private el: ElementRef,
        private renderer: Renderer2,
        private router: Router,
        private overlay: Overlay,
        private viewContainerRef: ViewContainerRef,
        private positionBuilder: OverlayPositionBuilder
    ) {
        this.routerNavigationEndSubscription = router.events.subscribe((e) => {
            if (e instanceof NavigationEnd) {
                this.closeMenu();
            }
        });
    }

    @HostListener("click", ["$event"]) onClick(event) {
        this.showMenu = !this.showMenu;
        this.toggleMenu();
    }

    @HostListener("window:click", ["$event"]) onWindowClick(event) {
        let isClickInsideDropdown = false;
        if (this.dropdownToggle instanceof HTMLElement) {
            isClickInsideDropdown = (this.dropdownToggle as HTMLElement).contains(event.target as Node);
        }
        if (event.target !== this.el.nativeElement && !this.el.nativeElement.contains(event.target) && event.target !== this.dropdownToggle && !isClickInsideDropdown) {
            this.closeMenu();
        }
    }

    @HostListener("document:keydown.escape", ["$event"]) onKeydownHandler(event: KeyboardEvent) {
        this.closeMenu();
    }

    ngOnDestroy(): void {
        this.routerNavigationEndSubscription.unsubscribe();
        this.closeMenu();
    }

    /**
     * toggleMenu: toggles dropdown open/close, chooses overlay or in-flow logic
     */
    toggleMenu(show: boolean = null) {
        if (!this.dropdownToggle) return;

        if (show != null) {
            this.showMenu = show;
        }

        // If dropdownToggle is a TemplateRef, use overlay (attach to body)
        if (this.attachToBody && this.dropdownToggle instanceof TemplateRef) {
            if (this.showMenu) {
                this.openOverlay(this.dropdownToggle);
            } else {
                this.closeMenu();
            }
        }
        // If dropdownToggle is an in-flow HTMLElement (div, ul, etc.), use in-flow logic
        else if (this.dropdownToggle instanceof HTMLElement) {
            if (this.showMenu) {
                this.renderer.addClass(this.dropdownToggle as HTMLElement, this.classString);
            } else {
                this.renderer.removeClass(this.dropdownToggle as HTMLElement, this.classString);
            }
        }
    }

    /**
     * openOverlay: creates overlay, attaches template, and sets up scroll/visibility tracking
     */
    openOverlay(template: TemplateRef<any>) {
        if (this.overlayRef) {
            this.closeMenu();
        }
        const positionStrategy = this.positionBuilder.flexibleConnectedTo(this.el).withPositions([
            {
                originX: "start",
                originY: "bottom",
                overlayX: "start",
                overlayY: "top",
            },
        ]);
        this.overlayRef = this.overlay.create({ positionStrategy, hasBackdrop: true, backdropClass: "dropdown-overlay-no-pointer" });
        this.overlayRef.attach(new TemplatePortal(template, this.viewContainerRef, this.dropdownContext));
        this.overlayRef.backdropClick().subscribe(() => this.closeMenu());
        // Add window scroll listener to update overlay position
        // scrollListener: closes overlay if host is hidden in any scrollable ancestor or viewport, else updates position
        this.scrollListener = () => {
            if (this.overlayRef) {
                const el = this.el.nativeElement as HTMLElement;
                let isVisible = true;
                let current: HTMLElement | null = el;
                // Traverse up ancestors to check if element is visible in all scrollable containers
                while (current && current !== document.body) {
                    const parent = current.parentElement;
                    if (parent && parent !== document.body && parent.scrollHeight > parent.clientHeight) {
                        const parentRect = parent.getBoundingClientRect();
                        const elRect = el.getBoundingClientRect();
                        if (elRect.bottom <= parentRect.top || elRect.top >= parentRect.bottom || elRect.right <= parentRect.left || elRect.left >= parentRect.right) {
                            isVisible = false;
                            break;
                        }
                    }
                    current = parent;
                }
                // Also check viewport
                const rect = el.getBoundingClientRect();
                const inViewport =
                    rect.bottom > 0 &&
                    rect.right > 0 &&
                    rect.left < (window.innerWidth || document.documentElement.clientWidth) &&
                    rect.top < (window.innerHeight || document.documentElement.clientHeight);
                // If not visible in any scrollable ancestor or viewport, close overlay
                if (!isVisible || !inViewport) {
                    this.closeMenu();
                } else {
                    // Otherwise, update overlay position to stay anchored
                    this.overlayRef.updatePosition();
                }
            }
        };
        window.addEventListener("scroll", this.scrollListener, true);
    }

    closeMenu() {
        this.showMenu = false;
        if (this.overlayRef) {
            this.overlayRef.detach();
            this.overlayRef.dispose();
            this.overlayRef = null;
        }
        if (this.scrollListener) {
            window.removeEventListener("scroll", this.scrollListener, true);
            this.scrollListener = null;
        }
        if (!this.attachToBody && this.dropdownToggle instanceof HTMLElement) {
            this.renderer.removeClass(this.dropdownToggle as HTMLElement, this.classString);
        }
    }
}
