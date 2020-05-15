import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  register() {
    this.authService.register(this.model).subscribe(
      (next) => {
        console.log('Registration Succesful');
      },
      (err) => {
        console.log('Registration failed', err);
      }
    );
  }

  cancel() {
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }
}
