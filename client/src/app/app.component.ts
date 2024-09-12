import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'client';
  users:any;

  constructor(private http:HttpClient) {
    
  }
  ngOnInit(): void {
    this.http.get<any>('https://localhost:7025/api/Users').subscribe({
      next:(data:any)=>{
        debugger
        this.users=data},
        error:(data)=>console.log(data)
    })
  }
}
