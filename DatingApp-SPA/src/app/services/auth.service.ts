import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url = 'http://localhost:5000/api/';
  jwtHelper = new JwtHelperService();
  decodeToken: any;

  constructor(private http: HttpClient) {}
  Login(model: any) {
    console.log('Service model is ' + model);

    return this.http.post(this.url + 'Auth/Login', model).pipe(
      map((res: any) => {
        const user = res;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodeToken = this.jwtHelper.decodeToken(user.token);
          console.log('Decoded Token is ' + JSON.stringify(this.decodeToken));
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.url + 'Auth/Register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
