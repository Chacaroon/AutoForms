import { Component, Input, OnInit } from '@angular/core';
import { AfFormArray, AfFormNodeType } from '@auto-forms/client';

@Component({
    selector: 'app-form-array',
    templateUrl: './form-array.component.html'
})
export class FormArrayComponent implements OnInit {

    @Input() formArray?: AfFormArray<any>;
    @Input() validationEnabled: boolean;

    controls?: AfFormNodeType<any>[];

    constructor() {
    }

    ngOnInit() {
        this.controls = this.formArray!.controls;
    }

    addControl() {
        this.formArray!.addControl();
    }

    removeControl(control: AfFormNodeType<any>) {
        this.formArray.removeAt(this.formArray.controls.findIndex(x => x == control));
    }
}
