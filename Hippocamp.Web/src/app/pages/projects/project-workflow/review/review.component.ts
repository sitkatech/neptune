import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/project-document-simple-dto';
import { ProjectNetworkSolveHistorySimpleDto } from 'src/app/shared/generated/model/project-network-solve-history-simple-dto';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { ProjectNetworkSolveHistoryStatusTypeEnum } from 'src/app/shared/models/enums/project-network-solve-history-status-type.enum';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'hippocamp-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {

  private projectID: number;
  public project: ProjectSimpleDto;
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public delineations: Array<DelineationUpsertDto>;
  public projectNetworkSolveHistories: Array<ProjectNetworkSolveHistorySimpleDto>;
  public attachments: Array<ProjectDocumentSimpleDto>;
  public customRichTextTypeID = CustomRichTextType.Review;

  public isLoadingSubmit = false;
  public shareOCTAM2Tier2Scores: boolean;

  constructor(
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private treatmentBMPService: TreatmentBMPService,
    private route: ActivatedRoute,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        forkJoin({
          project: this.projectService.getByID(this.projectID),
          treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
          delineations: this.projectService.getDelineationsByProjectID(this.projectID),
          projectNetworkSolveHistories: this.projectService.getNetworkSolveHistoriesByProjectID(this.projectID),
          attachments: this.projectService.getAttachmentsByProjectID(this.projectID)
        }).subscribe(({project, treatmentBMPs, delineations, projectNetworkSolveHistories, attachments}) => {
          this.treatmentBMPs = treatmentBMPs;
          this.delineations = delineations;
          this.projectNetworkSolveHistories = projectNetworkSolveHistories;
          this.project = project;
          this.shareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores;
          this.attachments = attachments;
        });
      }
    });
  }

  showModelResultsPanel(): boolean {
    return !this.project?.DoesNotIncludeTreatmentBMPs && this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories.length > 0 && this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded).length > 0;
  }

  private mapProjectToUpsertDto(): ProjectUpsertDto {
    return new ProjectUpsertDto({
      ProjectName: this.project.ProjectName,
      OrganizationID: this.project.OrganizationID,
      StormwaterJurisdictionID: this.project.StormwaterJurisdictionID,
      PrimaryContactPersonID: this.project.PrimaryContactPersonID,
      ProjectDescription: this.project.ProjectDescription,
      AdditionalContactInformation: this.project.AdditionalContactInformation,
      DoesNotIncludeTreatmentBMPs: this.project.DoesNotIncludeTreatmentBMPs,
      CalculateOCTAM2Tier2Scores: this.project.CalculateOCTAM2Tier2Scores
    });
  }

  onSubmit() {
    this.isLoadingSubmit = true;

    var model = this.mapProjectToUpsertDto();
    model.ShareOCTAM2Tier2Scores = this.shareOCTAM2Tier2Scores;
    console.log(model.ShareOCTAM2Tier2Scores, this.shareOCTAM2Tier2Scores);

    this.projectService.updateProject(this.projectID, model).subscribe(() => {
      this.isLoadingSubmit = false;
      this.alertService.pushAlert(new Alert("Your project was successfully updated.", AlertContext.Success));
      window.scroll(0,0);
    }, error => {
      this.isLoadingSubmit = false;
      window.scroll(0,0);
    })
  }
}
