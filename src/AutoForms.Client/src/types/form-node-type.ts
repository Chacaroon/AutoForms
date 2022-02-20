import { FbFormControl } from "../models/form-control";
import { FbFormGroup } from "../models/form-group";
import { FbFormArray } from "../models/form-array";

export type FbFormNodeType<T> =
    T extends string | number | Date
        ? FbFormControl<T>
        : T extends Array<any>
            ? FbFormArray<T>
            : FbFormGroup<T>;
