import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(        private router: Router,
    private accountService: AccountService) {
      if (this.accountService.authValue.token) {
        this.router.navigate(['/']);
    }
     }

  ngOnInit(): void {
  }

}
