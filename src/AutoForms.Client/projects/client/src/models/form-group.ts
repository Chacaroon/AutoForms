import { AbstractControlOptions, AsyncValidatorFn, FormGroup, ValidatorFn } from "@angular/forms";
import { Observable } from "rxjs";
import { AfFormNodeType } from "../types";
import { FormValueDescriptor } from "./form-value.model";

export class AfFormGroup<T> extends FormGroup {

    override readonly controls: { [key in keyof T]: AfFormNodeType<T[key]> };
    override readonly value: FormValueDescriptor<T> | undefined;
    override readonly valueChanges!: Observable<FormValueDescriptor<T>>;

    constructor(controls: { [key in keyof T]: AfFormNodeType<T[key]> },
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(controls, validatorOrOpts, asyncValidator);
        this.controls = controls;
    }
}
