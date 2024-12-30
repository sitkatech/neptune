import { Component, Input } from '@angular/core';
import { IconComponent } from '../icon/icon.component';
import { NgIf } from '@angular/common';

@Component({
    selector: 'hippocamp-toggle-status',
    templateUrl: './toggle-status.component.html',
    styleUrls: ['./toggle-status.component.scss'],
    standalone: true,
    imports: [NgIf, IconComponent]
})
export class ToggleStatusComponent {
  @Input() opened: boolean = false;

}
