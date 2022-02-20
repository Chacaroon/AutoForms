import { AbstractControlOptions, AsyncValidatorFn, FormArray, ValidatorFn } from "@angular/forms";
import { FbFormNodeType } from "../types/form-node-type";

export class FbFormArray<T> extends FormArray {

    value: T | undefined;

    constructor(public controls: FbFormNodeType<T[keyof T]>[],
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
    }
}
