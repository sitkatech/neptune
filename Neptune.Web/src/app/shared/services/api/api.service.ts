import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, throwError as _throw } from "rxjs";
import { AlertService } from "../alert.service";
import { Alert } from "src/app/shared/models/alert";
import { OAuthService } from "angular-oauth2-oidc";
import { AlertContext } from "../../models/enums/alert-context.enum";

@Injectable({
    providedIn: "root",
})
export class ApiService {
    constructor(
        private alertService: AlertService,
        private oauthService: OAuthService,
        private router: Router
    ) {}

    handleError(error: any, supressErrorMessage = false): Observable<any> {
        if (!supressErrorMessage) {
            if (error && error.status === 401) {
                this.alertService.pushAlert(
                    new Alert("Access token expired...")
                );
                this.oauthService.initCodeFlow();
            } else if (error && error.status === 403) {
                this.router.navigate(["/"]).then(() => {
                    this.alertService.pushNotFoundUnauthorizedAlert();
                });
            } else if (error && error.status === 404) {
                // let the caller handle not found appropriate to whatever it was doing
            } else if (error.error && typeof error.error === "string") {
                //We shouldn't make any assumption about the kind of error, just send out the string
                this.alertService.pushAlert(
                    new Alert(error.error, AlertContext.Danger)
                );
            } else if (error.error && !(error.error instanceof ProgressEvent)) {
                //This is for handling errors that are generated by .NET
                //Eg. When model binding fails
                //https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0#automatic-http-400-responses
                if (
                    typeof error.error === "object" &&
                    error.error !== null &&
                    error.error.hasOwnProperty("Errors")
                ) {
                    const errorsList: {
                        groupName: string;
                        errorMessage: string;
                    }[] = [];
                    const errors = error.error.Errors;
                    console.log(errors);
                    for (const key of Object.keys(errors)) {
                        (errors[key] as string[]).forEach((fe: string) => {
                            const fieldNameParts: string[] = key.split(".");
                            if (fieldNameParts.length > 1) {
                                errorsList.push({
                                    groupName: fieldNameParts[0],
                                    errorMessage: fe,
                                });
                            } else {
                                errorsList.push({
                                    groupName: null,
                                    errorMessage:
                                        this.formatFieldName(
                                            fieldNameParts[0]
                                        ) +
                                        ": " +
                                        fe,
                                });
                            }
                        });
                    }
                    let errorsFormatted: string = errorsList
                        .filter((item) => item.groupName === null)
                        .map((item) => item.errorMessage)
                        .join("<br />");
                    if (errorsFormatted.length > 0) {
                        errorsFormatted += "<br />";
                    }
                    const uniqueGroupNames = [
                        ...new Set(
                            errorsList
                                .filter((item) => item.groupName !== null)
                                .map((item) => item.groupName)
                        ),
                    ];
                    uniqueGroupNames.forEach((groupName) => {
                        const groupNameFormatted =
                            this.formatGroupName(groupName);
                        const errorsForGroup = errorsList
                            .filter((e) => e.groupName === groupName)
                            .map((x) => "<li>" + x.errorMessage + "</li>")
                            .join("");
                        errorsFormatted +=
                            groupNameFormatted +
                            "<ul>" +
                            errorsForGroup +
                            "</ul>";
                    });
                    this.alertService.pushAlert(
                        new Alert(errorsFormatted, AlertContext.Danger)
                    );
                } else {
                    let errorString = this.errorStringFromObject(
                        error.error,
                        true
                    );
                    this.alertService.pushAlert(
                        new Alert(errorString, AlertContext.Danger)
                    );
                }
            } else {
                this.alertService.pushAlert(
                    new Alert(
                        "Oops! Something went wrong and we couldn't complete the action..."
                    )
                );
            }
        }

        return _throw(error);
    }

    private formatGroupName(groupName: string) {
        const groupNameParts = groupName.split("[");
        if (groupNameParts.length > 1) {
            return (
                this.formatFieldName(groupNameParts[0]) +
                " row " +
                (parseInt(
                    groupNameParts[1].substring(
                        0,
                        groupNameParts[1].indexOf("]")
                    )
                ) +
                    1)
            );
        } else {
            return this.formatFieldName(groupName);
        }
    }

    private formatFieldName(fieldName: string): string {
        const result = fieldName.replace(/([A-Z])/g, " $1");
        const finalResult = result.charAt(0).toUpperCase() + result.slice(1);
        return finalResult;
    }

    private errorStringFromObject(
        errorObject: any,
        includeIntro: boolean,
        nestedKey?: string
    ): string {
        let errorString = includeIntro
            ? "Oops! Something went wrong and we couldn't complete the action. If present, details are below: <br/><div class='ms-2'>"
            : "";
        for (const key of Object.keys(errorObject)) {
            if (typeof errorObject[key] !== "object") {
                errorString += `${nestedKey ? nestedKey : key} : ${
                    errorObject[key]
                } <br/>`;
            } else {
                errorString += this.errorStringFromObject(
                    errorObject[key],
                    false,
                    key
                );
            }
        }
        errorString += includeIntro ? "</div>" : "";
        return errorString;
    }
}
