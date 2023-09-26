import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { OrganizationService } from 'src/app/services/organization/organization.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { OrganizationSimpleDto } from 'src/app/shared/generated/model/organization-simple-dto';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { StormwaterJurisdictionSimpleDto } from 'src/app/shared/generated/model/stormwater-jurisdiction-simple-dto';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { Subscription } from 'rxjs/internal/Subscription';

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
  public stormwaterJurisdictions: Array<StormwaterJurisdictionSimpleDto>;
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
        this.projectService.getByID(this.projectID).subscribe(project => {
          this.mapProjectSimpleDtoToProjectModel(project);
        });
      }
  }

  private mapProjectSimpleDtoToProjectModel(project: ProjectSimpleDto) {
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