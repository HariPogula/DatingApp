import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  Login() {
    console.log('Component model is ' + JSON.stringify(this.model));

    this.authService.Login(this.model).subscribe(
      (next) => {
        console.log('Logged in Successfully.');
      },
      (err) => {
        console.log('Something went wrong..');
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
  }
}
