import { Component } from '@angular/core';
import { AuthResultModel } from './models';
import { AccountService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  authValue:AuthResultModel;

  constructor(private accountService: AccountService) { 

    this.accountService.authResult.subscribe(response=>{
      this.authValue = response;
    });
  }



  ngOnInit(): void {
  }
  logout() {
    this.accountService.logout();
  }
}
