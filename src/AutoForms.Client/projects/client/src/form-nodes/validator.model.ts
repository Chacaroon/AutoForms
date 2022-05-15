export interface AfValidator {
  type: AfValidatorType
  message: string
  value: any
}

export enum AfValidatorType {
  Required = 1,
  MinLength,
  MaxLength
}
