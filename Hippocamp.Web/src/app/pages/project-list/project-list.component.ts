import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from 'src/app/services/project/project.service';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef } from 'ag-grid-community';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { RoleEnum } from 'src/app/shared/models/enums/role.enum';

@Component({
  selector: 'hippocamp-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  @ViewChild('projectsGrid') projectsGrid: AgGridAngular;

  private watchUserChangeSubscription: any;
  private currentUser: PersonDto;

  public projects: Array<ProjectSimpleDto>;
  public projectColumnDefs: Array<ColDef>;
  public defaultColDef: ColDef;

  constructor(
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService,
    private utilityFunctionsService: UtilityFunctionsService
  ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      this.createProjectGridColDefs();

      if (this.isAdministrator()) {
        this.projectService.getAllProjects().subscribe(projects => {
          this.projects = projects;
        });
      }
      if (this.isManagerOrEditor()) {
        this.projectService.getProjectsByPersonID(this.currentUser.PersonID).subscribe(projects => {
          this.projects = projects;
          this.projectsGrid.columnApi.autoSizeAllColumns();
        })
      }

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  private createProjectGridColDefs() {
    this.projectColumnDefs = [
      { headerName: 'Project Name', field: 'ProjectName' },
      { headerName: 'Organization', field: 'Organization.OrganizationName' },
      { headerName: 'Jurisdiction', field: 'StormwaterJurisdiction.StormwaterJurisdictionID' },
      { headerName: 'Status', field: 'ProjectStatus.ProjectStatusName' },
      this.utilityFunctionsService.createDateColumnDef('Date Created', 'DateCreated', 'M/d/yyyy', 120),
      { headerName: 'Project Description', field: 'ProjectDescription' }
    ];
    
    this.defaultColDef = {
      filter: true, sortable: true, resizable: true
    };
  }

  public isAdministrator(): boolean {
    return this.currentUser.Role.RoleID == RoleEnum.Admin || this.currentUser.Role.RoleID == RoleEnum.SitkaAdmin;
  }

  public isManagerOrEditor(): boolean {
    return this.currentUser.Role.RoleID == RoleEnum.JurisdictionManager || this.currentUser.Role.RoleID == RoleEnum.JurisdictionEditor;
  }
}
