import { LoadingDialogComponent } from './../components/common/loading-dialog/loading-dialog.component';
import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Event, NavigationEnd, Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  ref: MatDialogRef<LoadingDialogComponent>;
  constructor(private router: Router, private dialog: MatDialog) {}

  start(message?): void {
    this.ref = this.dialog.open(LoadingDialogComponent, {
      disableClose: true,
      data: message === '' || message === undefined ? 'Загрузка...' : message,
      panelClass: 'background-gray',
    });
  }

  stop(): void {
    if (this.ref) {
      this.ref.close();
    }
  }
}
