import { Component, Input, OnInit } from "@angular/core";
import { FormControl, NG_VALUE_ACCESSOR, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FormInputOption } from "../../forms/form-field/form-field.component";
import { NgFor } from "@angular/common";

@Component({
    selector: "select-dropdown",
    templateUrl: "./select-dropdown.component.html",
    styleUrls: ["./select-dropdown.component.scss"],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: SelectDropdownComponent,
            multi: true,
        },
    ],
    standalone: true,
    imports: [FormsModule, ReactiveFormsModule, NgFor],
})
export class SelectDropdownComponent implements OnInit {
    @Input() formInputOptions: FormInputOption[];

    constructor() {}

    @Input() formControl: FormControl;

    ngOnInit(): void {}

    // begin ControlValueAccessor
    public disabled = false;
    public touched = false;
    onChange: any = () => {};
    onTouch: any = () => {};
    public val: boolean;

    set value(val: boolean) {
        this.val = val;
        this.onChange(val);
        this.onTouch(val);
    }

    writeValue(val: boolean) {
        this.value = val;
    }
    registerOnChange(onChange: any) {
        this.onChange = onChange;
    }
    registerOnTouched(onTouched: any) {
        this.onTouch = onTouched;
    }
    setDisabledState?(isDisabled: boolean) {
        this.disabled = isDisabled;
        if (isDisabled) {
            this.formControl.disable();
        } else {
            this.formControl.enable();
        }
    }

    markAsTouched() {
        if (!this.touched) {
            this.onTouch();
            this.touched = true;
        }
    }
    // end ControlValueAccessor
}

export interface SelectDropdownOption extends FormInputOption {}
