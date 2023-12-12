import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NotFoundComponent } from './pages';
import { HeaderNavComponent } from './components';
import { UnauthenticatedComponent } from './pages/unauthenticated/unauthenticated.component';
import { SubscriptionInsufficientComponent } from './pages/subscription-insufficient/subscription-insufficient.component';
import { RouterModule } from '@angular/router';
import { LinkRendererComponent } from './components/ag-grid/link-renderer/link-renderer.component';
import { FontAwesomeIconLinkRendererComponent } from './components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { MultiLinkRendererComponent } from './components/ag-grid/multi-link-renderer/multi-link-renderer.component';
import { CustomRichTextComponent } from './components/custom-rich-text/custom-rich-text.component'
import { FieldDefinitionComponent } from './components/field-definition/field-definition.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AlertDisplayComponent } from './components/alert-display/alert-display.component';
import { FieldDefinitionGridHeaderComponent } from './components/field-definition-grid-header/field-definition-grid-header.component';
import { CustomPinnedRowRendererComponent } from './components/ag-grid/custom-pinned-row-renderer/custom-pinned-row-renderer.component';
import { CustomDropdownFilterComponent } from './components/custom-dropdown-filter/custom-dropdown-filter.component';
import { CsvDownloadButtonComponent } from './components/csv-download-button/csv-download-button.component';
import { ClearGridFiltersButtonComponent } from './components/clear-grid-filters-button/clear-grid-filters-button.component';
import { ProjectWizardSidebarComponent } from './components/projects/project-wizard-sidebar/project-wizard-sidebar.component';
import { ProgressIconComponent } from './components/progress-icon/progress-icon.component';
import { ToggleStatusComponent } from './components/toggle-status/toggle-status.component';
import { OrderableAccordionComponent, OrderablePanel, OrderablePanelContent, OrderablePanelControls, OrderablePanelTitle, OrderablePanelToggle } from './components/orderable-accordion/orderable-accordion.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { IconComponent } from './components/icon/icon.component';
import { UnderConstructionComponent } from './components/under-construction/under-construction.component';
import { NeptuneModelingResultSigFigPipe } from './pipes/neptune-modeling-result-sig-fig.pipe';
import { ConfirmModalComponent } from './components/confirm-modal/confirm-modal.component';
import { TreatmentBmpMapEditorAndModelingAttributesComponent } from './components/projects/treatment-bmp-map-editor-and-modeling-attributes/treatment-bmp-map-editor-and-modeling-attributes.component';
import { ModelResultsComponent } from './components/projects/model-results/model-results.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { AttachmentsDisplayComponent } from './components/projects/attachments-display/attachments-display.component';
import { OctaPrioritizationDetailPopupComponent } from './components/octa-prioritization-detail-popup/octa-prioritization-detail-popup.component';
import { GrantScoresComponent } from './components/projects/grant-scores/grant-scores.component';
import { EditorModule, TINYMCE_SCRIPT_SRC } from "@tinymce/tinymce-angular";
import { AlertComponent } from './components/alert/alert.component';

@NgModule({
    declarations: [
        AlertDisplayComponent,
        AlertComponent,
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
        NeptuneModelingResultSigFigPipe,
        ConfirmModalComponent,
        TreatmentBmpMapEditorAndModelingAttributesComponent,
        ModelResultsComponent,
        AttachmentsDisplayComponent,
        OctaPrioritizationDetailPopupComponent,
        GrantScoresComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        RouterModule,
        NgbModule,
        DragDropModule,
        NgSelectModule,
        EditorModule
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
        NeptuneModelingResultSigFigPipe,
        TreatmentBmpMapEditorAndModelingAttributesComponent,
        ModelResultsComponent,
        AttachmentsDisplayComponent,
        OctaPrioritizationDetailPopupComponent,
        GrantScoresComponent,
        EditorModule
    ],
    providers:[
        { provide: TINYMCE_SCRIPT_SRC, useValue: 'assets/tinymce/tinymce.min.js' }
    ]
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
