import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { AfFormGroup, buildForm, FormBuilderClient } from "@auto-forms/client";
import { ToDoListModel } from "../../models";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    data$: Observable<ToDoListModel[]>

    constructor(private http: HttpClient) {
    }

    ngOnInit() {
        this.data$ = this.http.get<ToDoListModel[]>('/ToDoItems');
    }
}
