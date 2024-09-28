import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Register } from 'src/app/Models/register';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model!:Register;
@Output() eventEmitter:EventEmitter<boolean>
registrForm!:FormGroup
constructor(private accountService:AccountService,private toast:ToastrService,private fb: FormBuilder) {
  this.eventEmitter=new EventEmitter<boolean>();
}
  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm(){
    this.registrForm=this.fb.group({
      userName:(['',Validators.required]),
      gender: (['male',Validators.required]),
      knownAs:(['', Validators.required]),
      dateOfBirth: (['', Validators.required]),
      city: (['', Validators.required]),
      country: (['', Validators.required]),
      password:(['',Validators.required,Validators.minLength(4),Validators.maxLength(12)]),
      confirmPassword:(['',this.matchValues('password')])
    });
    // this.registrForm=new FormGroup({
    //   userName:new FormControl('',Validators.required),
    //   gender: new FormControl(['male']),
    //   knownAs:new FormControl(['', Validators.required]),
    //   dateOfBirth: new FormControl(['', Validators.required]),
    //   city: new FormControl(['', Validators.required]),
    //   country: new FormControl(['', Validators.required]),
    //   password:new FormControl('',[Validators.required,Validators.minLength(4),Validators.maxLength(12)]),
    //   confirmPassword:new FormControl('',this.matchValues('password'))
    // })
    this.registrForm.controls['password'].valueChanges.subscribe({
      next: () => this.registrForm.controls['confirmPassword'].updateValueAndValidity()
    });
  }
  matchValues(password:string):ValidatorFn{
    return (control:AbstractControl)=>{
      return control.value===control.parent?.get(password)?.value?null:{notMatching:true};
    }
  }
register(){
  debugger;
  if(this.registrForm!=null){
    this.accountService.register(this.registrForm.value).subscribe({
      next:(data)=>this.cancel(),
      error:(err)=>this.toast.error(err.error.message)
    })
  }
}
cancel(){
this.eventEmitter.emit(false);
}
}
