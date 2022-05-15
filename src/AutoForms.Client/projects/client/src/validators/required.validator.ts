import { ValidationErrors, ValidatorFn, Validators } from "@angular/forms";

export function required(message: string): ValidatorFn {
    return (control): ValidationErrors | null => Validators.required(control)
        ? { required: message }
        : null;
}
