import { Component, OnInit } from '@angular/core';
import { AuthResultModel, UserModel } from '../models';
import { AccountService, UserService } from '../services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user:UserModel
  constructor(private userService: UserService) { }

  ngOnInit(): void {

    this.userService.getUser()
      .subscribe(response=>{
        this.user = response;
      },error=>{

      })
  }


}
