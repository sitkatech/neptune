import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProjectService } from 'src/app/services/project/project.service';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectDocumentUpsertDto } from 'src/app/shared/models/project-document-upsert-dto';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/project-document-simple-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'hippocamp-project-attachments',
  templateUrl: './project-attachments.component.html',
  styleUrls: ['./project-attachments.component.scss']
})
export class ProjectAttachmentsComponent implements OnInit, OnDestroy {
  @ViewChild('fileUpload') fileUpload: any;

  public currentUser: PersonDto;
  
  public projectID: number;
  public model: ProjectDocumentUpsertDto;
  public attachments: Array<ProjectDocumentSimpleDto>;

  public isLoadingSubmit: boolean = false;
  public requiredFileIsUploaded: boolean = false;

  public displayErrors: any = {};
  public displayFileErrors: boolean = false;
  public invalidFields: Array<string> = [];

  public fileName: string;
  
  private mainAppApiUrl = environment.mainAppApiUrl;

  private acceptedFileTypes: Array<string> = ["PDF", "ZIP", "DOC", "DOCX", "XLS", "XLSX", "JPG", "PNG"];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.model = new ProjectDocumentUpsertDto();
    this.projectID = parseInt(this.route.snapshot.paramMap.get("projectID"));
    this.model.ProjectID = this.projectID;
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;
      this.cdr.detectChanges();
    });
    this.refreshAttachments();
  }

  ngOnDestroy() {
    this.cdr.detach();
  }
  
  fileEvent() {
    let file = this.getFile();
    this.model.FileResource = file;
    this.displayErrors = false;
    this.requiredFileIsUploaded = true;

    if (file && !this.acceptedFileTypes.includes(file.name.split(".").pop().toUpperCase())) {
      this.invalidFields.push("FileResource");
      this.displayFileErrors = true;
    } else {
      this.displayFileErrors = false;
    }

    this.cdr.detectChanges();
  }

  public getFile(): File {
    if (!this.fileUpload) {
      return null;
    }
    return this.fileUpload.nativeElement.files[0];
  }

  public getFileName(): string {
    let file = this.getFile();
    if (!file) {
      return ""
    }

    return file.name;
  }

  public openFileUpload() {
    this.fileUpload.nativeElement.click();
  }

  public getFileLinkValue(attachment: ProjectDocumentSimpleDto): string {
    return `${this.mainAppApiUrl}/FileResource/${attachment.FileResource.FileResourceGUID}`;
  }

  public isFieldInvalid(fieldName: string) {
    return this.invalidFields.indexOf(fieldName) > -1;
  }

  refreshAttachments(): void {
    this.projectService.getAttachmentsByProjectID(this.projectID).subscribe(attachments => {
      this.attachments = attachments;
      this.cdr.detectChanges();
    });
  }

  resetAttachmentForm(addAttachmentForm: HTMLFormElement): void {
    addAttachmentForm.reset();
    //clear file field manually
    this.fileUpload.nativeElement.value = "";
    this.model.FileResource = null;
    this.refreshAttachments();
  }

  public onSubmit(addAttachmentForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;
    this.invalidFields = [];
    this.alertService.clearAlerts();

    this.projectService.addAttachmentByProjectID(this.projectID, this.model)
      .subscribe(response => {
        this.onSubmitSuccess(addAttachmentForm, "Project attachment '" + response.DisplayName + "' successfully added.", this.projectID);
      }, error => {
        this.onSubmitFailure(error);
      });
  }

  private onSubmitSuccess(addAttachmentForm: HTMLFormElement, successMessage: string, projectID: number) {
    this.resetAttachmentForm(addAttachmentForm);
    this.isLoadingSubmit = false;    
    this.router.navigateByUrl(`/projects/edit/${projectID}/attachments`).then(() => {
      this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
    });
  }

  private onSubmitFailure(error) {
    if (error.error?.errors) {
      for (let key of Object.keys(error.error.errors)) {
        this.invalidFields.push(key);
      }
    }
    this.isLoadingSubmit = false;
    window.scroll(0,0);
    this.cdr.detectChanges();
  }

}
