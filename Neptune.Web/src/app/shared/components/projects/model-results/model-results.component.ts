import { Component, Input, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { ProjectNetworkSolveHistoryStatusTypeEnum } from 'src/app/shared/generated/enum/project-network-solve-history-status-type-enum';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectLoadReducingResultDto } from 'src/app/shared/generated/model/project-load-reducing-result-dto';
import { ProjectNetworkSolveHistorySimpleDto } from 'src/app/shared/generated/model/project-network-solve-history-simple-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { TreatmentBMPHRUCharacteristicsSummarySimpleDto } from 'src/app/shared/generated/model/treatment-bmphru-characteristics-summary-simple-dto';

@Component({
  selector: 'hippocamp-model-results',
  templateUrl: './model-results.component.html',
  styleUrls: ['./model-results.component.scss']
})
export class ModelResultsComponent implements OnInit {
  public ModeledPerformanceDisplayTypeEnum = ModeledPerformanceDisplayTypeEnum;
  public activeID = ModeledPerformanceDisplayTypeEnum.Total;
  public modelingSelectListOptions: { TreatmentBMPID: number, TreatmentBMPName: string }[] = [];
  public treatmentBMPIDForSelectedProjectLoadReducingResult = 0;
  public projectLoadReducingResults: Array<ProjectLoadReducingResultDto>;
  public selectedProjectLoadReducingResult: ProjectLoadReducingResultDto;
  public treatmentBMPHRUCharacteristicSummaries: Array<TreatmentBMPHRUCharacteristicsSummarySimpleDto>;
  public selectedTreatmentBMPHRUCharacteristicSummaries: Array<TreatmentBMPHRUCharacteristicsSummarySimpleDto>;
  public selectedTreatmentBMPHRUCharacteristicSummaryTotal: TreatmentBMPHRUCharacteristicsSummarySimpleDto = {
    LandUse: "Total",
    Area: 0,
    ImperviousCover: 0
  };
  public ProjectNetworkHistoryStatusTypeEnum = ProjectNetworkSolveHistoryStatusTypeEnum;

  @Input('projectNetworkSolveHistories') projectNetworkSolveHistories: ProjectNetworkSolveHistorySimpleDto[];
  @Input('treatmentBMPs') treatmentBMPs: Array<TreatmentBMPUpsertDto> = [];
  @Input('delineations') delineations: Array<DelineationUpsertDto> = [];
  @Input('projectID') projectID: number;

  constructor(
    private projectService: ProjectService,
  ) { }

  ngOnInit(): void {
    if (this.projectNetworkSolveHistories != null &&
      this.projectNetworkSolveHistories != undefined &&
      this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded).length > 0) {
      forkJoin({
        modeledResults: this.projectService.projectsProjectIDLoadReducingResultsGet(this.projectID),
        treatmentBMPHRUCharacteristicSummaries: this.projectService.projectsProjectIDTreatmentBmpHruCharacteristicsGet(this.projectID)
      })
        .subscribe(({ modeledResults, treatmentBMPHRUCharacteristicSummaries }) => {
          this.projectLoadReducingResults = modeledResults;
          this.treatmentBMPHRUCharacteristicSummaries = treatmentBMPHRUCharacteristicSummaries;
          this.populateModeledResultsOptions();
          this.updateSelectedProjectLoadReducingResult();
        });
    }
  }

  populateModeledResultsOptions() {
    var tempOptions = [];
    tempOptions.push({ TreatmentBMPID: 0, TreatmentBMPName: "All Treatment BMPs" });
    this.projectLoadReducingResults.forEach(x => {
      var treatmentBMP = this.treatmentBMPs.find(y => y.TreatmentBMPID == x.TreatmentBMPID);
      tempOptions.push({ TreatmentBMPID: treatmentBMP.TreatmentBMPID, TreatmentBMPName: treatmentBMP.TreatmentBMPName });
    });
    this.modelingSelectListOptions = [...this.modelingSelectListOptions, ...tempOptions];
  }

  updateSelectedProjectLoadReducingResult() {
    if (this.treatmentBMPIDForSelectedProjectLoadReducingResult != 0) {
      this.selectedProjectLoadReducingResult = this.projectLoadReducingResults.find(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedProjectLoadReducingResult);
      this.selectedTreatmentBMPHRUCharacteristicSummaries = this.hruCharacteristicsGroupByLandUse(this.treatmentBMPHRUCharacteristicSummaries.filter(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedProjectLoadReducingResult));
      this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();
      return;
    }

    this.selectedProjectLoadReducingResult = new ProjectLoadReducingResultDto();
    //We get the property names of the first one so we have a fully populated object because Typescript doesn't always populate the keys which is VERY annoying
    if(this.projectLoadReducingResults.length > 0)
    {
      for (let key of Object.getOwnPropertyNames(this.projectLoadReducingResults[0])) {
        this.selectedProjectLoadReducingResult[key] = this.projectLoadReducingResults.reduce((sum, current) => sum + (current[key] ?? 0), 0);
      }
    }

    this.selectedTreatmentBMPHRUCharacteristicSummaries = this.hruCharacteristicsGroupByLandUse([...new Map(this.treatmentBMPHRUCharacteristicSummaries.map(item => [item['ProjectHRUCharacteristicID'], item])).values()]);
    this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();

  }

  private hruCharacteristicsGroupByLandUse(distinctHRUCharacteristicSummaries: TreatmentBMPHRUCharacteristicsSummarySimpleDto[]): TreatmentBMPHRUCharacteristicsSummarySimpleDto[] {
    return [...distinctHRUCharacteristicSummaries.reduce((r, o) => {
      const key = o.LandUse;

      const item = r.get(key) || Object.assign({}, o, {
        Area: 0,
        ImperviousCover: 0
      });

      item.Area += o.Area;
      item.ImperviousCover += o.ImperviousCover;

      return r.set(key, item);
    }, new Map).values()].sort((a, b) => { if (a.LandUse > b.LandUse) { return 1; } if (b.LandUse > a.LandUse) { return -1; } return 0; });
  }

  updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal() {
    this.selectedTreatmentBMPHRUCharacteristicSummaryTotal.Area = this.selectedTreatmentBMPHRUCharacteristicSummaries.reduce((sum, current) => sum + current.Area, 0);
    this.selectedTreatmentBMPHRUCharacteristicSummaryTotal.ImperviousCover = this.selectedTreatmentBMPHRUCharacteristicSummaries.reduce((sum, current) => sum + current.ImperviousCover, 0);
  }

  //Helps to prevent keyvalue pipe from trying to do sorting
  returnZero(): number {
    return 0;
  }

  getModelResultsLastCalculatedText(): string {
    if (this.projectNetworkSolveHistories == null || this.projectNetworkSolveHistories == undefined || this.projectNetworkSolveHistories.length == 0) {
      return "";
    }

    //These will be ordered by date by the api
    var successfulResults = this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded);

    if (successfulResults == null || successfulResults.length == 0) {
      return "";
    }

    return `Results last calculated at ${new Date(successfulResults[0].LastUpdated).toLocaleString()}`
  }

  isMostRecentHistoryOfType(type: ProjectNetworkSolveHistoryStatusTypeEnum): boolean {
    return this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories.length > 0 && this.projectNetworkSolveHistories[0].ProjectNetworkSolveHistoryStatusTypeID == type;
  }

  getNotFullyParameterizedBMPNames(): string[] {
    return this.treatmentBMPs.filter(x => !x.IsFullyParameterized).map(x => x.TreatmentBMPName);
  }

  getBMPNamesForDelineationsWithDiscrepancies(): string[] {
    if (this.delineations == null || this.delineations.length == 0) {
      return [];
    }

    var treatmentBMPIDsForDelineationsWithDiscrepancies = this.delineations.filter(x => x.HasDiscrepancies).map(x => x.TreatmentBMPID);

    if (treatmentBMPIDsForDelineationsWithDiscrepancies == null || treatmentBMPIDsForDelineationsWithDiscrepancies.length == 0) {
      return [];
    }

    return this.treatmentBMPs.filter(x => treatmentBMPIDsForDelineationsWithDiscrepancies.includes(x.TreatmentBMPID)).map(x => x.TreatmentBMPName);
  }
}

export enum ModeledPerformanceDisplayTypeEnum {
  Total = "Total",
  Dry = "Dry",
  Wet = "Wet"
}
