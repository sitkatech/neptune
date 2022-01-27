import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent, UnauthenticatedComponent, SubscriptionInsufficientComponent } from './shared/pages';
import { UnauthenticatedAccessGuard } from './shared/guards/unauthenticated-access/unauthenticated-access.guard';
import { ManagerOnlyGuard } from "./shared/guards/unauthenticated-access/manager-only-guard";
import { HomeIndexComponent } from './pages/home/home-index/home-index.component';
import { UserDetailComponent } from './pages/user-detail/user-detail.component';
import { LoginCallbackComponent } from './pages/login-callback/login-callback.component';
import { CreateUserCallbackComponent } from './pages/create-user-callback/create-user-callback.component';
import { FieldDefinitionListComponent } from './pages/field-definition-list/field-definition-list.component';
import { FieldDefinitionEditComponent } from './pages/field-definition-edit/field-definition-edit.component';
import { TrainingComponent } from './pages/training/training.component';
import { ProjectListComponent } from './pages/project-list/project-list.component';
import { AboutComponent } from './pages/about/about.component';
import { ProjectNewComponent } from './pages/project-new/project-new.component';
import { JurisdictionManagerOrEditorOnlyGuard } from './shared/guards/unauthenticated-access/jurisdiction-manager-or-editor-only-guard.guard';
import { ProjectNewSidebarComponent } from './shared/components/project-new-sidebar/project-new-sidebar.component';
import { ProjectInstructionsComponent } from './pages/project-new/project-instructions/project-instructions.component';
import { ProjectBasicsComponent } from './pages/project-new/project-basics/project-basics.component';
import { UnderConstructionComponent } from './shared/components/under-construction/under-construction.component';

const routes: Routes = [
  { path: "labels-and-definitions/:id", component: FieldDefinitionEditComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
  { path: "labels-and-definitions", component: FieldDefinitionListComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
  { path: "projects", component: ProjectListComponent, canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard] },
  { path: "projects/new", component: ProjectNewComponent, canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard], children: [
    { path: "", redirectTo: 'instructions', pathMatch: 'full' },
    { path: "instructions", component:  ProjectInstructionsComponent},
    { path: "project-basics", component:  ProjectBasicsComponent},
    { path: "stormwater-treatments", children: [
      { path: "", redirectTo: 'treatment-bmps', pathMatch: 'full' },
      { path: "treatment-bmps", component:  UnderConstructionComponent},
      { path: "delineations", component:  UnderConstructionComponent},
      { path: "modeled-performance", component:  UnderConstructionComponent},
    ]},
    { path: "attachments", component:  UnderConstructionComponent},
    { path: "review", component:  UnderConstructionComponent},
  ]},
  { path: "projects/edit/:id", component: ProjectNewComponent, canActivate: [UnauthenticatedAccessGuard, JurisdictionManagerOrEditorOnlyGuard], children: [
    { path: "", redirectTo: 'instructions', pathMatch: 'full' },
    { path: "instructions", component:  ProjectInstructionsComponent},
    { path: "project-basics", component:  ProjectBasicsComponent},
    { path: "stormwater-treatments", children: [
      { path: "", redirectTo: 'treatment-bmps', pathMatch: 'full' },
      { path: "treatment-bmps", component:  UnderConstructionComponent},
      { path: "delineations", component:  UnderConstructionComponent},
      { path: "modeled-performance", component:  UnderConstructionComponent},
    ]},
    { path: "attachments", component:  UnderConstructionComponent},
    { path: "review", component:  UnderConstructionComponent},
  ]},
  { path: "training", component: TrainingComponent, canActivate: [UnauthenticatedAccessGuard] },
  { path: "about", component: AboutComponent, canActivate: [UnauthenticatedAccessGuard] },
  { path: "users/:id", component: UserDetailComponent, canActivate: [UnauthenticatedAccessGuard, ManagerOnlyGuard] },
  { path: "create-user-callback", component: CreateUserCallbackComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: 'subscription-insufficient', component: SubscriptionInsufficientComponent },
  { path: 'unauthenticated', component: UnauthenticatedComponent },
  { path: "signin-oidc", component: LoginCallbackComponent },
  { path: "", component: HomeIndexComponent},
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
