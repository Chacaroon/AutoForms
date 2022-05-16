import { Component, Input, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
    selector: 'app-form-control',
    templateUrl: './form-control.component.html'
})
export class FormControlComponent implements ControlValueAccessor {

    @Input() validationEnabled: boolean;

    value: any;
    changeFn?: (value: any) => void;
    disabled?: boolean;

    constructor(@Optional() @Self() public ngControl: NgControl) {
        if (this.ngControl) {
            this.ngControl.valueAccessor = this;
        }
    }

    registerOnChange(fn: any): void {
        this.changeFn = fn;
    }

    registerOnTouched(fn: any): void {
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    writeValue(obj: any): void {
        this.value = obj;
    }

    onChange(value: any) {
        this.changeFn?.(value);
    }
}
