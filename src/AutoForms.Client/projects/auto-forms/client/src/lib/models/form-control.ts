import { AbstractControlOptions, AsyncValidatorFn, FormControl, ValidatorFn } from "@angular/forms";
import { Observable } from "rxjs";

export class AfFormControl<T> extends FormControl {

    override value: T | undefined;
    override valueChanges!: Observable<T>;

    constructor(formState?: T,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(formState, validatorOrOpts, asyncValidator);
        this.value = formState;
    }
}
