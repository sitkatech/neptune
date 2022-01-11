import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { CustomPageService } from 'src/app/services/custom-page.service';
import { MenuItemService } from 'src/app/services/menu-item.service';
import { RoleService } from 'src/app/services/role/role.service';
import { MenuItemDto, RoleDto } from 'src/app/shared/generated/model/models';
import { Alert } from 'src/app/shared/models/alert';
import { CustomPageUpsertDto } from 'src/app/shared/models/custom-page-upsert-dto';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { RoleEnum } from 'src/app/shared/models/enums/role.enum';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'hippocamp-custom-page-create',
  templateUrl: './custom-page-create.component.html',
  styleUrls: ['./custom-page-create.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class CustomPageCreateComponent implements OnInit, OnDestroy {
    private watchUserChangeSubscription: any;

    public menuItems: Array<MenuItemDto>;
    public roles: Array<RoleDto>;
    public model: CustomPageUpsertDto;
    
    public isLoadingSubmit: boolean = false;

    constructor(
        private cdr: ChangeDetectorRef,
        private router: Router, 
        private customPageService: CustomPageService,
        private menuItemService: MenuItemService,
        private roleService: RoleService,
        private authenticationService: AuthenticationService, 
        private alertService: AlertService
    ) { }

    ngOnInit(): void {
        this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(() => {
            this.menuItemService.getMenuItems().subscribe(result => {
              this.menuItems = result;
              this.cdr.detectChanges();
            });
            this.roleService.getRoles().subscribe(roles => {
                // remove admin from role picker as admins default to viewable for all custom pages
                // and remove disabled users as well since they should not have viewable rights by default
                this.roles = roles.filter(role => 
                    role.RoleID !== RoleEnum.Admin &&
                    role.RoleID !== RoleEnum.Disabled);
                this.cdr.detectChanges();
            });
            this.model = new CustomPageUpsertDto();
            this.model.ViewableRoleIDs = [];

            this.cdr.detectChanges();
        });
    }

    ngOnDestroy() {
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

    onSubmit(createNewCustomPageForm: HTMLFormElement): void {
        this.isLoadingSubmit = true;

        this.customPageService.createNewCustomPage(this.model)
            .subscribe(response => {
                this.isLoadingSubmit = false;
                createNewCustomPageForm.reset();
                this.router.navigateByUrl(`/custom-pages/${response.CustomPageVanityUrl}`).then(() => {
                    this.alertService.pushAlert(new Alert("The custom page was successfully created.", AlertContext.Success));
                });
            },
            error => {
                this.isLoadingSubmit = false;
                this.cdr.detectChanges();
            });
    }
}