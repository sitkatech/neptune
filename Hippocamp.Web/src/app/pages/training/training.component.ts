import { Component, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';

@Component({
  selector: 'hippocamp-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.scss']
})
export class TrainingComponent implements OnInit {

  constructor() { }

  public richTextTypeID : number = NeptunePageTypeEnum.HippocampTraining;

  ngOnInit() {
  }

}
