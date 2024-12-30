import { Directive, ElementRef, Input, Renderer2 } from "@angular/core";

@Directive({
    selector: "[copyToClipboard]",
    standalone: true,
})
export class CopyToClipboardDirective {
    @Input("copyToClipboard") valueOverride: string;
    private button: Element;
    constructor(
        private el: ElementRef,
        private renderer: Renderer2
    ) {
        this.button = document.createElement("button");
        renderer.setAttribute(this.button, "class", "clipboard-copy-button");
        renderer.setAttribute(this.button, "title", "Copy to clipboard");

        const icon = document.createElement("i");
        renderer.setAttribute(icon, "class", "far fa-copy");
        renderer.appendChild(this.button, icon);
        this.button.addEventListener("click", () => this.copyValueToClipboard());

        renderer.appendChild(el.nativeElement, this.button);
        renderer.addClass(el.nativeElement, "clipboard-copy-wrapper");
    }

    private copyValueToClipboard() {
        this.renderer.addClass(this.button, "copied");
        this.renderer.setAttribute(this.button, "title", "Copied!");

        if (this.valueOverride) {
            navigator.clipboard.writeText(this.valueOverride);
        } else {
            navigator.clipboard.writeText(this.el.nativeElement.textContent.trim());
        }

        setTimeout(() => {
            this.renderer.removeClass(this.button, "copied");
            this.renderer.setAttribute(this.button, "title", "Copy to clipboard");
        }, 2000);
    }
}
