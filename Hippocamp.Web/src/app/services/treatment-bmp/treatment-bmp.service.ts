import { Injectable } from '@angular/core';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Observable } from 'rxjs';
import { ApiService } from 'src/app/shared/services';
import { TreatmentBMPTypeSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-type-simple-dto';
import { TimeOfConcentrationDto } from 'src/app/shared/generated/model/time-of-concentration-dto';
import { RoutingConfigurationDto } from 'src/app/shared/generated/model/routing-configuration-dto';
import { MonthsOfOperationDto } from 'src/app/shared/generated/model/months-of-operation-dto';
import { UnderlyingHydrologicSoilGroupDto } from 'src/app/shared/generated/model/underlying-hydrologic-soil-group-dto';
import { DryWeatherFlowOverrideDto } from 'src/app/shared/generated/model/dry-weather-flow-override-dto';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';


@Injectable({
  providedIn: 'root'
})
export class TreatmentBMPService {

  constructor(
    private apiService: ApiService
  ) { }

  getTreatmentBMPsByProjectID(projectID: number): Observable<Array<TreatmentBMPUpsertDto>> {
    let route = `/treatmentBMPs/${projectID}/getByProjectID`;
    return this.apiService.getFromApi(route);
  }

  getTypes(): Observable<Array<TreatmentBMPTypeSimpleDto>> {
    let route = `treatmentBMPs/types`;
    return this.apiService.getFromApi(route);
  }

  getModelingAttributesDropdownitems(): Observable<Array<TreatmentBMPModelingAttributeDropdownItemDto>> {
    let route = `treatmentBMPs/modelingAttributeDropdownItems`;
    return this.apiService.getFromApi(route);
  }

  getTimesOfConcentration(): Observable<Array<TimeOfConcentrationDto>> {
    let route = `treatmentBMPs/timesOfConcentration`;
    return this.apiService.getFromApi(route);
  }

  getRoutingConfigurations(): Observable<Array<RoutingConfigurationDto>> {
    let route = `treatmentBMPs/routingConfiguration`;
    return this.apiService.getFromApi(route);
  }

  getMonthsOfOperation(): Observable<Array<MonthsOfOperationDto>> {
    let route = `treatmentBMPs/monthsOfOperation`;
    return this.apiService.getFromApi(route);
  }

  getUnderlyingHydrologicSoilGroups(): Observable<Array<UnderlyingHydrologicSoilGroupDto>> {
    let route = `treatmentBMPs/underlyingHydrologicSoilGroup`;
    return this.apiService.getFromApi(route);
  }

  getDryWeatherFlowOverrides(): Observable<Array<DryWeatherFlowOverrideDto>> {
    let route = `treatmentBMPs/dryWeatherFlowOverride`;
    return this.apiService.getFromApi(route);
  }
}
