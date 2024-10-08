import { Component, Input } from '@angular/core';
import { Members, Photo } from 'src/app/Models/members';
import { FileUploader } from 'ng2-file-upload';
import { enviroment } from 'src/enviroment';
import { UserLogin } from 'src/app/Models/user-login';
import { AccountService } from 'src/app/Services/account.service';
import { MembersService } from 'src/app/Services/members.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent {
  @Input() member: Members | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = enviroment.apiUrl;
  user: UserLogin | undefined;

  constructor(private accountService: AccountService, private memberService: MembersService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user
      }
    })
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  setMainPhoto(photo:Photo){
    this.memberService.setMainPhoto(photo.id).subscribe({
      next:_=>{
        if (this.user && this.member) {
          this.user.photoUrl = photo.url;
          this.accountService.setCurrentUser(this.user);
          this.member.photoUrl = photo.url;
          this.member.photos.forEach(p => {
            if (p.isMain) p.isMain = false;
            if (p.id === photo.id) p.isMain = true;
          })
      }
    }});
  }
  deletePhoto(photoId:number){
    this.memberService.deletePhoto(photoId).subscribe({
      next:_=>{
        if (this.member) {
          this.member.photos=this.member.photos.filter(p=>p.id!==photoId)
      }
    }});
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);

        this.member?.photos.push(photo);
        // if(photo.isMain&&this.member&&this.user){
        //   this.member.photoUrl=photo.url;
        //   this.user.photoUrl=photo.url;
        // this.accountService.setCurrentUser(this.user)
        }
      }
    }
  
}

