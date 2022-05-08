import { AbstractControlOptions, AsyncValidatorFn, FormArray, ValidatorFn } from "@angular/forms";
import { AfFormNodeType } from "../types/form-node-type";
import { Node } from "@babel/core";
import { AfNode } from "../form-nodes/node";

export class AfFormArray<T> extends FormArray {

    value: T | undefined;

    constructor(public controls: AfFormNodeType<T[keyof T]>[],
                public nodeSchema: AfNode,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
    }
}
