import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { OrganizationSimpleDto } from 'src/app/shared/generated/model/organization-simple-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { PersonDto, ProjectDto, StormwaterJurisdictionDto } from 'src/app/shared/generated/model/models';

@Component({
  selector: 'hippocamp-project-workflow-outlet',
  templateUrl: './project-workflow-outlet.component.html',
  styleUrls: ['./project-workflow-outlet.component.scss']
})
export class ProjectWorkflowOutletComponent implements OnInit {

  
  public currentUser: PersonDto;

  private workflowUpdateSubscription: Subscription;
  
  public projectID: number;
  public projectModel: ProjectUpsertDto;
  public organizations: Array<OrganizationSimpleDto>;
  public stormwaterJurisdictions: Array<StormwaterJurisdictionDto>;
  public isLoadingSubmit = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private projectWorkflowService: ProjectWorkflowService
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      
      this.projectModel = new ProjectUpsertDto();
      this.parseURLForProjectIDAndGetEntitiesIfPresent();

      this.workflowUpdateSubscription = this.projectWorkflowService.workflowUpdate.subscribe(() => {
        this.parseURLForProjectIDAndGetEntitiesIfPresent();
      })

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
    this.cdr.detach();
  }

  private parseURLForProjectIDAndGetEntitiesIfPresent() {
    const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        this.projectService.projectsProjectIDGet(this.projectID).subscribe(project => {
          this.mapProjectSimpleDtoToProjectModel(project);
        });
      }
  }

  private mapProjectSimpleDtoToProjectModel(project: ProjectDto) {
    this.projectModel.ProjectName = project.ProjectName;
    this.projectModel.OrganizationID = project.OrganizationID;
    this.projectModel.StormwaterJurisdictionID = project.StormwaterJurisdictionID;
    this.projectModel.PrimaryContactPersonID = project.PrimaryContactPersonID;
    this.projectModel.ProjectDescription = project.ProjectDescription;
    this.projectModel.AdditionalContactInformation = project.AdditionalContactInformation;
    this.projectModel.DoesNotIncludeTreatmentBMPs = project.DoesNotIncludeTreatmentBMPs;
    this.projectModel.CalculateOCTAM2Tier2Scores = project.CalculateOCTAM2Tier2Scores;
    this.projectModel.ShareOCTAM2Tier2Scores = project.ShareOCTAM2Tier2Scores;
  }

}
