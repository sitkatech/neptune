import { Component, Input, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ProjectService } from 'src/app/services/project/project.service';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectNetworkSolveHistorySimpleDto } from 'src/app/shared/generated/model/project-network-solve-history-simple-dto';
import { TreatmentBMPModeledResultSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-modeled-result-simple-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { TreatmentBMPHRUCharacteristicsSummarySimpleDto } from 'src/app/shared/generated/model/treatment-bmphru-characteristics-summary-simple-dto';
import { ProjectNetworkSolveHistoryStatusTypeEnum } from 'src/app/shared/models/enums/project-network-solve-history-status-type.enum';

@Component({
  selector: 'hippocamp-model-results',
  templateUrl: './model-results.component.html',
  styleUrls: ['./model-results.component.scss']
})
export class ModelResultsComponent implements OnInit {
  public ModeledPerformanceDisplayTypeEnum = ModeledPerformanceDisplayTypeEnum;
  public activeID = ModeledPerformanceDisplayTypeEnum.Total;
  public modelingSelectListOptions: { TreatmentBMPID: number, TreatmentBMPName: string }[] = [];
  public treatmentBMPIDForSelectedModelResults = 0;
  public modeledResults: Array<TreatmentBMPModeledResultSimpleDto>;
  public selectedModelResults: TreatmentBMPModeledResultSimpleDto;
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
        modeledResults: this.projectService.getModeledResultsForProject(this.projectID),
        treatmentBMPHRUCharacteristicSummaries: this.projectService.getTreatmentBMPHRUCharacteristicSummariesForProject(this.projectID)
      })
        .subscribe(({ modeledResults, treatmentBMPHRUCharacteristicSummaries }) => {
          this.modeledResults = modeledResults;
          this.treatmentBMPHRUCharacteristicSummaries = treatmentBMPHRUCharacteristicSummaries;
          this.populateModeledResultsOptions();
          this.updateSelectedModelResults();
        });
    }
  }

  populateModeledResultsOptions() {
    var tempOptions = [];
    tempOptions.push({ TreatmentBMPID: 0, TreatmentBMPName: "All Treatment BMPs" });
    this.modeledResults.forEach(x => {
      var treatmentBMP = this.treatmentBMPs.filter(y => y.TreatmentBMPID == x.TreatmentBMPID)[0];
      tempOptions.push({ TreatmentBMPID: treatmentBMP.TreatmentBMPID, TreatmentBMPName: treatmentBMP.TreatmentBMPName });
    });
    debugger;
    this.modelingSelectListOptions = [...this.modelingSelectListOptions, ...tempOptions];
  }

  updateSelectedModelResults() {
    if (this.treatmentBMPIDForSelectedModelResults != 0) {
      this.selectedModelResults = this.modeledResults.filter(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedModelResults)[0];
      this.selectedTreatmentBMPHRUCharacteristicSummaries = this.treatmentBMPHRUCharacteristicSummaries.filter(x => x.TreatmentBMPID == this.treatmentBMPIDForSelectedModelResults).sort((a, b) => { if (a.LandUse > b.LandUse) { return 1; } if (b.LandUse > a.LandUse) { return -1; } return 0 });
      this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();
      return;
    }

    this.selectedModelResults = new TreatmentBMPModeledResultSimpleDto();
    //We get the property names of the first one so we have a fully populated object because Typescript doesn't always populate the keys which is VERY annoying
    for (let key of Object.getOwnPropertyNames(this.modeledResults[0])) {
      this.selectedModelResults[key] = this.modeledResults.reduce((sum, current) => sum + (current[key] ?? 0), 0);
    }

    this.selectedTreatmentBMPHRUCharacteristicSummaries = [...this.treatmentBMPHRUCharacteristicSummaries.reduce((r, o) => {
      const key = o.LandUse;

      const item = r.get(key) || Object.assign({}, o, {
        Area: 0,
        ImperviousCover: 0
      });

      item.Area += o.Area;
      item.ImperviousCover += o.ImperviousCover;

      return r.set(key, item);
    }, new Map).values()].sort((a, b) => { if (a.LandUse > b.LandUse) { return 1; } if (b.LandUse > a.LandUse) { return -1; } return 0 });
    this.updateSelectedTreatmentBMPHRUCharacteristicSummaryTotal();

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
