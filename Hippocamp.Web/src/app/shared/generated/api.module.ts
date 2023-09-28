import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { CustomRichTextService } from './api/custom-rich-text.service';
import { DelineationService } from './api/delineation.service';
import { FieldDefinitionService } from './api/field-definition.service';
import { FileResourceService } from './api/file-resource.service';
import { HealthService } from './api/health.service';
import { OrganizationService } from './api/organization.service';
import { ProjectService } from './api/project.service';
import { RoleService } from './api/role.service';
import { StormwaterJurisdictionService } from './api/stormwater-jurisdiction.service';
import { TreatmentBMPService } from './api/treatment-bmp.service';
import { UserService } from './api/user.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    CustomRichTextService,
    DelineationService,
    FieldDefinitionService,
    FileResourceService,
    HealthService,
    OrganizationService,
    ProjectService,
    RoleService,
    StormwaterJurisdictionService,
    TreatmentBMPService,
    UserService,
     ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
