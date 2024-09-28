import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/Models/messages';
import { MesaagesService } from 'src/app/Services/mesaages.service';

@Component({
  selector: 'app-memberMessages',
  templateUrl: './memberMessages.component.html',
  styleUrls: ['./memberMessages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input() messages:Message[]|undefined
  @Input() memberId?:string
  @ViewChild('messageForm') messageForm?: NgForm;
  loading = false;
  messageContent=''
  constructor(private messageService:MesaagesService){
  }
  ngOnInit(): void {
  }
  sendMessage() {
    debugger
    if (!this.memberId) return;
    this.loading = true;
    this.messageService.sendMessage(this.memberId, this.messageContent).subscribe({
      next:mess=>{
        this.messages?.push(mess);
        this.messageForm?.reset()
      }
    })
  }
  
  }
  
