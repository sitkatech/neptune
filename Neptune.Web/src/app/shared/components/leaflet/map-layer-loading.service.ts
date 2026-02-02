import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, distinctUntilChanged, finalize, map, shareReplay } from "rxjs";

@Injectable()
export class MapLayerLoadingService {
    private readonly loadingCountSubject = new BehaviorSubject<number>(0);

    public readonly isLoading$: Observable<boolean> = this.loadingCountSubject.pipe(
        map((count) => count > 0),
        distinctUntilChanged(),
        shareReplay({ bufferSize: 1, refCount: true })
    );

    public track$<T>(source$: Observable<T>): Observable<T> {
        this.increment();
        return source$.pipe(finalize(() => this.decrement()));
    }

    /**
     * Marks a loading operation as in-progress and returns a function to mark it complete.
     * Safe to call the returned function multiple times.
     */
    public begin(): () => void {
        this.increment();

        let completed = false;
        return () => {
            if (completed) return;
            completed = true;
            this.decrement();
        };
    }

    private increment(): void {
        this.loadingCountSubject.next((this.loadingCountSubject.value ?? 0) + 1);
    }

    private decrement(): void {
        this.loadingCountSubject.next(Math.max(0, (this.loadingCountSubject.value ?? 0) - 1));
    }
}
