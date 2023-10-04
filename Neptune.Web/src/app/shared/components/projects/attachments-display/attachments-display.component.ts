import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ProjectDocumentSimpleDto } from 'src/app/shared/generated/model/project-document-simple-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'hippocamp-attachments-display',
  templateUrl: './attachments-display.component.html',
  styleUrls: ['./attachments-display.component.scss']
})
export class AttachmentsDisplayComponent implements OnInit {

  @Input('attachments') attachments: Array<ProjectDocumentSimpleDto>;
  @Input('readOnly') readOnly: boolean = true;

  @Output('onDeleteTriggered') onDeleteTriggered = new EventEmitter<number>();
  @Output('onEditTriggered') onEditTriggered = new EventEmitter<ProjectDocumentSimpleDto>();

  constructor() { }

  ngOnInit(): void {
  }

  public getFileLinkValue(attachment: ProjectDocumentSimpleDto): string {
    return `${environment.mainAppApiUrl}/FileResource/${attachment.FileResource.FileResourceGUID}`;
  }

  emitDeleteAttachmentTriggered(attachmentIDToDelete : number) {
    this.onDeleteTriggered.emit(attachmentIDToDelete);
  }

  emitEditAttachmentTriggered(attachmentToEdit: ProjectDocumentSimpleDto) {
    this.onEditTriggered.emit(attachmentToEdit);
  }

}
