import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import 'leaflet.fullscreen';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomRichTextType } from 'src/app/shared/models/enums/custom-rich-text-type.enum';
import { PersonDto } from 'src/app/shared/generated/model/person-dto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TreatmentBmpMapEditorAndModelingAttributesComponent } from 'src/app/shared/components/projects/treatment-bmp-map-editor-and-modeling-attributes/treatment-bmp-map-editor-and-modeling-attributes.component';
import { ProjectService } from 'src/app/shared/generated/api/project.service';

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
        this.projectService.projectsProjectIDGet(parseInt(projectID)).subscribe(project => {
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