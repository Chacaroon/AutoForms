import { AfFormNodeType } from './types';
import {
  AfNode,
  AfValidator,
  AfValidatorType,
  FormArrayNode,
  FormControlNode,
  FormGroupNode,
  NodeType
} from "./form-nodes";
import { AfFormArray, AfFormControl, AfFormGroup } from "./models";
import { ValidatorFn } from "@angular/forms";
import * as AfValidators from './validators'


export class FormBuilderClient {
  public build<T>(form: AfNode): AfFormNodeType<T> {
    const validators = this.mapValidators(form.validators);
    switch (form.type) {
      case NodeType.Control: {
        return new AfFormControl<T>((form as FormControlNode).value, validators) as AfFormNodeType<T>;
      }
      case NodeType.Array: {
        const nodes = (form as FormArrayNode).nodes.map(x => this.build<T>(x));

        return new AfFormArray<T>(nodes, (form as FormArrayNode).nodeSchema, this, validators) as AfFormNodeType<T>;
      }
      case NodeType.Group: {
        const nodes = Object.entries((form as FormGroupNode).nodes)
          .reduce((prev, [key, node]) => ({
            ...prev,
            [key]: this.build(node)
          }), {} as { [key in keyof T]: AfFormNodeType<T[key]> });

        return new AfFormGroup<T>(nodes, validators) as AfFormNodeType<T>;
      }
    }
  }

  private mapValidators(validators: AfValidator[]): ValidatorFn[] {
    const validatorsMap = {
      [AfValidatorType.Required]: (validator: AfValidator) => AfValidators.required(validator.message),
      [AfValidatorType.MinLength]: (validator: AfValidator) => AfValidators.minLength(validator.value, validator.message),
      [AfValidatorType.MaxLength]: (validator: AfValidator) => AfValidators.maxLength(validator.value, validator.message),
    };

    return validators ? validators.map(x => validatorsMap[x.type](x)) : [];
  }
}
