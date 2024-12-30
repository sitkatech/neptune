import { Component, Input } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { NgIf } from '@angular/common';

@Component({
    selector: 'hippocamp-clear-grid-filters-button',
    templateUrl: './clear-grid-filters-button.component.html',
    styleUrls: ['./clear-grid-filters-button.component.scss'],
    standalone: true,
    imports: [NgIf]
})
export class ClearGridFiltersButtonComponent {
  @Input() grid: AgGridAngular;

  public clearFilters() {
    this.grid.api.setFilterModel(null);
  }  

  public isFilterActive() {
    if (this.grid && this.grid.api) {
      return this.grid.api.isAnyFilterPresent();
    }
  }
}
