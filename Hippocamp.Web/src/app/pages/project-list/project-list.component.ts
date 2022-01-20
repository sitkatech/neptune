import { Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from 'src/app/services/project/project.service';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef } from 'ag-grid-community';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';

@Component({
  selector: 'hippocamp-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  @ViewChild('projectsGrid') projectsGrid: AgGridAngular;

  public projects: Array<ProjectSimpleDto>;
  public projectColumnDefs: Array<ColDef>;
  public defaultColDef: ColDef;

  constructor(
    private projectService: ProjectService,
    private utilityFunctionsService: UtilityFunctionsService
  ) { }

  ngOnInit(): void {
    this.projectColumnDefs = [
      { headerName: 'Project Name', field: 'ProjectName', width: 180 },
      { headerName: 'Organization', field: 'Organization.OrganizationName', width: 180 },
      { headerName: 'Jurisdiction', field: 'StormwaterJurisdiction.StormwaterJurisdictionID', width: 100 },
      { headerName: 'Status', field: 'ProjectStatus.ProjectStatusName', width: 100 },
      this.utilityFunctionsService.createDateColumnDef('Date Created', 'DateCreated', 'M/d/yyyy', 120),
      { headerName: 'Project Description', field: 'ProjectDescription', width: 250 }
    ];

    this.defaultColDef = {
      filter: true, sortable: true, resizable: true
    }

    this.projectService.getProjects().subscribe(projects => {
      this.projects = projects;
    });
  }
}
