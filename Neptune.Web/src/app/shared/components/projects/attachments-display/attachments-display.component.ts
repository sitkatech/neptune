import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ProjectDocumentDto } from 'src/app/shared/generated/model/project-document-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'hippocamp-attachments-display',
  templateUrl: './attachments-display.component.html',
  styleUrls: ['./attachments-display.component.scss']
})
export class AttachmentsDisplayComponent implements OnInit {

  @Input('attachments') attachments: Array<ProjectDocumentDto>;
  @Input('readOnly') readOnly: boolean = true;

  @Output('onDeleteTriggered') onDeleteTriggered = new EventEmitter<number>();
  @Output('onEditTriggered') onEditTriggered = new EventEmitter<ProjectDocumentDto>();

  constructor() { }

  ngOnInit(): void {
  }

  public getFileLinkValue(attachment: ProjectDocumentDto): string {
    return `${environment.mainAppApiUrl}/FileResource/${attachment.FileResource.FileResourceGUID}`;
  }

  emitDeleteAttachmentTriggered(attachmentIDToDelete : number) {
    this.onDeleteTriggered.emit(attachmentIDToDelete);
  }

  emitEditAttachmentTriggered(attachmentToEdit: ProjectDocumentDto) {
    this.onEditTriggered.emit(attachmentToEdit);
  }

}
