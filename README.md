# AutoForms

[![Build and test](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/AutoForms.svg)](https://www.nuget.org/packages/AutoForms/)

AutoForms is a system of libraries designed to synchronize a data structure between parts of a web application based on ASP.NET 6 and Angular.

## Get Started

To start using AutoForms you must install the necessary packages to your ASP.NET and Angular applications.

### Setup AutoForms for ASP.NET

#### Install NuGet package

In order to get AutoForms for ASP.NET, you can [grab the latest NuGet package](https://www.nuget.org/packages/AutoForms/) or just run

```shell
Install-Package AutoForms
```

#### Register dependencies to IoC container

Register [FormBuilderFactory](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms/FormBuilderFactory.cs) and other dependencies.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddAutoForms();
}
```

Congratulations! Server-side preparations are done!

### Setup AutoForms for Angular

#### Install AutoForms client library

Install `@auto-forms/client` using NPM.

```shell
npm install @auto-forms/client
```

#### Import AutoFormsModule to AppModule

Add the `AutoFormsModule` to the `AppModule`, so you'll be able to inject [FormBuilderCllient](https://github.com/Chacaroon/AutoForms/blob/feature/readme/src/AutoForms.Client/projects/client/src/form-builder-client.ts).

```js
import { AutoFormsModule } from '@auto-forms/client';

@NgModule({
    declarations: [AppComponent],
    imports: [AutoFormsModule.forRoot()],
    bootstrap: [AppComponent]
})
export class AppModule {}
```

Client-side preparations are done as well!

Now you can build your first model structure, pass one to an Angular application and enjoy using auto-generated strongly-typed forms!

## First steps

### Build data structure

Prepare a model for building its structure.

```csharp
class ValuesModel
{
    public string Name { get; set; }

    public IEnumerable<int> SimpleValues { get; set; }

    public ValueModel ComplexValue { get; set; }
}

class ValueModel
{
    public string NestedValue { get; set; }
}
```

Inject [FormBuilderFactory](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms/FormBuilderFactory.cs) to the controller.

```csharp
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly FormBuilderFactory _formBuilderFactory;

    public ValuesController(FormBuilderFactory formBuilderFactory)
    {
        _formBuilderFactory = formBuilderFactory;
    }
}
```

Now you can create an instance of the [FormBuilder](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms/FormBuilder.cs) with the `ValuesModel` as a generic type. Then call `Build()` to convert the passed type to a model's structure and send the returned object to the Angular application.

```csharp
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly FormBuilderFactory _formBuilderFactory;

    ...

    [HttpGet]
    public IActionResult Get()
    {
        var formBuilder = _formBuilderFactory.CreateFormBuilder<ValuesModel>();
        var node = formBuilder.Build();
        
        return Ok(node);
    }
}
```

Now let's go to the Angular application to process just created data structure. 

### Convert data structure to Angular FormGroup

First, prepare TypeScript interfaces.

```ts
interface ValuesModel {
    name: string;
    values: ValueModel[];
    value: ValueModel;

}

interface ValueModel {
    value: string;
}
```

Now you can create an angular component and send an HTTP request to the `/api/values` endpoint to obtain the built data structure.
<br/><br/>
In order to convert obtained data structure to a `FormGroup` you can use `Observable.pipe()`. Depending on your targets you can either map the data structure with the injected `FormBuilderClient` or use the `buildForm()` operator that is part of the `@auto-forms/client` library.
<br/><br/>
As a result, you have `Observable` that emits values of a type `AfFormGroup<ValuesModel>`. This type extends Angular's [FormGroup](https://angular.io/api/forms/FormGroup).

```ts
import { Component, OnInit } from '@angular/core';
import { AfFormGroup, AfNode, buildForm, FormBuilderClient } from '@auto-forms/client';
import { map, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-values',
    templateUrl: './values.component.html',
    styleUrls: ['./values.component.scss']
})
export class ValuesComponent implements OnInit {

    form$: Observable<AfFormGroup<ValuesModel>>;

    constructor(
        private http: HttpClient,
        private formBuilderClient: FormBuilderClient) {
    }

    ngOnInit(): void {
        this.form$ = this.http.get<AfNode>('/values').pipe(
            map(node => this.formBuilderClient.build<ValuesModel>(node)),
            shareReplay({ bufferSize: 1, refCount: true })
        );
        // or
        this.form$ = this.http.get<AfNode>('/values').pipe(
            buildForm<ValuesModel>()
        );
    }
    
    onSubmit(form: AfFormGroup<ValuesModel>) {
        this.http.post('/values', form.value).subscribe();
    }
}
```

Now it's time to use the obtained form. Let's bind one to the ValuesComponent's template. You can use `AfFormGroup`, `AfFormArray` and `AfFormControl` as corresponding [reactive forms](https://angular.io/guide/reactive-forms) classes.


```html
<form *ngIf="form$ | async as form" [formGroup]="form" (ngSubmit)="onSubmit(form)">
    <input type="text" formControlName="name">
    
    <ng-container formGroupName="complexValue">
        <input type="text" formGroupName="nestedValue">
    </ng-container>

    <div formArrayName="simpleValues">
        <ng-container *ngFor="let _ of form.controls.simpleValues.controls; let i = index">
            <input type="number" [formControlName]="i">
            <button (click)="form.controls.simpleValues.removeAt(i)">Remove control</button>
            <br>
        </ng-container>
    </div>

    <button (click)="form.controls.simpleValues.addControl()">Add control</button>

    <button type="submit">Submit</button>
</form>
```

Once you've hit the **Submit** button, the form value will be sent to the API.
Let's extend the `ValuesController` to receive the sent value.

```csharp
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    ...
    
    [HttpPost]
    public IActionResult Post([FromBody] ValuesModel model)
    {
        // Do stuff...

        return Ok();
    }
}
```

Finally, the `Post()` endpoint receives exactly those models for which the data structure has been built.
