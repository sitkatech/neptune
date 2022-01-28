import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  selector: 'hippocamp-project-new',
  templateUrl: './project-new.component.html',
  styleUrls: ['./project-new.component.scss']
})
export class ProjectNewComponent implements OnInit {

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
    private projectService: ProjectService
  ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      
      this.projectModel = new ProjectCreateDto();
      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        this.projectService.getByID(this.projectID).subscribe(project => {
          this.mapProjectSimpleDtoToProjectModel(project);
        });
      }

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

}
