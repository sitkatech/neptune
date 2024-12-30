import { Component, OnInit } from '@angular/core';
import { AlertDisplayComponent } from '../../components/alert-display/alert-display.component';

@Component({
    selector: 'not-found',
    templateUrl: './not-found.component.html',
    styleUrls: ['./not-found.component.scss'],
    standalone: true,
    imports: [AlertDisplayComponent]
})
export class NotFoundComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
