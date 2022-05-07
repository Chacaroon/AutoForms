import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  data$: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.data$ = http.get(baseUrl + 'api/form');
  }
}
