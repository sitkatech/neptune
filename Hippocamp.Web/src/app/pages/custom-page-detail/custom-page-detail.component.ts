import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserDetailedDto } from 'src/app/shared/models';
import { CustomPageService } from 'src/app/services/custom-page.service';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import * as ClassicEditor from 'src/assets/main/ckeditor/ckeditor.js';
import { environment } from 'src/environments/environment';
import { CustomPageUpsertDto } from 'src/app/shared/models/custom-page-upsert-dto';
import { CustomPageDto } from 'src/app/shared/generated/model/models';

@Component({
  selector: 'hippocamp-custom-page-detail',
  templateUrl: './custom-page-detail.component.html',
  styleUrls: ['./custom-page-detail.component.scss']
})
export class CustomPageDetailComponent implements OnInit {
  @Input() customPageVanityUrl: string;
  public customPageContent: SafeHtml;
  public customPageDisplayName: string;
  public viewableRoleIDs: Array<number>;
  public isLoading: boolean = true;
  public isEditing: boolean = false;
  public isEmptyContent: boolean = false;
  
  public watchUserChangeSubscription: any;
  public Editor = ClassicEditor;
  public editor;
  public editedContent: string;
  
  private currentUser: UserDetailedDto;
  
  //For media embed https://ckeditor.com/docs/ckeditor5/latest/api/module_media-embed_mediaembed-MediaEmbedConfig.html
  //Only some embeds will work, and if we want others to work we'll likely need to write some extra functions
  public ckConfig = {mediaEmbed: {previewsInData: true}};
  public customPage: CustomPageDto;

  constructor(
    private customPageService: CustomPageService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private cdr: ChangeDetectorRef,
    private sanitizer: DomSanitizer) {
      // force route reload whenever params change
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }

  ngOnInit() {
    this.watchUserChangeSubscription = this.authenticationService.currentUserSetObservable.subscribe(currentUser => {
      this.currentUser = currentUser;
    });

    const vanityUrl = this.route.snapshot.paramMap.get("vanity-url");

    if (vanityUrl) {
      this.customPageService.getCustomPageByVanityUrl(vanityUrl).subscribe(customPage => {
        this.loadCustomPage(customPage);
        this.customPageContent = this.sanitizer.bypassSecurityTrustHtml(customPage.CustomPageContent);
        this.customPageDisplayName = customPage.CustomPageDisplayName;
        this.editedContent = customPage.CustomPageContent;
      });
      this.customPageService.getCustomPageRolesByVanityUrl(vanityUrl).subscribe(pageRoleDtos => {
        this.viewableRoleIDs = pageRoleDtos.map(pageRole => pageRole.RoleID);
      });
    }
  }

  ngOnDestroy() {
    this.watchUserChangeSubscription.unsubscribe();
    this.authenticationService.dispose();
    this.cdr.detach();
  }

  // see https://ckeditor.com/docs/ckeditor5/latest/framework/guides/deep-dive/upload-adapter.html#how-does-the-image-upload-work
  public ckEditorReady(editor) {
    const customPageService = this.customPageService
    this.editor = editor;

    editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
      editor.isReadOnly = true;
      return new CkEditorUploadAdapter(loader, customPageService, environment.mainAppApiUrl, editor);
    };
  }

  public isUserAnAdministrator(): boolean {
    return this.authenticationService.isUserAnAdministrator(this.currentUser);
  }

  public showEditButton(): boolean {
    return this.isUserAnAdministrator();
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
    const updateDto = new CustomPageUpsertDto({
      CustomPageDisplayName: this.customPageDisplayName,
      CustomPageVanityUrl: this.customPage.CustomPageVanityUrl,
      CustomPageContent: this.editedContent,
      MenuItemID: this.customPage.MenuItem.MenuItemID,
      ViewableRoleIDs: this.viewableRoleIDs
     });

    this.customPageService.updateCustomPageByID(this.customPage.CustomPageID, updateDto).subscribe(x => {
      this.customPageContent = this.sanitizer.bypassSecurityTrustHtml(x.CustomPageContent);
      this.editedContent = x.CustomPageContent;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.alertService.pushAlert(new Alert("There was an error updating the rich text content", AlertContext.Danger, true));
    });
  }

  private loadCustomPage(customPage: CustomPageDto)
  {
    this.customPage = customPage; 
    this.isEmptyContent = !!customPage.CustomPageContent;
    this.isLoading = false;
  }

  public isUploadingImage(): boolean {
    return this.editor && this.editor.isReadOnly;
  }

}

class CkEditorUploadAdapter {
  loader;
  service: CustomPageService;
  apiUrl: string;
  editor;

  constructor(loader, uploadService: CustomPageService, apiUrl: string, editor) {
    this.loader = loader;
    this.service = uploadService;
    this.apiUrl = apiUrl;
    this.editor = editor;
  }

  upload() {
    const editor = this.editor;
    const service = this.service;

    return this.loader.file.then(file => new Promise((resolve, reject) => {
      service.uploadFile(file).subscribe(x => {
        const imageUrl = `${this.apiUrl}${x.imageUrl}`;
        editor.isReadOnly = false;

        resolve({
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
