export interface AfValidator {
  type: AfValidatorType
  message: string
  value: any
}

export enum AfValidatorType {
  Required = 1,
  MinLength = 2,
  MaxLength = 3,
  RegularExpression = 4,
}
