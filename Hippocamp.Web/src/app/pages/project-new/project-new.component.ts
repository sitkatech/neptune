import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

@Component({
  selector: 'hippocamp-project-new',
  templateUrl: './project-new.component.html',
  styleUrls: ['./project-new.component.scss']
})
export class ProjectNewComponent implements OnInit {

  private watchUserChangeSubscription: any;
  public currentUser: PersonDto;
  
  public projectModel: ProjectCreateDto;
  public organizations: Array<OrganizationSimpleDto>;
  public stormwaterJurisdictions: Array<StormwaterJurisdictionSimpleDto>;
  public isLoadingSubmit = false;

  constructor(
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
      this.projectModel.PrimaryContactPersonID = this.currentUser.PersonID;

      forkJoin(
        this.organizationService.getAllOrganizations(),
        this.stormwaterJurisdictionService.getByPersonID(this.currentUser.PersonID)
      ).subscribe(([organizations, stormwaterJurisdictions]) => {
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

  public onSubmit(createProjectForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;

    this.projectService.newProject(this.projectModel)
      .subscribe(() => {
        this.isLoadingSubmit = false;
        createProjectForm.reset();
        
        this.router.navigateByUrl("/projects").then(x => {
          this.alertService.pushAlert(new Alert("Your project was successfully created.", AlertContext.Success));
        });
      }, error => {
        this.isLoadingSubmit = false;
        window.scroll(0,0);
        this.cdr.detectChanges();
      }
    );
  }
}
