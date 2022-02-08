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
import { PersonSimpleDto } from 'src/app/shared/generated/model/person-simple-dto';
import { UserService } from 'src/app/services/user/user.service';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-project-basics',
  templateUrl: './project-basics.component.html',
  styleUrls: ['./project-basics.component.scss']
})
export class ProjectBasicsComponent implements OnInit {
  public currentUser: PersonDto;
  
  public projectID: number;
  public projectModel: ProjectCreateDto;
  public organizations: Array<OrganizationSimpleDto>;
  public users: Array<PersonSimpleDto>;
  public stormwaterJurisdictions: Array<StormwaterJurisdictionSimpleDto>;
  public invalidFields: Array<string> = [];
  public isLoadingSubmit = false;
  public customRichTextTypeID : number = CustomRichTextType.ProjectBasics;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private organizationService: OrganizationService,
    private stormwaterJurisdictionService: StormwaterJurisdictionService,
    private projectService: ProjectService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      this.projectModel = new ProjectCreateDto();
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
        stormwaterJurisdictions: this.stormwaterJurisdictionService.getByPersonID(this.currentUser.PersonID),
        users: this.userService.getUsers()
      }).subscribe(({organizations, stormwaterJurisdictions, users}) => {
        this.organizations = organizations;
        this.stormwaterJurisdictions = stormwaterJurisdictions;
        this.users = users

        if (stormwaterJurisdictions.length == 1) {
          this.projectModel.StormwaterJurisdictionID = stormwaterJurisdictions[0].StormwaterJurisdictionID;
        }
      });

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
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

  public isFieldInvalid(fieldName: string) {
    return this.invalidFields.indexOf(fieldName) > -1;
  }

  private onSubmitSuccess(createProjectForm: HTMLFormElement, successMessage: string, projectID: number) {
    this.isLoadingSubmit = false;    
    this.router.navigateByUrl(`/projects/edit/${projectID}/project-basics`).then(() => {
      this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
    });
  }

  private onSubmitFailure(error) {
    if (error.error?.errors) {
      for (let key of Object.keys(error.error.errors)) {
        this.invalidFields.push(key);
      }
    }
    this.isLoadingSubmit = false;
    window.scroll(0,0);
    this.cdr.detectChanges();
  }

  public onSubmit(createProjectForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;
    this.invalidFields = [];
    this.alertService.clearAlerts();

    if (this.projectID) {
      this.projectService.updateProject(this.projectID, this.projectModel).subscribe(() => {
        this.onSubmitSuccess(createProjectForm, "Your project was successfully updated.", this.projectID)
      }, error => {
        this.onSubmitFailure(error);
      });
    } else {
      this.projectService.newProject(this.projectModel).subscribe(project => {
        this.onSubmitSuccess(createProjectForm, "Your project was successfully created.", project.ProjectID)
      }, error => {
        this.onSubmitFailure(error);
      });
    }
  }
}
