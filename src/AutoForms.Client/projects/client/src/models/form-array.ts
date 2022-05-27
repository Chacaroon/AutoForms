import { AbstractControlOptions, AsyncValidatorFn, FormArray, ValidatorFn } from '@angular/forms';
import { Observable } from "rxjs";
import { AfFormNodeType } from "../types";
import { FormBuilderClient } from "../form-builder-client";
import { AfNode } from "../form-nodes";
import { FormValueDescriptor } from "./form-value.model";

export class AfFormArray<T> extends FormArray {

    override value: FormValueDescriptor<T>[] | undefined;
    override valueChanges!: Observable<FormValueDescriptor<T>[]>;
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
