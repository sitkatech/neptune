import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from "@angular/core";

import { FormsModule } from "@angular/forms";
import { IconComponent } from "../icon/icon.component";

export interface AiChatInputContext {
    showKnowledgeScope?: boolean;
    knowledgeScopeOptions?: { value: string; label: string; placeholder?: string; filters?: { [key: string]: any } }[];
    defaultScope?: string;
}

@Component({
    selector: "ai-chat-input",
    templateUrl: "./ai-chat-input.component.html",
    styleUrls: ["./ai-chat-input.component.scss"],
    standalone: true,
    imports: [FormsModule, IconComponent],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AiChatInputComponent implements AfterViewInit {
    @ViewChild("textarea") textarea!: ElementRef<HTMLTextAreaElement>;
    @Input() placeholder: string = "Type your message...";
    @Input() isReceiving: boolean = false;
    @Input() isDisabled: boolean = false;
    @Input() context?: AiChatInputContext;
    @Output() send = new EventEmitter<{ message: string; knowledgeScope?: string }>();
    @Output() knowledgeScopeChange = new EventEmitter<string>();

    knowledgeScope: string = "";

    constructor(private cdr: ChangeDetectorRef) {}

    ngAfterViewInit(): void {
        this.autoResize(); // Initial resize
        if (this.context && this.context.defaultScope) {
            this.knowledgeScope = this.context.defaultScope;
        } else if (this.context && this.context.knowledgeScopeOptions && this.context.knowledgeScopeOptions.length > 0) {
            this.knowledgeScope = this.context.knowledgeScopeOptions[0].value;
        }
        this.emitScopeChange();
        this.cdr.detectChanges();
    }

    onSend(message: string): void {
        if (message.trim() && !this.isReceiving) {
            this.send.emit({ message: message.trim(), knowledgeScope: this.knowledgeScope });
            this.textarea.nativeElement.value = "";
            this.autoResize();
        }
    }

    onScopeChange(event: any): void {
        this.knowledgeScope = event;
        this.emitScopeChange();
        this.cdr.detectChanges();
    }

    emitScopeChange() {
        this.knowledgeScopeChange.emit(this.knowledgeScope);
    }

    get effectivePlaceholder(): string {
        if (this.context && this.context.knowledgeScopeOptions) {
            const found = this.context.knowledgeScopeOptions.find((opt) => opt.value === this.knowledgeScope);
            if (found && found.placeholder) return found.placeholder;
        }
        return this.placeholder;
    }

    autoResize(): void {
        const el = this.textarea.nativeElement;
        el.style.height = "auto"; // Reset height
        el.style.height = `${el.scrollHeight}px`; // Set to scrollHeight
    }

    addNewLineToTextarea(textarea): void {
        textarea.value += "\n";
        this.cdr.detectChanges();
        this.autoResize();
    }
}
