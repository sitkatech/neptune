import { Component, Input, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { CustomRichTextComponent } from '../../custom-rich-text/custom-rich-text.component';
import { FieldDefinitionComponent } from '../../field-definition/field-definition.component';
import { NgbNav } from '@ng-bootstrap/ng-bootstrap';
import { NgIf, DecimalPipe, PercentPipe } from '@angular/common';

@Component({
    selector: 'hippocamp-grant-scores',
    templateUrl: './grant-scores.component.html',
    styleUrls: ['./grant-scores.component.scss'],
    standalone: true,
    imports: [NgIf, NgbNav, FieldDefinitionComponent, CustomRichTextComponent, DecimalPipe, PercentPipe]
})
export class GrantScoresComponent implements OnInit {
  @Input('project') project: ProjectSimpleDto;
  public OCTAM2Tier2RichTextTypeID = NeptunePageTypeEnum.OCTAM2Tier2GrantProgramMetrics

  constructor(
  ) { }

  ngOnInit(): void {
  }
}
