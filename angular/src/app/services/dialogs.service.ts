import { AlertDialogComponent } from '../components/common/alert-dialog/alert-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Injectable } from '@angular/core';
import { DialogData } from '../model/dialog-data.mode';

@Injectable({
  providedIn: 'root',
})
export class DialogsService {
  dialogs: MatDialogRef<any>[] = [];
  constructor(private dialog: MatDialog) {}
  push(dialogData: DialogData): void {
    const data = Object.assign(
      dialogData.config ?? { panelClass: 'background-gray' },
      {
        disableClose: false,
        data: dialogData.data,
      }
    );
    const ref = this.dialog.open(dialogData.component, data);
    if (dialogData.onInstance) {
      dialogData.onInstance(ref.componentInstance);
    }

    this.dialogs.push(ref);
  }
  pop(): void {
    const ref = this.dialogs.pop();
    ref.close();
  }
  pushAlert(message: string, title = 'Внимание!'): void {
    this.push({ component: AlertDialogComponent, data: { message, title } });
  }
}
