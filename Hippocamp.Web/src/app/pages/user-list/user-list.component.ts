import { Component, OnInit, ChangeDetectorRef, OnDestroy, ViewChild } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';
import { UserDetailedDto } from 'src/app/shared/models';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from 'src/app/shared/components/ag-grid/link-renderer/link-renderer.component';
import { DatePipe, DecimalPipe } from '@angular/common';
import { AgGridAngular } from 'ag-grid-angular';
import { UtilityFunctionsService } from 'src/app/services/utility-functions.service';
import { RoleEnum } from 'src/app/shared/models/enums/role.enum';
import { CustomPinnedRowRendererComponent } from 'src/app/shared/components/ag-grid/custom-pinned-row-renderer/custom-pinned-row-renderer.component';
import { CustomDropdownFilterComponent } from 'src/app/shared/components/custom-dropdown-filter/custom-dropdown-filter.component';

declare var $:any;

@Component({
  selector: 'hippocamp-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit, OnDestroy {
  @ViewChild('usersGrid') usersGrid: AgGridAngular;
  @ViewChild('unassignedUsersGrid') unassignedUsersGrid: AgGridAngular;

  private watchUserChangeSubscription: any;
  private currentUser: UserDetailedDto;

  public rowData = [];
  columnDefs: ColDef[];
  columnDefsUnassigned: ColDef[];
  defaultColDef;
  users: UserDetailedDto[];
  unassignedUsers: UserDetailedDto[];
  pinnedBottomRowData;
  frameworkComponents: any;

  constructor(private cdr: ChangeDetectorRef, private authenticationService: AuthenticationService, private utilityFunctionsService: UtilityFunctionsService, private userService: UserService, private decimalPipe: DecimalPipe) { }

  ngOnInit() {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.currentUser = currentUser;
      this.usersGrid.api.showLoadingOverlay();
      let _decimalPipe = this.decimalPipe;
      let datePipe = new DatePipe('en-US');

      this.columnDefs = [
        {
          headerName: 'Name', valueGetter: function (params: any) {
            return { LinkValue: params.data.UserID, LinkDisplay: params.data.FullName };
          }, cellRendererFramework: LinkRendererComponent,
          cellRendererParams: { inRouterLink: "/users/" },
          filterValueGetter: function (params: any) {
            return params.node.rowPinned ? null : params.data.FullName;
          },
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
          sortable: true, filter: true, width: 170
        },
        { headerName: 'Email', field: 'Email', sortable: true, filter: true },
        { 
          headerName: 'Role', field: 'Role.RoleDisplayName', sortable: true, width: 100,
          filterFramework: CustomDropdownFilterComponent,
          filterParams: {
            field: 'Role.RoleDisplayName'
          }
        },
        { 
          headerName: 'Receives System Communications?', field: 'ReceiveSupportEmails', 
          valueGetter: function (params) { 
            if (params.node != null && params.node.rowPinned) {
              return null;
            }
            return params.data.ReceiveSupportEmails ? "Yes" : "No";
          }, 
          filterFramework: CustomDropdownFilterComponent,
          filterParams: {
            field: 'ReceiveSupportEmails'
          }, 
          sortable: true, width: 250
        },
        this.createDateColumnDef('Create Date', 'CreateDate', 'M/d/yyyy'),
        { 
          headerName: 'User ID', valueGetter: function (params: any) {
          return params.node.rowPinned ? "Total: " + params.data.UserIDTotal : params.data.UserID;
          },
          pinnedRowCellRendererFramework: CustomPinnedRowRendererComponent,
          pinnedRowCellRendererParams: { filter: true },
          filter: 'agNumberColumnFilter',
          sortable: true
        }
      ];
    
      this.defaultColDef = { 
        resizable : true,
      };
    });
  }
  
  private dateFilterComparator(filterLocalDateAtMidnight, cellValue) {
    const cellDate = Date.parse(cellValue);
    if (cellDate == filterLocalDateAtMidnight) {
      return 0;
    }
    return (cellDate < filterLocalDateAtMidnight) ? -1 : 1;
  }
  
  private createDateColumnDef(headerName: string, fieldName: string, dateFormat: string): ColDef {
    let datePipe = new DatePipe('en-US');
  
    return {
      headerName: headerName, valueGetter: function (params: any) {
        return datePipe.transform(params.data[fieldName], dateFormat);
      },
      comparator: this.dateFilterComparator,
      filter: 'agDateColumnFilter',
      filterParams: {
        filterOptions: ['inRange'],
        comparator: this.dateFilterComparator
      },
      resizable: true,
      sortable: true
    };
  }

  onUsersGridReady(gridEvent) {
    this.userService.getUsers().subscribe(users => {
      this.rowData = users;
      this.usersGrid.api.hideOverlay();
      this.users = users;
      
      this.unassignedUsers = users.filter(u =>{ return u.Role.RoleID === RoleEnum.Unassigned});

      this.pinnedBottomRowData = [
        { 
          UserIDTotal: this.rowData.map(x => x.UserID).reduce((sum, x) => sum+x, 0)
        }
      ];
  
      this.usersGrid.api.sizeColumnsToFit();
    });
  }

  onFilterChanged(gridEvent) {

    gridEvent.api.setPinnedBottomRowData([
      {
        UserIDTotal: gridEvent.api.getModel().rowsToDisplay.map(x=>x.data.UserID).reduce((sum, x) => sum+x, 0)
      }
    ]);
  }

  private refreshView(){
    this.unassignedUsersGrid.api.refreshView();
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }
}

