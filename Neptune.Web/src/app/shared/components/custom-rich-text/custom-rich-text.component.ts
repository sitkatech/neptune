import { Component, OnInit, Input, ViewChild, AfterViewChecked, ChangeDetectorRef, OnDestroy } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { AlertService } from "../../services/alert.service";
import { Alert } from "../../models/alert";
import { AlertContext } from "../../models/enums/alert-context.enum";
import { EditorComponent, TINYMCE_SCRIPT_SRC } from "@tinymce/tinymce-angular";
import TinyMCEHelpers from "../../helpers/tiny-mce-helpers";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { NeptunePageDto } from "../../generated/model/neptune-page-dto";
import { CustomRichTextService } from "../../generated/api/custom-rich-text.service";
import { FormsModule } from "@angular/forms";
import { NgIf } from "@angular/common";
import { LoadingDirective } from "../../directives/loading.directive";
import { IconComponent } from "../icon/icon.component";
import { PersonDto } from "../../generated/model/person-dto";

@Component({
    selector: "custom-rich-text",
    templateUrl: "./custom-rich-text.component.html",
    styleUrls: ["./custom-rich-text.component.scss"],
    standalone: true,
    imports: [LoadingDirective, NgIf, IconComponent, FormsModule, EditorComponent],
    providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: "tinymce/tinymce.min.js" }],
})
export class CustomRichTextComponent implements OnInit, AfterViewChecked, OnDestroy {
    @Input() customRichTextTypeID: number;
    @Input() showLoading: boolean = true;
    @Input() showInfoIcon: boolean = true;
    @ViewChild("tinyMceEditor") tinyMceEditor: EditorComponent;
    public tinyMceConfig: object;

    private currentUser: PersonDto;

    public customRichTextTitle: string;
    public editedTitle: string;
    public showTitle: boolean = false;

    public customRichTextContent: SafeHtml;
    public editedContent: string;
    public isEmptyContent: boolean = false;

    public isLoading: boolean = true;
    public isEditing: boolean = false;

    constructor(
        private customRichTextService: CustomRichTextService,
        private publicService: CustomRichTextService,
        private authenticationService: AuthenticationService,
        private alertService: AlertService,
        private sanitizer: DomSanitizer,
        private cdr: ChangeDetectorRef
    ) {}

    ngAfterViewChecked(): void {
        // We need to use ngAfterViewInit because the image upload needs a reference to the component
        // to setup the blobCache for image base64 encoding
        this.tinyMceConfig = TinyMCEHelpers.DefaultInitConfig(this.tinyMceEditor);
    }

    ngOnInit() {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;
        });

        this.publicService.customRichTextCustomRichTextTypeIDGet(this.customRichTextTypeID).subscribe((x) => {
            this.loadCustomRichText(x);
        });
    }

    ngOnDestroy(): void {
        this.cdr.detach();
    }

    private loadCustomRichText(customRichText: NeptunePageDto) {
        this.editedTitle = this.customRichTextTitle;

        this.customRichTextContent = this.sanitizer.bypassSecurityTrustHtml(customRichText.NeptunePageContent);
        this.editedContent = customRichText.NeptunePageContent;
        this.isEmptyContent = customRichText.IsEmptyContent;
        this.isLoading = false;
        this.cdr.detectChanges();
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
                this.loadCustomRichText(x);
            },
            (error) => {
                this.isLoading = false;
                this.alertService.pushAlert(new Alert("There was an error updating the rich text content", AlertContext.Danger, true));
            }
        );
    }
}
