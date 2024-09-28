import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MemberListComponent } from './Component/member-list/member-list.component';
import { MemberDetailComponent } from './Component/member-detail/member-detail.component';
import { ListsComponent } from './Component/lists/lists.component';
import { MessagesComponent } from './Component/messages/messages.component';
import { HomeComponent } from './Component/home/home.component';
import { authGuard } from './guards/auth.guard';
import { MemberEditComponent } from './Component/member-edit/member-edit.component';
import { linkGuardGuard } from './guards/link-guard.guard';

const routes: Routes = [
  {path:'',component:HomeComponent,pathMatch:'full'},
  {path:'',runGuardsAndResolvers:'always',canActivate:[authGuard],children:[
    {path:'members',component:MemberListComponent},
    {path:'members/:id',component:MemberDetailComponent},
    {path:'member/edit',component:MemberEditComponent,canDeactivate:[linkGuardGuard]},
    {path:'lists',component:ListsComponent},
    {path:'messages',component:MessagesComponent}
  ]},
  {path:'**',component:HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
