import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { ProjectDocumentUpsertDto } from 'src/app/shared/models/project-document-upsert-dto';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/project-document-simple-dto';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProjectService } from 'src/app/shared/generated/api/project.service';
import { NeptunePageTypeEnum } from 'src/app/shared/generated/enum/neptune-page-type-enum';
import { ProjectDocumentUpdateDto } from 'src/app/shared/generated/model/project-document-update-dto';

@Component({
  selector: 'hippocamp-project-attachments',
  templateUrl: './project-attachments.component.html',
  styleUrls: ['./project-attachments.component.scss']
})
export class ProjectAttachmentsComponent implements OnInit, OnDestroy {
  @ViewChild('fileUpload') fileUpload: any;
  @ViewChild('deleteAttachmentModal') deleteAttachmentModal: any;
  @ViewChild('editAttachmentModal') editAttachmentModal: any

  public currentUser: PersonDto;
  public richTextTypeID = NeptunePageTypeEnum.HippocampProjectAttachments;

  public projectID: number;
  public model: ProjectDocumentUpsertDto;
  public attachments: Array<ProjectDocumentSimpleDto>;

  public isLoadingSubmit: boolean = false;
  public requiredFileIsUploaded: boolean = false;

  public displayErrors: any = {};
  public displayFileErrors: boolean = false;
  public invalidFields: Array<string> = [];

  public fileName: string;
  private modalReference: NgbModalRef;
  private isLoadingDelete = false;
  private isLoadingUpdate = false;

  private acceptedFileTypes: Array<string> = ["PDF", "ZIP", "DOC", "DOCX", "XLS", "XLSX", "JPG", "PNG"];
  public attachmentIDToRemove: number;

  public editModel: ProjectDocumentUpdateDto;
  public editAttachmentID: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
    private alertService: AlertService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.projectID = parseInt(this.route.snapshot.paramMap.get("projectID"));
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      this.projectService.projectsProjectIDGet(this.projectID).subscribe(project => {
        // redirect to review step if project is shared with OCTA grant program
        if (project.ShareOCTAM2Tier2Scores) {
          this.router.navigateByUrl(`projects/edit/${this.projectID}/review-and-share`);
        }

        this.model = new ProjectDocumentUpsertDto();
        this.model.ProjectID = this.projectID;
      });
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

  public isFieldInvalid(fieldName: string) {
    return this.invalidFields.indexOf(fieldName) > -1;
  }

  private mapProjectDocumentSimpleDtoToUpdate(projectDocumentSimpleDto: ProjectDocumentSimpleDto): ProjectDocumentUpdateDto {
    let projectDocumentUpdateDto = new ProjectDocumentUpdateDto()

    projectDocumentUpdateDto.DisplayName = projectDocumentSimpleDto.DisplayName;
    projectDocumentUpdateDto.ProjectID = projectDocumentSimpleDto.ProjectID;
    projectDocumentUpdateDto.DocumentDescription = projectDocumentSimpleDto.DocumentDescription;

    return projectDocumentUpdateDto;
  }

  public launchDeleteModal(modalContent: any, attachmentIDToRemove: number): void {
    this.attachmentIDToRemove = attachmentIDToRemove;
    this.modalReference = this.modalService.open(
      modalContent, 
      { 
        ariaLabelledBy: 'deleteAttachmentModal', beforeDismiss: () => this.checkIfDeleting(), backdrop: 'static', keyboard: false
      });
  }

  public launchEditModal(modalContent: any, attachment: ProjectDocumentSimpleDto): void {
    this.editAttachmentID = attachment.ProjectDocumentID;
    this.editModel = this.mapProjectDocumentSimpleDtoToUpdate(attachment);

    this.modalReference = this.modalService.open(
      modalContent, 
      { 
        ariaLabelledBy: 'editAttachmentModal', beforeDismiss: () => this.checkIfUpdating(), backdrop: 'static', keyboard: false, size: 'lg'
      });
  }

  private checkIfDeleting(): boolean {
    return this.isLoadingDelete;
  }

  private checkIfUpdating(): boolean {
    return this.isLoadingUpdate;
  }

  public deleteAttachment() { 
    this.isLoadingDelete = true;
    this.alertService.clearAlerts();

    this.projectService.projectsAttachmentsAttachmentIDDelete(this.attachmentIDToRemove).subscribe(() => {
      this.isLoadingDelete = false;
      this.modalReference.close();
      this.alertService.pushAlert(new Alert('Attachment was successfully deleted.', AlertContext.Success, true));
      this.refreshAttachments();
    }, error => {
      this.isLoadingDelete = false;
      window.scroll(0,0);
      this.cdr.detectChanges();
    });
  }

  refreshAttachments(): void {
    this.projectService.projectsProjectIDAttachmentsGet(this.projectID).subscribe(attachments => {
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

    if(!this.model.FileResource){
      this.alertService.pushAlert(new Alert("No File found. Please upload a file.", AlertContext.Danger, true));
      this.isLoadingSubmit = false;
    }
    if(!this.model.DisplayName){
      this.alertService.pushAlert(new Alert("Please include a display name.", AlertContext.Danger, true));
      this.isLoadingSubmit = false;
    }

    this.projectService.projectsProjectIDAttachmentsPost(this.projectID, this.model.ProjectID, this.model.FileResource, this.model.DisplayName, this.model.DocumentDescription)
      .subscribe(response => {
        this.onSubmitSuccess(addAttachmentForm, "Project attachment '" + response.DisplayName + "' successfully added.");
      }, error => {
        this.onSubmitFailure(error);
      });
  }

  public onEditSubmit(editAttachmentForm: HTMLFormElement): void {
    this.isLoadingSubmit = true;
    this.isLoadingUpdate = true;
    this.invalidFields = [];
    this.alertService.clearAlerts();

    this.projectService.projectsAttachmentsAttachmentIDPut(this.editAttachmentID, this.editModel)
      .subscribe(response => {
        this.isLoadingUpdate = false;
        this.modalReference.close('editAttachmentModal');
        this.onSubmitSuccess(editAttachmentForm, "Project attachment '" + response.DisplayName + "' successfully updated.");
      }, error => {
        this.onSubmitFailure(error);
        this.isLoadingSubmit = false;
      });
  }

  private onSubmitSuccess(addAttachmentForm: HTMLFormElement, successMessage: string) {
    this.resetAttachmentForm(addAttachmentForm);
    this.isLoadingSubmit = false;
    this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
    window.scroll(0,0);
    this.cdr.detectChanges();
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

  continueToNextStep() {
    this.router.navigateByUrl(`/projects/edit/${this.projectID}/review-and-share`)
  }
}
