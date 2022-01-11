import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { UserDetailedDto } from 'src/app/shared/models';
import { UserService } from 'src/app/services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { forkJoin } from 'rxjs';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';

@Component({
    selector: 'template-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserDetailComponent implements OnInit, OnDestroy {
    private watchUserChangeSubscription: any;
    private currentUser: UserDetailedDto;

    public user: UserDetailedDto;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private authenticationService: AuthenticationService,
        private cdr: ChangeDetectorRef,
        private alertService: AlertService
    ) {
        // force route reload whenever params change;
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }

    ngOnInit() {
        this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
            this.currentUser = currentUser;
            const id = parseInt(this.route.snapshot.paramMap.get("id"));
            if (id) {
                forkJoin(
                    this.userService.getUserFromUserID(id),
                ).subscribe(([user]) => {
                    this.user = user instanceof Array
                        ? null
                        : user as UserDetailedDto;
                    this.cdr.detectChanges();
                });
            }
        });
    }

    impersonateUser(userID: number) {
        this.userService.impersonateUser(userID).subscribe(response => {
            this.currentUser = response;
            this.cdr.detectChanges();
            this.router.navigateByUrl("/").then(x => {
                this.alertService.pushAlert(new Alert(`Successfully impersonating user: ${this.currentUser.FullName}.`, AlertContext.Success));
            });
        })
    }

    ngOnDestroy() {
        this.watchUserChangeSubscription.unsubscribe();
        this.authenticationService.dispose();
        this.cdr.detach();
    }

    public currentUserIsAdmin(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public userIsAdministrator(): boolean{
        return this.authenticationService.isUserAnAdministrator(this.user);
    }
}
