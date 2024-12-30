import { Component, OnInit, Input, ChangeDetectorRef, AfterViewChecked, ViewChild } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { AlertService } from "../../services/alert.service";
import { Alert } from "../../models/alert";
import { AlertContext } from "../../models/enums/alert-context.enum";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { NeptunePageDto } from "../../generated/model/neptune-page-dto";
import { PersonDto } from "../../generated/model/person-dto";
import { EditorComponent, EditorModule, TINYMCE_SCRIPT_SRC } from "@tinymce/tinymce-angular";
import TinyMCEHelpers from "../../helpers/tiny-mce-helpers";
import { CustomRichTextService } from "../../generated/api/custom-rich-text.service";
import { FormsModule } from "@angular/forms";
import { AsyncPipe, NgIf } from "@angular/common";
import { Observable, tap } from "rxjs";
import { LoadingDirective } from "../../directives/loading.directive";

@Component({
    selector: "custom-rich-text",
    templateUrl: "./custom-rich-text.component.html",
    styleUrls: ["./custom-rich-text.component.scss"],
    standalone: true,
    imports: [NgIf, EditorModule, FormsModule, LoadingDirective, AsyncPipe],
    providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: "tinymce/tinymce.min.js" }],
})
export class CustomRichTextComponent implements OnInit, AfterViewChecked {
    @ViewChild("tinyMceEditor") tinyMceEditor: EditorComponent;
    public tinyMceConfig: object;
    @Input() customRichTextTypeID: number;
    public customRichText$: Observable<NeptunePageDto>;
    public customRichTextContent: SafeHtml;
    public isLoading: boolean = true;
    public isEditing: boolean = false;
    public isEmptyContent: boolean = false;
    public watchUserChangeSubscription: any;
    public editedContent: string;

    currentUser: PersonDto;

    constructor(
        private customRichTextService: CustomRichTextService,
        private authenticationService: AuthenticationService,
        private alertService: AlertService,
        private sanitizer: DomSanitizer
    ) {}

    ngOnInit() {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;
        });

        this.customRichText$ = this.customRichTextService.customRichTextCustomRichTextTypeIDGet(this.customRichTextTypeID).pipe(
            tap((customRichText) => {
                this.customRichTextContent = this.sanitizer.bypassSecurityTrustHtml(customRichText.NeptunePageContent);
                this.editedContent = customRichText.NeptunePageContent;
                this.isEmptyContent = customRichText.IsEmptyContent;
                this.isLoading = false;
            })
        );
    }

    ngAfterViewChecked() {
        // We need to use ngAfterViewInit because the image upload needs a reference to the component
        // to setup the blobCache for image base64 encoding
        this.tinyMceConfig = TinyMCEHelpers.DefaultInitConfig(this.tinyMceEditor);
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
        this.customRichTextService.customRichTextCustomRichTextTypeIDPut(this.customRichTextTypeID, updateDto).subscribe(
            (x) => {
                this.customRichTextContent = this.sanitizer.bypassSecurityTrustHtml(x.NeptunePageContent);
                this.editedContent = x.NeptunePageContent;
                this.isLoading = false;
            },
            (error) => {
                this.isLoading = false;
                this.alertService.pushAlert(new Alert("There was an error updating the rich text content", AlertContext.Danger, true));
            }
        );
    }
}
