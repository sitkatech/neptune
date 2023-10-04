import { Component, Input } from '@angular/core';

@Component({
  selector: 'hippocamp-toggle-status',
  templateUrl: './toggle-status.component.html',
  styleUrls: ['./toggle-status.component.scss']
})
export class ToggleStatusComponent {
  @Input() opened: boolean = false;

}
