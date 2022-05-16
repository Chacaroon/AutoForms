import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { FormControlRootComponent } from './components/form-control-root/form-control-root.component';
import { FormControlComponent } from './components/form-control-root/form-control/form-control.component';
import { FormGroupComponent } from './components/form-control-root/form-group/form-group.component';
import { FormArrayComponent } from './components/form-control-root/form-array/form-array.component';
import { AutoFormsModule } from '@auto-forms/client'
import { BaseUrlInterceptor } from "./interceptors/base-url.interceptor";
import { CreateTodoListComponent } from './components/create-todo-list/create-todo-list.component';
import { TodoItemComponent } from './components/todo-item/todo-item.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        FormControlRootComponent,
        FormControlComponent,
        FormGroupComponent,
        FormArrayComponent,
        CreateTodoListComponent,
        TodoItemComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        AutoFormsModule.forRoot(),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'create-todo-list', component: CreateTodoListComponent },
            { path: 'update-todo-list/:id', component: CreateTodoListComponent }
        ]),
    ],
    providers: [
        {
            provide: 'BASE_URL',
            useValue: 'https://localhost:5001/api'
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: BaseUrlInterceptor,
            multi: true,
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
