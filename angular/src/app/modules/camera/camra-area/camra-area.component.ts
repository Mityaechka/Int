import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import * as SimplePeer from 'simple-peer';

@Component({
  selector: 'app-camra-area',
  templateUrl: './camra-area.component.html',
  styleUrls: ['./camra-area.component.css'],
})
export class CamraAreaComponent implements AfterViewInit {
  video: HTMLVideoElement;
  @ViewChild('video')
  set mainLocalVideo(el: ElementRef) {
    this.video = el.nativeElement;
  }

  @Input() stream: MediaStream;
  constructor() {}

  ngAfterViewInit(): void {
    //const remoteStream = new MediaStream();
    this.video.srcObject = this.stream;
    // this.peerConnection.addEventListener('track', async (event) => {
    //   remoteStream.addTrack(event.track);
    // });
  }
}
