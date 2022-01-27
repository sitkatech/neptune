import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user/user.service';
import { PersonDto } from '../../generated/model/person-dto';
import { ProjectCreateDto } from '../../generated/model/project-create-dto';

@Component({
  selector: 'hippocamp-project-new-sidebar',
  templateUrl: './project-new-sidebar.component.html',
  styleUrls: ['./project-new-sidebar.component.scss']
})
export class ProjectNewSidebarComponent implements OnInit, OnChanges {
  @Input() projectModel : ProjectCreateDto;

  private currentUser: PersonDto;
  
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
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser
    })

    let path = this.router.url;
    if (path.includes('stormwater-treatments')) {
      this.activeAccordionIds = [ "stormwater-treatments" ];
    } 

  }

}
