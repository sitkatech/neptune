import { Component, Input } from "@angular/core";
import { AgGridAngular } from "ag-grid-angular";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { IconComponent } from "../icon/icon.component";

@Component({
    selector: "neptune-csv-download-button",
    templateUrl: "./csv-download-button.component.html",
    styleUrls: ["./csv-download-button.component.scss"],
    imports: [IconComponent]
})
export class CsvDownloadButtonComponent {
    @Input() grid: AgGridAngular;
    @Input() fileName: string;
    @Input() colIDsToExclude: string[] = [];

    constructor(private utilityFunctionsService: UtilityFunctionsService) {}

    public exportToCsv() {
        const columnsKeys = this.grid.api.getAllDisplayedColumns();
        const columnIDs = columnsKeys.map((keys) => keys.getColId()).filter((x) => this.colIDsToExclude.indexOf(x) < 0);

        this.utilityFunctionsService.exportGridToCsv(this.grid, this.fileName + ".csv", columnIDs);
    }
}
