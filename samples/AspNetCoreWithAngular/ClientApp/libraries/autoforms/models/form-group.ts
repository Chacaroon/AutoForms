import { AbstractControlOptions, AsyncValidatorFn, FormGroup, ValidatorFn } from "@angular/forms";
import { AfFormNodeType } from "../types/form-node-type";

export class AfFormGroup<T> extends FormGroup {

    readonly value: T | undefined;

    constructor(public controls: { [key in keyof T]: AfFormNodeType<T[key]> },
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
    }
}
