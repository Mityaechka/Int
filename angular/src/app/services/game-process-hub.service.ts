import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

import { ApiModel } from './../model/api.model';
import { Injectable, EventEmitter, Inject } from '@angular/core';
import { HubEvent } from '../model/hub-event.model';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
declare var Peer: any;

@Injectable({
  providedIn: 'root',
})
export class GameProcessHubService {
  constructor() {}

  private connectionEstablished = new EventEmitter<boolean>();
  public hubConnection: HubConnection;

  connectionId: string;

  peer: any;
  private stream: MediaStream;
  private streams: { [index: string]: MediaStream } = {};

  private get streamValues() {
    return Object.values(this.streams);
  }
  streamsEmitter = new EventEmitter<MediaStream[]>();

  private config = {
    host: 'localhost',
    port: 9000,
    path: '/myapp',
  };

  private videoConstraints = {
    width: { min: 640, ideal: 640, max: 640 },
    height: { min: 400, ideal: 400 },
    aspectRatio: 1.777777778,
    frameRate: { max: 15 },
    facingMode: { exact: 'user' },
  };

  init(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44346/game')
      .build();
    this.startConnection();
  }
  private startConnection(): void {
    this.hubConnection
      .start()
      .then(async () => {
        console.log('Hub connection started');
        this.connectionId = await this.hubConnection.invoke('GetConnectionId');

        this.connectionEstablished.emit(true);

        this.initPeer();
      })
      .catch((err) => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(() => {
          this.startConnection();
        }, 5000);
      });
  }

  registerEvent<T>(data: { event: string; action: (result: T) => void }): void {
    const emitter = new EventEmitter<any>();
    emitter.subscribe((e: T) => data.action(e));
    this.hubConnection.on(data.event, (result: any) => {
      emitter.emit(result);
    });
  }
  public onConnect(action: () => void) {
    this.connectionEstablished.subscribe(action);
  }
  invoke(method: { name: string; data?: any }) {
    if (method.data != null) {
      this.hubConnection.invoke(method.name, method.data);
    } else {
      this.hubConnection.invoke(method.name);
    }
  }
  async initPeer() {
    this.stream = await this.getMediaStream();
    this.addStream(this.connectionId, this.stream);
    this.peer = new Peer('', {
      host: 'localhost',
      port: 9000,
      path: '/myapp',
    });
    this.peer.on('call', (call: { answer: (arg0: MediaStream) => void }) => {
      call.answer(this.stream);
    });

    this.hubConnection.on('Peer', (peerId: string, connectionId: string) => {
      const call = this.peer.call(peerId, this.stream);
      call.on('stream', (stream: MediaStream) => {
        console.log('stream');
        this.addStream(connectionId, stream);
      });
    });
    this.hubConnection.on('NewConnection', () =>
      this.hubConnection.invoke('SendMyPeer', this.peer.id)
    );

    setTimeout(() => this.hubConnection.invoke('NewConnection'), 3000);
  }
  getMediaStream() {
    return navigator.mediaDevices.getUserMedia({
      video: true,
      audio: true,
    });
  }

  addStream(connectionId: string, stream: MediaStream) {
    this.streams[connectionId] = stream;
    this.emitStreams();
  }
  removeStream(connectionId: string) {
    delete this.streams[connectionId];
    this.emitStreams();
  }
  emitStreams() {
    this.streamsEmitter.emit(this.streamValues);
  }
}
