import { Component, Input, OnInit } from '@angular/core';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-grant-scores',
  templateUrl: './grant-scores.component.html',
  styleUrls: ['./grant-scores.component.scss']
})
export class GrantScoresComponent implements OnInit {
  @Input('project') project: ProjectSimpleDto;
  public OCTAM2Tier2RichTextTypeID = CustomRichTextType.OCTAM2Tier2GrantProgramMetrics

  constructor(
  ) { }

  ngOnInit(): void {
  }
}
