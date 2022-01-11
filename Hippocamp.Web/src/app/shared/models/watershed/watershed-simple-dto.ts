export class WatershedSimpleDto {
    WatershedID: number;
    WatershedName: string;

    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}