import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { AfFormNodeType } from '@auto-forms/client';

@Component({
    selector: 'app-form-group',
    templateUrl: './form-group.component.html'
})
export class FormGroupComponent implements OnInit {
    @Input() validationEnabled: boolean;

    controls?: { [key: string]: AfFormNodeType<any> };

    constructor(@Optional() @Self() public ngModel: FormGroupDirective) {
    }

    ngOnInit() {
        if (this.ngModel) {
            this.controls = this.ngModel.control.controls as unknown as { [key: string]: AfFormNodeType<any> };
        }
    }
}
