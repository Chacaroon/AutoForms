import { BrowserModule } from '@angular/platform-browser';
import { InjectionToken, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FormBuilderClient } from '../../libraries/autoforms/form-builder-client';
import { FormControlRootComponent } from './components/form-control-root/form-control-root.component';
import { FormControlComponent } from './components/form-control-root/form-control/form-control.component';
import { FormGroupComponent } from './components/form-control-root/form-group/form-group.component';
import { FormArrayComponent } from './components/form-control-root/form-array/form-array.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    FormControlRootComponent,
    FormControlComponent,
    FormGroupComponent,
    FormArrayComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'fetch-data', component: FetchDataComponent},
    ])
  ],
  providers: [
    FormBuilderClient,
    {
      provide: 'BASE_URL',
      useValue: 'https://localhost:5001/'
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
