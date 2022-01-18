import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';
import { CustomRichTextService } from '../../services/custom-rich-text.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AlertService } from '../../services/alert.service';
import { Alert } from '../../models/alert';
import { AlertContext } from '../../models/enums/alert-context.enum';
import * as ClassicEditor from 'src/assets/main/ckeditor/ckeditor.js';
import { environment } from 'src/environments/environment';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { NeptunePageDto } from '../../generated/model/neptune-page-dto';
import { PersonDto } from '../../generated/model/person-dto';

@Component({
  selector: 'custom-rich-text',
  templateUrl: './custom-rich-text.component.html',
  styleUrls: ['./custom-rich-text.component.scss']
})
export class CustomRichTextComponent implements OnInit {
  @Input() customRichTextTypeID: number;
  public customRichTextContent: SafeHtml;
  public isLoading: boolean = true;
  public isEditing: boolean = false;
  public isEmptyContent: boolean = false;
  public watchUserChangeSubscription: any;
  public Editor = ClassicEditor;
  public editedContent: string;
  public editor;

  currentUser: PersonDto;

  //For media embed https://ckeditor.com/docs/ckeditor5/latest/api/module_media-embed_mediaembed-MediaEmbedConfig.html
  //Only some embeds will work, and if we want others to work we'll likely need to write some extra functions
  public ckConfig = {mediaEmbed: {previewsInData: true}};

  constructor(private customRichTextService: CustomRichTextService,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.currentUser = currentUser;
    });

    this.customRichTextService.getCustomRichText(this.customRichTextTypeID).subscribe(x => {
      this.customRichTextContent = this.sanitizer.bypassSecurityTrustHtml(x.NeptunePageContent);
      this.isEmptyContent = x.IsEmptyContent;
      this.editedContent = x.NeptunePageContent;
      this.isLoading = false;
    });
  }

  // tell CkEditor to use the class below as its upload adapter
  // see https://ckeditor.com/docs/ckeditor5/latest/framework/guides/deep-dive/upload-adapter.html#how-does-the-image-upload-work
  public ckEditorReady(editor) {
    const customRichTextService = this.customRichTextService
    this.editor = editor;

    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
      // disable the editor until the image comes back
      editor.isReadOnly = true;
      return new CkEditorUploadAdapter(loader, customRichTextService, environment.mainAppApiUrl, editor);
    };
  }

  public showEditButton(): boolean {
    return this.authenticationService.isUserAnAdministrator(this.currentUser);
  }

  public enterEdit(): void {
    this.isEditing = true;
  }

  public cancelEdit(): void {
    this.isEditing = false;
  }

  public saveEdit(): void {
    this.isEditing = false;
    this.isLoading = true;
    const updateDto = new NeptunePageDto({ NeptunePageContent: this.editedContent });
    this.customRichTextService.updateCustomRichText(this.customRichTextTypeID, updateDto).subscribe(x => {
      this.customRichTextContent = this.sanitizer.bypassSecurityTrustHtml(x.NeptunePageContent);
      this.editedContent = x.NeptunePageContent;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.alertService.pushAlert(new Alert("There was an error updating the rich text content", AlertContext.Danger, true));
    });
  }
  
  public isUploadingImage():boolean{
    return this.editor && this.editor.isReadOnly;
  }

}

class CkEditorUploadAdapter {
  loader;
  service: CustomRichTextService;
  apiUrl: string;
  editor;

  constructor(loader, uploadService: CustomRichTextService, apiUrl: string, editor) {
    // The file loader instance to use during the upload.
    this.loader = loader;
    this.service = uploadService;
    this.apiUrl = apiUrl;
    this.editor = editor;
  }

  // Starts the upload process.
  upload() {
    const editor = this.editor;
    const service = this.service;


    return this.loader.file.then(file => new Promise((resolve, reject) => {
      service.uploadFile(file).subscribe(x => {
        const imageUrl = `${this.apiUrl}${x.imageUrl}`;
        editor.isReadOnly = false;

        resolve({
          // todo: this should be correct instead of incorrect.
          default: imageUrl
        });
      }, error => {
        editor.isReadOnly = false;

        reject("There was an error uploading the file. Please try again.")
      });
    })
    );
  }
}
