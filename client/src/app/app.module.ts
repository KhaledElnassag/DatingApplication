import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { NavComponent } from './Component/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './Component/home/home.component';
import { RegisterComponent } from './Component/register/register.component';
import { MemberListComponent } from './Component/member-list/member-list.component';
import { MemberDetailComponent } from './Component/member-detail/member-detail.component';
import { MessagesComponent } from './Component/messages/messages.component';
import { ListsComponent } from './Component/lists/lists.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    MessagesComponent,
    ListsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({    // ToastrModule added with global config
      timeOut: 2000,          // Display duration in milliseconds
      positionClass: 'toast-bottom-right',  // Toast position
      preventDuplicates: true,            // Prevent duplicate toasts
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
