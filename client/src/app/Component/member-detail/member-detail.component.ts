import { AfterViewChecked, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { Members } from 'src/app/Models/members';
import { Message } from 'src/app/Models/messages';
import { MembersService } from 'src/app/Services/members.service';
import { MesaagesService } from 'src/app/Services/mesaages.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit,AfterViewChecked {
member?:Members
galleryOptions:NgxGalleryOptions[]=[];
galleryImages:NgxGalleryImage[]=[];
@ViewChild('memberTabs') memberTabs?: TabsetComponent;
activeTab?: TabDirective;
  messages?: Message[];
  used=0
constructor(private route:ActivatedRoute,private memberService:MembersService,private messageService:MesaagesService,private curRoute:ActivatedRoute,private toast:ToastrService) {
  
}
  ngOnInit(): void {
    const id=this.curRoute.snapshot.paramMap.get("id");
    if(id){
    this.memberService.getMember(id).subscribe({
      next:user=>{
        this.member=user;
        this.galleryImages=this.getImages();
      },
      error:err=>this.toast.error(err.error.message)
    })
   
  }
  
  this.galleryOptions=[{
    width:'500px',
    height:'500px',
    imagePercent:100,
    thumbnailsColumns:4,
    imageAnimation: NgxGalleryAnimation.Slide,
    preview:false
   }]
   
  }
  ngAfterViewChecked(): void {
    if(this.memberTabs&&this.used==0){
      this.used=1
    this.route.queryParams.subscribe({
      next: params => {debugger;
        params['tab'] && this.selectTab(params['tab'])}

    })
  }
  }
  getImages(){
    if(!this.member)return [];
    const imageUrls=[]
    for(const photo of this.member.photos){
      imageUrls.push({
        small:photo.url,
        medium:photo.url,
        big:photo.url,
      })
    }
    return imageUrls;
  }
  selectTab(heading: string) {
    debugger;
    if (this.memberTabs) {
      this.memberTabs.tabs.find(x => x.heading === heading)!.active = true;
    }

  }
  onTabActivated(data: any) {
    this.activeTab = data as TabDirective;
    if (this.activeTab.heading === 'Messages') {
      this.loadMessages()
        } else {
    }
  } 
  loadMessages(){
    if(this.member)
  this.messageService.getMessageThread(this.member.id).subscribe({
    next:res=>{
      if(res){
        this.messages=res;
      }
    }
  })}
}
