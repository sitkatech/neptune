import { RoleDto } from "../generated/model/models";
import { CustomPageDetailedDto } from "./custom-page-detailed-dto"

export class CustomPageWithRolesDto extends CustomPageDetailedDto{
    public ViewableRoles: Array<RoleDto>;

    constructor(obj?: any){
        super();
        Object.assign(this, obj);
    }
}