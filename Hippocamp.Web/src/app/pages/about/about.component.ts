import { Component, OnInit } from '@angular/core';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';

@Component({
  selector: 'hippocamp-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

  constructor() { }

  public richTextTypeID : number = NeptunePageTypeEnum.HippocampAbout;
  
  ngOnInit(): void {
  }

}
