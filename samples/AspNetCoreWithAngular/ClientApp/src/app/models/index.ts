export interface SelectListItem<T> {
    id: number;
    value: T;
}

export interface ToDoListModel {
    id: number;
    name: string;
    tags: SelectListItem<string>[];
    toDoItems: ToDoItemModel[];
}

export interface ToDoItemModel {
    name: string;
    done: boolean;
}
