import { Component, Input, OnInit } from '@angular/core';
import { AfFormArray } from '../../../../../libraries/autoforms/models/form-array';
import { AfFormNodeType } from '../../../../../libraries/autoforms/types/form-node-type';

@Component({
  selector: 'app-form-array',
  templateUrl: './form-array.component.html'
})
export class FormArrayComponent implements OnInit {

  @Input() formArray?: AfFormArray<any>;

  controls?: AfFormNodeType<any>[];

  constructor() {
  }

  ngOnInit() {
    this.controls = this.formArray!.controls;
    this.formArray.valueChanges.subscribe(x => console.log(x));
  }

  addControl() {
    this.formArray!.addControl();
  }

  removeControl(control: AfFormNodeType<any>) {
    this.formArray.removeAt(this.formArray.controls.findIndex(x => x == control));
  }
}
