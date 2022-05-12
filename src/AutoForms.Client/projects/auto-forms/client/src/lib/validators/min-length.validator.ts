import { ValidationErrors, ValidatorFn, Validators } from "@angular/forms";

export function minLength(minLength: number, message: string): ValidatorFn {
    return (control): ValidationErrors | null => Validators.minLength(minLength)(control)
        ? { minLength: message }
        : null;
}
