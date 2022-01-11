export class CustomPageUpsertDto {
    CustomPageDisplayName : string
    CustomPageVanityUrl : string
    CustomPageContent : string
    MenuItemID : number
    SortOrder : number
    ViewableRoleIDs: Array<number>

    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}