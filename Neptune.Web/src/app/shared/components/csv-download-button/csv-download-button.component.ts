import { Component, Input } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { NgbTooltip } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'hippocamp-csv-download-button',
    templateUrl: './csv-download-button.component.html',
    styleUrls: ['./csv-download-button.component.scss'],
    standalone: true,
    imports: [NgbTooltip]
})
export class CsvDownloadButtonComponent {
  @Input() grid: AgGridAngular;
  @Input() fileName: string;
  @Input() colIDsToExclude = [];

  constructor(private utilityFunctionsService: UtilityFunctionsService) {}

  public exportToCsv() {   
    let columnsKeys = this.grid.columnApi.getAllDisplayedColumns();
    let columnIDs = columnsKeys.map(keys => keys.getColId()).filter(x => this.colIDsToExclude.indexOf(x) < 0);
    
    this.utilityFunctionsService.exportGridToCsv(this.grid, this.fileName + '.csv', columnIDs);
  }  
}