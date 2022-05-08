import { AbstractControlOptions, AsyncValidatorFn, FormArray, ValidatorFn } from '@angular/forms';
import { AfFormNodeType } from '../types/form-node-type';
import { AfNode } from '../form-nodes/node';
import { FormBuilderClient } from '../_form-builder-client';

export class AfFormArray<T> extends FormArray {

  value: T | undefined;

  constructor(public controls: AfFormNodeType<T>[],
              public nodeSchema: AfNode,
              public formBuilder: FormBuilderClient,
              validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
              asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
    super(controls, validatorOrOpts, asyncValidator);
  }

  public addControl() {
    this.push(this.formBuilder.build(this.nodeSchema));
  }
}
