import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AfControl } from "../form-nodes";
import { AfFormNodeType } from "../types";
import { FormBuilderClient } from "../form-builder-client";

export function buildForm<T>() {
    return function (source: Observable<AfControl>): Observable<AfFormNodeType<T>> {
        return source.pipe(
            map(x => new FormBuilderClient().build<T>(x)),
            shareReplay({ bufferSize: 1, refCount: true }));
    }
}
