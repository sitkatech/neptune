import { Component, Input, OnInit } from '@angular/core';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';

@Component({
  selector: 'hippocamp-grant-scores',
  templateUrl: './grant-scores.component.html',
  styleUrls: ['./grant-scores.component.scss']
})
export class GrantScoresComponent implements OnInit {
  @Input('projectID') projectID: number;

  public project: ProjectSimpleDto;

  constructor(
  ) { }

  ngOnInit(): void {
  }
}
