import { CustomRichTextDto } from "../generated/model/models";

export class CustomRichTextDetailedDto extends CustomRichTextDto{
    public IsEmptyContent: boolean;

    constructor(obj?: any){
        super();
        Object.assign(this, obj);
    }
}