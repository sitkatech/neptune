import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { CustomAttributeTypeService } from './api/custom-attribute-type.service';
import { CustomRichTextService } from './api/custom-rich-text.service';
import { DelineationService } from './api/delineation.service';
import { FieldDefinitionService } from './api/field-definition.service';
import { FileResourceService } from './api/file-resource.service';
import { LandUseBlockService } from './api/land-use-block.service';
import { NereidService } from './api/nereid.service';
import { OnlandVisualTrashAssessmentService } from './api/onland-visual-trash-assessment.service';
import { OnlandVisualTrashAssessmentAreaService } from './api/onland-visual-trash-assessment-area.service';
import { OnlandVisualTrashAssessmentObservationService } from './api/onland-visual-trash-assessment-observation.service';
import { OrganizationService } from './api/organization.service';
import { ProjectService } from './api/project.service';
import { ProjectDocumentService } from './api/project-document.service';
import { RegionalSubbasinService } from './api/regional-subbasin.service';
import { StormwaterJurisdictionService } from './api/stormwater-jurisdiction.service';
import { SystemInfoService } from './api/system-info.service';
import { TrashGeneratingUnitService } from './api/trash-generating-unit.service';
import { TrashGeneratingUnitByStormwaterJurisdictionService } from './api/trash-generating-unit-by-stormwater-jurisdiction.service';
import { TreatmentBMPService } from './api/treatment-bmp.service';
import { TreatmentBMPTypeService } from './api/treatment-bmp-type.service';
import { TreatmentBMPTypeCustomAttributeTypeService } from './api/treatment-bmp-type-custom-attribute-type.service';
import { UserService } from './api/user.service';
import { UserClaimsService } from './api/user-claims.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    CustomAttributeTypeService,
    CustomRichTextService,
    DelineationService,
    FieldDefinitionService,
    FileResourceService,
    LandUseBlockService,
    NereidService,
    OnlandVisualTrashAssessmentService,
    OnlandVisualTrashAssessmentAreaService,
    OnlandVisualTrashAssessmentObservationService,
    OrganizationService,
    ProjectService,
    ProjectDocumentService,
    RegionalSubbasinService,
    StormwaterJurisdictionService,
    SystemInfoService,
    TrashGeneratingUnitService,
    TrashGeneratingUnitByStormwaterJurisdictionService,
    TreatmentBMPService,
    TreatmentBMPTypeService,
    TreatmentBMPTypeCustomAttributeTypeService,
    UserService,
    UserClaimsService,
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
