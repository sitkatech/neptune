import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { OrganizationService } from 'src/app/services/organization/organization.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { OrganizationSimpleDto } from 'src/app/shared/generated/model/organization-simple-dto';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectCreateDto } from 'src/app/shared/generated/model/project-create-dto';
import { StormwaterJurisdictionSimpleDto } from 'src/app/shared/generated/model/stormwater-jurisdiction-simple-dto';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';

@Component({
  selector: 'hippocamp-project-basics',
  templateUrl: './project-basics.component.html',
  styleUrls: ['./project-basics.component.scss']
})
export class ProjectBasicsComponent implements OnInit {

  private watchUserChangeSubscription: any;
  public currentUser: PersonDto;
  
  public projectID: number;
  public projectModel: ProjectCreateDto;
  public organizations: Array<OrganizationSimpleDto>;
  public stormwaterJurisdictions: Array<StormwaterJurisdictionSimpleDto>;
  public isLoadingSubmit = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private organizationService: OrganizationService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private projectService: ProjectService
  ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      
      this.projectModel = new ProjectCreateDto();
      const projectID = this.route.snapshot.paramMap.get("id");
      if (projectID) {
        this.projectID = parseInt(projectID);
        this.projectService.getByID(this.projectID).subscribe(project => {
          this.mapProjectSimpleDtoToProjectModel(project);
        });
      } else {
        this.projectModel.PrimaryContactPersonID = this.currentUser.PersonID;
      }

      forkJoin({
        organizations: this.organizationService.getAllOrganizations(),
        stormwaterJurisdictions: this.stormwaterJurisdictionService.getByPersonID(this.currentUser.PersonID)
      }).subscribe(({organizations, stormwaterJurisdictions}) => {
        this.organizations = organizations;
        this.stormwaterJurisdictions = stormwaterJurisdictions;

        if (stormwaterJurisdictions.length == 1) {
          this.projectModel.StormwaterJurisdictionID = stormwaterJurisdictions[0].StormwaterJurisdictionID;
        }
      });

      this.organizationService.getAllOrganizations().subscribe(organizations => {
        this.organizations = organizations;
      });

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  private mapProjectSimpleDtoToProjectModel(project: ProjectSimpleDto) {
    this.projectModel.ProjectName = project.ProjectName;
    this.projectModel.OrganizationID = project.OrganizationID;
    this.projectModel.StormwaterJurisdictionID = project.StormwaterJurisdictionID;
    this.projectModel.PrimaryContactPersonID = project.PrimaryContactPersonID;
    this.projectModel.ProjectDescription = project.ProjectDescription;
    this.projectModel.AdditionalContactInformation = project.AdditionalContactInformation;
  }

  private onSubmitSuccess(createProjectForm: HTMLFormElement, successMessage: string) {
    
      this.isLoadingSubmit = false;
      createProjectForm.reset();
      
      this.router.navigateByUrl("/projects").then(x => {
        this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
      });
  }

  private onSubmitFailure() {
    this.isLoadingSubmit = false;
    window.scroll(0,0);
    this.cdr.detectChanges();
  }

  public onSubmit(createProjectForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;

    if (this.projectID) {
      this.projectService.updateProject(this.projectID, this.projectModel).subscribe(() => {
        this.onSubmitSuccess(createProjectForm, "Your project was successfully updated.")
      }, error => {
        this.onSubmitFailure();
      });
    } else {
      this.projectService.newProject(this.projectModel).subscribe(() => {
        this.onSubmitSuccess(createProjectForm, "Your project was successfully created.")
      }, error => {
        this.onSubmitFailure();
      });
    }
  }
}
