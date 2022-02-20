import { FbFormNodeType } from "./types/form-node-type";
import { FbNode, FormArrayNode, FormControlNode, FormGroupNode, NodeType } from "./form-nodes/node";
import { FbFormControl } from "./models/form-control";
import { FbFormArray } from "./models/form-array";
import { FbFormGroup } from "./models/form-group";

export class FormBuilderClient {
    public build<T>(form: FbNode): FbFormNodeType<T> {
        switch (form.nodeType) {
            case NodeType.Control: {
                return new FbFormControl<T>((form as FormControlNode).value) as FbFormNodeType<T>;
            }
            case NodeType.Array: {
                const nodes = (form as FormArrayNode).nodes.map(x => this.build<T[keyof T]>(x));

                return new FbFormArray<T>(nodes) as FbFormNodeType<T>;
            }
            case NodeType.Group: {
                const nodes = Object.entries((form as FormGroupNode).nodes)
                    .reduce((prev, [key, node]) => ({
                        ...prev,
                        [key]: this.build(node)
                    }), {} as { [key in keyof T]: FbFormNodeType<T[key]> });

                return new FbFormGroup<T>(nodes) as FbFormNodeType<T>;
            }
        }
    }
}
