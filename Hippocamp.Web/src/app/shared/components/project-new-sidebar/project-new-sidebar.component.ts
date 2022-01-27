import { ChangeDetectorRef, Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user/user.service';
import { PersonDto } from '../../generated/model/person-dto';

@Component({
  selector: 'hippocamp-project-new-sidebar',
  templateUrl: './project-new-sidebar.component.html',
  styleUrls: ['./project-new-sidebar.component.scss']
})
export class ProjectNewSidebarComponent implements OnInit, OnChanges {

  private currentUser: PersonDto;

  public activeNavSection: string;

  public activeAccordionIds: string[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private userService: UserService,
    private authenticationService: AuthenticationService,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.cdr.detectChanges();
  }

  ngOnInit() {
    this.activeNavSection = this.getActiveNavSection();

    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser
    })

    let path = this.router.url;
    if (path.includes('stormwater-treatments')) {
      this.activeAccordionIds = [ "stormwater-treatments" ];
    } 

  }

  getActiveNavSection(): string {
    var routerUrl = this.router.url;
    if(routerUrl.includes('section-a')) {
      return "sectionA";
    } else if(routerUrl.includes('section-b')) {
      return "sectionB";
    } else if(routerUrl.includes('section-c')){
      return "sectionC";
    } else {
      return "sectionA";
    }
  }

}
