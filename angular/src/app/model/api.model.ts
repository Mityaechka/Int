export class ApiModel<T>{
  public isSuccess: boolean;
  public errorMessage: string;
  public data: T;
  constructor(isSuccess: boolean, errorMessage: string, data: T) {
    this.isSuccess = isSuccess;
    this.errorMessage = errorMessage;
    this.data = data;
  }
}
