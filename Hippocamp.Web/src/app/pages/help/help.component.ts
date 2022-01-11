import { Component, OnInit } from '@angular/core';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss']
})
export class HelpComponent implements OnInit {

  public richTextTypeID : number = CustomRichTextType.Help;

  constructor() { }

  ngOnInit() {
  }

}
