import { ChangeDetectorRef, Component, ComponentRef, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";
import { ProjectDocumentUpsertDto } from "src/app/shared/models/project-document-upsert-dto";
import { Alert } from "src/app/shared/models/alert";
import { AlertContext } from "src/app/shared/models/enums/alert-context.enum";
import { AlertService } from "src/app/shared/services/alert.service";
import { ProjectService } from "src/app/shared/generated/api/project.service";
import { NeptunePageTypeEnum } from "src/app/shared/generated/enum/neptune-page-type-enum";
import { PersonDto, ProjectDocumentDto, ProjectDocumentUpdateDto } from "src/app/shared/generated/model/models";
import { AttachmentsDisplayComponent } from "../../../../shared/components/projects/attachments-display/attachments-display.component";
import { FormsModule, NgForm } from "@angular/forms";
import { CustomRichTextComponent } from "../../../../shared/components/custom-rich-text/custom-rich-text.component";
import { NgIf, NgClass } from "@angular/common";
import { PageHeaderComponent } from "../../../../shared/components/page-header/page-header.component";
import { WorkflowBodyComponent } from "../../../../shared/components/workflow-body/workflow-body.component";
import { AlertDisplayComponent } from "../../../../shared/components/alert-display/alert-display.component";
import { ModalService, ModalSizeEnum, ModalThemeEnum } from "src/app/shared/services/modal/modal.service";
import { ConfirmService } from "src/app/shared/services/confirm/confirm.service";
import { ModalComponent } from "src/app/shared/components/modal/modal.component";
import { ConfirmOptions } from "src/app/shared/services/confirm/confirm-options";
import { ProjectDocumentService } from "src/app/shared/generated/api/project-document.service";

@Component({
    selector: "project-attachments",
    templateUrl: "./project-attachments.component.html",
    styleUrls: ["./project-attachments.component.scss"],
    standalone: true,
    imports: [NgIf, CustomRichTextComponent, FormsModule, NgClass, AttachmentsDisplayComponent, PageHeaderComponent, WorkflowBodyComponent, AlertDisplayComponent],
})
export class ProjectAttachmentsComponent implements OnInit, OnDestroy {
    @ViewChild("fileUpload") fileUpload: any;
    @ViewChild("editAttachmentModal") editAttachmentModal: any;
    private editAttachmentModalComponent: ComponentRef<ModalComponent>;

    public currentUser: PersonDto;
    public customRichTextTypeID = NeptunePageTypeEnum.HippocampProjectAttachments;

    public projectID: number;
    public model: ProjectDocumentUpsertDto;
    public attachments: Array<ProjectDocumentDto>;

    public isLoadingSubmit: boolean = false;
    public requiredFileIsUploaded: boolean = false;

    public displayErrors: any = {};
    public displayFileErrors: boolean = false;
    public invalidFields: Array<string> = [];

    public fileName: string;
    public isLoadingDelete = false;
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
        private projectDocumentService: ProjectDocumentService,
        private cdr: ChangeDetectorRef,
        private alertService: AlertService,
        private modalService: ModalService,
        private confirmService: ConfirmService
    ) {}

    ngOnInit(): void {
        this.projectID = parseInt(this.route.snapshot.paramMap.get("projectID"));
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;

            this.projectService.projectsProjectIDGet(this.projectID).subscribe((project) => {
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
            return "No file chosen...";
        }
        return file.name;
    }

    public openFileUpload() {
        this.fileUpload.nativeElement.click();
    }

    public isFieldInvalid(fieldName: string) {
        return this.invalidFields.indexOf(fieldName) > -1;
    }

    private mapProjectDocumentSimpleDtoToUpdate(projectDocumentSimpleDto: ProjectDocumentDto): ProjectDocumentUpdateDto {
        let projectDocumentUpdateDto = new ProjectDocumentUpdateDto();

        projectDocumentUpdateDto.DisplayName = projectDocumentSimpleDto.DisplayName;
        projectDocumentUpdateDto.ProjectID = projectDocumentSimpleDto.ProjectID;
        projectDocumentUpdateDto.DocumentDescription = projectDocumentSimpleDto.DocumentDescription;

        return projectDocumentUpdateDto;
    }

    public launchDeleteModal(attachmentIDToRemove: number): void {
        this.attachmentIDToRemove = attachmentIDToRemove;
        const options = {
            title: "Confirm: Delete Treatment Attachment",
            message: "<p>Are you sure you wish to delete this attachment?<br />This action cannot be undone</p>",
            buttonClassYes: "btn-danger",
            buttonTextYes: "Confirm",
            buttonTextNo: "Cancel",
        } as ConfirmOptions;
        this.confirmService.confirm(options).then((confirmed) => {
            if (confirmed) {
                this.isLoadingDelete = true;
                this.alertService.clearAlerts();

                this.projectDocumentService.projectDocumentsProjectDocumentIDDelete(this.attachmentIDToRemove).subscribe(
                    (response) => {
                        this.isLoadingDelete = false;
                        this.alertService.pushAlert(new Alert("Attachment was successfully deleted.", AlertContext.Success, true));
                        this.refreshAttachments();
                    },
                    (error) => {
                        this.isLoadingDelete = false;
                        window.scroll(0, 0);
                        this.cdr.detectChanges();
                    }
                );
            }
        });
    }

    openEditAttachmentModal(attachment: ProjectDocumentDto): void {
        this.editAttachmentID = attachment.ProjectDocumentID;
        this.editModel = this.mapProjectDocumentSimpleDtoToUpdate(attachment);
        this.editAttachmentModalComponent = this.modalService.open(this.editAttachmentModal, null, {
            ModalTheme: ModalThemeEnum.Light,
            ModalSize: ModalSizeEnum.Medium,
        });
    }

    closeEditAttachmentModal(): void {
        if (!this.editAttachmentModalComponent) return;
        this.modalService.close(this.editAttachmentModalComponent);
    }

    refreshAttachments(): void {
        this.projectService.projectsProjectIDAttachmentsGet(this.projectID).subscribe((attachments) => {
            this.attachments = attachments;
            this.cdr.detectChanges();
        });
    }

    resetAttachmentForm(addAttachmentForm: NgForm): void {
        addAttachmentForm.reset();
        //clear file field manually
        this.fileUpload.nativeElement.value = "";
        this.model.FileResource = null;
        this.refreshAttachments();
    }

    public onSubmit(addAttachmentForm: NgForm): void {
        this.isLoadingSubmit = true;
        this.invalidFields = [];
        this.alertService.clearAlerts();

        if (!this.model.FileResource) {
            this.alertService.pushAlert(new Alert("No File found. Please upload a file.", AlertContext.Danger, true));
            this.isLoadingSubmit = false;
        }
        if (!this.model.DisplayName) {
            this.alertService.pushAlert(new Alert("Please include a display name.", AlertContext.Danger, true));
            this.isLoadingSubmit = false;
        }

        this.projectService
            .projectsProjectIDAttachmentsPost(this.projectID, this.model.ProjectID, this.model.FileResource, this.model.DisplayName, this.model.DocumentDescription)
            .subscribe(
                (response) => {
                    this.onSubmitSuccess(addAttachmentForm, "Project attachment '" + response.DisplayName + "' successfully added.");
                },
                (error) => {
                    this.onSubmitFailure(error);
                }
            );
    }

    public onEditSubmit(editAttachmentForm: NgForm): void {
        this.isLoadingSubmit = true;
        this.isLoadingUpdate = true;
        this.invalidFields = [];
        this.alertService.clearAlerts();

        this.projectDocumentService.projectDocumentsProjectDocumentIDPut(this.editAttachmentID, this.editModel).subscribe(
            (response) => {
                this.isLoadingUpdate = false;
                this.closeEditAttachmentModal();
                this.onSubmitSuccess(editAttachmentForm, "Project attachment '" + response.DisplayName + "' successfully updated.");
            },
            (error) => {
                this.onSubmitFailure(error);
                this.isLoadingSubmit = false;
            }
        );
    }

    private onSubmitSuccess(addAttachmentForm: NgForm, successMessage: string) {
        this.resetAttachmentForm(addAttachmentForm);
        this.isLoadingSubmit = false;
        this.alertService.pushAlert(new Alert(successMessage, AlertContext.Success));
        window.scroll(0, 0);
        this.cdr.detectChanges();
    }

    private onSubmitFailure(error) {
        if (error.error?.errors) {
            for (let key of Object.keys(error.error.errors)) {
                this.invalidFields.push(key);
            }
        }
        this.isLoadingSubmit = false;
        window.scroll(0, 0);
        this.cdr.detectChanges();
    }

    continueToNextStep() {
        this.router.navigateByUrl(`/projects/edit/${this.projectID}/review-and-share`);
    }
}
