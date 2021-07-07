import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(    private router: Router,
    private http: HttpClient) { }

    getUser(): Observable<UserModel> {
      return this.http.get<UserModel>(`${environment.apiUrl}User`);
  }
}
