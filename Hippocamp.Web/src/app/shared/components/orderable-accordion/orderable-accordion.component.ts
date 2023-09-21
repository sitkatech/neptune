import { CdkDrag, CdkDragDrop } from '@angular/cdk/drag-drop';
import {
    AfterContentChecked, Component, ContentChildren, Directive,
    EventEmitter, Host, Input, OnDestroy, OnInit, Optional, Output, QueryList, TemplateRef
} from '@angular/core';
import { DropListsService } from '../../services/dropLists.service';

let nextId = 0;

export interface OrderablePanelContext {
    opened: boolean;
}

@Directive({ selector: 'ng-template[orderablePanelTitle]' })
export class OrderablePanelTitle {
    constructor(public templateRef: TemplateRef<any>) { }
}

@Directive({ selector: 'ng-template[orderablePanelControls]' })
export class OrderablePanelControls {
    constructor(public templateRef: TemplateRef<any>) { }
}

@Directive({ selector: 'ng-template[orderablePanelContent]' })
export class OrderablePanelContent {
    constructor(public templateRef: TemplateRef<any>) { }
}

@Directive({ selector: 'orderable-panel' })
export class OrderablePanel implements AfterContentChecked {
    /**
     *  An optional id for the panel that must be unique on the page.
     *
     *  If not provided, it will be auto-generated in the `ngb-panel-xxx` format.
     */
    @Input() id = `orderable-panel-${nextId++}`;

    public isOpen = false;
    public bgTypeClass = "";
    public textTypeClass = "";

    /**
     * Type of the current panel.
     *
     * Bootstrap provides styles for the following types: `'success'`, `'info'`, `'warning'`, `'danger'`, `'primary'`,
     * `'secondary'`, `'light'` and `'dark'`.
     */
    @Input() set type(type: string) {
        this.bgTypeClass = `bg-${type}`;

        let lightBackground = (type == "light" || type == "warning" || type == "white");
        this.textTypeClass = `text-${lightBackground ? "dark" : "white"}`;
    }

    titleTemplate: OrderablePanelTitle;
    controlsTemplate: OrderablePanelControls;
    contentTemplate: OrderablePanelContent;

    @ContentChildren(OrderablePanelTitle, { descendants: true }) titleTemplates: QueryList<OrderablePanelTitle>;
    @ContentChildren(OrderablePanelControls, { descendants: true }) controlsTemplates: QueryList<OrderablePanelControls>;
    @ContentChildren(OrderablePanelContent, { descendants: true }) contentTemplates: QueryList<OrderablePanelContent>;

    ngAfterContentChecked() {
        // @ContentChild doesn't support { descendants: false } yet, so need to use @ContentChildren.first
        // see https://github.com/ng-bootstrap/ng-bootstrap/issues/2240 for more explanation
        // and https://github.com/angular/angular/issues/31921 to track solution
        this.titleTemplate = this.titleTemplates.first;
        this.controlsTemplate = this.controlsTemplates.first;
        this.contentTemplate = this.contentTemplates.first;
    }
}

export type OrderableAccordionData = {
    PanelID: string,
    ItemGroup: string,
}

/**
 * id must be globally unique id
 * items from OrderableAccordions with the same itemGroup can be dragged between eachother
 * sortPredicate allows constraining the order that items can be dragged into, for example
 *     to make it so optional chapters must come after all required chapters.
 */
@Component({
    selector: 'hippocamp-orderable-accordion',
    exportAs: 'hippocampOrderableAccordion',
    templateUrl: './orderable-accordion.component.html',
    styleUrls: ['./orderable-accordion.component.scss'],
    host: { 'class': 'accordion sortlist', 'role': 'tablist', '[attr.aria-multiselectable]': 'true' },
})
export class OrderableAccordionComponent implements OnInit, OnDestroy {
    @Input() id: string;
    @Input() itemGroup: string;
    @Input() sortPredicate: (index: number, item: CdkDrag<OrderableAccordionData>) => boolean;

    @Output() dropped: EventEmitter<CdkDragDrop<OrderableAccordionData>> = new EventEmitter();

    @ContentChildren(OrderablePanel) panels: QueryList<OrderablePanel>;

    constructor(
        public dropListsService: DropListsService,
    ) { }

    ngOnInit() {
        this.dropListsService.register(this.id);

        if (!this.sortPredicate) {
            this.sortPredicate = () => true;
        }
    }

    ngOnDestroy() {
        this.dropListsService.deregister(this.id);
    }

    expand(panelId: string): void {
        let panel = this.findPanelById(panelId);
        if (panel) {
            panel.isOpen = true;
        }
    }

    expandAll(): void {
        this.panels.forEach((panel) => { panel.isOpen = false });
    }

    collapse(panelId: string): void {
        let panel = this.findPanelById(panelId);
        if (panel) {
            panel.isOpen = false;
        }
    }

    collapseAll() {
        this.panels.forEach((panel) => { panel.isOpen = false });
    }

    toggle(panelId: string): void {
        const panel = this.findPanelById(panelId);
        if (panel) {
            panel.isOpen = !panel.isOpen;
        }
    }

    _onDrop(event: CdkDragDrop<OrderableAccordionData>) {
        this.dropped.emit(event);
    }

    enterPredicate(item: CdkDrag<OrderableAccordionData>) {
        let itemGroup = this["data"];
        let sameItemGroup = item.data.ItemGroup == itemGroup;
        return sameItemGroup;
    }

    private findPanelById(panelId: string): OrderablePanel | null {
        return this.panels.find(p => p.id === panelId) || null;
    }
}

@Directive({
    selector: 'button[orderablePanelToggle]',
    host: {
        'type': 'button',
        '[class.collapsed]': '!panel.isOpen',
        '[attr.aria-expanded]': 'panel.isOpen',
        '[attr.aria-controls]': 'panel.id',
        '(click)': 'accordion.toggle(panel.id)'
    }
})
export class OrderablePanelToggle {
    static ngAcceptInputType_orderablePanelToggle: OrderablePanel | '';

    @Input()
    set orderablePanelToggle(panel: OrderablePanel) {
        if (panel) {
            this.panel = panel;
        }
    }

    constructor(public accordion: OrderableAccordionComponent, @Optional() @Host() public panel: OrderablePanel) { }
}
