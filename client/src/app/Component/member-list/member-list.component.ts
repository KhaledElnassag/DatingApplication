import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Members } from 'src/app/Models/members';
import { Pagination } from 'src/app/Models/Pagination';
import { UserParams } from 'src/app/Models/UserParams';
import { AccountService } from 'src/app/Services/account.service';
import { MembersService } from 'src/app/Services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit{
members?:Members[]
pagination?:Pagination<Members[]>
params:UserParams={pageIndex:1,pageSize:5,minAge:18,maxAge:100,gender:'male'}
pageEvent:any;
genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];
constructor(public accountService:AccountService,private memberService:MembersService,private toast:ToastrService) {
 
}
ngOnInit(): void {
  debugger;
 this.load();
  }
  load(){
    this.memberService.getAllMembers(this.params).subscribe({
      next:(data:Pagination<Members[]>)=>{
        debugger;
        this.pagination=data},
      error:err=>this.toast.error(err.error.message)
    })
  }
  pageChanged(event:any){
if(this.pageEvent!=event){
  this.params.pageIndex=event.page;
  this.load();
}
  }
  resetFilters(){
    this.params={pageIndex:1,pageSize:5,minAge:18,maxAge:100,gender:'male'}
    this.load();
  }
}
