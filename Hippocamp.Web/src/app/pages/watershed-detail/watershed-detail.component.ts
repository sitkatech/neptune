import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { WatershedService } from 'src/app/services/watershed/watershed.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { forkJoin } from 'rxjs';
import { UserDetailedDto } from 'src/app/shared/models';
import { WatershedDto } from 'src/app/shared/generated/model/models';

@Component({
  selector: 'template-watershed-detail',
  templateUrl: './watershed-detail.component.html',
  styleUrls: ['./watershed-detail.component.scss']
})
export class WatershedDetailComponent implements OnInit, OnDestroy {
  private watchUserChangeSubscription: any;
  private currentUser: UserDetailedDto;

  public watershed: WatershedDto;

  public today: Date = new Date();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private watershedService: WatershedService,
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef
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
          this.watershedService.getWatershedByWatershedID(id),
        ).subscribe(([watershed]) => {
          this.watershed = watershed instanceof Array
            ? null
            : watershed as WatershedDto;
        });
      }
    });
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  public getSelectedWatershedIDs(): Array<number> {
    return this.watershed !== undefined ? [this.watershed.WatershedID] : [];
  }
}