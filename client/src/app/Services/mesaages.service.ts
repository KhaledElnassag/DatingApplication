import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Message } from '../Models/messages';
import { enviroment } from 'src/enviroment';

@Injectable({
  providedIn: 'root'
})
export class MesaagesService {
baseUrl=enviroment.apiUrl;
  constructor(private http:HttpClient) {
   }
   getMessagesForUser( container:string){
    let params = new HttpParams();
    params = params.append('container', container);
    return this.http.get<Message[]>(`${this.baseUrl}messages`,{observe:'response',params})
   }

   getMessageThread(userId: string) {
    return this.http.get<Message[]>(`${this.baseUrl}messages/thread/${userId}`);
  }
  sendMessage(userName: string, content: string) {
    debugger
    return this.http.post<Message>(`${this.baseUrl}messages`,{recipientUsername:userName,content:content})
  }
}
