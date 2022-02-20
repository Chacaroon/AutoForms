export interface FbNode {
    nodeType: NodeType
}

export enum NodeType {
    Control = 1,
    Group = 2,
    Array = 3
}

export interface FormControlNode extends FbNode {
    nodeType: NodeType.Control;
    value: any;
}

export interface FormGroupNode extends FbNode {
    nodeType: NodeType.Group;
    nodes: { [key: string]: FbNode }
}

export interface FormArrayNode extends FbNode {
    nodeType: NodeType.Array;
    nodes: FbNode[];
}
