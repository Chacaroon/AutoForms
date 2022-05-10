import { ValidatorFn, Validators } from "@angular/forms";

export function maxLength(maxLength: number, message: string): ValidatorFn {
    return control => Validators.maxLength(maxLength)(control)
        ? { maxLength: message }
        : null;
}
