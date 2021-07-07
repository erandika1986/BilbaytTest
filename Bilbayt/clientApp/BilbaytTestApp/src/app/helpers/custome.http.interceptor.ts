import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../services/account/account.service';
import { environment } from 'src/environments/environment';


@Injectable()
export class CustomeHttpInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const authvalue = this.accountService.authValue;
        const isLoggedIn = authvalue && authvalue.token;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${authvalue.token}`
                }
            });
        }

        return next.handle(request);
    }
}