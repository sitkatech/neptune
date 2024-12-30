import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { CustomRichTextComponent } from '../../../../shared/components/custom-rich-text/custom-rich-text.component';

@Component({
    selector: 'hippocamp-project-instructions',
    templateUrl: './project-instructions.component.html',
    styleUrls: ['./project-instructions.component.scss'],
    standalone: true,
    imports: [CustomRichTextComponent]
})
export class ProjectInstructionsComponent implements OnInit {

  public customRichTextTypeID : number = NeptunePageTypeEnum.HippocampProjectInstructions;
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
