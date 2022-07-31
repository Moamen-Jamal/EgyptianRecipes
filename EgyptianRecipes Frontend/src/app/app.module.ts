import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HashLocationStrategy , LocationStrategy } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AsideComponent } from './aside/aside.component';
import { MasterComponent } from './Adminstration/master/master.component';
import { MasteradminComponent } from './MasterModules/admin/masteradmin/masteradmin.component';
import { AdminsCrudComponent } from './Adminstration/admins-crud/admins-crud.component';
import { AdminDetailsComponent } from './Adminstration/admins-crud/admindetailspopup/admin-details/admin-details.component';
import { AddadminComponent } from './Adminstration/admins-crud/addadmin/addadmin.component';
import { UpdateadminComponent } from './Adminstration/admins-crud/updateadmin/updateadmin.component';
import { AdminaccountComponent } from './Adminstration/admins-crud/adminaccount/adminaccount.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from './Shared/User/user.service';
import { HomeComponent } from './home/home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { AuthenticationGuard } from './Guards/authentication.guard';
import { ForbiddenComponent } from './forbidden/forbidden/forbidden.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';

import { SignInComponent } from './sign-in/sign-in.component';
import { DatePipe } from '@angular/common';
import { UniqueUsernameValidatorDirective } from './Shared/unique-username-validator.directive';
import { UniqueEmailValidatorDirective } from './Shared/unique-email-validator.directive';
import { UpdatebranchComponent } from './Adminstration/branchs-crud/updatebranch/updatebranch.component';
import { CustomersCrudComponent } from './Adminstration/customer-crud/customers-crud/customers-crud.component';
import { AddbranchComponent } from './Adminstration/branchs-crud/addbranch/addbranch.component';
import { BranchsCrudComponent } from './Adminstration/branchs-crud/branchs-crud.component';
import { UpdatecustomerComponent } from './Adminstration/customer-crud/updatecustomer/updatecustomer.component';
import { CustomerdetailspopupComponent } from './Adminstration/customer-crud/customerdetailspopup/customerdetailspopup.component';
import { AddcustomerComponent } from './Adminstration/customer-crud/addcustomer/addcustomer.component';
import { RatingModule } from 'ng-starrating';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    AsideComponent,
   
    MasterComponent,
    MasteradminComponent,
   
    AdminsCrudComponent,
    
    AdminDetailsComponent,
    AddadminComponent,
    UpdateadminComponent,
    
    AdminaccountComponent,
    HomeComponent,
    
    ForbiddenComponent,
    
    SignInComponent,
    
    UniqueUsernameValidatorDirective,
    UniqueEmailValidatorDirective,
    AddcustomerComponent,
    CustomerdetailspopupComponent,
    UpdatecustomerComponent,
    BranchsCrudComponent,
    AddbranchComponent,
    UpdatebranchComponent,
    CustomersCrudComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatDialogModule,
    FormsModule,
    HttpClientModule,
    NgxPaginationModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    RatingModule 
  ],
  entryComponents: [ AdminDetailsComponent, AddadminComponent,AddbranchComponent,
    UpdateadminComponent, CustomerdetailspopupComponent, AddcustomerComponent, UpdatecustomerComponent
    ,UpdatebranchComponent,AdminaccountComponent],
  providers: [
    UserService,
    AuthenticationGuard,
    DatePipe,

    {provide : LocationStrategy , useClass :HashLocationStrategy}
    //{
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AuthInterceptor,
    //   multi: true
    // }
  ],
  bootstrap: [AppComponent],
  exports: [
    UniqueEmailValidatorDirective,
    UniqueUsernameValidatorDirective
  ]
})
export class AppModule { }
