import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  Input,
  ChangeDetectorRef,
} from '@angular/core';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CountdownComponent implements OnInit {
  countDown: Subscription;
  @Input() time: Date;
  counter: number = 0;
  tick = 1000;

  constructor(private detector: ChangeDetectorRef) {}

  ngOnInit(): void {
    const now = new Date();
    this.counter = this.time.getTime() / 1000 - now.getTime() / 1000;
    timer(0, 1000).subscribe(() => {
      --this.counter;
      if (this.counter <= 0) {
        this.counter = 0;
      }
      this.detector.markForCheck();
    });
  }
}
