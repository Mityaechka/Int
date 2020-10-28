import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, OnInit, Input, Inject } from '@angular/core';

@Component({
  templateUrl: './alert-dialog.component.html',
  styleUrls: ['./alert-dialog.component.css']
})
export class AlertDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string, title: string }) { }

  ngOnInit(): void {
  }

}
