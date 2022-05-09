import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilderClient } from '../../../libraries/autoforms/_form-builder-client';
import { map, shareReplay, startWith, switchMap, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AfNode } from '../../../libraries/autoforms/form-nodes/node';
import { AfFormGroup } from '../../../libraries/autoforms/models/form-group';
import { SchoolModel } from '../models';
import { buildForm } from "../../../libraries/autoforms/pipable-operators/build-form.operator";
import { AfFormNodeType } from "../../../libraries/autoforms/types/form-node-type";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  data$?: Observable<AfNode>;
  formValue$?: Observable<SchoolModel>;
  form$?: Observable<AfFormGroup<SchoolModel>>;

  response$?: Observable<SchoolModel>;

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.data$ = this.http.get<AfNode>(this.baseUrl + 'api/form').pipe(
      shareReplay(1),
    );

    this.form$ = this.data$.pipe(
      buildForm<SchoolModel>()
    );

    this.formValue$ = this.form$.pipe(
      switchMap(x => x.valueChanges.pipe(startWith(x.value))),
    );
  }

  onSubmit(form: AfFormNodeType<SchoolModel>) {
    if (form.invalid) {
      console.log(form);
      return;
    }

    this.response$ = this.http.post<SchoolModel>(this.baseUrl + 'api/form', form.value);
  }
}
