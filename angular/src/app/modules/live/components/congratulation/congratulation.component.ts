import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../../../entities/user.entity';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-congratulation',
  templateUrl: './congratulation.component.html',
  styleUrls: ['./congratulation.component.css'],
})
export class CongratulationComponent implements OnInit {
  user: User;
  constructor(private autService: AuthService) {}

  ngOnInit(): void {
    this.user = this.autService.user;
  }
}
