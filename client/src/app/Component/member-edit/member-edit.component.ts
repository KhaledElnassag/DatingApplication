import { Component, HostListener, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Members } from 'src/app/Models/members';
import { UserLogin } from 'src/app/Models/user-login';
import { AccountService } from 'src/app/Services/account.service';
import { MembersService } from 'src/app/Services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent {
  member?:Members
  @ViewChild('editForm') form:NgForm|undefined
  user:UserLogin|null=null
  @HostListener('window:beforeunload',['$event'])unloadNotification($event:any){
    if(this.form?.dirty){
      $event.returnValue=true
    }
  }
  constructor(private accountService:AccountService,private memberService:MembersService,
    private toastr:ToastrService){
  
  }
    ngOnInit(): void {
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next:userRes=>this.user=userRes
      })
      this.loadMember();
    }
  loadMember(){
    if(!this.user)return;
    this.memberService.getMember(this.user.userName).subscribe({
      next:user=>{
        this.member=user;
      },
      error:err=>{
        if(err)
        this.toastr.error(err.error.message)
      }
    });
  }
  editMember(){
    this.memberService.updateMember(this.form?.value).subscribe({
      next:_=>{
        this.toastr.success("Submitted")
        this.form?.reset(this.member)
      }
    })
    
  }
}
