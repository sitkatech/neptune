import { Component, OnInit } from '@angular/core';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.scss']
})
export class TrainingComponent implements OnInit {

  constructor() { }

  public richTextTypeID : number = CustomRichTextType.Training;

  ngOnInit() {
  }

}
