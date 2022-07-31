# AutoForms

[![NuGet](https://img.shields.io/nuget/v/AutoForms.svg)](https://www.nuget.org/packages/AutoForms/)
[![Build AutoForms](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Chacaroon/FormBuilder/actions/workflows/dotnet.yml)
[![npm](https://img.shields.io/npm/v/@auto-forms/client)](https://www.npmjs.com/package/@auto-forms/client)
[![Build AutoForms.Client](https://github.com/Chacaroon/AutoForms/actions/workflows/npm-publish.yml/badge.svg)](https://github.com/Chacaroon/AutoForms/actions/workflows/npm-publish.yml)
[![codecov](https://codecov.io/gh/Chacaroon/AutoForms/branch/master/graph/badge.svg?token=HXSK3LNOZI)](https://codecov.io/gh/Chacaroon/AutoForms)

AutoForms is a system of libraries designed to synchronize a data structure between parts of a web application based on ASP.NET 6 and Angular.

## Setup AutoForms for ASP.NET

### Install NuGet package

In order to get AutoForms for ASP.NET, you can [grab the latest NuGet package](https://www.nuget.org/packages/AutoForms/) or just run

```shell
Install-Package AutoForms
```

### Register dependencies to IoC container

Register [FormBuilderFactory](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms/FormBuilderFactory.cs) and other dependencies.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddAutoForms();
	services.AddValidationProcessor();
}
```

## Setup AutoForms for Angular

### Install AutoForms client library

Install [@auto-forms/client](https://www.npmjs.com/package/@auto-forms/client) using NPM.

```shell
npm install @auto-forms/client
```

### Import AutoFormsModule to AppModule

Add the `AutoFormsModule` to the `AppModule`, so you'll be able to inject [FormBuilderCllient](https://github.com/Chacaroon/AutoForms/blob/master/src/AutoForms.Client/projects/client/src/form-builder-client.ts).

```js
import { AutoFormsModule } from '@auto-forms/client';

@NgModule({
    declarations: [AppComponent],
    imports: [AutoFormsModule.forRoot()],
    bootstrap: [AppComponent]
})
export class AppModule {}
```

## Get Started

To start working with AutoForms, check out the [getting started guide](docs/GetStarted.md).
