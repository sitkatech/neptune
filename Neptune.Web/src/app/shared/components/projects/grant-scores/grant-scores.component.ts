import { Component, Input, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';

@Component({
  selector: 'hippocamp-grant-scores',
  templateUrl: './grant-scores.component.html',
  styleUrls: ['./grant-scores.component.scss']
})
export class GrantScoresComponent implements OnInit {
  @Input('project') project: ProjectSimpleDto;
  public OCTAM2Tier2RichTextTypeID = NeptunePageTypeEnum.OCTAM2Tier2GrantProgramMetrics

  constructor(
  ) { }

  ngOnInit(): void {
  }
}
