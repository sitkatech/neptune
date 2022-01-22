import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { OrganizationService } from 'src/app/services/organization/organization.service';
import { OrganizationSimpleDto } from 'src/app/shared/generated/model/organization-simple-dto';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectCreateDto } from 'src/app/shared/generated/model/project-create-dto';

@Component({
  selector: 'hippocamp-project-new',
  templateUrl: './project-new.component.html',
  styleUrls: ['./project-new.component.scss']
})
export class ProjectNewComponent implements OnInit {

  private watchUserChangeSubscription: any;
  public currentUser: PersonDto;
  
  public projectModel: ProjectCreateDto;
  public organizations: Array<OrganizationSimpleDto>;
  public isLoadingSubmit = false;

  constructor(
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef,
    private organizationService: OrganizationService
  ) { }

  ngOnInit(): void {
    this.watchUserChangeSubscription = this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      this.projectModel = new ProjectCreateDto();
      this.projectModel.PrimaryContactPersonID = this.currentUser.PersonID;

      this.organizationService.getAllOrganizations().subscribe(organizations => {
        this.organizations = organizations;
      });

      this.cdr.detectChanges();
    });
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }
}
