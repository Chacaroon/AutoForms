import { AfFormArray, AfFormControl, AfFormGroup } from "../models";

export type AfFormNodeType<T> =
    T extends string | number | Date | boolean
        ? AfFormControl<T>
        : T extends (infer ElementType)[]
            ? AfFormArray<ElementType>
            : AfFormGroup<T>;
