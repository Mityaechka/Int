import { Route, Router, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { GameProcessHubService } from './../../../services/game-process-hub.service';
import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  ChangeDetectorRef,
  Query,
  QueryList,
  ViewChildren,
} from '@angular/core';

import * as SimplePeer from 'simple-peer';
import * as wrtc from 'wrtc';
import { Stream } from 'stream';
import { HubEvent } from '../../../model/hub-event.model';
import { HubEventEnum } from '../../../enums/hub-event.enum';
declare var Peer: any;
export class PeerConnection {
  connection: SimplePeer.Instance;
  stream: MediaStream;
  offer: any;
  isConnected = false;
  answersFrom: string[] = [];
  isAnswerSend = false;
  connectionId: string;
}
// import wrtc = require('wrtc')
@Component({
  selector: 'app-camera-main',
  templateUrl: './camera-main.component.html',
  styleUrls: ['./camera-main.component.css'],
})
export class CameraMainComponent implements OnInit {
  @ViewChild('myvideo') myVideo: any;

  peer;
  anotherid;
  mypeerid;

  config = {
    host: 'localhost',
    port: 9000,
    path: '/myapp',
  };
  constraints = {
    width: { min: 640, ideal: 640, max: 640 },
    height: { min: 400, ideal: 400 },
    aspectRatio: 1.777777778,
    frameRate: { max: 15 },
    facingMode: { exact: 'user' },
  };

  peers: { [index: string]: any };
  connectionId: string;
  hub: HubConnection;
  stream: MediaStream;
  streams: { [index: string]: MediaStream } = {};
  get s() {
    return Object.values(this.streams);
  }
  constructor() {}

  ngOnInit() {
    this.hub = new HubConnectionBuilder()
      .withUrl('https://localhost:44346/camera')
      .build();
    this.hub.start().then(async () => {
      this.connectionId = await this.hub.invoke('GetconnectionId');
      this.stream = await this.getMediaStream();
      //this.stream.co;
      this.peer = new Peer('', {
        host: 'localhost',
        port: 9000,
        path: '/myapp',
      });

      this.peer.on('connection', function (conn) {
        conn.on('data', function (data) {
          console.log(data);
        });
      });
      this.peer.on('call', (call) => {
        // Answer the call, providing our mediaStream
        call.answer(this.stream);
      });
      setTimeout(() => this.hub.invoke('NewConnection'), 3000);
    });

    this.hub.on('Peer', (peerId: string, connectionId: string) => {
      const call = this.peer.call(peerId, this.stream);
      call.on('stream', (stream) => {
        console.log('stream');
        this.streams[connectionId] = stream;
      });
    });
    this.hub.on('NewConnection', () =>
      this.hub.invoke('SendMyPeer', this.peer.id)
    );

    // this.peer.on('call', function (call) {
    //   n.getUserMedia(
    //     { video: true, audio: true },
    //     function (stream) {
    //       call.answer(stream);
    //       call.on('stream', function (remotestream) {
    //         video.srcObject = remotestream;
    //         video.play();
    //       });
    //     },
    //     function (err) {
    //       console.log('Failed to get stream', err);
    //     }
    //   );
    // });
  }
  getMediaStream() {
    return navigator.mediaDevices.getUserMedia({
      video: {
        width: { min: 640, ideal: 640 },
        height: { min: 400, ideal: 640 },
        aspectRatio: { ideal: 1.7777777778 },
        frameRate: { max: 15 },
      },
      audio: true,
    });
  }
  // getMediaStream() {
  //   return navigator.mediaDevices.getUserMedia({
  //     video: true,
  //     audio: true,
  //   });

  //   var navigator = <any>navigator;

  //   navigator.getUserMedia =
  //     navigator.getUserMedia ||
  //     navigator.webkitGetUserMedia ||
  //     navigator.mozGetUserMedia ||
  //     navigator.msGetUserMedia;

  //   return navigator.getUserMedia({ video: true, audio: true });
  // }
  videoconnect() {
    // let video = this.myVideo.nativeElement;
    // var localvar = this.peer;
    // var fname = this.anotherid;
    // var getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    // var n = <any>navigator;
    // n.getUserMedia =
    //   n.getUserMedia ||
    //   n.webkitGetUserMedia ||
    //   n.mozGetUserMedia ||
    //   n.msGetUserMedia;
    // n.getUserMedia(
    //   { video: true, audio: true },
    //   function (stream) {
    //     var call = localvar.call(fname, stream);
    //     call.on('stream', function (remotestream) {
    //       video.srcObject = remotestream;
    //       video.play();
    //     });
    //   },
    //   function (err) {
    //     console.log('Failed to get stream', err);
    //   }
    // );
  }

  // constructor(
  //   private gameProcessHubService: GameProcessHubService,
  //   private route: ActivatedRoute,
  //   private det: ChangeDetectorRef
  // ) {}
  // @ViewChildren('video') videos: QueryList<HTMLVideoElement>;
  // // video: HTMLVideoElement;
  // // @ViewChild('video')
  // // set mainLocalVideo(el: ElementRef) {
  // //   this.video = el.nativeElement;
  // // }
  // connectionId: string;
  // connections: { [index: string]: PeerConnection } = {};

  // get conn() {
  //   return Object.values(this.connections);
  // }
  // hub: HubConnection;

  // async ngOnInit() {
  //   this.hub = new HubConnectionBuilder()
  //     .withUrl('https://localhost:44346/camera')
  //     .build();
  //   this.hub
  //     .start()
  //     .then(async () => {
  //       this.hub.on('NewConnection', () => {
  //         this.hub.invoke(
  //           'SendMyOffer',
  //           JSON.stringify(this.connections[this.connectionId].offer)
  //         );
  //         this.showCams();
  //       });
  //       this.hub.on('Offer', (data: string, connectionId: string) => {
  //         const offer = JSON.parse(data);
  //         let peerConnection = this.connections[connectionId];
  //         if (!peerConnection) {
  //           peerConnection = new PeerConnection();
  //           peerConnection.connectionId = connectionId;
  //           this.connections[connectionId] = peerConnection;
  //           const peer = new SimplePeer({
  //             trickle: true,
  //             initiator: false,
  //             wrtc,
  //           });
  //           peerConnection.connection = peer;
  //           peer.on('signal', (data) => {
  //             console.log(peerConnection, data);
  //             if (data.type === 'answer') {
  //               this.hub.invoke(
  //                 'SendAnswer',
  //                 JSON.stringify(data),
  //                 peerConnection.connectionId
  //               );
  //             }
  //             this.showCams();
  //           });
  //           peer.on('error', (error) => console.log('ERROR', error));
  //           peer.on('data', (data) => console.log('data', data));
  //           peer.on('connect', () => {
  //             console.log('connect');

  //             this.showCams();
  //           });
  //           peer.on('stream', (Stream) => {
  //             peerConnection.stream = Stream;
  //             this.showCams();
  //           });
  //           peerConnection.connection.signal(offer);
  //         }
  //       });
  //       this.hub.on(
  //         'Answer',
  //         (data: string, connectionId: string, list: string[]) => {
  //           const answer = JSON.parse(data);
  //           var connection = this.connections[this.connectionId];
  //           console.log('answer send', answer, connection, connectionId);

  //           console.log(answer);
  //           //if (!connection.isAnswerSend) {
  //           connection.connection.signal(answer);
  //           connection.isAnswerSend = true;
  //           //}
  //           this.showCams();
  //         }
  //       );

  //       console.log('Hub connection started');
  //       this.connectionId = await this.hub.invoke('GetConnectionId');
  //       const peerConnection = new PeerConnection();
  //       peerConnection.connectionId = this.connectionId;
  //       this.connections[this.connectionId] = peerConnection;
  //       const mediaStream = await this.getMediaStream();
  //       const peer = new SimplePeer({
  //         trickle: true,
  //         initiator: true,
  //         wrtc,
  //         stream: mediaStream,
  //       });
  //       peerConnection.stream = mediaStream;
  //       peerConnection.connection = peer;
  //       peer.on('signal', (data) => {
  //         console.log(this.connectionId, data.type);
  //         if (data.type === 'offer') {
  //           peerConnection.offer = data;
  //           this.hub.invoke('NewConnection');
  //           this.showCams();
  //           // this.hub.invoke(
  //           //   'SendMyOffer',
  //           //   JSON.stringify(this.connections[this.connectionId].offer)
  //           // );
  //         }
  //       });
  //       peer.on('error', (error) => console.log('ERROR', error));
  //       peer.on('connect', () => {
  //         console.log('connect');
  //         peerConnection.isConnected = true;
  //         setInterval(() => {
  //           peerConnection.connection.send(this.connectionId);
  //         }, 1000);
  //         this.showCams();
  //       });
  //       // peer.on('stream', (stream) => {
  //       //   peerConnection.stream = stream;
  //       //   this.showCams();
  //       // });
  //       this.det.detectChanges();
  //     })
  //     .catch((err) => {
  //       console.log('Error while establishing connection, retrying...');
  //     });
  // }

  // getMediaStream() {
  //   return navigator.mediaDevices.getUserMedia({
  //     video: true,
  //     audio: true,
  //   });
  // }
  // createNotInitiator(connectionId: string, recreate: boolean, offer?: any) {
  //   let peer = this.connections[connectionId];
  //   if (!peer || recreate) {
  //     peer = this.createConnection(false);
  //     this.connections[connectionId] = peer;
  //     peer.connection.on('signal', (data) => {
  //       console.log(connectionId, data.type);
  //       if (data.type === 'answer') {
  //         this.hub.invoke('Answer', JSON.stringify(data), connectionId);
  //       }
  //     });
  //     peer.connection.on('stream', (stream) => {
  //       console.log('stream');
  //       peer.stream = stream;
  //       // this.v.srcObject = stream;
  //       this.det.detectChanges();
  //     });
  //     peer.connection.on('error', (err) => {
  //       console.log('error', err);
  //     });
  //     peer.connection.on('close', (err) => {
  //       console.log('close', err);

  //       this.createNotInitiator(connectionId, true);
  //     });
  //   }
  //   if (!peer.isConnected && offer) {
  //     peer.connection.signal(JSON.parse(offer));
  //     peer.isConnected = true;
  //   }
  // }
  // initCamera() {
  //   this.hub = this.gameProcessHubService.hubConnection;
  //   this.hub.on('Offers', (offers: { offer: any; connectionId: string }[]) => {
  //     offers.forEach((offer) => {
  //       let peer = this.connections[offer.connectionId];
  //       if (!peer) {
  //         peer = this.createConnection(false);
  //         this.connections[offer.connectionId] = peer;
  //         peer.connection.on('signal', (data) => {
  //           if (data.type === 'answer') {
  //             this.hub.invoke(
  //               'Answer',
  //               JSON.stringify(data),
  //               offer.connectionId
  //             );
  //           }
  //         });
  //         peer.connection.on('stream', (stream) => {
  //           console.log('stream');
  //           peer.stream = stream;
  //           //this.v.srcObject = stream;
  //           this.det.detectChanges();
  //         });
  //         peer.connection.on('error', (err) => {
  //           console.log('error', err);
  //         });
  //         peer.connection.on('close', (err) => {
  //           console.log('close', err);
  //         });
  //       }
  //       if (!peer.isConnected) {
  //         peer.connection.signal(JSON.parse(offer.offer));
  //         peer.isConnected = true;
  //       }
  //     });
  //     this.det.detectChanges();
  //   });
  //   this.hub.on('NewOffer', () => {
  //     this.hub.invoke('GetOffers');
  //   });
  //   this.hub.on('Answer', (data: any) => {
  //     const peer = this.connections[this.connectionId];
  //     peer.connection.signal(JSON.parse(data));
  //   });
  //   navigator.mediaDevices
  //     .getUserMedia({
  //       video: true,
  //       audio: true,
  //     })
  //     .then(async (stream) => {
  //       this.connectionId = await this.hub.invoke('GetConnectionId');
  //       this.createInitiator(this.connectionId, stream);
  //     });
  // }
  // createInitiator(connectionId: string, stream: MediaStream) {
  //   console.log('INITIATOR create');
  //   const peer = this.createConnection(true, stream);
  //   peer.stream = stream;
  //   this.connections[connectionId] = peer;
  //   peer.connection.on('error', (err) => console.log('error', err));

  //   peer.connection.on('signal', (data) => {
  //     if (data.type === 'offer') {
  //       this.hub.invoke('NewOffer', JSON.stringify(data));
  //     }
  //   });
  //   peer.connection.on('close', (err) => {
  //     console.log('CLOSE INIITIATOR');
  //     this.createInitiator(connectionId, stream);
  //   });
  //   peer.connection.on('connect', () => {
  //     console.log('CONNECT');
  //   });
  // }
  // createConnection(initiator: boolean, stream?: MediaStream): PeerConnection {
  //   // return {
  //   //   connection: new SimplePeer({
  //   //     trickle: false,
  //   //     initiator,
  //   //     wrtc,
  //   //     stream,
  //   //   }),
  //   //   stream: undefined,
  //   //   isConnected: false,
  //   // };
  //   return undefined;
  // }
  // showCams() {
  //   const valuse = Object.values(this.connections);
  //   this.videos.forEach((v: HTMLVideoElement, i: number) => {
  //     v.srcObject = valuse[i].stream;
  //   });
  // }
}
