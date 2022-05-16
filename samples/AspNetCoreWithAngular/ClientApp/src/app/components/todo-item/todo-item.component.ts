import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AfFormGroup } from "@auto-forms/client";
import { ToDoItemModel } from "../../models";

@Component({
    selector: 'app-todo-item',
    templateUrl: './todo-item.component.html'
})
export class TodoItemComponent implements OnInit {

    @Input() formGroup: AfFormGroup<ToDoItemModel>

    @Output() remove = new EventEmitter<AfFormGroup<ToDoItemModel>>();

    constructor() {
    }

    ngOnInit(): void {
    }
}
