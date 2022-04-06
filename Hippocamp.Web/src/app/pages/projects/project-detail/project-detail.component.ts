import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';

@Component({
  selector: 'hippocamp-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})
export class ProjectDetailComponent implements OnInit {

  private projectID: number;
  public project: ProjectSimpleDto;

  constructor(
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(result => {
      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectID = parseInt(projectID);
        this.projectService.getByID(this.projectID).subscribe(project => {
          this.project = project;
        });
      }
    });
  }

}
