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
import { WatershedDetailPopupComponent } from './components/watershed-detail-popup/watershed-detail-popup.component';
import { LinkRendererComponent } from './components/ag-grid/link-renderer/link-renderer.component';
import { FontAwesomeIconLinkRendererComponent } from './components/ag-grid/fontawesome-icon-link-renderer/fontawesome-icon-link-renderer.component';
import { WatershedMapComponent } from './components/watershed-map/watershed-map.component';
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

@NgModule({
    declarations: [
        AlertDisplayComponent,
        HeaderNavComponent,
        NotFoundComponent,
        UnauthenticatedComponent,
        SubscriptionInsufficientComponent,
        WatershedMapComponent,
        WatershedDetailPopupComponent,
        LinkRendererComponent,
        FontAwesomeIconLinkRendererComponent,
        MultiLinkRendererComponent,
        CustomRichTextComponent,
        FieldDefinitionComponent,
        FieldDefinitionGridHeaderComponent,
        CustomPinnedRowRendererComponent,
        CustomDropdownFilterComponent,
        CsvDownloadButtonComponent,
        ClearGridFiltersButtonComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        NgProgressModule,
        RouterModule,
        CKEditorModule,
        NgbModule
    ],
    exports: [
        AlertDisplayComponent,
        CommonModule,
        FormsModule,
        NotFoundComponent,
        WatershedMapComponent,
        HeaderNavComponent,
        CustomRichTextComponent,
        FieldDefinitionComponent,
        FieldDefinitionGridHeaderComponent,
        CsvDownloadButtonComponent,
        ClearGridFiltersButtonComponent
    ],
    entryComponents:[
        WatershedDetailPopupComponent,
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
