import { Injectable, NgZone } from "@angular/core";
import { Observable, Subscriber } from "rxjs";
import { SSE } from "sse.js";

/**
 * Server-Sent Events service
 */
@Injectable({
    providedIn: "root",
})
export class EventSourceService {
    private eventSource: SSE;

    /**
     * constructor
     * @param zone - we need to use zone while working with server-sent events
     * because it's an asynchronous operations which are run outside of change detection scope
     * and we need to notify Angular about changes related to SSE events
     */
    constructor(private zone: NgZone) {}

    /**
     * Method for creation of the EventSource instance
     * @param url - SSE server api path
     * @param options - configuration object for SSE
     */
    getEventSource(url: string, options: any): SSE {
        return new SSE(url, options);
    }

    /**
     * Method for establishing connection and subscribing to events from SSE
     * @param url - SSE server api path
     * @param options - configuration object for SSE
     * @param eventNames - all event names except error (listens by default) you want to listen to
     */
    connectToServerSentEvents(url: string, options: any, eventNames: string[] = []): Observable<Event> {
        this.eventSource = this.getEventSource(url, options);

        return new Observable((subscriber: Subscriber<Event>) => {
            this.eventSource.onerror = (error) => {
                this.zone.run(() => subscriber.error(error));
            };

            eventNames.forEach((event: string) => {
                this.eventSource.addEventListener(event, (data) => {
                    this.zone.run(() => subscriber.next(data));
                });
            });
        });
    }

    /**
     * Method for closing the connection
     */
    close(): void {
        if (!this.eventSource) {
            return;
        }

        this.eventSource.close();
        this.eventSource = null;
    }
}
