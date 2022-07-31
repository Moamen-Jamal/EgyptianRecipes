import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MasteradminComponent } from './MasterModules/admin/masteradmin/masteradmin.component';
import { AdminsCrudComponent } from './Adminstration/admins-crud/admins-crud.component';

import { MasterComponent } from './Adminstration/master/master.component';

import { AuthenticationGuard } from './Guards/authentication.guard';
import { HomeComponent } from './home/home/home.component';
import { ForbiddenComponent } from './forbidden/forbidden/forbidden.component';

import { SignInComponent } from './sign-in/sign-in.component';
import { CustomersCrudComponent } from './Adminstration/customer-crud/customers-crud/customers-crud.component';
import { BranchsCrudComponent } from './Adminstration/branchs-crud/branchs-crud.component';



const routes: Routes = [
  
  
  {path:'Admin' ,component: MasteradminComponent, canActivate:[AuthenticationGuard] ,
   data: { permittedRoles: ['Admin'] },
  children:[
    {path:'' , component : MasterComponent, canActivate:[AuthenticationGuard] , data: { permittedRoles: ['Admin'] },},
    {path:'AdminHome' ,component:MasterComponent , canActivate:[AuthenticationGuard], data: { permittedRoles: ['Admin'] },},
    {path:'AdminCrud' ,component: AdminsCrudComponent , canActivate:[AuthenticationGuard], data: { permittedRoles: ['Admin'] },},
    {path:'CustomerCrud' ,component: CustomersCrudComponent , canActivate:[AuthenticationGuard], data: { permittedRoles: ['Admin'] },},
    {path:'BranchCrud' ,component: BranchsCrudComponent , canActivate:[AuthenticationGuard], data: { permittedRoles: ['Admin'] },},
  ] } ,
  { path: 'Login', component: SignInComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  

  { path: '', redirectTo: 'Login', pathMatch: 'full' },

  {
    path: 'Home', component: HomeComponent,canActivate:[AuthenticationGuard] ,
    data: { permittedRoles: ['Customer'] }
  }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
