import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroment';
import { Members } from '../Models/members';
import { Pagination } from '../Models/Pagination';
import { UserParams } from '../Models/UserParams';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl=enviroment.apiUrl;
  constructor(private httpClient:HttpClient) { }

  getAllMembers(params:UserParams):Observable<Pagination<Members[]>>{
    return this.httpClient.post<Pagination<Members[]>>(`${this.baseUrl}users`,params)
  }
  getMember(userName:string):Observable<Members>{
    return this.httpClient.get<Members>(`${this.baseUrl}users/${userName}`)
  }
  updateMember(data:any){
    return this.httpClient.put(`${this.baseUrl}users/update`,data)
  }
  setMainPhoto(photoId:number){
    return this.httpClient.put(`${this.baseUrl}users/set-main-photo/${photoId}`,{})
  }
  deletePhoto(photoId:number){
    return this.httpClient.delete(`${this.baseUrl}users/delete-photo/${photoId}`)
  }
  addLike(userName:string){
    return this.httpClient.get(`${this.baseUrl}likes/${userName}`);
  }
  getLikes(){
    return this.httpClient.get<Members[]>(`${this.baseUrl}likes`);
  }
  // getOptions(){
  //   const userJson=localStorage.getItem('user');
  //   if(!userJson)return;
  //   const user =JSON.parse(userJson);
  //   return {
  //     headers:new HttpHeaders({
  //       Authorization:`Bearer ${user.token}`
  //     })
  //   }
  // }
}
