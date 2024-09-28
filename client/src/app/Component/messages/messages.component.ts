import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/Models/messages';
import { MesaagesService } from 'src/app/Services/mesaages.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages:Message[]|undefined
  container='Outbox'
  constructor(private messageService:MesaagesService){

  }
ngOnInit(): void {
  this.loadMessages();
}
loadMessages(){
this.messageService.getMessagesForUser(this.container).subscribe({
  next:res=>{
    debugger;
    if(res){
      this.messages=res.body as Message[];
      console.log(this.messages)
    }
  }
})
}

}
