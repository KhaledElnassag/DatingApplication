import { Component, OnInit } from '@angular/core';
import { Login } from '../../Models/login';
import { AccountService } from 'src/app/Services/account.service';
import { UserLogin } from 'src/app/Models/user-login';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
user:Login={userName:'',password: ''};
// isLogged:Boolean=false;
userLogged:UserLogin|null=null;
constructor(public accountService:AccountService,private route:Router,private toast:ToastrService) {
}
ngOnInit(): void {
  debugger;
    // this.accountService.currentUser$.subscribe({
    //   next:(user)=>{
    //     debugger
    //     this.isLogged=!!user;
    //   console.log(this.isLogged)
    //   },
    //   error:err=>console.log(err)
    // })
  }
login(){
  if(this.user!=null){
    this.accountService.login(this.user).subscribe({
      next:()=>this.route.navigate(['/members']),
      error:(err)=>this.toast.error(err.error.message)
    })
  }
  
}
logout(){
  this.accountService.logout();
  this.route.navigate(['/'])
}
}
