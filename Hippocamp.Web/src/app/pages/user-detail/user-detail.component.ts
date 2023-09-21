import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { forkJoin } from 'rxjs';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';

@Component({
    selector: 'template-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserDetailComponent implements OnInit, OnDestroy {
    
    private currentUser: PersonDto;

    public user: PersonDto;

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
        this.authenticationService.getCurrentUser().subscribe(currentUser => {
            this.currentUser = currentUser;
            const id = parseInt(this.route.snapshot.paramMap.get("id"));
            if (id) {
                forkJoin(
                    this.userService.getUserFromUserID(id),
                ).subscribe(([user]) => {
                    this.user = user instanceof Array
                        ? null
                        : user as PersonDto;
                    this.cdr.detectChanges();
                });
            }
        });
    }

    ngOnDestroy() {
        
        
        this.cdr.detach();
    }

    public currentUserIsAdmin(): boolean {
        return this.authenticationService.isUserAnAdministrator(this.currentUser);
    }

    public userIsAdministrator(): boolean{
        return this.authenticationService.isUserAnAdministrator(this.user);
    }
}
