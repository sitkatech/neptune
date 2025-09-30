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
    treatmentBMPID: "treatmentBMPID",
};

export const routes: Routes = [
    {
        path: "inventory",
        title: "New Inventory Module",
        loadComponent: () => import("./pages/inventory-module/inventory-site-layout/inventory-site-layout.component").then((m) => m.InventorySiteLayoutComponent),
        children: [
            {
                path: "",
                title: "Inventory Home",
                loadComponent: () => import("./pages/inventory-module/inventory-home/inventory-home.component").then((m) => m.InventoryHomeComponent),
            },
            // BMP Inventory
            {
                path: "jurisdictions",
                title: "Jurisdictions",
                loadComponent: () => import("./pages/inventory-module/jurisdictions/jurisdictions.component").then((m) => m.JurisdictionsComponent),
            },
            {
                path: "find-bmp",
                title: "Find a BMP",
                loadComponent: () => import("./pages/inventory-module/find-bmp/find-bmp.component").then((m) => m.FindBmpComponent),
            },
            {
                path: "modeling-parameters",
                title: "Modeling Parameters",
                loadComponent: () => import("./pages/inventory-module/modeling-parameters/modeling-parameters.component").then((m) => m.ModelingParametersComponent),
            },
            {
                path: "view-all-bmps",
                title: "View All BMPs",
                loadComponent: () => import("./pages/inventory-module/view-all-bmps/view-all-bmps.component").then((m) => m.ViewAllBmpsComponent),
            },
            {
                path: `treatment-bmp-detail/:${routeParams.treatmentBMPID}`,
                title: "Treatment BMP Detail",
                loadComponent: () => import("./pages/inventory-module/treatment-bmp-detail/treatment-bmp-detail.component").then((m) => m.TreatmentBmpDetailComponent),
            },
            {
                path: "latest-bmp-assessments",
                title: "View Latest BMP Assessments",
                loadComponent: () => import("./pages/inventory-module/latest-bmp-assessments/latest-bmp-assessments.component").then((m) => m.LatestBmpAssessmentsComponent),
            },
            {
                path: "field-records",
                title: "View All Field Records",
                loadComponent: () => import("./pages/inventory-module/field-records/field-records.component").then((m) => m.FieldRecordsComponent),
            },
            {
                path: "wqmps",
                title: "Water Quality Management Plans",
                loadComponent: () => import("./pages/inventory-module/wqmps/wqmps.component").then((m) => m.WqmpsComponent),
            },
            {
                path: "wqmp-map",
                title: "WQMP Map",
                loadComponent: () => import("./pages/inventory-module/wqmp-map/wqmp-map.component").then((m) => m.WqmpMapComponent),
            },
            {
                path: "wqmp-annual-report",
                title: "WQMP Annual Report",
                loadComponent: () => import("./pages/inventory-module/wqmp-annual-report/wqmp-annual-report.component").then((m) => m.WqmpAnnualReportComponent),
            },
            {
                path: "parcels",
                title: "Parcels",
                loadComponent: () => import("./pages/inventory-module/parcels/parcels.component").then((m) => m.ParcelsComponent),
            },
            // Program Info
            {
                path: "program-info/observation-types",
                title: "Observation Types",
                loadComponent: () => import("./pages/inventory-module/program-info/observation-types.component").then((m) => m.ObservationTypesComponent),
            },
            {
                path: "program-info/treatment-bmp-types",
                title: "Treatment BMP Types",
                loadComponent: () => import("./pages/inventory-module/program-info/treatment-bmp-types.component").then((m) => m.TreatmentBmpTypesComponent),
            },
            {
                path: "program-info/funding-sources",
                title: "Funding Sources",
                loadComponent: () => import("./pages/inventory-module/program-info/funding-sources.component").then((m) => m.FundingSourcesComponent),
            },
            // Dashboard
            {
                path: "dashboard",
                title: "Dashboard",
                loadComponent: () => import("./pages/inventory-module/dashboard/dashboard.component").then((m) => m.DashboardComponent),
            },
            // Delineation
            {
                path: "delineation/delineation-map",
                title: "Delineation Map",
                loadComponent: () => import("./pages/inventory-module/delineation/delineation-map.component").then((m) => m.DelineationMapComponent),
            },
            {
                path: "delineation/delineation-reconciliation-report",
                title: "Delineation Reconciliation Report",
                loadComponent: () =>
                    import("./pages/inventory-module/delineation/delineation-reconciliation-report.component").then((m) => m.DelineationReconciliationReportComponent),
            },
            // Data Hub
            { path: "data-hub", title: "Data Hub", loadComponent: () => import("./pages/inventory-module/data-hub/data-hub.component").then((m) => m.DataHubComponent) },
            // Manage
            {
                path: "manage/homepage-configuration",
                title: "Homepage Configuration",
                loadComponent: () => import("./pages/inventory-module/manage/homepage-configuration.component").then((m) => m.HomepageConfigurationComponent),
            },
            {
                path: "manage/page-content",
                title: "Page Content",
                loadComponent: () => import("./pages/inventory-module/manage/page-content.component").then((m) => m.PageContentComponent),
            },
            {
                path: "manage/custom-labels-definitions",
                title: "Custom Labels & Definitions",
                loadComponent: () => import("./pages/inventory-module/manage/custom-labels-definitions.component").then((m) => m.CustomLabelsDefinitionsComponent),
            },
            { path: "manage/users", title: "Users", loadComponent: () => import("./pages/inventory-module/manage/users.component").then((m) => m.UsersComponent) },
            {
                path: "manage/organizations",
                title: "Organizations",
                loadComponent: () => import("./pages/inventory-module/manage/organizations.component").then((m) => m.OrganizationsComponent),
            },
            {
                path: "manage/jurisdictions",
                title: "Jurisdictions (Manage)",
                loadComponent: () => import("./pages/inventory-module/manage/jurisdictions.component").then((m) => m.ManageJurisdictionsComponent),
            },
            {
                path: "manage/observation-types-manage",
                title: "Observation Types (Manage)",
                loadComponent: () => import("./pages/inventory-module/manage/observation-types-manage.component").then((m) => m.ObservationTypesManageComponent),
            },
            {
                path: "manage/treatment-bmp-types-manage",
                title: "Treatment BMP Types (Manage)",
                loadComponent: () => import("./pages/inventory-module/manage/treatment-bmp-types-manage.component").then((m) => m.TreatmentBmpTypesManageComponent),
            },
            {
                path: "manage/custom-attributes",
                title: "Custom Attributes",
                loadComponent: () => import("./pages/inventory-module/manage/custom-attributes.component").then((m) => m.CustomAttributesComponent),
            },
            {
                path: "manage/load-generating-units",
                title: "Load Generating Units",
                loadComponent: () => import("./pages/inventory-module/manage/load-generating-units.component").then((m) => m.LoadGeneratingUnitsComponent),
            },
            {
                path: "manage/hru-characteristics",
                title: "HRU Characteristics",
                loadComponent: () => import("./pages/inventory-module/manage/hru-characteristics.component").then((m) => m.HruCharacteristicsComponent),
            },
            {
                path: "manage/regional-subbasins",
                title: "Regional Subbasins",
                loadComponent: () => import("./pages/inventory-module/manage/regional-subbasins.component").then((m) => m.RegionalSubbasinsComponent),
            },
            {
                path: "manage/regional-subbasin-grid",
                title: "Regional Subbasin Grid",
                loadComponent: () => import("./pages/inventory-module/manage/regional-subbasin-grid.component").then((m) => m.RegionalSubbasinGridComponent),
            },
            {
                path: "manage/regional-subbasin-revision-requests",
                title: "Regional Subbasin Revision Requests",
                loadComponent: () =>
                    import("./pages/inventory-module/manage/regional-subbasin-revision-requests.component").then((m) => m.RegionalSubbasinRevisionRequestsComponent),
            },
            {
                path: "manage/wqmp-lgu-audit",
                title: "Water Quality Management Plan LGU Audit",
                loadComponent: () => import("./pages/inventory-module/manage/wqmp-lgu-audit.component").then((m) => m.WqmpLguAuditComponent),
            },
        ],
    },
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
