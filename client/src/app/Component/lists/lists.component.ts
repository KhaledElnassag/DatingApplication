import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Members } from 'src/app/Models/members';
import { MembersService } from 'src/app/Services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent {
  members:Members[]|undefined;
  constructor(private memberService:MembersService,private toaster:ToastrService){
  }
    ngOnInit(): void {
      this.loadLikes();
    }
  
    loadLikes(){
      this.memberService.getLikes().subscribe({
        next:(membersRes)=>{
          if(membersRes)
           this.members=membersRes
          else this.toaster.success("You have not like any one yet!")
        }
      })
    }
}
