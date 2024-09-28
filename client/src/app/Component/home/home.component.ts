import { Component, OnInit } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/Services/account.service';
import { MembersService } from 'src/app/Services/members.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent  {
  registerMode = false;
constructor(public accountService:AccountService,private memberService:MembersService,private toast:ToastrService) {
 
}
 
registerToggle() {
  this.registerMode = !this.registerMode
}
setRegister(event:boolean){
  this.registerMode=event;
}

}
