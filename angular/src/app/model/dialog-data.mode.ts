import { MatDialogConfig } from '@angular/material/dialog';

export class DialogData {
  constructor(
    public component?: any,
    public data?: any,
    public onInstance?: (instance: any) => void,
    public config?: MatDialogConfig<any>
  ) {}
}
