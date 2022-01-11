import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef } from 'ag-grid-community';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CustomPageService } from 'src/app/services/custom-page.service';
import { FontAwesomeIconLinkRendererComponent } from 'src/app/shared/components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { CustomDropdownFilterComponent } from 'src/app/shared/components/custom-dropdown-filter/custom-dropdown-filter.component';
import { UserDetailedDto } from 'src/app/shared/models';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'hippocamp-custom-page-list',
  templateUrl: './custom-page-list.component.html',
  styleUrls: ['./custom-page-list.component.scss']
})
export class CustomPageListComponent implements OnInit, OnDestroy {
  @ViewChild('pageGrid') pageGrid: AgGridAngular;
  @ViewChild('deletePageModal') deleteEntity: any;
  
  public richTextTypeID : number = CustomRichTextType.CustomPage;
  public rowData = [];
  public columnDefs: ColDef[];
  
  private gridApi: any;

  public modalReference: NgbModalRef;
  public customPageIDToRemove: number;
  public currentUser: UserDetailedDto;
  public isPerformingAction: boolean = false;
  public closeResult: string;
  public watchUserChangeSubscription: any;

  constructor(
    private alertService: AlertService,
    private cdr: ChangeDetectorRef, 
    private authenticationService: AuthenticationService,
    private customPageService: CustomPageService,
    private modalService: NgbModal) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.currentUser = currentUser;
      this.currentUser = currentUser;
      this.pageGrid.api.showLoadingOverlay();
      this.initializeGrid();
      this.customPageService.getAllCustomPagesWithRoles().subscribe(customPagesWithRoles => {
        this.rowData = customPagesWithRoles;
        this.cdr.detectChanges();
      });
      this.pageGrid.api.hideOverlay();
    });
  }

  initializeGrid() {
    this.columnDefs = [
      {
        width: 30,
        cellRendererFramework: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { isSpan: true, fontawesomeIconName: 'trash' }
      },
      {
        width: 30,
        valueGetter: function (params: any) {
          return params.data.CustomPageVanityUrl;
        },
        cellRendererFramework: FontAwesomeIconLinkRendererComponent,
        cellRendererParams: { 
          inRouterLink: "/custom-pages/edit-properties/",
          fontawesomeIconName: "edit"
        }
      },
      {
        headerName: 'Page Name', valueGetter: function (params: any) {
          return { LinkValue: params.data.CustomPageVanityUrl, LinkDisplay: params.data.CustomPageDisplayName };
        },
        cellRendererFramework: LinkRendererComponent,
        cellRendererParams: { inRouterLink: "/custom-pages/" },
        comparator: function (id1: any, id2: any) {
          let link1 = id1.LinkDisplay;
          let link2 = id2.LinkDisplay;
          if (link1 < link2) {
            return -1;
          }
          if (link1 > link2) {
            return 1;
          }
          return 0;
        },
        filter: true,
        filterValueGetter: function (params: any) {
          return params.data.CustomPageDisplayName;
        },
        sortable: true
      },
      { headerName: 'Menu', field: 'MenuItem.MenuItemDisplayName',
        filterFramework: CustomDropdownFilterComponent,
        filterParams: {
          field: 'MenuItem.MenuItemDisplayName'
        },
        sortable: true
      },
      { headerName: 'Has Content', field: 'IsEmptyContent', valueGetter: function (params) {
        return params.data.IsEmptyContent ? 'No' : 'Yes'; },
        filterFramework: CustomDropdownFilterComponent,
        filterParams: {
          field: 'IsEmptyContent'
        },
        sortable: true
      },
      { 
        headerName: 'Viewable By', field: 'ViewableRoles', valueGetter: function (params) {
        return params.data.ViewableRoles
              .map(x => x.RoleDisplayName)
              .join(', ');
        },
        filterFramework: CustomDropdownFilterComponent,
        filterParams: {
          field: 'RoleDisplayName'
        },
        sortable: true
      }
    ]; 

    this.columnDefs.forEach(x => {
      x.resizable = true;
    });
  }

  public onFirstDataRendered(params): void {
    this.gridApi = params.api;
    this.gridApi.sizeColumnsToFit();
    this.updateGridData();
  }

  public updateGridData(): void {
    this.customPageService.getAllCustomPagesWithRoles().subscribe(customPagesWithRoles => {
      this.rowData = customPagesWithRoles;
      this.cdr.detectChanges();
    });
    this.pageGrid.api.hideOverlay();
  }

  public onCellClicked(event: any): void {
    if (event.column.colId == "0") {
      this.customPageIDToRemove = event.data.CustomPageID;
      this.launchModal(this.deleteEntity, 'deleteAnnouncementEntity')
    }
  }

  public launchModal(modalContent: any, modalTitle: string): void {
    this.modalReference = this.modalService.open(modalContent, { ariaLabelledBy: modalTitle, beforeDismiss: () => this.checkIfSubmitting(), backdrop: 'static', keyboard: false });
    this.modalReference.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed`;
    });
  }
  
  public checkIfSubmitting(): boolean {
    return !this.isPerformingAction;
  }

  public removePageByID(): void {
    this.isPerformingAction = true;
    this.customPageService.deleteCustomPageByID(this.customPageIDToRemove).subscribe(() => {
      this.modalReference.close();
      this.isPerformingAction = false;
      this.alertService.pushAlert(new Alert(`Custom page successfully deleted`, AlertContext.Success, true));
      this.updateGridData();
    }, error => {
      this.modalReference.close();
      this.isPerformingAction = false;
      this.alertService.pushAlert(new Alert(`There was an error deleting the page. Please try again`, AlertContext.Danger, true));
    })
  }

  ngOnDestroy(): void {
    this.authenticationService.dispose();
    this.cdr.detach();
  }
}
