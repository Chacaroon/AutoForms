import { Component, Input, OnInit } from '@angular/core';
import { AfFormNodeType } from '../../../../libraries/autoforms/types/form-node-type';
import { AfFormControl } from '../../../../libraries/autoforms/models/form-control';
import { AfFormGroup } from '../../../../libraries/autoforms/models/form-group';
import { AfFormArray } from '../../../../libraries/autoforms/models/form-array';

@Component({
    selector: 'app-form-control-root',
    templateUrl: './form-control-root.component.html'
})
export class FormControlRootComponent implements OnInit {

    @Input() form!: AfFormNodeType<any>;

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
