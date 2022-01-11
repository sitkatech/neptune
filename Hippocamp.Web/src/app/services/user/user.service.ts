import { Injectable } from '@angular/core';
import { UserDetailedDto } from 'src/app/shared/models';
import { ApiService } from 'src/app/shared/services';
import { Observable } from 'rxjs';
import { UserCreateDto } from 'src/app/shared/models/user/user-create-dto';
import { UnassignedUserReportDto } from 'src/app/shared/models/user/unassigned-user-report-dto';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private apiService: ApiService) { }

    inviteUser(userInviteDto: any): Observable<UserDetailedDto> {
        let route = `/users/invite`;
        return this.apiService.postToApi(route, userInviteDto);
    }

    createNewUser(userCreateDto: UserCreateDto): Observable<UserDetailedDto> {
        let route = `/users/`;
        return this.apiService.postToApi(route, userCreateDto);
    }

    getUsers(): Observable<UserDetailedDto[]> {
        let route = `/users`;
        return this.apiService.getFromApi(route);
    }

    getUserFromUserID(userID: number): Observable<UserDetailedDto> {
        let route = `/users/${userID}`;
        return this.apiService.getFromApi(route);
    }

    getUserFromGlobalID(globalID: string): Observable<UserDetailedDto> {
        let route = `/user-claims/${globalID}`;
        return this.apiService.getFromApi(route);
    }

    updateUser(userID: number, userUpdateDto: any): Observable<UserDetailedDto> {
        let route = `/users/${userID}`;
        return this.apiService.putToApi(route, userUpdateDto);
    }
    
    getUnassignedUserReport(): Observable<UnassignedUserReportDto> {
        let route = `/users/unassigned-report`;
        return this.apiService.getFromApi(route);
    }

    setDisclaimerAcknowledgedDate(userID: number): Observable<UserDetailedDto> {
        let route = `/users/set-disclaimer-acknowledged-date`
        return this.apiService.putToApi(route, userID);
    }

    impersonateUser(userID: number): Observable<UserDetailedDto> {
        let route = `/impersonate/${userID}`;
        return this.apiService.postToApi(route, {});
    }

    stopImpersonation(): Observable<UserDetailedDto> {
        let route = `/impersonate/stop-impersonation`;
        return this.apiService.postToApi(route, {});
    }

}
