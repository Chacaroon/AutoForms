import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CreateModelComponent } from './create-model/create-model.component';
import { FormControlRootComponent } from './components/form-control-root/form-control-root.component';
import { FormControlComponent } from './components/form-control-root/form-control/form-control.component';
import { FormGroupComponent } from './components/form-control-root/form-group/form-group.component';
import { FormArrayComponent } from './components/form-control-root/form-array/form-array.component';
import { PlaygroundComponent } from './playground/playground.component';
import { AutoFormsModule } from '@auto-forms/client'


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CreateModelComponent,
        FormControlRootComponent,
        FormControlComponent,
        FormGroupComponent,
        FormArrayComponent,
        PlaygroundComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        AutoFormsModule.forRoot(),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'create-model', component: CreateModelComponent },
            { path: 'update-model', component: CreateModelComponent },
            { path: 'playground', component: PlaygroundComponent },
        ]),
    ],
    providers: [
        {
            provide: 'BASE_URL',
            useValue: 'https://localhost:5001/'
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
