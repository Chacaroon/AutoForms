import { AbstractControlOptions, AsyncValidatorFn, FormControl, ValidatorFn } from "@angular/forms";
import { Observable } from "rxjs";
import { FormValueDescriptor } from "./form-value.model";

export class AfFormControl<T> extends FormControl {

    override value: FormValueDescriptor<T> | undefined;
    override valueChanges!: Observable<FormValueDescriptor<T>>;

    constructor(formState?: FormValueDescriptor<T>,
                validatorOrOpts?: ValidatorFn | ValidatorFn[] | AbstractControlOptions,
                asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[]) {
        super(formState, validatorOrOpts, asyncValidator);
        this.value = formState;
    }
}
