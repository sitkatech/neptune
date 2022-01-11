import { Component, OnInit, DoCheck, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'hippocamp-watershed-detail-popup',
  templateUrl: './watershed-detail-popup.component.html',
  styleUrls: ['./watershed-detail-popup.component.scss']
})
export class WatershedDetailPopupComponent implements OnInit {
  constructor(private cdr: ChangeDetectorRef) { }

  public feature : any;
  
  ngOnInit() {
  }

  public detectChanges() : void{
    this.cdr.detectChanges();
  } 
}
