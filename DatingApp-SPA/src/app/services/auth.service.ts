import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url = 'http://localhost:5000/api/';
  constructor(private http: HttpClient) {}
  Login(model: any) {
    console.log('Service model is ' + model);

    return this.http.post(this.url + 'Auth/Login', model).pipe(
      map((res: any) => {
        const user = res;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.url + 'Auth/Register', model);
  }
}
