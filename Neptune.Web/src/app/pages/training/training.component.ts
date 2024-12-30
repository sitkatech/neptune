import { Component, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { CustomRichTextComponent } from '../../shared/components/custom-rich-text/custom-rich-text.component';
import { AlertDisplayComponent } from '../../shared/components/alert-display/alert-display.component';

@Component({
    selector: 'hippocamp-training',
    templateUrl: './training.component.html',
    styleUrls: ['./training.component.scss'],
    standalone: true,
    imports: [AlertDisplayComponent, CustomRichTextComponent]
})
export class TrainingComponent implements OnInit {

  constructor() { }

  public richTextTypeID : number = NeptunePageTypeEnum.HippocampTraining;

  ngOnInit() {
  }

}
