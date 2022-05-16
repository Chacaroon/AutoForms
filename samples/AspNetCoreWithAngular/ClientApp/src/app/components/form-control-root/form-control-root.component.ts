import { Component, Input, OnInit } from '@angular/core';
import { AfFormArray, AfFormControl, AfFormGroup } from '@auto-forms/client';
import { AfFormNodeType } from '@auto-forms/client';

@Component({
    selector: 'app-form-control-root',
    templateUrl: './form-control-root.component.html'
})
export class FormControlRootComponent implements OnInit {

    @Input() form!: AfFormNodeType<any>;
    @Input() validationEnabled: boolean;

    formType?: 'control' | 'group' | 'array';

    constructor() {
    }

    ngOnInit() {
        if (this.form instanceof AfFormControl) {
            this.formType = 'control';
        } else if (this.form instanceof AfFormGroup) {
            this.formType = 'group';
        } else if (this.form instanceof AfFormArray) {
            this.formType = 'array';
        }
    }

}
