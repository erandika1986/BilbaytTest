import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserModel } from 'src/app/models/Account/user.model';
import { LoginModel } from 'src/app/models/Account/Login.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { AuthResultModel } from 'src/app/models/Account/auth.result.model';
import { RegisterRequestModel } from 'src/app/models/Account/register.request.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public authResultSubject:BehaviorSubject<AuthResultModel>;
  public authResult:Observable<AuthResultModel>;
  
  constructor(
    private router: Router,
    private http: HttpClient) { 
      const userJson = localStorage.getItem('currentUser');
      let result = userJson !== null ? JSON.parse(userJson) : new AuthResultModel();
      this.authResultSubject = new BehaviorSubject<AuthResultModel>(result);
      this.authResult = this.authResultSubject.asObservable();
    }

  public get authValue(): AuthResultModel {
      const userJson = localStorage.getItem('currentUser');
      return userJson !== null ? JSON.parse(userJson) : new AuthResultModel();

  }

    login(loginVm:LoginModel) {
      return this.http.post<AuthResultModel>(`${environment.apiUrl}Account/login`, loginVm)
          .pipe(map(response => {
              localStorage.setItem('currentUser', JSON.stringify(response));
              this.authResultSubject.next(response);
              return response;
          }));
  }

  logout() {

    localStorage.removeItem('currentUser');
    this.authResultSubject.next(new AuthResultModel());
    this.router.navigate(['/account/login']);
  }


  register(user: RegisterRequestModel) {
    return this.http.post(`${environment.apiUrl}Account/register`, user);
}
}
