import { AfFormControl } from '../models/form-control';
import { AfFormGroup } from '../models/form-group';
import { AfFormArray } from '../models/form-array';

export type AfFormNodeType<T> =
    T extends string | number | Date
        ? AfFormControl<T>
        : T extends (infer ElementType)[]
            ? AfFormArray<ElementType>
            : AfFormGroup<T>;
