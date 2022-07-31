import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from 'src/app/Shared/User/user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormControl, Validators } from '@angular/forms';



@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError: boolean = false;
  ErrorMessage: string = "";
  constructor(private userService: UserService, private router: Router ,public fb: FormBuilder ) { }
 
  ngOnInit(): void {
  }
  //  payLoad = JSON.parse(window.atob(localStorage.getItem('userToken').split('.')[1]));
  //  userRole = this.payLoad.Roles;
  OnSubmit(userName, password) 
  {
    this.userService.UserAuthentication(userName, password).subscribe((data: any) => {
      if (data.Successed == true) {
        localStorage.setItem('userToken', data.Data.Token);
        localStorage.setItem('userID', data.Data.ID);
        localStorage.setItem('userName', data.Data.UserName);
        if (data.Data.Role == "Customer"){
          this.router.navigate(['/Home']);
        }
        
        if (data.Data.Role == "Admin"){
          this.router.navigate(['/Admin']);
        }
        
        
      } else {
        this.ErrorMessage = data.Message;
        this.isLoginError = true;
      }
    },
      () => {
        this.ErrorMessage = "حدث خطأ ما";
        this.isLoginError = true;
      });
    
  }

  /////////////////////////////
  
  form =  this.fb.group({
    UserN: new FormControl('',   Validators.required),
    pass: new FormControl('',Validators.required)
 });
 get UserN(){
  return this.form.get('UserN')
}
get pass(){
  return this.form.get('UserN')
}
  
  

}
