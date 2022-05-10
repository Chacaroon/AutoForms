import { Observable } from "rxjs";
import { AfNode } from "../form-nodes/node";
import { FormBuilderClient } from "../_form-builder-client";
import { map, shareReplay } from "rxjs/operators";

export function buildForm<T>() {
    return function (source: Observable<AfNode>) {
        return source.pipe(
            map(x => new FormBuilderClient().build<T>(x)),
            shareReplay({ bufferSize: 1, refCount: true }));
    }
}
