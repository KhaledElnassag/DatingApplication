import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { enviroment } from 'src/enviroment';
import { UserLogin } from '../Models/user-login';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
hubUrl=enviroment.hubUrl
public hubConnection?:HubConnection
private activeUsersSubject= new BehaviorSubject<String[]>([])
public activeUsers$= this.activeUsersSubject.asObservable();
constructor(private toaster:ToastrService) {
debugger;
 }
 createHubConnection(user:UserLogin){
  debugger;
  if(!this.hubConnection){
  this.hubConnection=new HubConnectionBuilder().withUrl(this.hubUrl+'presence',{
    accessTokenFactory:()=>user.token
  }).withAutomaticReconnect().build();}
this.hubConnection.start().catch(err=>console.log(err));
this.hubConnection.on('UserOnlion',userName=>{
  debugger;
  console.log(this.hubConnection?.state)
  this.toaster.info(`${userName} Is Connected`);
})
this.hubConnection.on('UserOfflion',userName=>{
  debugger;
  console.log(this.hubConnection?.state)
  this.toaster.warning(`${userName} Is DisConnected`);
})
this.hubConnection.on('GetOnlionUsers',users=>{
  debugger;
  console.log(users)
  this.activeUsersSubject.next(users);
})
}
stopHubConnection(){
  this.hubConnection?.stop().catch(err=>{
    debugger
    console.log(this.hubConnection?.state)
  }
  );
  
}

}
