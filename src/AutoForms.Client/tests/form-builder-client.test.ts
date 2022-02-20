import { FormArrayNode, FormControlNode, FormGroupNode, NodeType } from "../src/form-nodes/node";
import { FormBuilderClient } from "../src/_form-builder-client";
import { FormArray, FormControl, FormGroup } from "@angular/forms";

const formControlModel: FormControlNode = {
    nodeType: NodeType.Control,
    value: "value"
};

const formGroupModel: FormGroupNode = {
    nodeType: NodeType.Group,
    nodes: {
        control: {
            nodeType: NodeType.Control,
            value: "value"
        } as FormControlNode,
        array: {
            nodeType: NodeType.Array,
            nodes: [
                {
                    nodeType: NodeType.Control,
                    value: "value"
                } as FormControlNode
            ]
        } as FormArrayNode
    }
};

const formArrayModel: FormArrayNode = {
    nodeType: NodeType.Array,
    nodes: [
        {
            nodeType: NodeType.Control,
            value: "value"
        } as FormControlNode
    ]
};

const formBuilderClient = new FormBuilderClient();

test('FormBuilderClient builds FormControlNode to FormControl', () => {
    const control = formBuilderClient.build<string>(formControlModel);

    expect(control).toBeInstanceOf(FormControl);
});

test('FormBuilderClient builds FormGroupNode to FormGroup with proper nested controls', () => {
    const group = formBuilderClient.build<{ control: string, array: { a: string }[] }>(formGroupModel);

    expect(group).toBeInstanceOf(FormGroup);
    expect(group.controls.control).toBeInstanceOf(FormControl);
    expect(group.controls.array).toBeInstanceOf(FormArray);
});

test('FormBuilderClient builds FormArrayNode to FormArray with proper nested controls', () => {
    const array = formBuilderClient.build<string[]>(formArrayModel);

    expect(array).toBeInstanceOf(FormArray);
});


