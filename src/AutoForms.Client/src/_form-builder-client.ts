import { AfFormNodeType } from "./types/form-node-type";
import {
    AfNode,
    FormArrayNode,
    FormControlNode,
    FormGroupNode,
    NodeType,
    AfValidator,
    AfValidatorType
} from "./form-nodes/node";
import { AfFormControl } from "./models/form-control";
import { AfFormArray } from "./models/form-array";
import { AfFormGroup } from "./models/form-group";
import { ValidatorFn, Validators } from "@angular/forms";

export class FormBuilderClient {
    public build<T>(form: AfNode): AfFormNodeType<T> {
        const validators = this.mapValidators(form.validators);
        switch (form.nodeType) {
            case NodeType.Control: {
                return new AfFormControl<T>((form as FormControlNode).value, validators) as AfFormNodeType<T>;
            }
            case NodeType.Array: {
                const nodes = (form as FormArrayNode).nodes.map(x => this.build<T[keyof T]>(x));

                return new AfFormArray<T>(nodes, (form as FormArrayNode).nodeSchema, validators) as AfFormNodeType<T>;
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
            [AfValidatorType.Required]: (validator: AfValidator) => Validators.required,
            [AfValidatorType.MinLength]: (validator: AfValidator) => Validators.minLength(validator.value),
            [AfValidatorType.MaxLength]: (validator: AfValidator) => Validators.maxLength(validator.value),
        }

        return validators?.map(x => validatorsMap[x.type](x)) ?? [];
    }
}
