import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { PersonCreateDto } from 'src/app/shared/generated/model/person-create-dto';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private apiService: ApiService) { }

    inviteUser(userInviteDto: any): Observable<PersonDto> {
        let route = `/users/invite`;
        return this.apiService.postToApi(route, userInviteDto);
    }

    createNewUser(userCreateDto: PersonCreateDto): Observable<PersonDto> {
        let route = `/users/`;
        return this.apiService.postToApi(route, userCreateDto);
    }

    getUsers(): Observable<PersonDto[]> {
        let route = `/users`;
        return this.apiService.getFromApi(route);
    }

    getUserFromUserID(userID: number): Observable<PersonDto> {
        let route = `/users/${userID}`;
        return this.apiService.getFromApi(route);
    }

    getUserFromGlobalID(globalID: string): Observable<PersonDto> {
        let route = `/user-claims/${globalID}`;
        return this.apiService.getFromApi(route);
    }

    updateUser(userID: number, userUpdateDto: any): Observable<PersonDto> {
        let route = `/users/${userID}`;
        return this.apiService.putToApi(route, userUpdateDto);
    }
}
