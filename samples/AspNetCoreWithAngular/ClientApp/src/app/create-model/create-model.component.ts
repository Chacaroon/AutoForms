import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, shareReplay, startWith, switchMap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { SchoolModel } from '../models';
import { ActivatedRoute } from "@angular/router";
import { AfNode } from '@auto-forms/client';
import { AfFormGroup } from '@auto-forms/client';
import { buildForm } from '@auto-forms/client';
import { AfFormNodeType } from '@auto-forms/client';

@Component({
    selector: 'app-create-model',
    templateUrl: './create-model.component.html'
})
export class CreateModelComponent implements OnInit {
    data$?: Observable<AfNode>;
    formValue$?: Observable<SchoolModel>;
    form$?: Observable<AfFormGroup<SchoolModel>>;

    response$?: Observable<SchoolModel | { error: any }>;

    validationEnabled: boolean;

    constructor(private http: HttpClient,
                @Inject('BASE_URL') private baseUrl: string,
                private route: ActivatedRoute) {
    }

    ngOnInit() {
        const url = this.route.snapshot.url.some(x => x.path === 'create-model') ? 'create' : 'update';
        this.data$ = this.http.get<AfNode>(`${this.baseUrl}api/form/${url}`).pipe(
            shareReplay(1),
        );

        this.form$ = this.data$.pipe(
            buildForm<SchoolModel>()
        );

        this.formValue$ = this.form$.pipe(
            switchMap(x => x.valueChanges.pipe(startWith(x.value))),
        );
    }

    onSubmit(form: AfFormNodeType<SchoolModel>, validationEnabled: boolean) {
        this.response$ = of(null);
        if (form.invalid && validationEnabled) {
            console.log(form);
            return;
        }

        this.response$ = this.http.post<SchoolModel>(`${this.baseUrl}api/form/create`, form.value).pipe(
            catchError((err) => of(err.error.errors))
        );
    }
}
