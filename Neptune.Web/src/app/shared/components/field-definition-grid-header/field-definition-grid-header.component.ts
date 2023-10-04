import {Component, ElementRef, OnDestroy, ViewChild} from "@angular/core";
import {IAfterGuiAttachedParams, IHeaderParams} from "ag-grid-community";
import {IHeaderAngularComp} from "ag-grid-angular";

interface MyParams extends IHeaderParams {
    menuIcon: string;
}

@Component({
  selector: 'hippocamp-field-definition-grid-header',
  templateUrl: './field-definition-grid-header.component.html',
  styleUrls: ['./field-definition-grid-header.component.scss']
})
export class FieldDefinitionGridHeaderComponent implements OnDestroy, IHeaderAngularComp  {
  @ViewChild ('header') header: ElementRef;
    public params: any;
    public sorted: string;
    private elementRef: ElementRef;
    public showMenu: boolean = false;

    constructor(elementRef: ElementRef) {
        this.elementRef = elementRef;
    }
    refresh(params: IHeaderParams): boolean {
        return true;
    }
    afterGuiAttached?(params?: IAfterGuiAttachedParams): void {        
    }

    agInit(params: MyParams): void {
        this.params = params;
        //because of the way the popover sits and how it's triggered, it's best to just prevent the column from covering it
        //TODO make the css here more act more like the default ag-grid css
        this.params.column.minWidth = this.params.column.actualWidth;
        this.params.column.addEventListener('sortChanged', this.onSortChanged.bind(this));
        this.onSortChanged();
    }

    ngOnDestroy() {
    }

    onMenuClick(event:Event) {
        event.stopPropagation();
        this.params.showColumnMenu(this.querySelector('.customHeaderMenuButton'));
    }

    onSortRequested(event) {
      this.params.progressSort(event.shiftKey);
    };

    onSortChanged() {
        if (this.params.column.isSortAscending()) {
            this.sorted = 'asc'
        } else if (this.params.column.isSortDescending()) {
            this.sorted = 'desc'
        } else {
            this.sorted = ''
        }
    };


    private querySelector(selector: string) {
        return <HTMLElement>this.elementRef.nativeElement.querySelector(
            '.customHeaderMenuButton', selector);
    }
}