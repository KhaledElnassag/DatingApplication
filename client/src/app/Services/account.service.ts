import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../Models/login';
import { UserLogin } from '../Models/user-login';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Register } from '../Models/register';
import { enviroment } from 'src/enviroment';
import { PresenceService } from './Presence.service';
import { HubConnection } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
apiUrl:string=enviroment.apiUrl;
private loginSubject=new BehaviorSubject<UserLogin|null>(null);
currentUser$:Observable<UserLogin|null>;
connecton?:HubConnection
  constructor(private httpClient:HttpClient,private presenceService:PresenceService) {
    debugger;
      this.isExist();
      this.currentUser$=this.loginSubject.asObservable();
   }
   register(modal:Register):Observable<UserLogin>{
    debugger;
    return this.httpClient.post<UserLogin>(`${this.apiUrl}account/register`,modal).pipe(
     map(user=>{
       debugger;
       if(user != null){
         localStorage.setItem('user',JSON.stringify(user))
         this.loginSubject.next(user);
         this.presenceService.createHubConnection(user);
       }
       return user;
     })
    )
  }
  
   login(modal:Login):Observable<UserLogin>{
     return this.httpClient.post<UserLogin>(`${this.apiUrl}account/login`,modal).pipe(
      map(user=>{
        debugger;
        if(user != null){
          localStorage.setItem('user',JSON.stringify(user))
          this.loginSubject.next(user);
          this.presenceService.createHubConnection(user);
          const con=this.presenceService.hubConnection;
        }
        return user;
      })
     )
   }
   logout(){
    debugger;
    localStorage.removeItem('user');
    this.loginSubject.next(null);
    this.presenceService.stopHubConnection();
    const con=this.presenceService.hubConnection;
   }
   public isExist(){
    const user=localStorage.getItem('user');
    debugger;
    const userExist=(user != null)?JSON.parse(user):null;
    this.loginSubject.next(userExist);
    // if(userExist)
    // this.presenceService.createHubConnection(userExist);
    return userExist;
   }
   setCurrentUser(user:UserLogin){
    localStorage.setItem("user",JSON.stringify(user));
    this.loginSubject.next(user);
  }
}
