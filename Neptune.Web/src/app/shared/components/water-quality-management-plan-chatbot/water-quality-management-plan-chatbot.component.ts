import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { DialogRef } from "@ngneat/dialog";
import { ElementRef, ViewChild } from "@angular/core";
import { Observable, Subscription, SubscriptionLike } from "rxjs";
import { AuthenticationService } from "src/app/services/authentication.service";
import { EventSourceService } from "src/app/services/event-source.service";
import { environment } from "src/environments/environment";
import { IndexToCharPipe } from "src/app/shared/pipes/index-to-char.pipe";
import { ClipboardModule } from "@angular/cdk/clipboard";
import { AsyncPipe, DatePipe } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { AiChatInputComponent } from "src/app/shared/components/ai-chat-input/ai-chat-input.component";
import { IconComponent } from "../icon/icon.component";
import { AiBadgeComponent } from "../ai-badge/ai-badge.component";
import { LoadingDirective } from "../../directives/loading.directive";
import { WaterQualityManagementPlanService } from "../../generated/api/water-quality-management-plan.service";
import { PersonDto, WaterQualityManagementPlanDto } from "../../generated/model/models";

@Component({
    selector: "water-quality-management-plan-chatbot",
    templateUrl: "water-quality-management-plan-chatbot.component.html",
    styleUrls: ["./water-quality-management-plan-chatbot.component.scss"],
    standalone: true,
    imports: [IconComponent, AiBadgeComponent, IndexToCharPipe, DatePipe, LoadingDirective, AsyncPipe, ClipboardModule, FormsModule, AiChatInputComponent],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WaterQualityManagementPlanChatbotComponent implements OnDestroy, OnInit {
    @ViewChild("main") mainScrollContainer: ElementRef;
    @ViewChild("sidebar") sidebarScrollContainer: ElementRef;

    private eventSourceSubscription: SubscriptionLike;
    public waterQualityManagementPlan: WaterQualityManagementPlanDto | null = null;
    public currentUser$: Observable<PersonDto>;
    public keyDocument: any = null;
    private url: string = "";
    public selectedPrompt: string = null;
    public isReceiving: boolean = false; // Flag to indicate if we are receiving messages

    public messageDto = {
        Messages: [],
    };

    public prompts: string[] = [
        "You are an assistant that specializes in summarizing waterqualitymanagementplan proposals. The content you are given consists " +
            "of a proposal document that contains information about the scope of services being offered by our company. " +
            "You should summarize only the services in the proposal and nothing else. Utilize a narrative form in your " +
            "response and do not use bullet points. Ensure the response is in past tense rather than future tense. Whenever " +
            "the text refers to the proposer or the consultant use the name of our company (ESA). ",
        // "Summarize the key elements of this proposal, including the client's objective, the scope of work, primary tasks, timeline, and expected outcomes.",
        // "Write a plain-language summary of this proposal that explains what the waterqualitymanagementplan is about, why it matters, and what work will be done. Keep it accessible to a general audience with little or no technical background.",
        // "Provide a technical summary of the proposed work, including the methodology, deliverables, regulatory context (if relevant), and any notable data sources, tools, or models used. Focus on the implementation side of the waterqualitymanagementplan."
    ];

    currentUserSubscription: Subscription;
    constructor(
        public dialogRef: DialogRef<any>,
        private authenticationService: AuthenticationService,
        private eventSourceService: EventSourceService,
        private cdr: ChangeDetectorRef,
        private sanitizer: DomSanitizer,
        private waterQualityManagementPlanService: WaterQualityManagementPlanService
    ) {
        const data = dialogRef.data || {};
        this.waterQualityManagementPlan = data.WaterQualityManagementPlan || null;
        this.keyDocument = data.KeyDocument || null;

        // dialogRef.disableClose = true; // Uncomment if you want to prevent closing on backdrop click (ngneat/dialog does not support this directly)

        this.url =
            environment.mainAppApiUrl +
            "/ai/water-quality-management-plans/" +
            this.waterQualityManagementPlan?.WaterQualityManagementPlanID +
            "/documents/" +
            this.keyDocument?.WaterQualityManagementPlanDocumentID +
            "/ask";
    }

    ngOnInit(): void {
        this.currentUser$ = this.authenticationService.getCurrentUser();
    }

    ngOnDestroy(): void {
        this.eventSourceSubscription?.unsubscribe(); // Unsubscribe from any previous subscriptions
    }

    closeDialog(cancel: boolean): void {
        if (cancel) {
            this.dialogRef.close(null);
            return;
        }

        const dto = {
            SomeData: "SomeValue",
        };

        this.dialogRef.close(dto);
    }

    send(event: { message: string; knowledgeScope?: string }) {
        const message = event.message;
        const knowledgeScope = event.knowledgeScope;
        if (this.isReceiving || !message || this.editingIndex != null) return; // If we are already receiving messages, do not send again
        this.messageDto.Messages.push({
            Role: "user",
            Content: message,
            Date: new Date(),
        });

        this.sendMessages(this.messageDto);
        this.cdr.detectChanges(); // Trigger change detection to update the view
    }

    selectPrompt(prompt: string): void {
        this.selectedPrompt = prompt;

        this.messageDto.Messages.push({
            Role: "user",
            Content: prompt,
            Date: new Date(),
        });

        this.sendMessages(this.messageDto);
    }

    async sendMessages(messageDto): Promise<void> {
        // Fetch the access token asynchronously
        this.isReceiving = true; // Set the flag to indicate we are receiving messages
        try {
            const accessToken = this.authenticationService.getAccessToken();
            const options = {
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + accessToken,
                },
                payload: JSON.stringify(messageDto),
            };
            const eventNames = ["message"];
            this.eventSourceSubscription = this.eventSourceService.connectToServerSentEvents(this.url, options, eventNames).subscribe({
                next: this.handleEventData.bind(this),
                error: (error) => {
                    //handle error
                    console.error("Received err:", error);
                    this.isReceiving = false; // Set the flag to indicate we are no longer receiving messages
                    this.eventSourceSubscription.unsubscribe(); // Unsubscribe from the event source
                    this.cdr.detectChanges(); // Trigger change detection to update the view
                },
            });
            setTimeout(() => {
                this.messageDto.Messages.push({
                    Role: "assistant",
                    Content: "",
                    Date: new Date(),
                });
                this.cdr.detectChanges(); // Trigger change detection to update the view
            }, 100); // Add a new assistant message after a short delay
        } catch (error) {
            console.error("Failed to get access token:", error);
            this.isReceiving = false;
            this.cdr.detectChanges();
        }
    }

    handleEventData(data: any): void {
        var customEvent = data as any;

        if (customEvent.data.includes("---MessageCompleted---")) {
            this.isReceiving = false;
            this.eventSourceSubscription.unsubscribe();
            this.cdr.detectChanges();
            //return;
        }

        customEvent.data = customEvent.data.replace(/\\n/g, "\n");

        if (this.messageDto.Messages.length > 0) {
            var lastMessage = this.messageDto.Messages[this.messageDto.Messages.length - 1];
            let [intro, summaryHtml] = lastMessage.Content.split("---SUMMARY---");
            intro = intro ? intro.replace("---", "").replace("---SUMMARY", "").trim() : "";
            intro = intro ? intro.trim().replace(/(<br\s*\/?>\s*){1,2}$/i, "") : "";
            summaryHtml = summaryHtml ? summaryHtml.trim().replace(/^(<br\s*\/?>\s*){1,2}/i, "") : "";

            if (lastMessage.Role === "user") {
                // Parse and store intro/summaryHtml
                this.messageDto.Messages.push({
                    Role: "assistant",
                    Content: customEvent.data,
                    intro,
                    summaryHtml,
                    Date: new Date(),
                });
            } else {
                lastMessage.Content += customEvent.data;
                lastMessage.intro = intro;
                lastMessage.summaryHtml = summaryHtml;
            }
        }
        this.cdr.detectChanges();
    }

    reset(): void {
        this.messageDto = {
            Messages: [],
        };
        this.selectedPrompt = null;
        this.cdr.detectChanges(); // Trigger change detection to update the view
    }

    scrollToBottom(): void {
        try {
            this.mainScrollContainer.nativeElement.scrollTop = this.mainScrollContainer.nativeElement.scrollHeight;
            this.sidebarScrollContainer.nativeElement.scrollTop = this.sidebarScrollContainer.nativeElement.scrollHeight;
        } catch (err) {}
    }

    scrollMainTo(element: any): void {
        (document.getElementById(element) as HTMLElement).scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
    }

    copy(inputElement) {
        inputElement.select();
        document.execCommand("copy");
        inputElement.setSelectionRange(0, 0);
    }

    // --- Edit mode for AI responses ---
    public editingIndex: number | null = null;
    public editMessageValue: string = "";

    public editedIndexes: { [key: number]: boolean } = {};
    private originalSummaries: { [key: number]: string } = {};

    startEdit(index: number, value: string): void {
        this.editingIndex = index;
        this.editedIndexes[index] = false;
        this.cdr.detectChanges();
        // Scroll the TinyMCE editor into view after entering edit mode
        setTimeout(() => {
            const editorElement = document.querySelector('tinymce-editor[name="Description"]');
            if (editorElement) {
                (editorElement as HTMLElement).scrollIntoView({ behavior: "smooth", block: "center" });
            }
        }, 100);
    }

    onContentChange(newValue: string, index: number): void {
        if (!this.originalSummaries[index] || this.originalSummaries[index] === null) {
            // Store the original value when the user starts editing
            this.originalSummaries[index] = newValue;
        }

        this.editedIndexes[index] = newValue !== this.originalSummaries[index];
        this.cdr.detectChanges();
    }

    cancelEdit(index: number): void {
        if (this.editingIndex !== null) {
            this.messageDto.Messages[index].summaryHtml = this.originalSummaries[index];
            this.editingIndex = null;
            this.editedIndexes[index] = false;
            this.cdr.detectChanges();
        }
    }

    sanitizeSummaryHtml(html: string): SafeHtml {
        return this.sanitizer.bypassSecurityTrustHtml(html);
    }
}
