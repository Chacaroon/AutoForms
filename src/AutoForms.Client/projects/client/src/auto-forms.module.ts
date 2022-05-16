import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { FormBuilderClient } from "./form-builder-client";


@NgModule({})
export class AutoFormsModule {

  constructor(@Optional() @SkipSelf() parentModule?: AutoFormsModule) {
    if (parentModule) {
      throw new Error(
        'AutoFormsModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders<AutoFormsModule> {
    return {
      ngModule: AutoFormsModule,
      providers: [
        { provide: FormBuilderClient }
      ]
    };
  }
}
