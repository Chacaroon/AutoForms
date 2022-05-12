import { AbstractControlOptions, AsyncValidatorFn, FormArray, ValidatorFn } from '@angular/forms';
import { AfFormNodeType } from '../types/form-node-type';
import { AfNode } from '../form-nodes/node';
import { FormBuilderClient } from '../form-builder-client';
import { Observable } from "rxjs";

export class AfFormArray<T> extends FormArray {

    override value: T[] | undefined;
    override valueChanges!: Observable<T[]>;
    override controls: AfFormNodeType<T>[];
    nodeSchema: AfNode;
    formBuilder: FormBuilderClient;

    constructor(controls: AfFormNodeType<T>[],
                nodeSchema: AfNode,
                formBuilder: FormBuilderClient,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
        this.controls = controls;
        this.formBuilder = formBuilder;
        this.nodeSchema = nodeSchema;
    }

    public addControl() {
        this.push(this.formBuilder.build(this.nodeSchema));
    }
}
