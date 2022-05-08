import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilderClient } from '../../../libraries/autoforms/_form-builder-client';
import { map, shareReplay, startWith, switchMap, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AfNode } from '../../../libraries/autoforms/form-nodes/node';
import { AfFormGroup } from '../../../libraries/autoforms/models/form-group';
import { SchoolModel } from '../models';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  data$: Observable<AfNode>;
  formValue$: Observable<SchoolModel>;
  form$: Observable<AfFormGroup<SchoolModel>>;
  private form: AfFormGroup<SchoolModel>;

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string,
              private formBuilderClient: FormBuilderClient) {
  }

  ngOnInit() {
    this.data$ = this.http.get<AfNode>(this.baseUrl + 'api/form').pipe(
      tap(x => {
        const build = this.formBuilderClient.build<any>(x) as AfFormGroup<any>;

        this.form = build;

        return build;
      }),
      shareReplay(1),
    );

    this.form$ = this.data$.pipe(
      map(x => {
        const build = this.formBuilderClient.build<any>(x) as AfFormGroup<any>;

        this.form = build;

        return build;
      })
    );

    this.formValue$ = this.form$.pipe(
      switchMap(x => x.valueChanges.pipe(startWith(x.value))),
    );
  }

  addControl() {
    this.form.controls.classes.addControl();

    this.form.controls.classes.controls[0].controls.students.addControl();
  }
}
