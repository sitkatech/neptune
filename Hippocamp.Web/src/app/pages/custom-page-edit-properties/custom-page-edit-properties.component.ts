import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CustomPageService } from 'src/app/services/custom-page.service';
import { MenuItemService } from 'src/app/services/menu-item.service';
import { RoleService } from 'src/app/services/role/role.service';
import { CustomPageDto } from 'src/app/shared/generated/model/custom-page-dto';
import { MenuItemDto } from 'src/app/shared/generated/model/menu-item-dto';
import { RoleDto } from 'src/app/shared/generated/model/role-dto';
import { UserDetailedDto } from 'src/app/shared/models';
import { Alert } from 'src/app/shared/models/alert';
import { CustomPageUpsertDto } from 'src/app/shared/models/custom-page-upsert-dto';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { RoleEnum } from 'src/app/shared/models/enums/role.enum';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'hippocamp-custom-page-edit-properties',
  templateUrl: './custom-page-edit-properties.component.html',
  styleUrls: ['./custom-page-edit-properties.component.scss']
})
export class CustomPageEditPropertiesComponent implements OnInit, OnDestroy {
  private watchUserChangeSubscription: any;
  
  public currentUser: UserDetailedDto;
  public menuItems: Array<MenuItemDto>;
  public roles: Array<RoleDto>;
  public model: CustomPageUpsertDto;
  public customPage: CustomPageDto;
  
  public isLoading: boolean = true;
  public isLoadingSubmit: boolean = false;

  constructor(
      private cdr: ChangeDetectorRef,
      private customPageService: CustomPageService,
      private menuItemService: MenuItemService,
      private roleService: RoleService,
      private route: ActivatedRoute,     
      private router: Router, 
      private authenticationService: AuthenticationService, 
      private alertService: AlertService
    ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.currentUser = currentUser;
      this.model = new CustomPageUpsertDto();
      const vanityUrl = this.route.snapshot.paramMap.get("vanity-url");
      if (vanityUrl) {
        this.customPageService.getCustomPageRolesByVanityUrl(vanityUrl).subscribe(pageRoleDtos => {
          this.model.ViewableRoleIDs = pageRoleDtos.map(pageRole => pageRole.RoleID).sort();
        });
        this.customPageService.getCustomPageByVanityUrl(vanityUrl).subscribe(customPage => {
          this.customPage = customPage;
          this.model.CustomPageDisplayName = customPage.CustomPageDisplayName;
          this.model.CustomPageVanityUrl = customPage.CustomPageVanityUrl;
          this.model.CustomPageContent = customPage.CustomPageContent;
          this.model.MenuItemID = customPage.MenuItem.MenuItemID;
        });
        this.isLoading = false;
      }
  
      this.menuItemService.getMenuItems().subscribe(result => {
        this.menuItems = result;
      });
      
      this.roleService.getRoles().subscribe(roles => {
        // remove admin from role picker as admins default to viewable for all custom pages
        // and remove disabled users as well since they should not have viewable rights by default
        this.roles = roles.filter(role => 
          role.RoleID !== RoleEnum.Admin &&
          role.RoleID !== RoleEnum.Disabled);
      });
    });
  }

  ngOnDestroy(): void {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  slugifyPageName(event: any): void {
    const urlSlug = event?.toLowerCase().replace(/ /g, '-').replace(/[^\w-]+/g, '');
    this.model.CustomPageVanityUrl = urlSlug;
  }

  onViewableRolesChange(roleID: number): void {
    if (!this.model.ViewableRoleIDs.includes(roleID)) {
      this.model.ViewableRoleIDs.push(roleID);
    } else {
      this.model.ViewableRoleIDs = 
        this.model.ViewableRoleIDs.filter(x => x != roleID)
                                  .sort();
    }
  }

  validPageName(pageName: string): boolean {
    const pattern = /^[_A-Za-z0-9\-\s]{1,100}$/;
    return pattern.test(pageName);
  }

  validVanityUrl(vanityUrl: string): boolean {
    const pattern = /^[_A-Za-z0-9\-]{1,100}$/;
    return pattern.test(vanityUrl);
  }

  onSubmit(updateCustomPagePropertiesForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;

    this.customPageService.updateCustomPageByID(this.customPage.CustomPageID, this.model)
        .subscribe(response => {
            this.isLoadingSubmit = false;
            updateCustomPagePropertiesForm.reset();
            this.router.navigateByUrl(`/custom-pages`).then(() => {
                this.alertService.pushAlert(new Alert(`The custom page ${response.CustomPageDisplayName} was successfully updated.`, AlertContext.Success));
            });
        },
        error => {
            this.isLoadingSubmit = false;
            this.cdr.detectChanges();
        });
  }
}

