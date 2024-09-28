import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Members } from 'src/app/Models/members';
import { MembersService } from 'src/app/Services/members.service';
import { PresenceService } from 'src/app/Services/Presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
 @Input() member!:Members
  constructor(private memberService:MembersService,private toaster:ToastrService,public presence:PresenceService) {
  }
ngOnInit(): void {

}
addLike(userName:string){
  this.memberService.addLike(userName).subscribe({
    next:_=>this.toaster.success(`You have liked ${this.member?.knownAs}`)
  });
}
}
