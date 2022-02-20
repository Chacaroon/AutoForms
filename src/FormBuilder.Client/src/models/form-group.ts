import { AbstractControlOptions, AsyncValidatorFn, FormGroup, ValidatorFn } from "@angular/forms";
import { FbFormNodeType } from "../types/form-node-type";

export class FbFormGroup<T> extends FormGroup {

    readonly value: T | undefined;

    constructor(public controls: { [key in keyof T]: FbFormNodeType<T[key]> },
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
    }
}
