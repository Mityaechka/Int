import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-main-control',
  templateUrl: './main-control.component.html',
  styleUrls: ['./main-control.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainControlComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
