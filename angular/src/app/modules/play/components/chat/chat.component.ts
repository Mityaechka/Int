import { GameProcessService } from './../../../../services/game-process.service';
import { FormControl, Validators } from '@angular/forms';
import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { ChatMessage } from '../../../../entities/chat-message.entity';
import { DialogsService } from '../../../../services/dialogs.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit {
  @Input() messages: ChatMessage[];
  messageControl = new FormControl('', [Validators.required]);
  constructor(
    private gameProcessService: GameProcessService,
    private dialogs: DialogsService,
    private detector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {}
  async sendMessage() {
    const response = await this.gameProcessService.sendChatMessage(
      this.messageControl.value
    );
    if (response.isSuccess) {
      this.messageControl.setValue('');
    } else {
      this.dialogs.pushAlert(response.errorMessage);
    }
    this.detector.markForCheck();
  }
}
