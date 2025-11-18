import { Component, EventEmitter, inject, Input, OnDestroy, Output, SimpleChanges } from "@angular/core";
import { FileResourceSimpleDto } from "../../generated/model/models";
import { Subscription } from "rxjs";
import { FileResourceService } from "../../generated/api/file-resource.service";
import { DatePipe } from "@angular/common";
import saveAs from "file-saver";
import { IconComponent } from "../icon/icon.component";
import { FileUploadModalComponent, IFileResourceUpload } from "./file-upload-modal/file-upload-modal.component";
import { FileDescriptionUpdateModalComponent } from "./file-description-update-modal/file-description-update-modal.component";
import { DialogService } from "@ngneat/dialog";

@Component({
    selector: "file-resource-list",
    imports: [DatePipe, IconComponent],
    templateUrl: "./file-resource-list.component.html",
    styleUrl: "./file-resource-list.component.scss",
})
export class FileResourceListComponent implements OnDestroy {
    private fileResourceService: FileResourceService = inject(FileResourceService);
    private dialogService: DialogService = inject(DialogService);
    private subscriptions: Subscription[] = [];

    @Input() title: string = "Documents";
    @Input() fileResources: IHaveFileResource[];

    @Input() showHeader: boolean = true;
    @Input() allowUploading: boolean = true;
    @Input() allowEditing: boolean = true;

    @Output() fileResourceUploaded = new EventEmitter<IFileResourceUpload>();
    @Output() fileResourceUpdated = new EventEmitter<IHaveFileResource>();
    @Output() fileResourceDeleted = new EventEmitter<IHaveFileResource>();

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.fileResources) {
            this.markImageFiles();
        }
    }

    ngOnDestroy(): void {
        this.subscriptions.forEach((subscription) => {
            if (subscription.unsubscribe) {
                subscription.unsubscribe();
            }
        });
    }

    private markImageFiles(): void {
        if (!this.fileResources) {
            return;
        }

        for (const fileResource of this.fileResources) {
            const ext = fileResource?.OriginalFileExtension;
            fileResource.IsImage = ext ? this.isImageExtension(ext) : false;
        }
    }

    // Helper to mark image files for template use
    private isImageExtension(ext: string): boolean {
        return ["jpg", "jpeg", "png", "gif", "bmp", "webp", "svg"].includes(ext.toLowerCase());
    }

    public openFileUploadModal(): void {
        const dialogRef = this.dialogService.open(FileUploadModalComponent, {
            data: {},
            size: "sm",
        });

        dialogRef.afterClosed$.subscribe((result: IFileResourceUpload) => {
            if (result) {
                this.fileResourceUploaded.emit(result);
            }
        });
    }

    public downloadFileResource(fileResource: FileResourceSimpleDto) {
        let downloadFileSubscription = this.fileResourceService.displayResourceFileResource(fileResource.FileResourceGUID).subscribe((response) => {
            saveAs(response, `${fileResource.OriginalBaseFilename}`);
        });

        this.subscriptions.push(downloadFileSubscription);
    }

    public openFileDescriptionUpdateModal(fileResource: IHaveFileResource): void {
        const dialogRef = this.dialogService.open(FileDescriptionUpdateModalComponent, {
            data: { FileResource: fileResource },
            size: "sm",
        });

        dialogRef.afterClosed$.subscribe((result) => {
            if (result) {
                this.fileResourceUpdated.emit(result);
            }
        });
    }

    public deleteFileResource(fileResource: IHaveFileResource) {
        this.fileResourceDeleted.emit(fileResource);
    }

    // Map to store preview URLs for images by GUID
    public imagePreviewUrls: { [guid: string]: string } = {};

    // Call this to load the image preview for a file resource
    public loadImagePreview(fileResource: IHaveFileResource): void {
        const guid = fileResource?.FileResourceGUID;
        if (!guid || this.imagePreviewUrls[guid]) return;
        this.fileResourceService.displayResourceFileResource(guid).subscribe((blob: Blob) => {
            const url = URL.createObjectURL(blob);
            this.imagePreviewUrls[guid] = url;
        });
    }

    public hideImagePreview(fileResource: IHaveFileResource): void {
        const guid = fileResource?.FileResourceGUID;
        if (!guid || !this.imagePreviewUrls[guid]) {
            return;
        }
        URL.revokeObjectURL(this.imagePreviewUrls[guid]);
        delete this.imagePreviewUrls[guid];
    }
}

export interface IHaveFileResource {
    //FileResource?: FileResourceSimpleDto;
    FileResourceGUID?: string;
    OriginalBaseFilename?: string;
    OriginalFileExtension?: string;
    DocumentDescription?: string;
    IsImage?: boolean;
    CreateDate?: string;
}
