import { ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'hippocamp-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.scss']
})

/**
 * Trying to encapsulate the icons we use on the website in a single component
 * so that if we decide to change libraries or update fontawesome to a new version
 * we only have to update this single component.
 */
export class IconComponent implements OnInit, OnChanges {
  @Input() icon: string;
  @Input() titleText: string = "";
  @Input() additionalClasses: string = "";
  @Input() isLoadingSubmit : boolean = false;

  public iconClass: string;
  public iconFound: boolean = true;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.init();
  }

  ngOnInit(): void {
    this.init();
  }

  init() : void {
    switch(this.icon) {
      case 'check': {
        this.iconClass = "fas fa-check";
        break;
      }
      case 'cross': {
        this.iconClass = "fas fa-times";
        break;
      }
      case 'people-arrows': {
        this.iconClass = "fas fa-people-arrows";
        break;
      }
      case 'user': {
        this.iconClass = "fas fa-user";
        break;
      }
      case 'calendar-alt': {
        this.iconClass = "far fa-calendar-alt";
        break;
      }
      case 'calendar': {
        this.iconClass = "far fa-calendar";
        break;
      }
      case 'file': {
        this.iconClass = "fas fa-folder-open";
        break;
      }
      case 'spinner' : {
        this.iconClass = "fas fa-spinner loading-spinner";
        break;
      }
      case 'sort-up' : {
        this.iconClass = "fas fa-sort-up";
        break;
      }
      case 'sort-down' : {
        this.iconClass = "fas fa-sort-down";
        break;
      }
      case 'chevron-up' : {
        this.iconClass = "fas fa-chevron-up";
        break;
      }
      case 'chevron-down' : {
        this.iconClass = "fas fa-chevron-down";
        break;
      }
      case 'chevron-right' : {
        this.iconClass = "fas fa-chevron-right";
        break;
      }
      case 'grip-horizontal' : {
        this.iconClass = "fas fa-grip-horizontal";
        break;
      }
      case 'edit' : {
        this.iconClass = "fas fa-pencil-alt";
        break;
      }
      case 'poll' : {
        this.iconClass = "fas fa-poll-h";
        break;
      }
      case 'certificate' : {
        this.iconClass = "fas fa-certificate";
        break;
      }
      case 'trash' : {
        this.iconClass = "fas fa-trash";
        break;
      }
      case 'delete' : {
        this.iconClass = "fas fa-trash";
        break;
      }
      case 'star-open' : {
        this.iconClass = "far fa-star";
        break;
      }
      case 'star-solid' : {
        this.iconClass = "fas fa-star";
        break;
      }
      case 'pdf' : {
        this.iconClass = "fas fa-file-pdf";
        break;
      }
      case 'renew' : {
        this.iconClass = "fas fa-recycle";
        break;
      }
      case 'download' : {
        this.iconClass = "fas fa-download";
        break;
      }
      case 'info' : {
        this.iconClass = "fas fa-info";
        break;
      }
      default: {
          this.iconFound = false;
          break;
      }
    }

    if(this.additionalClasses) {
      this.iconClass += ' ' + this.additionalClasses;
    }
  }

}
