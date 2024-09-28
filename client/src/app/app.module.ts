import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { NavComponent } from './Component/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule} from 'ngx-bootstrap/tabs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './Component/home/home.component';
import { RegisterComponent } from './Component/register/register.component';
import { MemberListComponent } from './Component/member-list/member-list.component';
import { MemberDetailComponent } from './Component/member-detail/member-detail.component';
import { MessagesComponent } from './Component/messages/messages.component';
import { ListsComponent } from './Component/lists/lists.component';
import { ToastrModule } from 'ngx-toastr';
import { MemberCardComponent } from './Component/member-card/member-card.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { MemberEditComponent } from './Component/member-edit/member-edit.component';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { PhotoEditorComponent } from './Component/photo-editor/photo-editor.component';
import {FileUploadModule  } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TimeagoModule } from 'ngx-timeago';
import { MemberMessagesComponent } from './Component/memberMessages/memberMessages.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    MessagesComponent,
    ListsComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    MemberMessagesComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ToastrModule.forRoot({    // ToastrModule added with global config
      timeOut: 3000,          // Display duration in milliseconds
      positionClass: 'toast-bottom-right',  // Toast position
      preventDuplicates: true,            // Prevent duplicate toasts
    }),
    NgxGalleryModule,
    NgxSpinnerModule.forRoot(
      {
        type:"line-scale-party"
      }
    ),
    FileUploadModule,
    TimeagoModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
