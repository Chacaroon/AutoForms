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

export const tags: { id: number, value: string }[] = [
    {id: 1, value: 'Староста'},
    {id: 2, value: 'Футболіст'},
    {id: 3, value: 'Волонтер'},
];
