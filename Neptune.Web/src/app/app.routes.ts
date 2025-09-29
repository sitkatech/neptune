import { Routes } from "@angular/router";
import { UnauthenticatedAccessGuard } from "./shared/guards/unauthenticated-access/unauthenticated-access.guard";
import { ManagerOnlyGuard } from "./shared/guards/unauthenticated-access/manager-only-guard";
import { JurisdictionManagerOrEditorOnlyGuard } from "./shared/guards/unauthenticated-access/jurisdiction-manager-or-editor-only-guard.guard";
import { UnsavedChangesGuard } from "./shared/guards/unsaved-changes.guard";
import { OCTAGrantReviewerOnlyGuard } from "./shared/guards/unauthenticated-access/octa-grant-reviewer-only.guard";

export const routeParams = {
    definitionID: "definitionID",
    projectID: "projectID",
    onlandVisualTrashAssessmentID: "onlandVisualTrashAssessmentID",
    onlandVisualTrashAssessmentAreaID: "onlandVisualTrashAssessmentAreaID",
};

export const routes: Routes = [
    {
        path: "ai",
        title: "AI Module",
        loadComponent: () => import("./pages/ai-module/ai-site-layout.component").then((m) => m.AiSiteLayoutComponent),
        children: [
            {
                path: "",
                title: "AI Home",
                loadComponent: () => import("./pages/ai-module/ai-home/ai-home.component").then((m) => m.AiHomeComponent),
            },
        ],
    },
    {
        path: `planning`,
        title: "Stormwater Tools",
        loadComponent: () => import("./pages/planning-module/planning-site-layout/planning-site-layout.component").then((m) => m.PlanningSiteLayoutComponent),
        children: [
            {
                path: "",
                title: "Home",
                loadComponent: () => import("./pages/planning-module/planning-home/planning-home/planning-home.component").then((m) => m.PLanningHomeComponent),
            },
            {
                path: "about",
                loadComponent: () => import("./pages/planning-module/planning-about/planning-about.component").then((m) => m.PlanningAboutComponent),
                canActivate: [UnauthenticatedAccessGuard],
            },
            {
                path: "grant-programs",
                title: "Grant Program",
                canActivate: [UnauthenticatedAccessGuard, OCTAGrantReviewerOnlyGuard],
                children: [
                    {
                        path: "octa-m2-tier-2",
                        loadComponent: () =>
                            import("./pages/planning-module/grant-programs/octa-m2-tier2-dashboard/octa-m2-tier2-dashboard.component").then((m) => m.OCTAM2Tier2DashboardComponent),
                    },
                ],
            },
            {
                path: "projects",
                title: "Projects",
                canActivate: [UnauthenticatedAccessGuard],
                children: [
                    {
                        path: "",
                        loadComponent: () => import("./pages/planning-module/projects/project-list/project-list.component").then((m) => m.ProjectListComponent),
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                    },
                    {
                        path: "new",
                        loadComponent: () => import("./pages/planning-module/project-workflow/project-workflow-outlet.component").then((m) => m.ProjectWorkflowOutletComponent),
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            {
                                path: "instructions",
                                title: "Instructions",
                                loadComponent: () =>
                                    import("./pages/planning-module/project-workflow/project-instructions/project-instructions.component").then(
                                        (m) => m.ProjectInstructionsComponent
                                    ),
                            },
                            {
                                path: "project-basics",
                                title: "Basics",
                                loadComponent: () =>
                                    import("./pages/planning-module/project-workflow/project-basics/project-basics.component").then((m) => m.ProjectBasicsComponent),
                                canDeactivate: [UnsavedChangesGuard],
                            },
                        ],
                    },
                    {
                        path: `edit/:${routeParams.projectID}`,
                        loadComponent: () => import("./pages/planning-module/project-workflow/project-workflow-outlet.component").then((m) => m.ProjectWorkflowOutletComponent),
                        canActivate: [JurisdictionManagerOrEditorOnlyGuard],
                        children: [
                            { path: "", redirectTo: "instructions", pathMatch: "full" },
                            {
                                path: "instructions",
                                title: "Instructions",
                                loadComponent: () =>
                                    import("./pages/planning-module/project-workflow/project-instructions/project-instructions.component").then(
                                        (m) => m.ProjectInstructionsComponent
                                    ),
                            },
                            {
                                path: "project-basics",
                                title: "Basics",
                                loadComponent: () =>
                                    import("./pages/planning-module/project-workflow/project-basics/project-basics.component").then((m) => m.ProjectBasicsComponent),
                                canDeactivate: [UnsavedChangesGuard],
                            },
                            {
                                path: "stormwater-treatments",
                                children: [
                                    { path: "", redirectTo: "treatment-bmps", pathMatch: "full" },
                                    {
                                        path: "treatment-bmps",
                                        title: "Treatment BMPs",
                                        loadComponent: () =>
                                            import("./pages/planning-module/project-workflow/treatment-bmps/treatment-bmps.component").then((m) => m.TreatmentBmpsComponent),
                                        canDeactivate: [UnsavedChangesGuard],
                                    },
                                    {
                                        path: "delineations",
                                        title: "Delineations",
                                        loadComponent: () =>
                                            import("./pages/planning-module/project-workflow/delineations/delineations.component").then((m) => m.DelineationsComponent),
                                        canDeactivate: [UnsavedChangesGuard],
                                    },
                                    {
                                        path: "modeled-performance-and-metrics",
                                        title: "Modeled Performance and Metrics",
                                        loadComponent: () =>
                                            import("./pages/planning-module/project-workflow/modeled-performance/modeled-performance.component").then(
                                                (m) => m.ModeledPerformanceComponent
                                            ),
                                    },
                                ],
                            },
                            {
                                path: "attachments",
                                title: "Attachments",
                                loadComponent: () =>
                                    import("./pages/planning-module/project-workflow/project-attachments/project-attachments.component").then((m) => m.ProjectAttachmentsComponent),
                            },
                            {
                                path: "review-and-share",
                                title: "Review and Share",
                                loadComponent: () => import("./pages/planning-module/project-workflow/review/review.component").then((m) => m.ReviewComponent),
                            },
                        ],
                    },
                    {
                        path: `:${routeParams.projectID}`,
                        loadComponent: () => import("./pages/planning-module/projects/project-detail/project-detail.component").then((m) => m.ProjectDetailComponent),
                    },
                ],
            },
            {
                path: "planning-map",
                title: "Planning Map",
                loadComponent: () => import("./pages/planning-module/planning-map/planning-map.component").then((m) => m.PlanningMapComponent),
                canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard],
            },
            {
                path: "training",
                title: "Training",
                loadComponent: () => import("./pages/planning-module/training/training.component").then((m) => m.TrainingComponent),
                canActivate: [UnauthenticatedAccessGuard],
            },
        ],
    },
    {
        path: `trash`,
        title: "Stormwater Tools",
        loadComponent: () => import("./pages/trash-module/trash-site-layout/trash-site-layout.component").then((m) => m.TrashSiteLayoutComponent),
        children: [
            { path: "", title: "Home", loadComponent: () => import("./pages/trash-module/trash-home/trash-home.component").then((m) => m.TrashHomeComponent) },
            {
                path: "land-use-blocks",
                title: "Land Use Blocks",
                loadComponent: () => import("./pages/trash-module/trash-land-use-block-index/trash-land-use-block-index.component").then((m) => m.TrashLandUseBlockIndexComponent),
                canActivate: [UnauthenticatedAccessGuard],
            },
            {
                path: "onland-visual-trash-assessments",
                title: "OVTAs",
                children: [
                    {
                        path: "",
                        loadComponent: () => import("./pages/trash-module/ovtas/trash-ovta-index/trash-ovta-index.component").then((m) => m.TrashOvtaIndexComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: `:${routeParams.onlandVisualTrashAssessmentID}`,
                        loadComponent: () => import("./pages/trash-module/ovtas/trash-ovta-detail/trash-ovta-detail.component").then((m) => m.TrashOvtaDetailComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                ],
            },
            {
                path: "onland-visual-trash-assessments/new",
                title: "OVTAs",
                loadComponent: () =>
                    import("./pages/trash-module/ovta-workflow/trash-ovta-workflow-outlet/trash-ovta-workflow-outlet.component").then((m) => m.TrashOvtaWorkflowOutletComponent),
                children: [
                    { path: "", redirectTo: "instructions", pathMatch: "full" },
                    {
                        path: "instructions",
                        title: "Instructions",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-instructions/trash-ovta-instructions.component").then((m) => m.TrashOvtaInstructionsComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "initiate-ovta",
                        title: "Initiate OVTA",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-initiate-ovta/trash-initiate-ovta.component").then((m) => m.TrashInitiateOvtaComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                ],
            },
            {
                path: `onland-visual-trash-assessments/edit/:${routeParams.onlandVisualTrashAssessmentID}`,
                title: "OVTAs",
                loadComponent: () =>
                    import("./pages/trash-module/ovta-workflow/trash-ovta-workflow-outlet/trash-ovta-workflow-outlet.component").then((m) => m.TrashOvtaWorkflowOutletComponent),
                children: [
                    { path: "", redirectTo: "instructions", pathMatch: "full" },
                    {
                        path: "instructions",
                        title: "Instructions",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-instructions/trash-ovta-instructions.component").then((m) => m.TrashOvtaInstructionsComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "initiate-ovta",
                        title: "Initiate OVTA",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-initiate-ovta/trash-initiate-ovta.component").then((m) => m.TrashInitiateOvtaComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "record-observations",
                        title: "Record Observations",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-record-observations/trash-ovta-record-observations.component").then(
                                (m) => m.TrashOvtaRecordObservationsComponent
                            ),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "add-or-remove-parcels",
                        title: "Add or Remove Parcels",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-add-remove-parcels/trash-ovta-add-remove-parcels.component").then(
                                (m) => m.TrashOvtaAddRemoveParcelsComponent
                            ),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "refine-assessment-area",
                        title: "Refine Assessment Area",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-refine-assessment-area/trash-ovta-refine-assessment-area.component").then(
                                (m) => m.TrashOvtaRefineAssessmentAreaComponent
                            ),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: "review-and-finalize",
                        title: "Review and Finalize",
                        loadComponent: () =>
                            import("./pages/trash-module/ovta-workflow/trash-ovta-review-and-finalize/trash-ovta-review-and-finalize.component").then(
                                (m) => m.TrashOvtaReviewAndFinalizeComponent
                            ),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                ],
            },

            {
                path: "onland-visual-trash-assessment-areas",
                title: "OVTA Areas",
                children: [
                    {
                        path: "",
                        loadComponent: () => import("./pages/trash-module/ovtas/trash-ovta-area-index/trash-ovta-area-index.component").then((m) => m.TrashOvtaAreaIndexComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: `:${routeParams.onlandVisualTrashAssessmentAreaID}`,
                        loadComponent: () =>
                            import("./pages/trash-module/ovtas/trash-ovta-area-detail/trash-ovta-area-detail.component").then((m) => m.TrashOvtaAreaDetailComponent),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                    {
                        path: `:${routeParams.onlandVisualTrashAssessmentAreaID}/edit-location`,
                        loadComponent: () =>
                            import("./pages/trash-module/ovtas/trash-ovta-area-edit-location/trash-ovta-area-edit-location.component").then(
                                (m) => m.TrashOvtaAreaEditLocationComponent
                            ),
                        canActivate: [UnauthenticatedAccessGuard],
                    },
                ],
            },
            {
                path: "trash-analysis-areas",
                title: "Trash Analysis Areas",
                loadComponent: () =>
                    import("./pages/trash-module/trash-trash-generating-unit-index/trash-trash-generating-unit-index.component").then(
                        (m) => m.TrashTrashGeneratingUnitIndexComponent
                    ),
                canActivate: [UnauthenticatedAccessGuard],
            },
        ],
    },

    { path: "create-user-callback", loadComponent: () => import("./pages/create-user-callback/create-user-callback.component").then((m) => m.CreateUserCallbackComponent) },
    { path: "signin-oidc", loadComponent: () => import("./pages/login-callback/login-callback.component").then((m) => m.LoginCallbackComponent) },
    {
        path: ``,
        title: "Stormwater Tools",
        loadComponent: () => import("./pages/site-layout/site-layout.component").then((m) => m.SiteLayoutComponent),
        children: [
            { path: "", loadComponent: () => import("./pages/home/home-index/home-index.component").then((m) => m.HomeIndexComponent) },
            { path: "about", loadComponent: () => import("./pages/about/about.component").then((m) => m.AboutComponent) },
            { path: "modeling", loadComponent: () => import("./pages/modeling-about/modeling-about.component").then((m) => m.ModelingAboutComponent) },
            {
                path: `labels-and-definitions/:${routeParams.definitionID}`,
                loadComponent: () => import("./pages/field-definition-edit/field-definition-edit.component").then((m) => m.FieldDefinitionEditComponent),
                canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard],
            },
            {
                path: "labels-and-definitions",
                title: "Labels and Definitions",
                loadComponent: () => import("./pages/field-definition-list/field-definition-list.component").then((m) => m.FieldDefinitionListComponent),
                canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard],
            },
        ],
    },
    { path: "not-found", loadComponent: () => import("./shared/pages").then((m) => m.NotFoundComponent) },
    { path: "subscription-insufficient", loadComponent: () => import("./shared/pages").then((m) => m.SubscriptionInsufficientComponent) },
    { path: "unauthenticated", loadComponent: () => import("./shared/pages").then((m) => m.UnauthenticatedComponent) },
    { path: "**", loadComponent: () => import("./shared/pages").then((m) => m.NotFoundComponent) },
];
