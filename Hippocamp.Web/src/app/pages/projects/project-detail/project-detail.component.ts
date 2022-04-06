import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DelineationService } from 'src/app/services/delineation.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/models';
import { ProjectNetworkSolveHistorySimpleDto } from 'src/app/shared/generated/model/project-network-solve-history-simple-dto';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { ProjectNetworkSolveHistoryStatusTypeEnum } from 'src/app/shared/models/enums/project-network-solve-history-status-type.enum';
import { TreatmentBmpsComponent } from '../project-workflow/treatment-bmps/treatment-bmps.component';

@Component({
  selector: 'hippocamp-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})
export class ProjectDetailComponent implements OnInit {

  private projectID: number;
  public project: ProjectSimpleDto;
  public treatmentBMPs: Array<TreatmentBMPUpsertDto>;
  public delineations: Array<DelineationUpsertDto>;
  public projectNetworkSolveHistories: Array<ProjectNetworkSolveHistorySimpleDto>;
  public attachments: Array<ProjectDocumentSimpleDto>;


  constructor(
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        forkJoin({
          project: this.projectService.getByID(this.projectID),
          treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
          delineations: this.delineationService.getDelineationsByProjectID(this.projectID),
          projectNetworkSolveHistories: this.projectService.getNetworkSolveHistoriesForProject(this.projectID),
          attachments: this.projectService.getAttachmentsByProjectID(this.projectID)
        }).subscribe(({project, treatmentBMPs, delineations, projectNetworkSolveHistories, attachments}) => {
          this.treatmentBMPs = treatmentBMPs;
          this.delineations = delineations;
          this.projectNetworkSolveHistories = projectNetworkSolveHistories;
          this.project = project;
          this.attachments = attachments;
        });
      }
    });
  }

  showModelResultsPanel(): boolean {
    return !this.project?.DoesNotIncludeTreatmentBMPs && this.projectNetworkSolveHistories != null && this.projectNetworkSolveHistories.length > 0 && this.projectNetworkSolveHistories.filter(x => x.ProjectNetworkSolveHistoryStatusTypeID == ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded).length > 0;
  }

}
