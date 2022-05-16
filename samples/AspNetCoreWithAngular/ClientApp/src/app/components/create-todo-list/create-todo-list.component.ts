import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { AfFormGroup, AfNode, buildForm } from "@auto-forms/client";
import { ToDoListModel } from "../../models";
import { Observable } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'app-create-todo-list',
    templateUrl: './create-todo-list.component.html'
})
export class CreateTodoListComponent implements OnInit {

    form$: Observable<AfFormGroup<ToDoListModel>>;
    id: string;

    constructor(private http: HttpClient,
                private router: Router,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id');
        const url = this.id ? `/ToDoItems/update/${this.id}` : '/ToDoItems/create';
        this.form$ = this.http.get<AfNode>(url).pipe(
            buildForm<ToDoListModel>()
        )
    }

    onSave(form: AfFormGroup<ToDoListModel>) {
        if (form.invalid) {
            return;
        }

        const url = this.id ? `/ToDoItems/update/${this.id}` : '/ToDoItems/create';

        this.http.post(url, form.value).subscribe(() => this.router.navigateByUrl(''))
    }
}
