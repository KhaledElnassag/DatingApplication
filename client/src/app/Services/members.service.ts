import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroment';
import { Members } from '../Models/members';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl=enviroment.apiUrl;
  constructor(private httpClient:HttpClient) { }

  getAllMembers():Observable<Members[]>{
    return this.httpClient.get<Members[]>(`${this.baseUrl}users`,this.getOptions())
  }
  getMember(userName:string):Observable<Members>{
    return this.httpClient.get<Members>(`${this.baseUrl}users/${userName}`,this.getOptions())
  }
  getOptions(){
    const userJson=localStorage.getItem('user');
    if(!userJson)return;
    const user =JSON.parse(userJson);
    return {
      headers:new HttpHeaders({
        Authorization:`Bearer ${user.token}`
      })
    }
  }
}
