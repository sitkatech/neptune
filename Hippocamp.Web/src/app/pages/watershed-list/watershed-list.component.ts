import { Component, OnInit, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { UserDetailedDto } from 'src/app/shared/models';
import { WatershedService } from 'src/app/services/watershed/watershed.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DecimalPipe } from '@angular/common';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { forkJoin } from 'rxjs';
import { AgGridAngular } from 'ag-grid-angular';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { FieldDefinitionGridHeaderComponent } from 'src/app/shared/components/field-definition-grid-header/field-definition-grid-header.component';

@Component({
  selector: 'hippocamp-watershed-list',
  templateUrl: './watershed-list.component.html',
  styleUrls: ['./watershed-list.component.scss']
})
export class WatershedListComponent implements OnInit, OnDestroy {
  @ViewChild('watershedsGrid') watershedsGrid: AgGridAngular;

  private watchUserChangeSubscription: any;
  private currentUser: UserDetailedDto;

  public richTextTypeID : number = CustomRichTextType.WatershedList;
  public gridOptions: GridOptions;
  public watersheds = [];
  columnDefs: any;

  constructor(private cdr: ChangeDetectorRef,
    private authenticationService: AuthenticationService,
    private utilityFunctionsService: UtilityFunctionsService,
    private watershedService: WatershedService,
    private decimalPipe: DecimalPipe) { }

  ngOnInit() {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.gridOptions = <GridOptions>{};
      this.currentUser = currentUser;
      this.watershedsGrid.api.showLoadingOverlay();
      this.watershedService.getWatersheds().subscribe(watersheds => {
        this.watersheds = watersheds;
        this.watershedsGrid.api.hideOverlay();
        this.cdr.detectChanges();
      });

      let _decimalPipe = this.decimalPipe;
      this.columnDefs = [
        { valueGetter: function (params: any) {
          return { LinkDisplay: params.data.WatershedName, LinkValue: params.data.WatershedID };
        }, cellRendererFramework: LinkRendererComponent,
        cellRendererParams: { inRouterLink: "/watersheds/" },
        filterValueGetter: function (params: any) {
          return params.data.WatershedName;
        },
        comparator: function (id1: any, id2: any) {
          if (id1.LinkDisplay < id2.LinkDisplay) {
            return -1;
          }
          if (id1.LinkDisplay > id2.LinkDisplay) {
            return 1;
          }
          return 0;
        }, headerComponentFramework: FieldDefinitionGridHeaderComponent, headerName: 'Name', headerComponentParams: {fieldDefinitionType: 'Name'}, sortable: true, filter: true, width: 300 },
      ];

      this.columnDefs.forEach(x => {
        x.resizable = true;
      });
    });
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  public updateGridData() {
    this.watershedService.getWatersheds().subscribe(result => {
      this.watershedsGrid.api.setRowData(result);
    });
  }

  public getSelectedWatershedIDs(): Array<number> {
    return this.watersheds !== undefined ? this.watersheds.map(m => m.WatershedID) : [];
  }
}
