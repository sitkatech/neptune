import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FieldDefinitionService } from 'src/app/shared/services/field-definition-service';
import * as ClassicEditor from 'src/assets/main/ckeditor/ckeditor.js';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { FieldDefinitionDto, PersonDto } from 'src/app/shared/generated/model/models';

@Component({
  selector: 'hippocamp-field-definition-edit',
  templateUrl: './field-definition-edit.component.html',
  styleUrls: ['./field-definition-edit.component.scss']
})
export class FieldDefinitionEditComponent implements OnInit {
  
  private currentUser: PersonDto;

  public fieldDefinition: FieldDefinitionDto;
  public Editor = ClassicEditor;
  public editor;

  public ckConfig = {"removePlugins": ["MediaEmbed", "ImageUpload"]};
  isLoadingSubmit: boolean;

  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private alertService: AlertService,
      private fieldDefinitionService: FieldDefinitionService,
      private authenticationService: AuthenticationService,
      private cdr: ChangeDetectorRef
  ) {
  }

  ngOnInit() {
      this.authenticationService.getCurrentUser().subscribe(currentUser => {
          this.currentUser = currentUser;
          const id = parseInt(this.route.snapshot.paramMap.get("definitionID"));
          if (id) {
              this.fieldDefinitionService.getFieldDefinition(id).subscribe(fieldDefinition => {
                this.fieldDefinition = fieldDefinition;
              })
          }
      });
  }

  ngOnDestroy() {
           
      this.cdr.detach();
  }

  public currentUserIsAdmin(): boolean {
      return this.authenticationService.isUserAnAdministrator(this.currentUser);
  }

  // tell CkEditor to use the class below as its upload adapter
  // see https://ckeditor.com/docs/ckeditor5/latest/framework/guides/deep-dive/upload-adapter.html#how-does-the-image-upload-work
  public ckEditorReady(editor) {
    this.editor = editor;
  }

  saveDefinition(): void {
    this.isLoadingSubmit = true;

    this.fieldDefinitionService.updateFieldDefinition(this.fieldDefinition)
      .subscribe(response => {
        this.isLoadingSubmit = false;
        this.router.navigateByUrl("/labels-and-definitions").then(x => {
          this.alertService.pushAlert(new Alert(`The definition for ${this.fieldDefinition.FieldDefinitionType.FieldDefinitionTypeDisplayName} was successfully updated.`, AlertContext.Success));
        });
      }
        ,
        error => {
          this.isLoadingSubmit = false;
          this.cdr.detectChanges();
        }
      );
  }

}
