import { Component, Input, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectGrantScoreDto } from 'src/app/shared/generated/model/project-grant-score-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-grant-scores',
  templateUrl: './grant-scores.component.html',
  styleUrls: ['./grant-scores.component.scss']
})
export class GrantScoresComponent implements OnInit {
  @Input('projectID') projectID: number;

  public projectGrantScore: ProjectGrantScoreDto;
  public OCTAM2Tier2RichTextTypeID = CustomRichTextType.OCTAM2Tier2GrantProgramMetrics

  constructor(
    private projectService: ProjectService,
  ) { }

  ngOnInit(): void {
    if (this.projectID) {
      forkJoin({
        projectGrantScore: this.projectService.getGrantScoreByProjectID(this.projectID)
      })
        .subscribe(({ projectGrantScore }) => {
          this.projectGrantScore = projectGrantScore;
        });
    }
  }
}
