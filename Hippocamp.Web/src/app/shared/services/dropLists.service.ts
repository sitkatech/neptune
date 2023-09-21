import { Injectable } from '@angular/core';

/**
 * This service allows us to dynamically connect all CdkDropLists to each other, to allow more complex interactions.
 * The provided 'dropListID' must be unique, and specific as id=<dropListID> on the cdkDropList container.
 */
@Injectable({
    providedIn: 'root'
})
export class DropListsService {
    public dropListIDs: string[] = [];

    public register(dropListID: string) {
        this.dropListIDs.unshift(dropListID);
    }

    public deregister(dropListID: string) {
        let index = this.dropListIDs.findIndex(x => x == dropListID);
        this.dropListIDs.splice(index, 1);
    }
}
