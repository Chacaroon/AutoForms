import { ValidationErrors, ValidatorFn, Validators } from "@angular/forms";

export function maxLength(maxLength: number, message: string): ValidatorFn {
    return (control): ValidationErrors | null => Validators.maxLength(maxLength)(control)
        ? { maxLength: message }
        : null;
}
