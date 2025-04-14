import { DatePipe, DecimalPipe } from "@angular/common";
import { Injectable } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import {
    CellClassFunc,
    CellStyle,
    CellStyleFunc,
    ColDef,
    CsvExportParams,
    NumberFilter,
    SortDirection,
    ValueFormatterFunc,
    ValueGetterFunc,
    ValueGetterParams,
} from "ag-grid-community";
import { CustomDropdownFilterComponent } from "../shared/components/custom-dropdown-filter/custom-dropdown-filter.component";
import { FieldDefinitionGridHeaderComponent } from "../shared/components/field-definition-grid-header/field-definition-grid-header.component";
import { LinkRendererComponent } from "../shared/components/ag-grid/link-renderer/link-renderer.component";
import { ContextMenuRendererComponent } from "../shared/components/ag-grid/context-menu/context-menu-renderer.component";
import { MultiLinkRendererComponent } from "../shared/components/ag-grid/multi-link-renderer/multi-link-renderer.component";
import { PhonePipe } from "../shared/pipes/phone.pipe";

@Injectable({
    providedIn: "root",
})
export class UtilityFunctionsService {
    public static readonly months: string[] = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    constructor(private datePipe: DatePipe, private decimalPipe: DecimalPipe, private phonePipe: PhonePipe) {}

    public getMonthName(monthNumber) {
        return UtilityFunctionsService.months[monthNumber - 1];
    }

    public getNumberFromMonth(month: string) {
        return UtilityFunctionsService.months.indexOf(month) + 1;
    }

    public booleanValueGetter(value: boolean, allowNullValues: boolean = true) {
        if (allowNullValues && value == null) return null;

        return value ? "Yes" : "No";
    }

    public stringToKebabCase(string: string): string {
        return string.replace(/[A-Z]+(?![a-z])|[A-Z]/g, ($, ofs) => (ofs ? "-" : "") + $.toLowerCase());
    }

    public formatDate(date: Date, format: string): string {
        const _datePipe = this.datePipe;
        return _datePipe.transform(date, format);
    }

    public createActionsColumnDef(actionsValueGetter: ValueGetterFunc, hide: boolean = false): ColDef {
        return {
            headerName: "Actions",
            valueGetter: actionsValueGetter,
            cellRenderer: ContextMenuRendererComponent,
            cellClass: "context-menu-container",
            pinned: true,
            sortable: false,
            filter: false,
            suppressSizeToFit: true,
            suppressAutoSize: true,
            width: 100,
            maxWidth: 100,
            hide: hide,
        };
    }

    public createCheckboxSelectionColumnDef(): ColDef {
        return {
            checkboxSelection: true,
            headerCheckboxSelection: true,
            headerCheckboxSelectionFilteredOnly: true,
            headerCheckboxSelectionCurrentPageOnly: false,
            sortable: false,
            filter: false,
            resizable: false,
            pinned: true,
            suppressSizeToFit: true,
            suppressAutoSize: true,
            width: 50,
            maxWidth: 50,
        };
    }

    public defaultValueGetter(params: ValueGetterParams, fieldName: string, containingFieldName: string = "data") {
        const path = fieldName.split(".");
        return path.reduce((obj, key) => (obj != null ? obj[key] : null), containingFieldName ? params[containingFieldName] : params);
    }

    public createBasicColumnDef(headerName: string, fieldName: string, colDefParams?: QanatColumnDefParams): ColDef {
        const colDef: ColDef = {
            headerName: headerName,
            valueGetter: (params) => this.defaultValueGetter(params, fieldName),
        };

        this.applyDefaultQanatColumnDefParams(colDef, colDefParams);
        return colDef;
    }

    public customDecimalValueGetter(value: number, decimalPlacesToDisplay: number = 2) {
        const _decimalPipe = this.decimalPipe;
        const formatString = `1.${decimalPlacesToDisplay}-${decimalPlacesToDisplay}`;

        return value != null ? _decimalPipe.transform(value, formatString) : null;
    }

    public decimalValueGetter(params: any, fieldName: string): number {
        const fieldNames = fieldName.split(".");

        // checks that each part of a nested field is not null
        let fieldValue = params.data;
        fieldNames.forEach((x) => {
            fieldValue = fieldValue[x];
            if (!fieldValue) {
                fieldValue = 0;
                return;
            }
        });

        return fieldValue;
    }

    public decimalComparator(id1: string, id2: string) {
        if (!id1) return -1;
        if (!id2) return 1;

        const value1 = parseFloat(id1.replace(",", ""));
        const value2 = parseFloat(id2.replace(",", ""));
        return value1 == value2 ? 0 : value1 > value2 ? 1 : -1;
    }

    public convertStringToDecimal(value: string): number {
        if (!value) return null;

        // accounting for parseFloat() function treating commas as decimals
        return parseFloat(value.replace(",", ""));
    }

    public createDecimalColumnDef(headerName: string, fieldName: string, decimalColumnDefParams?: DecimalColumnDefParams) {
        const _decimalPipe = this.decimalPipe;

        const decimalPlacesToDisplay = decimalColumnDefParams?.DecimalPlacesToDisplay ?? 2;
        const decimalFormatString = "1." + decimalPlacesToDisplay + "-" + decimalPlacesToDisplay;

        const decimalColDef: ColDef = {
            headerName: headerName,
            cellStyle: { "justify-content": "flex-end" },
            valueGetter: (params) => {
                const value = this.defaultValueGetter(params, fieldName);
                return value != null
                    ? _decimalPipe.transform(value, decimalFormatString)
                    : decimalColumnDefParams?.ZeroFillNullValues
                    ? _decimalPipe.transform(0, decimalFormatString)
                    : decimalColumnDefParams?.StringForNullValues
                    ? decimalColumnDefParams?.StringForNullValues
                    : null;
            },
            filter: "agNumberColumnFilter",
            filterValueGetter: (params) => this.convertStringToDecimal(_decimalPipe.transform(this.defaultValueGetter(params, fieldName), decimalFormatString)),
            comparator: this.decimalComparator,
        };

        this.applyDefaultQanatColumnDefParams(decimalColDef, decimalColumnDefParams);
        return decimalColDef;
    }

    public createLatLonColumnDef(headerName: "Latitude" | "Longitude", fieldName: string) {
        return this.createDecimalColumnDef(headerName, fieldName, { DecimalPlacesToDisplay: 5 });
    }

    public createYearColumnDef(headerName: string, fieldName: string): ColDef {
        return {
            headerName: headerName,
            valueGetter: (params) => this.decimalValueGetter(params, fieldName),
            comparator: this.decimalComparator,
            filter: NumberFilter,
            cellStyle: { "justify-content": "flex-end" },
        };
    }

    public createPhoneNumberColumnDef(headerName: string, fieldName: string): ColDef {
        return {
            headerName: headerName,
            field: fieldName,
            valueFormatter: (params) => this.phonePipe.transform(params.value),
            filterParams: {
                textFormatter: this.phonePipe.gridFilterTextFormatter,
            },
        };
    }

    public linkRendererComparator(id1: any, id2: any) {
        if (id1.LinkDisplay == id2.LinkDisplay) {
            return 0;
        }
        if (id1.LinkDisplay == null){
            // id1.LinkDisplay is null and id2.LinkDisplay is not null. Sort id1.LinkDisplay before
            return -1;
        }
        if(id2.LinkDisplay == null){
            // id1.LinkDisplay is not null and id2.LinkDisplay is null. Sort id1.LinkDisplay after
            return 1;
        }
        // compare case insensitive
        return id1.LinkDisplay.localeCompare(id2.LinkDisplay, 'en', {'sensitivity': 'base'});
    }

    public createLinkColumnDef(headerName: string, fieldName: string, linkValueField: string, linkColumnDefParams?: LinkColumnDefParams) {
        const colDef: ColDef = {
            headerName: headerName,
            field: fieldName,
            valueGetter: (params) => {
                return {
                    LinkValue: this.defaultValueGetter(params, linkValueField),
                    LinkDisplay: this.defaultValueGetter(params, linkColumnDefParams?.LinkDisplayField ?? fieldName),
                };
            },
            filterValueGetter: (params) => this.defaultValueGetter(params, fieldName),
            comparator: this.linkRendererComparator,
            cellRenderer: LinkRendererComponent,
            cellRendererParams: { inRouterLink: linkColumnDefParams?.InRouterLink },
        };

        this.applyDefaultQanatColumnDefParams(colDef, linkColumnDefParams);
        return colDef;
    }

    public createLinkHrefColumnDef(headerName: string, fieldName: string, linkValueField: string, linkColumnDefParams?: LinkColumnDefParams) {
        const colDef: ColDef = {
            headerName: headerName,
            field: fieldName,
            valueGetter: (params) => {
                return {
                    LinkValue: this.defaultValueGetter(params, linkValueField),
                    LinkDisplay: this.defaultValueGetter(params, linkColumnDefParams?.LinkDisplayField ?? fieldName),
                    href: `${linkColumnDefParams.HrefTemplate}/${this.defaultValueGetter(params, linkValueField)}`
                };
            },
            filterValueGetter: (params) => this.defaultValueGetter(params, fieldName),
            comparator: this.linkRendererComparator,
            cellRenderer: LinkRendererComponent,
        };
        this.applyDefaultQanatColumnDefParams(colDef, linkColumnDefParams);
        return colDef;
    }

    public multiLinkRendererComparator(id1: any, id2: any) {
        if (id1.downloadDisplay == id2.downloadDisplay) {
            return 0;
        }
        return id1.downloadDisplay > id2.downloadDisplay ? 1 : -1;
    }

    public createMultiLinkColumnDef(
        headerName: string,
        listField: string,
        linkValueField: string,
        linkDisplayField: string,
        multiLinkColumnDefParams?: MultiLinkColumnDefParams
    ): ColDef {
        const colDef: ColDef = {
            headerName: headerName,
            valueGetter: (params) => {
                const names = this.defaultValueGetter(params, listField)?.map((x) => {
                    return { LinkValue: this.defaultValueGetter(x, linkValueField, ""), LinkDisplay: this.defaultValueGetter(x, linkDisplayField, "") };
                });
                const downloadDisplay = names?.map((x) => x.LinkDisplay).join(", ");
                return { links: names, downloadDisplay: downloadDisplay };
            },
            filterValueGetter: (params) =>
                this.defaultValueGetter(params, listField)
                    ?.map((x) => this.defaultValueGetter(x, linkDisplayField, ""))
                    .join(", "),
            comparator: this.multiLinkRendererComparator,
            cellRenderer: MultiLinkRendererComponent,
            cellRendererParams: { inRouterLink: multiLinkColumnDefParams?.InRouterLink },
        };

        this.applyDefaultQanatColumnDefParams(colDef, multiLinkColumnDefParams);
        return colDef;
    }

    private dateFilterComparator(filterLocalDateAtMidnight, cellValue) {
        const filterDate = Date.parse(filterLocalDateAtMidnight);
        const cellDate = Date.parse(cellValue);

        return cellDate == filterDate ? 0 : cellDate < filterDate ? -1 : 1;
    }

    private dateSortComparator(id1: any, id2: any) {
        const date1 = id1 ? Date.parse(id1) : Date.parse("1/1/1900");
        const date2 = id2 ? Date.parse(id2) : Date.parse("1/1/1900");

        return date1 == date2 ? 0 : date1 > date2 ? 1 : -1;
    }

    public createDateColumnDef(headerName: string, fieldName: string, dateFormat: string, dateColumnDefParams?: DateColumnDefParams): ColDef {
        const _datePipe = this.datePipe;
        const timezone = dateColumnDefParams?.IgnoreLocalTimezone ? "+0000" : null;

        const dateColDef: ColDef = {
            headerName: headerName,
            valueGetter: (params) => {
                const value = this.defaultValueGetter(params, fieldName);
                return _datePipe.transform(value, dateFormat, timezone);
            },
            comparator: this.dateSortComparator,
            filter: "agDateColumnFilter",
            filterParams: {
                filterOptions: ["inRange"],
                comparator: this.dateFilterComparator,
            },
            sort: dateColumnDefParams?.Sort,
        };

        this.applyDefaultQanatColumnDefParams(dateColDef, dateColumnDefParams);
        return dateColDef;
    }

    public applyDefaultQanatColumnDefParams(colDef: ColDef, params: QanatColumnDefParams) {
        if (!params) return;

        if (params.FieldDefinitionType) {
            colDef.headerComponent = FieldDefinitionGridHeaderComponent;
            colDef.headerComponentParams = {
                fieldDefinitionType: params.FieldDefinitionType,
                labelOverride: params.FieldDefinitionLabelOverride,
                enableSorting: true,
            };
        }

        if (params.UseCustomDropdownFilter || params.CustomDropdownFilterField) {
            colDef.filter = CustomDropdownFilterComponent;
            colDef.filterParams = {
                field: params.CustomDropdownFilterField,
                columnContainsMultipleValues: params.ColumnContainsMultipleValues,
            };
        }

        if (params.Width) colDef.width = params.Width;
        if (params.MaxWidth) colDef.maxWidth = params.MaxWidth;
        if (params.Hide) colDef.hide = params.Hide;
        if (params.ValueGetter) colDef.valueGetter = params.ValueGetter;
        if (params.FilterValueGetter) colDef.filterValueGetter = params.FilterValueGetter;
        if (params.ValueFormatter) colDef.valueFormatter = params.ValueFormatter;
        if (params.CellClass) colDef.cellClass = params.CellClass;
        if (params.CellStyle) colDef.cellStyle = params.CellStyle;
    }

    public exportGridToCsv(grid: AgGridAngular, fileName: string, columnKeys: Array<string>) {
        const params = {
            skipHeader: false,
            columnGroups: false,
            skipFooters: true,
            skipRowGroups: true,
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
                } else {
                    return p.value;
                }
            },
        } as CsvExportParams;
        if (columnKeys) {
            params.columnKeys = columnKeys;
        }
        grid.api.exportDataAsCsv(params);
    }
}

export interface QanatColumnDefParams {
    Width?: number;
    MaxWidth?: number;
    Hide?: boolean;
    FieldDefinitionType?: string;
    FieldDefinitionLabelOverride?: string;
    UseCustomDropdownFilter?: boolean; // use to enable CustomDropdownFilter without specifying a field
    CustomDropdownFilterField?: string;
    ColumnContainsMultipleValues?: boolean;
    ValueGetter?: ValueGetterFunc;
    FilterValueGetter?: ValueGetterFunc;
    ValueFormatter?: ValueFormatterFunc;
    CellClass?: string | string[] | CellClassFunc;
    CellStyle?: CellStyle | CellStyleFunc;
}

export interface LinkColumnDefParams extends QanatColumnDefParams {
    Width?: number;
    InRouterLink?: string;
    LinkDisplayField?: string;
    HrefTemplate?: string;
}

export interface MultiLinkColumnDefParams extends QanatColumnDefParams {
    InRouterLink?: string;
}

export interface DecimalColumnDefParams extends QanatColumnDefParams {
    DecimalPlacesToDisplay?: number;
    ZeroFillNullValues?: boolean;
    StringForNullValues?: string;
}

export interface DateColumnDefParams extends QanatColumnDefParams {
    Sort?: SortDirection;
    IgnoreLocalTimezone?: boolean;
}
