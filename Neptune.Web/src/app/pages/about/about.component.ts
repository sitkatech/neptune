import { Component, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { CustomRichTextComponent } from '../../shared/components/custom-rich-text/custom-rich-text.component';
import { AlertDisplayComponent } from '../../shared/components/alert-display/alert-display.component';

@Component({
    selector: 'hippocamp-about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.scss'],
    standalone: true,
    imports: [AlertDisplayComponent, CustomRichTextComponent]
})
export class AboutComponent implements OnInit {

  constructor() { }

  public richTextTypeID : number = NeptunePageTypeEnum.HippocampAbout;
  
  ngOnInit(): void {
  }

}
