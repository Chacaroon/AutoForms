# Get Started

This guide walks you through the entire process of setting up and using AutoForms.

## Build data structure

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

## Convert data structure to Angular FormGroup

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

## Bind FormGroup to HTML form

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

## Get model on API side

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

Finally, the `Post` endpoint receives exactly those models for which the data structure has been built.
