import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-project-instructions',
  templateUrl: './project-instructions.component.html',
  styleUrls: ['./project-instructions.component.scss']
})
export class ProjectInstructionsComponent implements OnInit {

  public customRichTextTypeID : number = CustomRichTextType.ProjectInstructions;
  public projectID: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const projectID = this.route.snapshot.paramMap.get("projectID");
    if (projectID) {
      this.projectID = parseInt(projectID);
    }
  }

  continueToNextStep() {
    if (this.projectID) {
      this.router.navigateByUrl(`/projects/edit/${this.projectID}/project-basics`)
    } else {
      this.router.navigateByUrl(`/projects/new/project-basics`)
    }
  }

}
