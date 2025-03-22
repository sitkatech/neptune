import { Routes } from "@angular/router";
import { NotFoundComponent, UnauthenticatedComponent, SubscriptionInsufficientComponent } from "./shared/pages";
import { UnauthenticatedAccessGuard } from "./shared/guards/unauthenticated-access/unauthenticated-access.guard";
import { ManagerOnlyGuard } from "./shared/guards/unauthenticated-access/manager-only-guard";
import { HomeIndexComponent } from "./pages/home/home-index/home-index.component";
import { LoginCallbackComponent } from "./pages/login-callback/login-callback.component";
import { CreateUserCallbackComponent } from "./pages/create-user-callback/create-user-callback.component";
import { FieldDefinitionListComponent } from "./pages/field-definition-list/field-definition-list.component";
import { FieldDefinitionEditComponent } from "./pages/field-definition-edit/field-definition-edit.component";
import { TrainingComponent } from "./pages/planning-module/training/training.component";
import { ProjectListComponent } from "./pages/planning-module/projects/project-list/project-list.component";
import { PlanningAboutComponent } from "./pages/planning-module/planning-about/planning-about.component";
import { ProjectWorkflowOutletComponent } from "./pages/planning-module/projects/project-workflow/project-workflow-outlet.component";
import { JurisdictionManagerOrEditorOnlyGuard } from "./shared/guards/unauthenticated-access/jurisdiction-manager-or-editor-only-guard.guard";
import { ProjectInstructionsComponent } from "./pages/planning-module/projects/project-workflow/project-instructions/project-instructions.component";
import { ProjectBasicsComponent } from "./pages/planning-module/projects/project-workflow/project-basics/project-basics.component";
import { TreatmentBmpsComponent } from "./pages/planning-module/projects/project-workflow/treatment-bmps/treatment-bmps.component";
import { ProjectAttachmentsComponent } from "./pages/planning-module/projects/project-workflow/project-attachments/project-attachments.component";
import { DelineationsComponent } from "./pages/planning-module/projects/project-workflow/delineations/delineations.component";
import { ModeledPerformanceComponent } from "./pages/planning-module/projects/project-workflow/modeled-performance/modeled-performance.component";
import { UnsavedChangesGuard } from "./shared/guards/unsaved-changes.guard";
import { ProjectDetailComponent } from "./pages/planning-module/projects/project-detail/project-detail.component";
import { ReviewComponent } from "./pages/planning-module/projects/project-workflow/review/review.component";
import { PlanningMapComponent } from "./pages/planning-module/planning-map/planning-map.component";
import { OCTAM2Tier2DashboardComponent } from "./pages/planning-module/grant-programs/octa-m2-tier2-dashboard/octa-m2-tier2-dashboard.component";
import { OCTAGrantReviewerOnlyGuard } from "./shared/guards/unauthenticated-access/octa-grant-reviewer-only.guard";
import { PlanningSiteLayoutComponent } from "./pages/planning-module/planning-site-layout/planning-site-layout.component";
import { TrashSiteLayoutComponent } from "./pages/trash-module/trash-site-layout/trash-site-layout.component";
import { TrashHomeComponent } from "./pages/trash-module/trash-home/trash-home.component";
import { PLanningHomeComponent as PlanningHomeComponent } from "./pages/planning-module/planning-home/planning-home/planning-home.component";
import { SiteLayoutComponent } from "./pages/site-layout/site-layout.component";
import { AboutComponent } from "./pages/about/about.component";
import { ModelingAboutComponent } from "./pages/modeling-about/modeling-about.component";
import { TrashOvtaIndexComponent } from "./pages/trash-module/ovtas/trash-ovta-index/trash-ovta-index.component";
import { TrashOvtaAreaDetailComponent } from "./pages/trash-module/ovtas/trash-ovta-area-detail/trash-ovta-area-detail.component";
import { TrashOvtaDetailComponent } from "./pages/trash-module/ovtas/trash-ovta-detail/trash-ovta-detail.component";
import { TrashLandUseBlockIndexComponent } from "./pages/trash-module/trash-land-use-block-index/trash-land-use-block-index.component";
import { TrashTrashGeneratingUnitIndexComponent } from "./pages/trash-module/trash-trash-generating-unit-index/trash-trash-generating-unit-index.component";
import { TrashOvtaAreaIndexComponent } from "./pages/trash-module/ovtas/trash-ovta-area-index/trash-ovta-area-index.component";
import { TrashOvtaAreaEditLocationComponent } from "./pages/trash-module/ovtas/trash-ovta-area-edit-location/trash-ovta-area-edit-location.component";
import { TrashOvtaWorkflowOutletComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-workflow-outlet/trash-ovta-workflow-outlet.component";
import { TrashOvtaInstructionsComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-instructions/trash-ovta-instructions.component";
import { TrashInitiateOvtaComponent } from "./pages/trash-module/ovta-workflow/trash-initiate-ovta/trash-initiate-ovta.component";
import { TrashOvtaRecordObservationsComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-record-observations/trash-ovta-record-observations.component";
import { TrashOvtaReviewAndFinalizeComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-review-and-finalize/trash-ovta-review-and-finalize.component";
import { TrashOvtaAddRemoveParcelsComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-add-remove-parcels/trash-ovta-add-remove-parcels.component";
import { TrashOvtaRefineAssessmentAreaComponent } from "./pages/trash-module/ovta-workflow/trash-ovta-refine-assessment-area/trash-ovta-refine-assessment-area.component";

export const routeParams = {
    definitionID: "definitionID",
    projectID: "projectID",
    onlandVisualTrashAssessmentID: "onlandVisualTrashAssessmentID",
    onlandVisualTrashAssessmentAreaID: "onlandVisualTrashAssessmentAreaID",
};

export const routes: Routes = [
    {
        path: `planning`,
        title: "Stormwater Tools",
        component: PlanningSiteLayoutComponent,
        children: [
            { path: "", title: "Home", component: PlanningHomeComponent },
            { path: "about", component: PlanningAboutComponent, canActivate: [UnauthenticatedAccessGuard] },
            {
                path: "grant-programs",
                title: "Grant Program",
                canActivate: [UnauthenticatedAccessGuard, OCTAGrantReviewerOnlyGuard],
                children: [{ path: "octa-m2-tier-2", component: OCTAM2Tier2DashboardComponent }],
            },
            {
                path: "projects",
                title: "Projects",
                canActivate: [UnauthenticatedAccessGuard],
                children: [
                    { path: "", component: ProjectListComponent, canActivate: [JurisdictionManagerOrEditorOnlyGuard] },
                    {
                        path: "new",
                        component: ProjectWorkflowOutletComponent,
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            { path: "instructions", title: "Instructions", component: ProjectInstructionsComponent },
                            { path: "project-basics", title: "Basics", component: ProjectBasicsComponent, canDeactivate: [UnsavedChangesGuard] },
                        ],
                    },
                    {
                        path: `edit/:${routeParams.projectID}`,
                        component: ProjectWorkflowOutletComponent,
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            { path: "instructions", title: "Instructions", component: ProjectInstructionsComponent },
                            { path: "project-basics", title: "Basics", component: ProjectBasicsComponent, canDeactivate: [UnsavedChangesGuard] },
                            {
                                path: "stormwater-treatments",
                                children: [
                                    { path: "", redirectTo: "treatment-bmps", pathMatch: "full" },
                                    { path: "treatment-bmps", title: "Treatment BMPs", component: TreatmentBmpsComponent, canDeactivate: [UnsavedChangesGuard] },
                                    { path: "delineations", title: "Delineations", component: DelineationsComponent, canDeactivate: [UnsavedChangesGuard] },
                                    { path: "modeled-performance-and-metrics", title: "Modeled Performance and Metrics", component: ModeledPerformanceComponent },
                                ],
                            },
                            { path: "attachments", title: "Attachments", component: ProjectAttachmentsComponent },
                            { path: "review-and-share", title: "Review and Share", component: ReviewComponent },
                        ],
                    },
                    { path: `:${routeParams.projectID}`, component: ProjectDetailComponent },
                ],
            },
            { path: "planning-map", title: "Planning Map", component: PlanningMapComponent, canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard] },
            { path: "training", title: "Training", component: TrainingComponent, canActivate: [UnauthenticatedAccessGuard] },
        ],
    },
    {
        path: `trash`,
        title: "Stormwater Tools",
        component: TrashSiteLayoutComponent,
        children: [
            { path: "", title: "Home", component: TrashHomeComponent },
            {
                path: "land-use-blocks",
                title: "Land Use Blocks",
                component: TrashLandUseBlockIndexComponent,
                canActivate: [UnauthenticatedAccessGuard],
            },
            {
                path: "onland-visual-trash-assessments",
                title: "OVTAs",
                children: [
                    { path: "", component: TrashOvtaIndexComponent, canActivate: [UnauthenticatedAccessGuard] },
                    { path: `:${routeParams.onlandVisualTrashAssessmentID}`, component: TrashOvtaDetailComponent, canActivate: [UnauthenticatedAccessGuard] },
                ],
            },
            {
                path: "onland-visual-trash-assessments/new",
                title: "OVTAs",
                component: TrashOvtaWorkflowOutletComponent,
                children: [
                    { path: "", redirectTo: "instructions", pathMatch: "full" },
                    { path: "instructions", title: "Instructions", component: TrashOvtaInstructionsComponent, canActivate: [UnauthenticatedAccessGuard] },
                    { path: "initiate-ovta", title: "Initiate OVTA", component: TrashInitiateOvtaComponent, canActivate: [UnauthenticatedAccessGuard] },
                ],
            },
            {
                path: `onland-visual-trash-assessments/edit/:${routeParams.onlandVisualTrashAssessmentID}`,
                title: "OVTAs",
                component: TrashOvtaWorkflowOutletComponent,
                children: [
                    { path: "", redirectTo: "instructions", pathMatch: "full" },
                    { path: "instructions", title: "Instructions", component: TrashOvtaInstructionsComponent, canActivate: [UnauthenticatedAccessGuard] },
                    { path: "initiate-ovta", title: "Initiate OVTA", component: TrashInitiateOvtaComponent, canActivate: [UnauthenticatedAccessGuard] },
                    { path: "record-observations", title: "Record Observations", component: TrashOvtaRecordObservationsComponent, canActivate: [UnauthenticatedAccessGuard] },
                    { path: "add-or-remove-parcels", title: "Add or Remove Parcels", component: TrashOvtaAddRemoveParcelsComponent, canActivate: [UnauthenticatedAccessGuard] },
                    {
                        path: "refine-assessment-area",
                        title: "Refine Assessment Area",
                        component: TrashOvtaRefineAssessmentAreaComponent,
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    { path: "review-and-finalize", title: "Review and Finalize", component: TrashOvtaReviewAndFinalizeComponent, canActivate: [UnauthenticatedAccessGuard] },
                ],
            },

            {
                path: "onland-visual-trash-assessment-areas",
                title: "OVTA Areas",
                children: [
                    { path: "", component: TrashOvtaAreaIndexComponent, canActivate: [UnauthenticatedAccessGuard] },
                    {
                        path: `:${routeParams.onlandVisualTrashAssessmentAreaID}`,
                        component: TrashOvtaAreaDetailComponent,
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: `:${routeParams.onlandVisualTrashAssessmentAreaID}/edit-location`,
                        component: TrashOvtaAreaEditLocationComponent,
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                ],
            },
            {
                path: "trash-analysis-areas",
                title: "Trash Analysis Areas",
                component: TrashTrashGeneratingUnitIndexComponent,
                canActivate: [UnauthenticatedAccessGuard],
            },
        ],
    },

    { path: "create-user-callback", component: CreateUserCallbackComponent },
    { path: "signin-oidc", component: LoginCallbackComponent },
    {
        path: ``,
        title: "Stormwater Tools",
        component: SiteLayoutComponent,
        children: [
            { path: "", component: HomeIndexComponent },
            { path: "about", component: AboutComponent },
            { path: "modeling", component: ModelingAboutComponent },
            { path: `labels-and-definitions/:${routeParams.definitionID}`, component: FieldDefinitionEditComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
            {
                path: "labels-and-definitions",
                title: "Labels and Definitions",
                component: FieldDefinitionListComponent,
                canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard],
            },
        ],
    },
    { path: "not-found", component: NotFoundComponent },
    { path: "subscription-insufficient", component: SubscriptionInsufficientComponent },
    { path: "unauthenticated", component: UnauthenticatedComponent },
    { path: "**", component: NotFoundComponent },
];
