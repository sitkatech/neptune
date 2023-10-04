import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

@Component({
  selector: 'hippocamp-octa-prioritization-detail-popup',
  templateUrl: './octa-prioritization-detail-popup.component.html',
  styleUrls: ['./octa-prioritization-detail-popup.component.scss']
})
export class OctaPrioritizationDetailPopupComponent implements OnInit {
  constructor(private cdr: ChangeDetectorRef) { }

  public feature : any;
  
  ngOnInit() {
  }

  public detectChanges() : void{
    this.cdr.detectChanges();
  } 

}
