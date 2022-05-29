// @ts-ignore
export interface FormValue<T> extends T {
  formValue: never
}

export type FormValueDescriptor<T> =
  T extends FormValue<infer TFormValue>
    ? TFormValue
    : T extends (infer ElementType)[]
      ? FormValueDescriptor<ElementType>[]
      : T extends { [key in keyof T]: T[key] }
        ? { [key in keyof T]: FormValueDescriptor<T[key]> }
        : T;
