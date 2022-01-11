import { CustomPageDto } from "../generated/model/models";

export class CustomPageDetailedDto extends CustomPageDto{
    public IsEmptyContent: boolean;

    constructor(obj?: any){
        super();
        Object.assign(this, obj);
    }
}