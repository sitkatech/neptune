import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { FieldDefinitionDto, PersonDto } from 'src/app/shared/generated/model/models';
import { FieldDefinitionService } from 'src/app/shared/generated/api/field-definition.service';
import { EditorComponent, EditorModule } from "@tinymce/tinymce-angular";
import TinyMCEHelpers from 'src/app/shared/helpers/tiny-mce-helpers';
import { FormsModule } from '@angular/forms';
import { AlertDisplayComponent } from '../../shared/components/alert-display/alert-display.component';
import { NgIf } from '@angular/common';

@Component({
    selector: 'hippocamp-field-definition-edit',
    templateUrl: './field-definition-edit.component.html',
    styleUrls: ['./field-definition-edit.component.scss'],
    standalone: true,
    imports: [NgIf, RouterLink, AlertDisplayComponent, EditorModule, FormsModule]
})
export class FieldDefinitionEditComponent implements OnInit {
  
  private currentUser: PersonDto;

  public fieldDefinition: FieldDefinitionDto;
  public editor;
  @ViewChild('tinyMceEditor') tinyMceEditor : EditorComponent;
  public tinyMceConfig: object;

  public isLoadingSubmit: boolean;

  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private alertService: AlertService,
      private fieldDefinitionService: FieldDefinitionService,
      private authenticationService: AuthenticationService,
      private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
      this.authenticationService.getCurrentUser().subscribe(currentUser => {
          this.currentUser = currentUser;
          const id = parseInt(this.route.snapshot.paramMap.get("definitionID"));
          if (id) {
              this.fieldDefinitionService.fieldDefinitionsFieldDefinitionTypeIDGet(id).subscribe(fieldDefinition => {
                this.fieldDefinition = fieldDefinition;
              })
          }
      });
  }

  ngAfterViewInit(): void {
    // We need to use ngAfterViewInit because the image upload needs a reference to the component
    // to setup the blobCache for image base64 encoding
    this.tinyMceConfig = TinyMCEHelpers.DefaultInitConfig(this.tinyMceEditor)
  }

  ngOnDestroy() {      
      this.cdr.detach();
  }

  public currentUserIsAdmin(): boolean {
      return this.authenticationService.isUserAnAdministrator(this.currentUser);
  }

  saveDefinition(): void {
    this.isLoadingSubmit = true;

    this.fieldDefinitionService.fieldDefinitionsFieldDefinitionTypeIDPut(this.fieldDefinition.FieldDefinitionType.FieldDefinitionTypeID, this.fieldDefinition)
      .subscribe(response => {
        this.isLoadingSubmit = false;
        this.router.navigateByUrl("/labels-and-definitions").then(x => {
          this.alertService.pushAlert(new Alert(`The definition for ${this.fieldDefinition.FieldDefinitionType.FieldDefinitionTypeDisplayName} was successfully updated.`, AlertContext.Success));
        });
      },
        error => {
          this.isLoadingSubmit = false;
          this.cdr.detectChanges();
        }
      );
  }

}

