import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  AfterViewInit,
} from '@angular/core';

@Component({
  selector: 'app-camera-component',
  templateUrl: './camera-component.component.html',
  styleUrls: ['./camera-component.component.css'],
})
export class CameraComponentComponent implements AfterViewInit {
  video: HTMLVideoElement;
  @ViewChild('video')
  set mainLocalVideo(el: ElementRef) {
    this.video = el.nativeElement;
  }

  @Input() stream: MediaStream;
  constructor() {}
  ngAfterViewInit(): void {
    this.video.srcObject = this.stream;
  }
}
