import { Component, Input } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';

@Component({
  selector: 'hippocamp-clear-grid-filters-button',
  templateUrl: './clear-grid-filters-button.component.html',
  styleUrls: ['./clear-grid-filters-button.component.scss']
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
