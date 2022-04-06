import { Component, OnInit } from '@angular/core';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';

@Component({
  selector: 'hippocamp-project-instructions',
  templateUrl: './project-instructions.component.html',
  styleUrls: ['./project-instructions.component.scss']
})
export class ProjectInstructionsComponent implements OnInit {

  public customRichTextTypeID : number = CustomRichTextType.ProjectInstructions;

  constructor() { }

  ngOnInit(): void {
  }

}
