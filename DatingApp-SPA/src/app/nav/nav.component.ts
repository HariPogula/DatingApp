import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {}

  Login() {
    console.log('Component model is ' + JSON.stringify(this.model));

    this.authService.Login(this.model).subscribe(
      (next) => {
        this.alertifyService.success('Logged in succesfully');
      },
      (err) => {
        this.alertifyService.error(err);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
    // const token = localStorage.getItem('token');
    // return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    this.alertifyService.message('Logged out');
  }
}
