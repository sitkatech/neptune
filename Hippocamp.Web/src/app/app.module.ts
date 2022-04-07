import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER, ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { CookieService } from 'ngx-cookie-service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './shared/interceptors/auth-interceptor';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { HomeIndexComponent } from './pages/home/home-index/home-index.component';
import { RouterModule } from '@angular/router';
import { UserDetailComponent } from './pages/user-detail/user-detail.component';
import { AgGridModule } from 'ag-grid-angular';
import { DecimalPipe, CurrencyPipe, DatePipe } from '@angular/common';
import { LinkRendererComponent } from './shared/components/ag-grid/link-renderer/link-renderer.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeIconLinkRendererComponent } from './shared/components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { LoginCallbackComponent } from './pages/login-callback/login-callback.component';
import { MultiLinkRendererComponent } from './shared/components/ag-grid/multi-link-renderer/multi-link-renderer.component';
import { CreateUserCallbackComponent } from './pages/create-user-callback/create-user-callback.component';
import { AppInitService } from './app.init';
import { FieldDefinitionListComponent } from './pages/field-definition-list/field-definition-list.component';
import { FieldDefinitionEditComponent } from './pages/field-definition-edit/field-definition-edit.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { HttpErrorInterceptor } from './shared/interceptors/httpErrorInterceptor';
import { environment } from 'src/environments/environment';
import { AppInsightsService } from './shared/services/app-insights.service';
import { GlobalErrorHandlerService } from './shared/services/global-error-handler.service';
import { SharedModule } from './shared/shared.module';
import { CookieStorageService } from './shared/services/cookies/cookie-storage.service';
import { TrainingComponent } from './pages/training/training.component';
import { ProjectListComponent } from './pages/projects/project-list/project-list.component';
import { AboutComponent } from './pages/about/about.component';
import { ProjectWorkflowOutletComponent } from './pages/projects/project-workflow/project-workflow-outlet.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ProjectBasicsComponent } from './pages/projects/project-workflow/project-basics/project-basics.component';
import { ProjectInstructionsComponent } from './pages/projects/project-workflow/project-instructions/project-instructions.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { TreatmentBmpsComponent } from './pages/projects/project-workflow/treatment-bmps/treatment-bmps.component';
import { ProjectAttachmentsComponent } from './pages/projects/project-workflow/project-attachments/project-attachments.component';
import { DelineationsComponent } from './pages/projects/project-workflow/delineations/delineations.component';
import { ModeledPerformanceComponent } from './pages/projects/project-workflow/modeled-performance/modeled-performance.component';
import { ProjectDetailComponent } from './pages/projects/project-detail/project-detail.component';
import { ReviewComponent } from './pages/projects/project-workflow/review/review.component';
import { PlanningMapComponent } from './pages/planning-map/planning-map.component';

export function init_app(appLoadService: AppInitService, appInsightsService:  AppInsightsService) {
  return () => appLoadService.init().then(() => {
    if (environment.appInsightsInstrumentationKey) {
      appInsightsService.initAppInsights();
    }
  });
}

@NgModule({
  declarations: [
    AppComponent,
    HomeIndexComponent,
    UserDetailComponent,
    LoginCallbackComponent,
    CreateUserCallbackComponent,
    FieldDefinitionListComponent,
    FieldDefinitionEditComponent,
    TrainingComponent,
	  ProjectListComponent,
	  AboutComponent,
    ProjectWorkflowOutletComponent,
    ProjectBasicsComponent,
    ProjectInstructionsComponent,
    TreatmentBmpsComponent,
    ProjectAttachmentsComponent,
    DelineationsComponent,
    ModeledPerformanceComponent,
    ProjectDetailComponent,
    ReviewComponent,
    PlanningMapComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    NgbModule,
    RouterModule,
    OAuthModule.forRoot(),
    SharedModule.forRoot(),
    FormsModule,
    BrowserAnimationsModule,
    AgGridModule.withComponents([]),
    CKEditorModule,
    DragDropModule,
    NgSelectModule,

  ],  
  providers: [
    CookieService,
    AppInitService,
    { provide: APP_INITIALIZER, useFactory: init_app, deps: [AppInitService, AppInsightsService], multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandlerService
    },
    DecimalPipe, CurrencyPipe, DatePipe,
    {
      provide: OAuthStorage,
      useClass: CookieStorageService
    }
  ],
  entryComponents: [LinkRendererComponent, FontAwesomeIconLinkRendererComponent, MultiLinkRendererComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
