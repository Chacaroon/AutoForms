import { AfValidator } from './validator.model';

export interface AfControl {
  type: ControlType,
  validators: AfValidator[]
}

export interface FormControlNode extends AfControl {
  type: ControlType.Control;
  value: any;
}

export interface FormGroupNode extends AfControl {
  type: ControlType.Group;
  controls: { [key: string]: AfControl }
}

export interface FormArrayNode extends AfControl {
  type: ControlType.Array;
  controls: AfControl[];
  controlSchema: AfControl
}

export enum ControlType {
  Control = 1,
  Group = 2,
  Array = 3
}
