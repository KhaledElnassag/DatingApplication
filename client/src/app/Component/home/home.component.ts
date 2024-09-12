import { Component } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/Services/account.service';
import { MembersService } from 'src/app/Services/members.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  registerMode = false;
constructor(public accountService:AccountService,memberService:MembersService,toast:ToastrService) {
  memberService.getAllMembers().subscribe({
    next:(data)=>console.log(data),
    error:err=>toast.error(err.error.message)
  })
}
registerToggle() {
  this.registerMode = !this.registerMode
}
setRegister(event:boolean){
  this.registerMode=event;
}

}
