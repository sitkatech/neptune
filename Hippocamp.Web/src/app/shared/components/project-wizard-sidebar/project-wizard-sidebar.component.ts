import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { forkJoin, Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DelineationService } from 'src/app/services/delineation.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { UserService } from 'src/app/services/user/user.service';
import { DelineationUpsertDto } from '../../generated/model/delineation-upsert-dto';
import { TreatmentBMPModeledResultSimpleDto } from '../../generated/model/models';
import { PersonDto } from '../../generated/model/person-dto';
import { ProjectCreateDto } from '../../generated/model/project-create-dto';
import { TreatmentBMPUpsertDto } from '../../generated/model/treatment-bmp-upsert-dto';

@Component({
  selector: 'hippocamp-project-wizard-sidebar',
  templateUrl: './project-wizard-sidebar.component.html',
  styleUrls: ['./project-wizard-sidebar.component.scss']
})
export class ProjectWizardSidebarComponent implements OnInit, OnChanges {
  @Input() projectModel : ProjectCreateDto;

  private currentUser: PersonDto;
  
  public activeAccordionIds: string[] = [];
  private _routeListener: Subscription;

  public projectID: number;
  public treatmentBMPs: TreatmentBMPUpsertDto[];
  public delineations: DelineationUpsertDto[];
  public modeledResults: TreatmentBMPModeledResultSimpleDto[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private treatmentBMPService: TreatmentBMPService,
    private delineationService: DelineationService,
    private projectService: ProjectService,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.cdr.detectChanges();
  }

  ngOnInit() {
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      this.checkForProjectIDInRouteAndGetEntitiesIfPresent()
      this._routeListener = this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe((e: NavigationEnd) => {
        this.checkForProjectIDInRouteAndGetEntitiesIfPresent();
      });
    })

    let path = this.router.url;
    if (path.includes('stormwater-treatments')) {
      this.activeAccordionIds = [ "stormwater-treatments" ];
    } 
  }

  checkForProjectIDInRouteAndGetEntitiesIfPresent() {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);
      this.getProjectRelatedEntities();
    }
  }

  getProjectRelatedEntities() {
    forkJoin({
      treatmentBMPs: this.treatmentBMPService.getTreatmentBMPsByProjectID(this.projectID),
      delineations: this.delineationService.getDelineationsByProjectID(this.projectID),
      modeledResults:  this.projectService.getModeledResultsForProject(this.projectID)
    }).subscribe(({ treatmentBMPs, delineations, modeledResults }) => {
      this.treatmentBMPs = treatmentBMPs;
      this.delineations = delineations;
      this.modeledResults = modeledResults;
    });
  }

  doAllTreatmentBMPsHaveCalculatedModelResults() : boolean {
    if (this.treatmentBMPs == null || this.treatmentBMPs == undefined || this.treatmentBMPs.length == 0 ||
        this.modeledResults == null || this.modeledResults == undefined || this.modeledResults.length == 0 ||
        this.treatmentBMPs.length != this.modeledResults.length) {
          return false;
        }

    return this.treatmentBMPs.every(x => this.modeledResults.some(y => y.TreatmentBMPID == x.TreatmentBMPID));
  }

}
