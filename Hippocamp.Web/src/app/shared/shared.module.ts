import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NotFoundComponent } from './pages';
import { HeaderNavComponent } from './components';
import { UnauthenticatedComponent } from './pages/unauthenticated/unauthenticated.component';
import { SubscriptionInsufficientComponent } from './pages/subscription-insufficient/subscription-insufficient.component';
import { NgProgressModule } from 'ngx-progressbar';
import { RouterModule } from '@angular/router';
import { LinkRendererComponent } from './components/ag-grid/link-renderer/link-renderer.component';
import { FontAwesomeIconLinkRendererComponent } from './components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { MultiLinkRendererComponent } from './components/ag-grid/multi-link-renderer/multi-link-renderer.component';
import { CustomRichTextComponent } from './components/custom-rich-text/custom-rich-text.component'
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FieldDefinitionComponent } from './components/field-definition/field-definition.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AlertDisplayComponent } from './components/alert-display/alert-display.component';
import { FieldDefinitionGridHeaderComponent } from './components/field-definition-grid-header/field-definition-grid-header.component';
import { CustomPinnedRowRendererComponent } from './components/ag-grid/custom-pinned-row-renderer/custom-pinned-row-renderer.component';
import { CustomDropdownFilterComponent } from './components/custom-dropdown-filter/custom-dropdown-filter.component';
import { CsvDownloadButtonComponent } from './components/csv-download-button/csv-download-button.component';
import { ClearGridFiltersButtonComponent } from './components/clear-grid-filters-button/clear-grid-filters-button.component';
import { ProjectWizardSidebarComponent } from './components/project-wizard-sidebar/project-wizard-sidebar.component';
import { ProgressIconComponent } from './components/progress-icon/progress-icon.component';
import { ToggleStatusComponent } from './components/toggle-status/toggle-status.component';
import { OrderableAccordionComponent, OrderablePanel, OrderablePanelContent, OrderablePanelControls, OrderablePanelTitle, OrderablePanelToggle } from './components/orderable-accordion/orderable-accordion.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { IconComponent } from './components/icon/icon.component';
import { UnderConstructionComponent } from './components/under-construction/under-construction.component';
import { NeptuneModelingResultSigFigPipe } from './pipes/neptune-modeling-result-sig-fig.pipe';

@NgModule({
    declarations: [
        AlertDisplayComponent,
        HeaderNavComponent,
        NotFoundComponent,
        UnauthenticatedComponent,
        SubscriptionInsufficientComponent,
        LinkRendererComponent,
        FontAwesomeIconLinkRendererComponent,
        MultiLinkRendererComponent,
        CustomRichTextComponent,
        FieldDefinitionComponent,
        FieldDefinitionGridHeaderComponent,
        CustomPinnedRowRendererComponent,
        CustomDropdownFilterComponent,
        CsvDownloadButtonComponent,
        ClearGridFiltersButtonComponent,
        ToggleStatusComponent,
        OrderableAccordionComponent,
        OrderablePanel,
        OrderablePanelToggle,
        OrderablePanelTitle,
        OrderablePanelControls,
        OrderablePanelContent,
        IconComponent,
        ProgressIconComponent,
        ProjectWizardSidebarComponent,
        UnderConstructionComponent,
        NeptuneModelingResultSigFigPipe
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        NgProgressModule,
        RouterModule,
        CKEditorModule,
        NgbModule,
        DragDropModule
    ],
    exports: [
        AlertDisplayComponent,
        CommonModule,
        FormsModule,
        NotFoundComponent,
        HeaderNavComponent,
        CustomRichTextComponent,
        FieldDefinitionComponent,
        FieldDefinitionGridHeaderComponent,
        CsvDownloadButtonComponent,
        ClearGridFiltersButtonComponent,
        ToggleStatusComponent,
        OrderableAccordionComponent,
        OrderablePanel,
        OrderablePanelToggle,
        OrderablePanelTitle,
        OrderablePanelControls,
        OrderablePanelContent,
        IconComponent,
        ProgressIconComponent,
        ProjectWizardSidebarComponent,
        NeptuneModelingResultSigFigPipe
    ],
    entryComponents:[]
})
export class SharedModule {
    static forRoot(): ModuleWithProviders<SharedModule> {
    return {
        ngModule: SharedModule,
        providers: []
    };
}

    static forChild(): ModuleWithProviders<SharedModule> {
    return {
        ngModule: SharedModule,
        providers: []
    };
}
}
