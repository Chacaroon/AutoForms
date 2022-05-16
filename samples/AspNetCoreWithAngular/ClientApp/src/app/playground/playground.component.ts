import { Component, Inject, OnInit } from '@angular/core';
import { shareReplay, startWith, switchMap } from "rxjs/operators";
import { SchoolModel } from "../models";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { AfFormGroup, AfNode, buildForm } from '@auto-forms/client';

@Component({
    selector: 'app-playground',
    templateUrl: './playground.component.html'
})
export class PlaygroundComponent implements OnInit {

    form$: Observable<AfFormGroup<SchoolModel>>;
    formValue$: Observable<SchoolModel>;

    private data$: Observable<AfNode>;

    constructor(private http: HttpClient,
                @Inject('BASE_URL') private baseUrl: string) {
    }

    ngOnInit(): void {
        this.data$ = this.http.get<AfNode>(`${this.baseUrl}api/form/create`).pipe(
            shareReplay(1),
        );

        this.form$ = this.data$.pipe(
            buildForm<SchoolModel>()
        );

        this.formValue$ = this.form$.pipe(
            switchMap(x => x.valueChanges.pipe(startWith(x.value))),
        );
    }

}
