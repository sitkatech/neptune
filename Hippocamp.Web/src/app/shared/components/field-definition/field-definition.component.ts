import { Component, OnInit, Input, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { Alert } from '../../models/alert';
import { FieldDefinitionService } from '../../services/field-definition-service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AlertService } from '../../services/alert.service';
import * as ClassicEditor from 'src/assets/main/ckeditor/ckeditor.js';
import { AlertContext } from '../../models/enums/alert-context.enum';
import { NgbPopover } from '@ng-bootstrap/ng-bootstrap';
import { FieldDefinitionTypeEnum } from '../../models/enums/field-definition-type.enum';
import { FieldDefinitionDto, PersonDto } from '../../generated/model/models';


declare var $ : any

@Component({
  selector: 'field-definition',
  templateUrl: './field-definition.component.html',
  styleUrls: ['./field-definition.component.scss']
})
export class FieldDefinitionComponent implements OnInit {

  @Input() fieldDefinitionType: string;
  @Input() labelOverride: string;
  @Input() labelAppend: string;
  @ViewChild('p') public popover: NgbPopover;
  @ViewChild('popContent') public content: any;
  public fieldDefinition: FieldDefinitionDto;
  public isLoading: boolean = true;
  public isEditing: boolean = false;
  public emptyContent: boolean = false;
  public watchUserChangeSubscription: any;
  public Editor = ClassicEditor;
  public editedContent: string;
  public editor;

  currentUser: PersonDto;

  public ckConfig = {"removePlugins": ["MediaEmbed", "ImageUpload"]};

  constructor(private fieldDefinitionService: FieldDefinitionService,
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef,
    private alertService: AlertService,
    private elem: ElementRef) { }

  ngOnInit() {
    this.fieldDefinitionService.getFieldDefinition(FieldDefinitionTypeEnum[this.fieldDefinitionType]).subscribe(x => {
      this.loadFieldDefinition(x);
    });
  }

  ngOnDestroy() {
    
    this.cdr.detach();
  }

  // tell CkEditor to use the class below as its upload adapter
  // see https://ckeditor.com/docs/ckeditor5/latest/framework/guides/deep-dive/upload-adapter.html#how-does-the-image-upload-work
  public ckEditorReady(editor) {
    this.editor = editor;
  }

  public getLabelText() {
    return (this.labelOverride !== null && this.labelOverride !== undefined ? this.labelOverride : this.fieldDefinition.FieldDefinitionType.FieldDefinitionTypeDisplayName) + (this.labelAppend != null && this.labelAppend != undefined ? this.labelAppend : "");
  }

  public showEditButton(): boolean {
    return this.authenticationService.isCurrentUserAnAdministrator();
  }

  public enterEdit(): void {
    this.editedContent = this.fieldDefinition.FieldDefinitionValue ?? "";
    this.isEditing = true;
  }

  public cancelEdit(): void {
    this.isEditing = false;
    this.popover.close();
  }

  public saveEdit(): void {
    this.isEditing = false;
    this.isLoading = true;
    this.fieldDefinition.FieldDefinitionValue = this.editedContent;
    this.fieldDefinitionService.updateFieldDefinition(this.fieldDefinition).subscribe(x => {
      this.loadFieldDefinition(x);
    }, error => {
      this.isLoading = false;
      this.alertService.pushAlert(new Alert("There was an error updating the field definition", AlertContext.Danger, true));
    });
  }

  private loadFieldDefinition(fieldDefinition:FieldDefinitionDto)
  {
    this.fieldDefinition = fieldDefinition;
    this.emptyContent = fieldDefinition.FieldDefinitionValue?.length > 0 ? false : true;
    this.isLoading = false;
  }

  public notEditingMouseEnter() {
    if (!this.isEditing) {
      this.popover.open();
      this.elem.nativeElement.closest('body')
                             .querySelector(".popover")
                             .addEventListener('mouseleave', this.mouseLeaveEvent.bind(this));
    }
  }

  public mouseLeaveEvent() {
    if (!this.isEditing) {
      this.popover.close();
    }
  }

  public notEditingMouseLeave() {
      setTimeout( () => {
        let hoveringPopover = this.elem.nativeElement.closest('body')
                                                     .querySelector(".popover:hover")
        if (!hoveringPopover && !this.isEditing) {
            this.popover.close();
        }
    }, 50);
  }
}
