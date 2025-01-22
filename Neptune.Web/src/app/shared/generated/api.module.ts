import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { CustomRichTextService } from './api/custom-rich-text.service';
import { DelineationService } from './api/delineation.service';
import { FieldDefinitionService } from './api/field-definition.service';
import { FileResourceService } from './api/file-resource.service';
import { NereidService } from './api/nereid.service';
import { OrganizationService } from './api/organization.service';
import { ProjectService } from './api/project.service';
import { ProjectDocumentService } from './api/project-document.service';
import { StormwaterJurisdictionService } from './api/stormwater-jurisdiction.service';
import { SystemInfoService } from './api/system-info.service';
import { TreatmentBMPService } from './api/treatment-bmp.service';
import { TreatmentBMPTypeService } from './api/treatment-bmp-type.service';
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
    NereidService,
    OrganizationService,
    ProjectService,
    ProjectDocumentService,
    StormwaterJurisdictionService,
    SystemInfoService,
    TreatmentBMPService,
    TreatmentBMPTypeService,
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
