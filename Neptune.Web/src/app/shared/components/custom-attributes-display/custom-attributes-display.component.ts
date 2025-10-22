import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CustomAttributeTypePurposeEnum, CustomAttributeTypePurposes } from "src/app/shared/generated/enum/custom-attribute-type-purpose-enum";
import { RouterLink } from "@angular/router";
import { CustomAttributeDto, TreatmentBMPTypeCustomAttributeTypeDto } from "../../generated/model/models";

@Component({
    selector: "custom-attributes-display",
    standalone: true,
    imports: [CommonModule, RouterLink],
    templateUrl: "./custom-attributes-display.component.html",
    styleUrls: ["./custom-attributes-display.component.scss"],
})
export class CustomAttributesDisplayComponent {
    @Input() canManage = false;
    @Input() editUrl = "";
    @Input() isAdmin = false;
    /**
     * Pass the enum value for the attribute type purpose (e.g., 'OtherDesignAttributes')
     */
    @Input() customAttributeTypePurposeEnum: CustomAttributeTypePurposeEnum = CustomAttributeTypePurposeEnum.OtherDesignAttributes;

    /**
     * Map enum to display name for header and empty state
     */
    get customAttributeTypePurposeDisplayName(): string {
        const entry = CustomAttributeTypePurposes.find((x) => x.Value === this.customAttributeTypePurposeEnum);
        return entry ? entry.DisplayName : String(this.customAttributeTypePurposeEnum);
    }
    @Input() treatmentBMPTypeCustomAttributeTypes: TreatmentBMPTypeCustomAttributeTypeDto[] = [];
    @Input() customAttributes: CustomAttributeDto[] = [];

    get hasAttributes(): boolean {
        return this.treatmentBMPTypeCustomAttributeTypes.some((x) => {
            const val = x.CustomAttributeType.CustomAttributeTypePurposeID;
            return Number(val) === this.customAttributeTypePurposeEnum;
        });
    }

    get filteredAttributeTypes(): TreatmentBMPTypeCustomAttributeTypeDto[] {
        return this.treatmentBMPTypeCustomAttributeTypes
            .filter((x) => {
                const val = x.CustomAttributeType.CustomAttributeTypePurposeID;
                return Number(val) === this.customAttributeTypePurposeEnum;
            })
            .sort((a, b) => a.SortOrder - b.SortOrder || a.CustomAttributeType.CustomAttributeTypeName.localeCompare(b.CustomAttributeType.CustomAttributeTypeName));
    }

    getAttributeValueWithUnits(customAttributeTypeID: number): string {
        const attr = this.customAttributes.find((a) => a.CustomAttributeTypeID === customAttributeTypeID);
        return attr ? attr.CustomAttributeValueWithUnits : "";
    }
}
