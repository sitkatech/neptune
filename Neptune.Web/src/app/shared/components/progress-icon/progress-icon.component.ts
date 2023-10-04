import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'hippocamp-progress-icon',
  templateUrl: './progress-icon.component.html',
  styleUrls: ['./progress-icon.component.scss']
})
export class ProgressIconComponent implements OnInit {
  @Input() isComplete: boolean = false;
  @Input() isInformational: boolean = false;

  public tooltipCompleteText: string = 'Completed Step';
  public tooltipIncompleteText: string = 'Incomplete Step';

  constructor() { }

  ngOnInit(): void {
  }

}
