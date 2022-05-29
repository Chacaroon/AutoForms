import { FormValue } from "@auto-forms/client";

export interface SelectListItem<T> {
    id: number;
    value: T;
}

export interface ToDoListModel {
    id: number;
    name: string;
    tags: FormValue<number[]>;
    toDoItems: ToDoItemModel[];
}

export interface ToDoItemModel {
    name: string;
    done: boolean;
}

export const tags = [...Array(5).keys()]
    .map(x => ({ id: x + 1, value: `Item ${x + 1}` }));
