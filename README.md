# AutoForms

AutoForms is a system of libraries designed to synchronize the data structure between parts of a web application based on ASP.NET 6 and Angular.

[![Build and test](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml)

## Get Started

To start using AutoForms you must install packages to your ASP.NET and Angular applications.

### Setup AutoForms for ASP.NET

#### Install NuGet package

In order to get AutoForms for ASP.NET you can [grab the latest NuGet package](https://www.nuget.org/packages/AutoForms/) or just run

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

```shell
npm install @auto-forms/client
```

#### Import AutoFormsModule to AppModule

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

Now you can build your first model structure, pass one to an Angular application and enjoy using auto-generated strongly-typed FormGroup!

## First steps

### Build data structure

Prepare model for building it's structure.

```csharp
class ValuesModel
{
    public string Name { get; set; }

    public IEnumerable<int> SimpleValues { get; set; }

    public ValuesModel ComplexValue { get; set; }
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

Now you can create instance of the [FormBuilder](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms/FormBuilder.cs) with the `ValuesModel` as a generic type. Then call `Build()` to convert the passed type to a model's structure and send received object to the Angular application.

```csharp
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

Now let's go to the Angular application to process just created data's structure. 
