import { Injectable } from "@angular/core";

@Injectable()
export class AuthResultModel
{
    isLoginSuccess:boolean;
    message:string;
    token:string;
    username:string;
}