import { Component, OnInit, Input, ViewChild, ChangeDetectorRef } from "@angular/core";
import { EditorComponent, EditorModule, TINYMCE_SCRIPT_SRC } from "@tinymce/tinymce-angular";
import { AuthenticationService } from "src/app/services/authentication.service";
import TinyMCEHelpers from "../../helpers/tiny-mce-helpers";
import { Alert } from "../../models/alert";
import { AlertContext } from "../../models/enums/alert-context.enum";
import { AlertService } from "../../services/alert.service";
import { FieldDefinitionDto } from "../../generated/model/field-definition-dto";
import { PersonDto } from "../../generated/model/person-dto";
import { NgbPopover } from "@ng-bootstrap/ng-bootstrap/popover/popover";
import { FieldDefinitionService } from "../../generated/api/field-definition.service";
import { FieldDefinitionTypeEnum } from "../../generated/enum/field-definition-type-enum";
import { NgbPopover as NgbPopover_1 } from "@ng-bootstrap/ng-bootstrap";
import { FormsModule } from "@angular/forms";
import { NgIf, NgClass } from "@angular/common";

declare var $: any;

@Component({
    selector: "field-definition",
    templateUrl: "./field-definition.component.html",
    styleUrls: ["./field-definition.component.scss"],
    standalone: true,
    imports: [NgIf, EditorModule, FormsModule, NgbPopover_1, NgClass],
    providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: "tinymce/tinymce.min.js" }],
})
export class FieldDefinitionComponent implements OnInit {
    @Input() fieldDefinitionType: string;
    @Input() labelOverride: string;
    @Input() inline: boolean = true;
    @Input() white?: boolean;
    @Input() useBodyContainer: boolean = false;
    @ViewChild("tinyMceEditor") tinyMceEditor: EditorComponent;
    @ViewChild("p") public p: NgbPopover;
    @ViewChild("editingPopover") public editingPopover: NgbPopover;
    @ViewChild("popContent") public content: any;
    public tinyMceConfig: object;

    private currentUser: PersonDto;

    public fieldDefinition: FieldDefinitionDto;
    public isLoading: boolean = true;
    public isEditing: boolean = false;
    public emptyContent: boolean = false;

    public editedContent: string;

    constructor(
        private fieldDefinitionService: FieldDefinitionService,
        private authenticationService: AuthenticationService,
        private cdr: ChangeDetectorRef,
        private alertService: AlertService
    ) {}

    ngAfterViewInit(): void {
        // We need to use ngAfterViewInit because the image upload needs a reference to the component
        // to setup the blobCache for image base64 encoding
        this.tinyMceConfig = TinyMCEHelpers.DefaultInitConfig(this.tinyMceEditor);
    }

    ngOnInit() {
        this.authenticationService.getCurrentUser().subscribe((currentUser) => {
            this.currentUser = currentUser;
        });

        this.fieldDefinitionService.fieldDefinitionsFieldDefinitionTypeIDGet(FieldDefinitionTypeEnum[this.fieldDefinitionType]).subscribe((x) => this.loadFieldDefinition(x));
    }

    ngOnDestroy() {
        this.cdr.detach();
    }

    private loadFieldDefinition(fieldDefinition: FieldDefinitionDto) {
        this.fieldDefinition = fieldDefinition;
        this.emptyContent = fieldDefinition.FieldDefinitionValue?.length == 0;
        this.isLoading = false;
    }

    public getLabelText() {
        if (this.labelOverride !== null && this.labelOverride !== undefined) {
            return this.labelOverride;
        }

        return this.fieldDefinition.FieldDefinitionType.FieldDefinitionTypeDisplayName;
    }

    public showEditButton(): boolean {
        return this.authenticationService.isCurrentUserAnAdministrator();
    }

    public enterEdit(event: any): void {
        event.preventDefault();

        this.editedContent = this.fieldDefinition.FieldDefinitionValue;
        this.isEditing = true;

        setTimeout(() => {
            this.editingPopover.open();
        }, 0);
    }

    public cancelEdit(): void {
        this.isEditing = false;
    }

    public saveEdit(): void {
        this.isEditing = false;
        this.isLoading = true;

        this.fieldDefinition.FieldDefinitionValue = this.editedContent;
        this.fieldDefinitionService.fieldDefinitionsFieldDefinitionTypeIDPut(this.fieldDefinition.FieldDefinitionID, this.fieldDefinition).subscribe(
            (x) => {
                this.loadFieldDefinition(x);

                setTimeout(() => {
                    this.p.open();
                }, 250);
            },
            (error) => {
                this.isLoading = false;
                this.alertService.pushAlert(new Alert("There was an error updating the field definition", AlertContext.Danger));
            }
        );
    }
}
