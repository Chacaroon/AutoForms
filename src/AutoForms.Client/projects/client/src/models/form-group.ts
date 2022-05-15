import { AbstractControlOptions, AsyncValidatorFn, FormGroup, ValidatorFn } from "@angular/forms";
import { Observable } from "rxjs";
import { AfFormNodeType } from "../types";

export class AfFormGroup<T> extends FormGroup {

    public override controls: { [key in keyof T]: AfFormNodeType<T[key]> };
    override readonly value: T | undefined;
    override valueChanges!: Observable<T>;

    constructor(controls: { [key in keyof T]: AfFormNodeType<T[key]> },
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
        this.controls = controls;
    }
}
