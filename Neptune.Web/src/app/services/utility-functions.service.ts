import { DatePipe, DecimalPipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, CsvExportParams, SortDirection } from 'ag-grid-community';
import { FieldDefinitionGridHeaderComponent } from '../shared/components/field-definition-grid-header/field-definition-grid-header.component';

@Injectable({
  providedIn: 'root'
})
export class UtilityFunctionsService {

  constructor(
    private datePipe: DatePipe,
    private decimalPipe: DecimalPipe
  ) { }

  public stringToKebabCase(string: string): string {
    return string.replace(/[A-Z]+(?![a-z])|[A-Z]/g, ($, ofs) => (ofs ? "-" : "") + $.toLowerCase())
  }

  public formatDate(date: Date, format: string): string {
    var _datePipe = this.datePipe;
    return _datePipe.transform(date, format);
  }

  public linkRendererComparator(id1: any, id2: any) {
    if (id1.LinkDisplay == id2.LinkDisplay) {
      return 0;
    }
    return id1.LinkDisplay > id2.LinkDisplay ? 1 : -1;
  }

  public multiLinkRendererComparator(id1: any, id2: any) {
    if (id1.downloadDisplay == id2.downloadDisplay) {
      return 0;
    }
    return id1.downloadDisplay > id2.downloadDisplay ? 1 : -1;
  }

  public decimalValueGetter(params: any, fieldName): number {
    const fieldNames = fieldName.split('.');
    if (fieldNames.length == 1) {
      return params.data[fieldName] ?? 0;
    }

    // checks that each part of a nested field is not null
    var fieldValue = params.data;
    fieldNames.forEach(x => {
      fieldValue = fieldValue[x];
      if (!fieldValue) {
        fieldValue = 0;
        return;
      }
    });

    return fieldValue;
  }

  public createDecimalColumnDef(headerName: string, fieldName: string, width?: number, decimalPlacesToDisplay?: number) {
    const _decimalPipe = this.decimalPipe;
    const decimalFormatString = decimalPlacesToDisplay != null ? 
      '1.' + decimalPlacesToDisplay + '-' + decimalPlacesToDisplay : '1.2-2';
  
    var decimalColDef: ColDef = {
      headerName: headerName, filter: 'agNumberColumnFilter', cellStyle: { textAlign: 'right' },
      valueGetter: params => _decimalPipe.transform(this.decimalValueGetter(params, fieldName), decimalFormatString),
      filterValueGetter: params => parseFloat(_decimalPipe.transform(this.decimalValueGetter(params, fieldName), decimalFormatString))
    }
    if (width) {
      decimalColDef.width = width
    }
  
    return decimalColDef;
  }

  public createDecimalColumnDefWithFieldDefinition(headerName: string, fieldName: string, fieldDefinitionType: string, labelOverride?: string, width?: number, decimalPlacesToDisplay?: number): ColDef {
    var colDef = this.createDecimalColumnDef(headerName, fieldName, width, decimalPlacesToDisplay);

    colDef.headerComponent = FieldDefinitionGridHeaderComponent;
    colDef.headerComponentParams = { fieldDefinitionType: fieldDefinitionType };

    if (labelOverride) {
      colDef.headerComponentParams.labelOverride = labelOverride;
    }

    return colDef;
  }

  private dateFilterComparator(filterLocalDateAtMidnight, cellValue) {
    const filterDate = Date.parse(filterLocalDateAtMidnight);
    const cellDate = Date.parse(cellValue);

    if (cellDate == filterDate) {
      return 0;
    }
    return (cellDate < filterDate) ? -1 : 1;
  }

  private dateSortComparator (id1: any, id2: any) {
    const date1 = id1 ? Date.parse(id1) : Date.parse("1/1/1900");
    const date2 = id2 ? Date.parse(id2) : Date.parse("1/1/1900");
    if (date1 < date2) {
      return -1;
    }
    return (date1 > date2)  ?  1 : 0;
  }

  public createDateColumnDef(headerName: string, fieldName: string, dateFormat: string, width?: number, sort:string = null): ColDef {
    const _datePipe = this.datePipe;
    var dateColDef: ColDef = {
      headerName: headerName, valueGetter: function (params: any) {
        return _datePipe.transform(params.data[fieldName], dateFormat);
      },
      comparator: this.dateSortComparator,
      filter: 'agDateColumnFilter',
      filterParams: {
        filterOptions: ['inRange'],
        comparator: this.dateFilterComparator
      }, 
      width: 110,
      resizable: true,
      sortable: true
    };
    if (width) {
      dateColDef.width = width;
    }
    
    if(sort) {
      dateColDef.sort = sort as SortDirection;
    }
    
    return dateColDef;
  }

  public createDateColumnDefWithFieldDefHeader(fieldDefinitionType: string, fieldName: string, dateFormat: string, headerName: string, width?: number): ColDef {
    const _datePipe = this.datePipe;
    var dateColDef: ColDef = { 
      headerName: headerName,
      field: fieldName,
      valueGetter: params => _datePipe.transform(params.data[fieldName], dateFormat, '+0000'), // we are just using the date part of the date provided, so set it as UTC and ignore local timezone
      headerComponent: FieldDefinitionGridHeaderComponent, 
      headerComponentParams: { fieldDefinitionType: fieldDefinitionType, labelOverride: headerName, enableSort: true },
      comparator: this.dateSortComparator,
      filter: 'agDateColumnFilter',
      filterParams: {
        filterOptions: ['inRange'],
        comparator: this.dateFilterComparator
      }, 
      width: 110,
      resizable: true,
      sortable: true
    };
    if (width) {
      dateColDef.width = width;
    }

    return dateColDef;
  }

  public exportGridToCsv(grid: AgGridAngular, fileName: string, columnKeys: Array<string>) {
    var params =
      {
        skipHeader: false,
        columnGroups: false,
        skipFooters: true,
        skipGroups: true,
        skipPinnedTop: true,
        skipPinnedBottom: true,
        allColumns: true,
        onlySelected: false,
        suppressQuotes: false,
        fileName: fileName,
        processCellCallback: function (p) {
          if (p.column.getColDef().cellRenderer) {
            if (p.value.downloadDisplay) {
              return p.value.downloadDisplay;
            } else {
              return p.value.LinkDisplay;
            }
          }
          else {
            return p.value;
          }
        }
      } as CsvExportParams
    if (columnKeys) {
      params.columnKeys = columnKeys;
    }
    grid.api.exportDataAsCsv(params);
  }
}