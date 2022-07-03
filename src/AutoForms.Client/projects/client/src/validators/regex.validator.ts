import { ValidationErrors, ValidatorFn, Validators } from "@angular/forms";

export function regex(pattern: string, message: string): ValidatorFn {
  return (control): ValidationErrors | null => Validators.pattern(pattern)(control)
    ? { pattern: message }
    : null;
}
