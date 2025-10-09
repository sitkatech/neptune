import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnDestroy, OnInit, Optional } from "@angular/core";
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
import { PersonDto, WaterQualityManagementPlanDocumentDto } from "../../generated/model/models";

@Component({
    selector: "water-quality-management-plan-chatbot",
    templateUrl: "water-quality-management-plan-chatbot.component.html",
    styleUrls: ["./water-quality-management-plan-chatbot.component.scss"],
    standalone: true,
    imports: [IconComponent, IndexToCharPipe, DatePipe, AsyncPipe, ClipboardModule, FormsModule, AiChatInputComponent],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WaterQualityManagementPlanChatbotComponent implements OnDestroy, OnInit {
    @ViewChild("main") mainScrollContainer: ElementRef;
    @ViewChild("sidebar") sidebarScrollContainer: ElementRef;

    private eventSourceSubscription: SubscriptionLike;
    public currentUser$: Observable<PersonDto>;
    @Input() waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto = null;
    private url: string = "";
    public selectedPrompt: string = null;
    public isReceiving: boolean = false; // Flag to indicate if we are receiving messages

    public messageDto = {
        Messages: [],
    };

    public prompts: string[] = [
        "Please extract the WaterQualityManagementPlan and all related tables from the document, using only the schema structure from WaterQualityManagementPlanExtractDto. For each field, explain your rationale and provide a snippet from the document showing why you chose that value. Respond in a clear, conversational format, streaming your response as you work. Do not use any sample data or context values—only parse the document itself.",
        // "Summarize the key elements of this proposal, including the client's objective, the scope of work, primary tasks, timeline, and expected outcomes.",
        // "Write a plain-language summary of this proposal that explains what the waterqualitymanagementplan is about, why it matters, and what work will be done. Keep it accessible to a general audience with little or no technical background.",
        // "Provide a technical summary of the proposed work, including the methodology, deliverables, regulatory context (if relevant), and any notable data sources, tools, or models used. Focus on the implementation side of the waterqualitymanagementplan."
    ];

    currentUserSubscription: Subscription;
    constructor(
        @Optional() public dialogRef: DialogRef<any>,
        private authenticationService: AuthenticationService,
        private eventSourceService: EventSourceService,
        private cdr: ChangeDetectorRef,
        private sanitizer: DomSanitizer
    ) {
        let data: any = {};
        if (dialogRef) {
            data = dialogRef.data || {};
        }
        this.waterQualityManagementPlanDocument = data.KeyDocument || this.waterQualityManagementPlanDocument || null;
        // dialogRef.disableClose = true; // Uncomment if you want to prevent closing on backdrop click (ngneat/dialog does not support this directly)

        this.url =
            environment.mainAppApiUrl + "/ai/water-quality-management-plans-documents/" + this.waterQualityManagementPlanDocument?.WaterQualityManagementPlanDocumentID + "/ask";
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
        this.isReceiving = true;
        console.log("[SSE] isReceiving set to true, starting stream");
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
            // Always push a new assistant message when a new response starts
            this.messageDto.Messages.push({
                Role: "assistant",
                Content: "",
                Date: new Date(),
            });
            this.eventSourceSubscription = this.eventSourceService.connectToServerSentEvents(this.url, options, eventNames).subscribe({
                next: (data) => {
                    this.handleEventData(data);
                    this.scrollToBottom(); // Scroll after each streamed chunk
                },
                error: (error) => {
                    console.error("[SSE] Received error:", error);
                    this.isReceiving = false;
                    this.cdr.detectChanges();
                    if (this.eventSourceSubscription) {
                        this.eventSourceSubscription.unsubscribe();
                    }
                },
            });
            this.cdr.detectChanges();
            this.scrollToBottom(); // Scroll after sending prompt
        } catch (error) {
            console.error("[SSE] Failed to get access token:", error);
            this.isReceiving = false;
            this.cdr.detectChanges();
        }
    }

    handleEventData(data: any): void {
        var customEvent = data as any;
        if (customEvent.data.includes("---MessageCompleted---")) {
            console.log("[SSE] MessageCompleted received, setting isReceiving to false");
            if (this.eventSourceSubscription) {
                this.eventSourceSubscription.unsubscribe();
            }
            this.isReceiving = false;
            this.cdr.detectChanges();
            this.scrollToBottom(); // Scroll after response completes
            return;
        }

        customEvent.data = customEvent.data.replace(/\\n/g, "\n");

        if (this.messageDto.Messages.length > 0) {
            // Always append streamed data to the last assistant message
            var lastMessage = this.messageDto.Messages[this.messageDto.Messages.length - 1];
            let [intro, summaryHtml] = lastMessage.Content.split("---SUMMARY---");
            intro = intro ? intro.replace("---", "").replace("---SUMMARY", "").trim() : "";
            intro = intro ? intro.trim().replace(/(<br\s*\/?>\s*){1,2}$/i, "") : "";
            summaryHtml = summaryHtml ? summaryHtml.trim().replace(/^(<br\s*\/?>\s*){1,2}/i, "") : "";

            // Only append to assistant message
            if (lastMessage.Role === "assistant") {
                lastMessage.Content += customEvent.data;
                lastMessage.intro = intro;
                lastMessage.summaryHtml = summaryHtml;
            }
        }
        this.cdr.detectChanges();
        this.scrollToBottom(); // Scroll after each streamed chunk
    }

    reset(): void {
        this.messageDto = {
            Messages: [],
        };
        this.selectedPrompt = null;
        this.isReceiving = false; // Always reset spinner
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

    exportChat(): void {
        // Simple export: download chat as JSON
        const dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(this.messageDto.Messages, null, 2));
        const downloadAnchorNode = document.createElement("a");
        downloadAnchorNode.setAttribute("href", dataStr);
        downloadAnchorNode.setAttribute("download", "chat-history.json");
        document.body.appendChild(downloadAnchorNode);
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }

    showHelp(): void {
        alert("To use the chat, select a prompt or type your own question. Use the Reset button to start over, Export to save the conversation, and Jump to Response to navigate.");
    }
}
