import { AbstractControlOptions, AsyncValidatorFn, FormControl, ValidatorFn } from "@angular/forms";

export class AfFormControl<T> extends FormControl {

    public override value: T | undefined;

    constructor(formState?: T,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(formState, validatorOrOpts, asyncValidator);
        this.value = formState;
    }
}
