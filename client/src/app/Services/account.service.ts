import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../Models/login';
import { UserLogin } from '../Models/user-login';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Register } from '../Models/register';
import { enviroment } from 'src/enviroment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
apiUrl:string=enviroment.apiUrl;
private loginSubject=new BehaviorSubject<UserLogin|null>(null);
currentUser$:Observable<UserLogin|null>;
  constructor(private httpClient:HttpClient) {
      this.isExist();
      this.currentUser$=this.loginSubject.asObservable();
   }
   register(modal:Register):Observable<UserLogin>{
    return this.httpClient.post<UserLogin>(`${this.apiUrl}account/register`,modal).pipe(
     map(user=>{
       debugger;
       if(user != null){
         localStorage.setItem('user',JSON.stringify(user))
         this.loginSubject.next(user);
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
        }
        return user;
      })
     )
   }
   logout(){
    debugger;
    localStorage.removeItem('user');
    this.loginSubject.next(null);
   }
   public isExist(){
    const user=localStorage.getItem('user');
    debugger;
    const userExist=(user != null)?JSON.parse(user):null;
    this.loginSubject.next(userExist);
    return userExist;
   }
}
