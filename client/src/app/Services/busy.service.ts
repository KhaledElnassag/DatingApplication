import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  constructor(private spinner:NgxSpinnerService) {

   }
   busy(){
    this.spinner.show(undefined,{
      type:"line-scale-party",
      bdColor:"rgba(255,255,255,0)",
      color:"#333333"
    })
   }
   idle(){
    this.spinner.hide()
   }
}
