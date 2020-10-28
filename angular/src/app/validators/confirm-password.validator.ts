import { FormGroup, ValidationErrors } from '@angular/forms';

export class ConfirmedValidator {
  static Validate(controlName: string, matchingControlName: string): ValidationErrors {
    return (formGroup: FormGroup) => {

      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (matchingControl.errors && !matchingControl.errors.confirmedValidator) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
}

