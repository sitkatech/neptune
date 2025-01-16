import { Routes } from "@angular/router";
import { NotFoundComponent, UnauthenticatedComponent, SubscriptionInsufficientComponent } from "./shared/pages";
import { UnauthenticatedAccessGuard } from "./shared/guards/unauthenticated-access/unauthenticated-access.guard";
import { ManagerOnlyGuard } from "./shared/guards/unauthenticated-access/manager-only-guard";
import { HomeIndexComponent } from "./pages/planning-module/home/home-index/home-index.component";
import { LoginCallbackComponent } from "./pages/login-callback/login-callback.component";
import { CreateUserCallbackComponent } from "./pages/create-user-callback/create-user-callback.component";
import { FieldDefinitionListComponent } from "./pages/field-definition-list/field-definition-list.component";
import { FieldDefinitionEditComponent } from "./pages/field-definition-edit/field-definition-edit.component";
import { TrainingComponent } from "./pages/planning-module/training/training.component";
import { ProjectListComponent } from "./pages/planning-module/projects/project-list/project-list.component";
import { AboutComponent } from "./pages/planning-module/about/about.component";
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
import { SiteLayoutComponent } from "./pages/planning-module/site-layout/site-layout.component";
import { TrashSiteLayoutComponent } from "./pages/trash-module/trash-site-layout/trash-site-layout.component";
import { TrashHomeComponent } from "./pages/trash-module/trash-home/trash-home.component";
import { TrashAboutComponent } from "./pages/trash-module/trash-about/trash-about.component";

export const routeParams = {
    definitionID: "definitionID",
    projectID: "projectID",
};

export const routes: Routes = [
    {
        path: `planning`,
        title: "Planning Module",
        component: SiteLayoutComponent,
        children: [
            { path: "", title: "Home", component: HomeIndexComponent },
            { path: `labels-and-definitions/:${routeParams.definitionID}`, component: FieldDefinitionEditComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
            { path: "labels-and-definitions", component: FieldDefinitionListComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
            {
                path: "projects",
                canActivate: [UnauthenticatedAccessGuard],
                children: [
                    { path: "", component: ProjectListComponent, canActivate: [JurisdictionManagerOrEditorOnlyGuard] },
                    {
                        path: "new",
                        component: ProjectWorkflowOutletComponent,
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            { path: "instructions", component: ProjectInstructionsComponent },
                            { path: "project-basics", component: ProjectBasicsComponent, canDeactivate: [UnsavedChangesGuard] },
                        ],
                    },
                    {
                        path: `edit/:${routeParams.projectID}`,
                        component: ProjectWorkflowOutletComponent,
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            { path: "instructions", component: ProjectInstructionsComponent },
                            { path: "project-basics", component: ProjectBasicsComponent, canDeactivate: [UnsavedChangesGuard] },
                            {
                                path: "stormwater-treatments",
                                children: [
                                    { path: "", redirectTo: "treatment-bmps", pathMatch: "full" },
                                    { path: "treatment-bmps", component: TreatmentBmpsComponent, canDeactivate: [UnsavedChangesGuard] },
                                    { path: "delineations", component: DelineationsComponent, canDeactivate: [UnsavedChangesGuard] },
                                    { path: "modeled-performance-and-metrics", component: ModeledPerformanceComponent },
                                ],
                            },
                            { path: "attachments", component: ProjectAttachmentsComponent },
                            { path: "review-and-share", component: ReviewComponent },
                        ],
                    },
                    { path: `:${routeParams.projectID}`, component: ProjectDetailComponent },
                ],
            },
            {
                path: "grant-programs",
                canActivate: [UnauthenticatedAccessGuard, OCTAGrantReviewerOnlyGuard],
                children: [{ path: "octa-m2-tier-2", component: OCTAM2Tier2DashboardComponent }],
            },
            { path: "planning-map", component: PlanningMapComponent, canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard] },
            { path: "training", component: TrainingComponent, canActivate: [UnauthenticatedAccessGuard] },
            { path: "about", component: AboutComponent, canActivate: [UnauthenticatedAccessGuard] },
        ],
    },
    {
        path: `trash`,
        title: "Trash Module",
        component: TrashSiteLayoutComponent,
        children: [
            { path: "", title: "Home", component: TrashHomeComponent },
            { path: `labels-and-definitions/:${routeParams.definitionID}`, component: FieldDefinitionEditComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
            { path: "labels-and-definitions", component: FieldDefinitionListComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
            { path: "about", component: TrashAboutComponent, canActivate: [UnauthenticatedAccessGuard] },
        ],
    },

    { path: "create-user-callback", component: CreateUserCallbackComponent },
    { path: "not-found", component: NotFoundComponent },
    { path: "subscription-insufficient", component: SubscriptionInsufficientComponent },
    { path: "unauthenticated", component: UnauthenticatedComponent },
    { path: "signin-oidc", component: LoginCallbackComponent },
    { path: "", redirectTo: "/planning", pathMatch: "full" },
    { path: "**", component: NotFoundComponent },
];
