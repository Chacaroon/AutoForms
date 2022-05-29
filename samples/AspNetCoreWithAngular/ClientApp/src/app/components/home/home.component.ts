import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, retry } from "rxjs";
import { tags, ToDoListModel } from "../../models";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    data$: Observable<ToDoListModel[]>

    tags = tags;

    constructor(private http: HttpClient) {
    }

    ngOnInit() {
        this.data$ = this.http.get<ToDoListModel[]>('/ToDoItems');
    }

    mapTags(tagIds: number[]): string {
        return tagIds?.map(x => this.tags.find(z => z.id == x).value).join(', ');
    }
}
