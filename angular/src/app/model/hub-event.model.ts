import { ApiModel } from './api.model';
import { HubEventEnum } from '../enums/hub-event.enum';

export class HubEvent<T> {
  constructor(public event: HubEventEnum, public result: ApiModel<T>) {}
}
