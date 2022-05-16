export interface SchoolModel {
    name: string;
    classes: ClassModel[];
    options: SchoolOptions;
}

export interface ClassModel {
    name: string;
    students: StudentModel[];
}

export interface StudentModel {
    name: string;
    age: number;
}

export interface SchoolOptions {
    isOpened: boolean;
}
