import { UserDto } from "../../generated/model/models";

export class UserDetailedDto extends UserDto {
    FullName : string;

    constructor(obj?: any) {
        super()
        Object.assign(this, obj);
    }
}

