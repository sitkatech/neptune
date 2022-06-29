import { ApplicationRef, ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import * as L from 'leaflet';
import {
  Control, FitBoundsOptions,
  GeoJSON,
  marker,
  map,
  Map,
  MapOptions,
  tileLayer,
  geoJSON,
  icon,
  latLng,
  Layer,
  DomEvent,
  DomUtil,
  WMSOptions,
  LeafletEvent
} from 'leaflet';
import 'leaflet.fullscreen';
import * as esri from 'esri-leaflet';
import { CustomCompileService } from 'src/app/shared/services/custom-compile.service';
import { BoundingBoxDto } from 'src/app/shared/generated/model/bounding-box-dto';
import { Feature } from 'geojson';
import { TreatmentBMPService } from 'src/app/services/treatment-bmp/treatment-bmp.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TreatmentBMPUpsertDto } from 'src/app/shared/generated/model/treatment-bmp-upsert-dto';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { StormwaterJurisdictionService } from 'src/app/services/stormwater-jurisdiction/stormwater-jurisdiction.service';
import { forkJoin } from 'rxjs';
import { TreatmentBMPTypeSimpleDto } from 'src/app/shared/generated/model/treatment-bmp-type-simple-dto';
import { TreatmentBMPModelingType } from 'src/app/shared/models/enums/treatment-bmp-modeling-type.enum';
import { TreatmentBMPModelingAttributeDropdownItemDto } from 'src/app/shared/generated/model/treatment-bmp-modeling-attribute-dropdown-item-dto';
import { environment } from 'src/environments/environment';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Alert } from 'src/app/shared/models/alert';
import { AlertContext } from 'src/app/shared/models/enums/alert-context.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { FieldDefinitionTypeEnum } from 'src/app/shared/models/enums/field-definition-type.enum';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TimeOfConcentrationEnum } from 'src/app/shared/models/enums/time-of-concentration.enum';
import { UnderlyingHydrologicSoilGroupEnum } from 'src/app/shared/models/enums/underlying-hydrologic-soil-group.enum';
import { DelineationService } from 'src/app/services/delineation.service';
import { DelineationUpsertDto } from 'src/app/shared/generated/model/delineation-upsert-dto';
import { ProjectUpsertDto } from 'src/app/shared/generated/model/project-upsert-dto';
import { ProjectService } from 'src/app/services/project/project.service';
import { ProjectSimpleDto } from 'src/app/shared/generated/model/project-simple-dto';
import { ProjectWorkflowService } from 'src/app/services/project-workflow.service';
import { TreatmentBmpMapEditorAndModelingAttributesComponent } from 'src/app/shared/components/projects/treatment-bmp-map-editor-and-modeling-attributes/treatment-bmp-map-editor-and-modeling-attributes.component';

declare var $: any

@Component({
  selector: 'hippocamp-treatment-bmps',
  templateUrl: './treatment-bmps.component.html',
  styleUrls: ['./treatment-bmps.component.scss']
})
export class TreatmentBmpsComponent implements OnInit {

  @ViewChild('treatmentBMPMapAndModelingAttributes') treatmentBMPMapAndModelingAttributes: TreatmentBmpMapEditorAndModelingAttributesComponent;
  private currentUser: PersonDto;
  public projectID: number;
  public customRichTextTypeID = CustomRichTextType.TreatmentBMPs;

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private projectService: ProjectService,
    private router: Router
  ) { }

  canExit(){
    if (this.projectID) {
      return this.treatmentBMPMapAndModelingAttributes.unsavedChangesCheck();
    }
    return true;
  };

  public ngOnInit(): void {
    this.authenticationService.getCurrentUser().subscribe(currentUser => {
      this.currentUser = currentUser;

      const projectID = this.route.snapshot.paramMap.get("projectID");
      if (projectID) {
        this.projectService.getByID(parseInt(projectID)).subscribe(project => {
          // redirect to review step if project is shared with OCTA grant program
          if (project.ShareOCTAM2Tier2Scores) {
            this.router.navigateByUrl(`projects/edit/${projectID}/review-and-share`);
          } else {
            this.projectID = parseInt(projectID);
          }
        });
        this.cdr.detectChanges();
      }
    });
  }
}