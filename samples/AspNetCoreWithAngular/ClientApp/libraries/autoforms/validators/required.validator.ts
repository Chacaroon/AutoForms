import { ValidationErrors, ValidatorFn, Validators } from "@angular/forms";

export function required(message: string): ValidatorFn {
    return (control): ValidationErrors => Validators.required(control)
        ? { required: message }
        : null;
}
