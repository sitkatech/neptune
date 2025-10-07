import { Component } from "@angular/core";
import { PageHeaderComponent } from "src/app/shared/components/page-header/page-header.component";
import { AlertDisplayComponent } from "src/app/shared/components/alert-display/alert-display.component";
import { AsyncPipe } from "@angular/common";
import { HRUCharacteristicService } from "src/app/shared/generated/api/hru-characteristic.service";
import { HRUCharacteristicDto } from "src/app/shared/generated/model/hru-characteristic-dto";
import { Observable } from "rxjs";
import { UtilityFunctionsService } from "src/app/services/utility-functions.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { HruCharacteristicsGridComponent } from "src/app/shared/components/hru-characteristics-grid/hru-characteristics-grid.component";

@Component({
    selector: "hru-characteristics",
    standalone: true,
    imports: [PageHeaderComponent, AlertDisplayComponent, AsyncPipe, HruCharacteristicsGridComponent],
    templateUrl: "./hru-characteristics.component.html",
})
export class HRUCharacteristicsComponent {
    public hruCharacteristics$: Observable<HRUCharacteristicDto[]>;
    public customRichTextTypeID = NeptunePageTypeEnum.HRUCharacteristics;

    constructor(private hruCharacteristicService: HRUCharacteristicService, private utilityFunctions: UtilityFunctionsService) {}

    ngOnInit(): void {
        this.hruCharacteristics$ = this.hruCharacteristicService.listHRUCharacteristic();
    }
}
