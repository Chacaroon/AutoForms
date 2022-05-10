import { ValidatorFn, Validators } from "@angular/forms";

export function minLength(minLength: number, message: string): ValidatorFn {
    return control => Validators.minLength(minLength)(control)
        ? { minLength: message }
        : null;
}
