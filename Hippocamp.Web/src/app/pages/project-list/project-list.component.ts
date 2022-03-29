import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ProjectService } from 'src/app/services/project/project.service';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef, GridApi } from 'ag-grid-community';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { FontAwesomeIconLinkRendererComponent } from 'src/app/shared/components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';


@Component({
  selector: 'hippocamp-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  @ViewChild('projectsGrid') projectsGrid: AgGridAngular;
  @ViewChild('deleteProjectModal') deleteProjectModal

  
  private currentUser: PersonDto;

  private gridApi: GridApi;
  public richTextTypeID = CustomRichTextType.ProjectsList;
  public projectColumnDefs: Array<ColDef>;
  public defaultColDef: ColDef;
  private modalReference: NgbModalRef;
  private projectIDToDelete: number;
  private projectNameToDelete: string;
  private deleteColumnID = 1;
  private isLoadingDelete = false;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef,
    private alertService: AlertService,
    private projectService: ProjectService,
    private utilityFunctionsService: UtilityFunctionsService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      this.createProjectGridColDefs();
      this.updateGridData();

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
    
    
    this.cdr.detach();
  }

  public onGridReady(params) {
    this.gridApi = params.api;
  }

  private createProjectGridColDefs() {
    this.projectColumnDefs = [
      { 
        valueGetter: params => params.data.ProjectID, 
        cellRendererFramework: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { inRouterLink: '/projects/edit/', fontawesomeIconName: 'edit', CssClasses: 'text-primary'},
        width: 40, sortable: false, filter: false
      },
      {
        cellRendererFramework: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { isSpan: true, fontawesomeIconName: 'trash', CssClasses: 'text-danger'},
        width: 40, sortable: false, filter: false
      },
      { headerName: 'Project ID', field: 'ProjectID' },
      { headerName: 'Project Name', field: 'ProjectName' },
      { headerName: 'Organization', field: 'Organization.OrganizationName' },
      { headerName: 'Jurisdiction', field: 'StormwaterJurisdiction.Organization.OrganizationName' },
      { headerName: 'Status', field: 'ProjectStatus.ProjectStatusName', width: 120 },
      this.utilityFunctionsService.createDateColumnDef('Date Created', 'DateCreated', 'M/d/yyyy', 120),
      { headerName: 'Project Description', field: 'ProjectDescription' }
    ];
    
    this.defaultColDef = {
      filter: true, sortable: true, resizable: true
    };
  }

  private updateGridData() {
    this.projectService.getProjectsByPersonID(this.currentUser.PersonID).subscribe(projects => {
      this.projectsGrid.api.setRowData(projects);
    });
  }

  public exportToCsv() {
    const columns = this.projectsGrid.columnApi.getAllGridColumns();
    const columnIDsToDownload = columns.slice(2).map(x => x.getId());

    this.utilityFunctionsService.exportGridToCsv(this.projectsGrid, 'projects.csv', columnIDsToDownload);
  }

  public onCellClicked(event: any): void {
    if (event.column.colId == this.deleteColumnID) {
      this.projectIDToDelete = event.data.ProjectID;
      this.projectNameToDelete = event.data.ProjectName;
      this.launchModal(this.deleteProjectModal, 'deleteProjectModalTitle');
    }
  }

  private launchModal(modalContent: any, modalTitle: string): void {
    this.modalReference = this.modalService.open(
      modalContent, 
      { ariaLabelledBy: modalTitle, beforeDismiss: () => this.checkIfDeleting(), backdrop: 'static', keyboard: false 
    });
  }

  private checkIfDeleting(): boolean {
    return this.isLoadingDelete;
  }

  public deleteProject() { 
    this.isLoadingDelete = true;

    this.projectService.deleteProject(this.projectIDToDelete).subscribe(() => {
      this.isLoadingDelete = false;
      this.modalReference.close();
      this.alertService.pushAlert(new Alert('Project was successfully deleted.', AlertContext.Success, true));
      this.updateGridData();
    }, error => {
      this.isLoadingDelete = false;
      window.scroll(0,0);
      this.cdr.detectChanges();
    });
  }
}
