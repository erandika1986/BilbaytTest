import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserModel } from '../models/Account/user.model';
import { AccountService } from '../services/account/account.service';



@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  
    constructor(
        private router: Router,
        private accountService: AccountService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const authValue = this.accountService.authValue;
        if (authValue.token) {
            return true;
        }

        this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}