import { Component, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Register } from 'src/app/Models/register';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
model:Register={userName:'',password: ''};
@Output() eventEmitter:EventEmitter<boolean>
/**
 *
 */
constructor(private accountService:AccountService,private toast:ToastrService) {
  this.eventEmitter=new EventEmitter<boolean>();
}
register(){
  if(this.model!=null){
    this.accountService.register(this.model).subscribe({
      next:(data)=>this.cancel(),
      error:(err)=>this.toast.error(err.error.message)
    })
  }
}
cancel(){
this.eventEmitter.emit(false);
}
}
