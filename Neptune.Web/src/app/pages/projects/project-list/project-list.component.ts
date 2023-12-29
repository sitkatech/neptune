import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
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
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { FieldDefinitionGridHeaderComponent } from 'src/app/shared/components/field-definition-grid-header/field-definition-grid-header.component';
import { CustomDropdownFilterComponent } from 'src/app/shared/components/custom-dropdown-filter/custom-dropdown-filter.component';
import { environment } from 'src/environments/environment';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';


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
  public richTextTypeID = NeptunePageTypeEnum.HippocampProjectsList;
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
        valueGetter: params => params.data.ProjectID + (params.data.ShareOCTAM2Tier2Scores ? '/review-and-share' : ''),
        cellRenderer: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { inRouterLink: '/projects/edit/', fontawesomeIconName: 'edit', CssClasses: 'text-primary' },
        width: 40, sortable: false, filter: false
      },
      {
        cellRenderer: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { isSpan: true, fontawesomeIconName: 'trash', CssClasses: 'text-danger' },
        width: 40, sortable: false, filter: false
      },
      {
        valueGetter: (params: any) => {
          return { LinkValue: params.data.ProjectID, LinkDisplay: "View", CssClasses: "btn btn-hippocamp btn-sm" };
        }, cellRenderer: LinkRendererComponent,
        cellRendererParams: { inRouterLink: "/projects/", },
        width: 57, sortable: false, filter: false
      },
      {
        headerName: 'Project ID', valueGetter: (params: any) => {
          return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectID };
        }, cellRenderer: LinkRendererComponent,
        cellRendererParams: { inRouterLink: "/projects/" },
        filterValueGetter: (params: any) => {
          return params.data.ProjectID;
        },
        comparator: this.utilityFunctionsService.linkRendererComparator
      },
      {
        headerName: 'Project Name', valueGetter: (params: any) => {
          return { LinkValue: params.data.ProjectID, LinkDisplay: params.data.ProjectName };
        }, cellRenderer: LinkRendererComponent,
        cellRendererParams: { inRouterLink: "/projects/" },
        filterValueGetter: (params: any) => {
          return params.data.ProjectID;
        },
        comparator: this.utilityFunctionsService.linkRendererComparator
      },
      { 
        headerComponent: FieldDefinitionGridHeaderComponent,
        headerComponentParams: { fieldDefinitionType: 'Organization' },
        field: 'Organization.OrganizationName' 
      },
      { 
        headerComponent: FieldDefinitionGridHeaderComponent,
        headerComponentParams: { fieldDefinitionType: 'Jurisdiction'},
        field: 'StormwaterJurisdiction.Organization.OrganizationName' 
      },
      this.utilityFunctionsService.createDateColumnDef('Date Created', 'DateCreated', 'M/d/yyyy', 120),
      { headerName: 'Project Description', field: 'ProjectDescription' },
      { 
        headerName: 'Shared with OCTA M2 Tier 2 Grant Program', 
        valueGetter: params => params.data.ShareOCTAM2Tier2Scores ? 'Yes' : 'No',
        filter: CustomDropdownFilterComponent,
        filterParams: { field: 'ShareOCTAM2Tier2Scores'}
      },
      this.utilityFunctionsService.createDateColumnDef('Last Shared with OCTA M2 Tier 2 Grant Program', 'OCTAM2Tier2ScoresLastSharedDate', 'short')
    ];

    this.defaultColDef = {
      filter: true, sortable: true, resizable: true
    };
  }

  private updateGridData() {
    this.projectService.projectsGet().subscribe(projects => {
      this.projectsGrid.api.setRowData(projects);
      this.projectsGrid.api.sizeColumnsToFit();
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
      {
        ariaLabelledBy: modalTitle, beforeDismiss: () => this.checkIfDeleting(), backdrop: 'static', keyboard: false
      });
  }

  private checkIfDeleting(): boolean {
    return this.isLoadingDelete;
  }

  public deleteProject() {
    this.isLoadingDelete = true;

    this.projectService.projectsProjectIDDeleteDelete(this.projectIDToDelete).subscribe(() => {
      this.isLoadingDelete = false;
      this.modalReference.close();
      this.alertService.pushAlert(new Alert('Project was successfully deleted.', AlertContext.Success, true));
      this.updateGridData();
    }, error => {
      this.isLoadingDelete = false;
      window.scroll(0, 0);
      this.cdr.detectChanges();
    });
  }

  public downloadProjectModelResults() {
    this.projectService.projectsDownloadGet().subscribe(csv => {
      //Create a fake object for us to click and download
      var a = document.createElement('a');
      a.href = URL.createObjectURL(csv);
      a.download = `project-modeled-results.csv`;
      document.body.appendChild(a);
      a.click();
      //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
      URL.revokeObjectURL(a.href);
      document.body.removeChild(a);
    }, (() => {
      this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
    }))
  }

  public downloadTreatmentBMPModelResults() {
    this.projectService.projectsTreatmentBMPsDownloadGet().subscribe(csv => {
      //Create a fake object for us to click and download
      var a = document.createElement('a');
      a.href = URL.createObjectURL(csv);
      a.download = `project-BMP-modeled-results.csv`;
      document.body.appendChild(a);
      a.click();
      //Revoke the generated url so the blob doesn't hang in memory https://javascript.info/blob
      URL.revokeObjectURL(a.href);
      document.body.removeChild(a);
    }, (() => {
      this.alertService.pushAlert(new Alert(`There was an error while downloading the file. Please refresh the page and try again.`, AlertContext.Danger));
    }))
  }

  public downloadTreatmentBMPDelineationShapefile(){
    return environment.geoserverMapServiceUrl + "/ows?service=WFS&version=2.0&request=GetFeature&typeName=OCStormwater:TreatmentBMPDelineation&outputFormat=shape-zip&SrsName=EPSG:4326";
  }

  public downloadTreatmentBMPLocationPointShapefile(){
    return environment.geoserverMapServiceUrl + "/ows?service=WFS&version=2.0&request=GetFeature&typeName=OCStormwater:TreatmentBMPPointLocation&outputFormat=shape-zip&SrsName=EPSG:4326";
  }
}
