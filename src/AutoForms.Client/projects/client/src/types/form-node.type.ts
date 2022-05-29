import { AfFormArray, AfFormControl, AfFormGroup, FormValue } from "../models";

export type AfFormNodeType<T> =
  T extends FormValue<infer TFormValue>
    ? AfFormControl<TFormValue>
    : T extends string | number | Date | boolean
      ? AfFormControl<T>
      : T extends (infer ElementType)[]
        ? AfFormArray<ElementType>
        : AfFormGroup<T>;
