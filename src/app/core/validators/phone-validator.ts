import { AbstractControl } from '@angular/forms';

export function phoneNumberValidator(
  control: AbstractControl
): { [key: string]: any } | null {
  let valid: boolean = false;
  if (control.value != '') {
    valid = (/^\d+$/.test(control.value) && ((control.value.length > 9) ? true : false));
  } else {
    valid = true;
  }
  return valid
    ? null
    : { invalidNumber: { valid: false, value: control.value } };
}