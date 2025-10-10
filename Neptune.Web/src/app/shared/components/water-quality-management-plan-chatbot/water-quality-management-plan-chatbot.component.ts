import { ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit } from "@angular/core";
import { ElementRef, ViewChild } from "@angular/core";
import { Observable, Subscription, SubscriptionLike, BehaviorSubject } from "rxjs";
import { map } from "rxjs/operators";
import { AuthenticationService } from "src/app/services/authentication.service";
import { EventSourceService } from "src/app/services/event-source.service";
import { environment } from "src/environments/environment";
import { ClipboardModule } from "@angular/cdk/clipboard";
import { AsyncPipe, DatePipe, CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { AiChatInputComponent } from "src/app/shared/components/ai-chat-input/ai-chat-input.component";
import { IconComponent } from "../icon/icon.component";
import { ChatMessageDto, ChatRequestDto, PersonDto, WaterQualityManagementPlanDocumentDto } from "../../generated/model/models";

@Component({
    selector: "water-quality-management-plan-chatbot",
    templateUrl: "water-quality-management-plan-chatbot.component.html",
    styleUrls: ["./water-quality-management-plan-chatbot.component.scss"],
    standalone: true,
    imports: [IconComponent, DatePipe, AsyncPipe, ClipboardModule, FormsModule, AiChatInputComponent, CommonModule],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WaterQualityManagementPlanChatbotComponent implements OnDestroy, OnInit {
    public assistantMessages$: Observable<ChatMessageDto[]>;
    private initialMessageSent = false;
    @ViewChild("main") mainScrollContainer: ElementRef;
    @ViewChild("sidebar") sidebarScrollContainer: ElementRef;

    private eventSourceSubscription: SubscriptionLike;
    public currentUser$: Observable<PersonDto>;
    @Input() waterQualityManagementPlanDocument: WaterQualityManagementPlanDocumentDto = null;
    private url: string = "";
    private isReceivingSubject = new BehaviorSubject<boolean>(false);
    public isReceiving$ = this.isReceivingSubject.asObservable();

    private initialExtractDataPrompt = `
        You are part of an automated workflow and should return only JSON in your response.
        Do not include any narrative and do not provide any follow-up suggestions.
        You are helping a stormwater technician extract data from a Water Quality Management Plan (WQMP).
        You will populate an existing JSON schema, and you must match this schema exactly.
        The WQMPs are for Orange County California, and used by the MS4 permitees including Orange County Public Works and cities.
        In the first step, you will extract the basic attributes of the WQMP. In follow-up prompts you will extract nested child records.
        Each attribute you extract will be placed in the "Value" property of the named attribute.
        The "ExtractionEvidence" will include a snippet from the WQMP PDF that shows the sentence in the WQMP where the data came from, plus the sentence before and after.
        If the attribute was extracted from a table or label just include relevant text nearby.\n\n
        The "DocumentSource" should include the Page # in the document where the attribute value was found.
        Here is the sub-schema for each extracted attribute:\n
        { "Value": "", "ExtractionEvidence": "", "DocumentSource": "" }\n\n
        A second LLM will be reviewing this work for accuracy, so please do not hallucinate data.
        If an attribute is not found simply put null in Value, ExtractionEvidence, and DocumentSource.
    `;

    private chatMessagesSubject = new BehaviorSubject<ChatMessageDto[]>([]);
    public chatMessages$: Observable<ChatMessageDto[]> = this.chatMessagesSubject.asObservable();
    public chatRequestDto = {
        Messages: [],
    };

    currentUserSubscription: Subscription;
    constructor(private authenticationService: AuthenticationService, private eventSourceService: EventSourceService, private sanitizer: DomSanitizer) {}

    ngOnInit(): void {
        this.currentUser$ = this.authenticationService.getCurrentUser();
        this.assistantMessages$ = this.chatMessages$.pipe(map((messages) => messages.filter((m) => m.Role === "assistant")));
    }

    ngOnChanges(changes: any): void {
        if (changes.waterQualityManagementPlanDocument && changes.waterQualityManagementPlanDocument.currentValue) {
            this.url =
                environment.mainAppApiUrl + "/ai/water-quality-management-plan-documents/" + this.waterQualityManagementPlanDocument?.WaterQualityManagementPlanDocumentID + "/ask";
            if (!this.initialMessageSent) {
                this.initialMessageSent = true;
                this.reset();
                this.send({ message: this.initialExtractDataPrompt });
            }
        }
    }

    ngOnDestroy(): void {
        this.eventSourceSubscription?.unsubscribe(); // Unsubscribe from any previous subscriptions
    }

    send(event: { message: string; knowledgeScope?: string }) {
        const message = event.message;
        const knowledgeScope = event.knowledgeScope;
        if (this.isReceivingSubject.value || !message) return; // If we are already receiving messages, do not send again
        this.chatRequestDto.Messages.push({
            Role: "user",
            Content: message,
            Date: new Date(),
        });
        this.chatMessagesSubject.next([...this.chatRequestDto.Messages]);
        this.sendMessages(this.chatRequestDto);
    }

    async sendMessages(messageDto: ChatRequestDto): Promise<void> {
        this.isReceivingSubject.next(true);
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
            this.chatRequestDto.Messages.push({
                Role: "assistant",
                Content: "",
                Date: new Date(),
            });
            this.chatMessagesSubject.next([...this.chatRequestDto.Messages]);
            this.eventSourceSubscription = this.eventSourceService.connectToServerSentEvents(this.url, options, eventNames).subscribe({
                next: (data) => {
                    this.handleEventData(data);
                    this.scrollToBottom(); // Scroll after each streamed chunk
                },
                error: (error) => {
                    console.error("[SSE] Received error:", error);
                    this.isReceivingSubject.next(false);
                    // Change detection handled by async pipe
                    if (this.eventSourceSubscription) {
                        this.eventSourceSubscription.unsubscribe();
                    }
                },
            });
            this.scrollToBottom(); // Scroll after sending prompt
        } catch (error) {
            console.error("[SSE] Failed to get access token:", error);
            this.isReceivingSubject.next(false);
        }
    }

    handleEventData(data: any): void {
        var customEvent = data as any;
        if (customEvent.data.includes("---MessageCompleted---")) {
            console.log("[SSE] MessageCompleted received, setting isReceiving to false");
            if (this.eventSourceSubscription) {
                this.eventSourceSubscription.unsubscribe();
            }
            this.isReceivingSubject.next(false);
            this.scrollToBottom(); // Scroll after response completes
            return;
        }

        customEvent.data = customEvent.data.replace(/\\n/g, "\n");

        if (this.chatRequestDto.Messages.length > 0) {
            // Always append streamed data to the last assistant message
            var lastMessage = this.chatRequestDto.Messages[this.chatRequestDto.Messages.length - 1];
            let [intro, summaryHtml] = lastMessage.Content.split("---SUMMARY---");
            intro = intro ? intro.replace("---", "").replace("---SUMMARY", "").trim() : "";
            intro = intro ? intro.trim().replace(/(<br\s*\/?>\s*){1,2}$/i, "") : "";
            summaryHtml = summaryHtml ? summaryHtml.trim().replace(/^(<br\s*\/?>\s*){1,2}/i, "") : "";

            // Only append to assistant message
            if (lastMessage.Role === "assistant") {
                lastMessage.Content += customEvent.data;
                lastMessage.Intro = intro;
                lastMessage.SummaryHtml = summaryHtml;
            }
        }
        this.chatMessagesSubject.next([...this.chatRequestDto.Messages]);
        this.scrollToBottom(); // Scroll after each streamed chunk
    }

    reset(): void {
        this.chatRequestDto = {
            Messages: [],
        };
        this.chatMessagesSubject.next([]);
        this.isReceivingSubject.next(false); // Always reset spinner
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

    sanitizeSummaryHtml(html: string): SafeHtml {
        return this.sanitizer.bypassSecurityTrustHtml(html);
    }

    exportChat(): void {
        // Simple export: download chat as JSON
        const dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(this.chatRequestDto.Messages, null, 2));
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
