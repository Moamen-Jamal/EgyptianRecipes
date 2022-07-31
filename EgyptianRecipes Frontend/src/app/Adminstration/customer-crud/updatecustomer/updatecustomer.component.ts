import { Component, Inject, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/Shared/Customer/customer.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Customer } from 'src/app/Shared/Customer/customer.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-updatecustomer',
  templateUrl: './updatecustomer.component.html',
  styleUrls: ['./updatecustomer.component.css']
})
export class UpdatecustomerComponent implements OnInit {

  url = environment.apiURL;
  ErrorMessage: string;
  constructor(@Inject(MAT_DIALOG_DATA) public datacustomer, private customerservice: CustomerService,
     private toaster: ToastrService, public fb: FormBuilder,private dialogRef: MatDialogRef<UpdatecustomerComponent>) { }

  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  phonePattern = "^01[0-2||5]{1}[0-9]{8}";
  numberPattern = "[+-]?([0-9]*[.])?[0-9]+";
  characterPattern = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_ ]*$";
  form = this.fb.group({
    fname: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(16),
      Validators.pattern(this.characterPattern)
    ]),
    
    email: new FormControl('', [
      Validators.required,
      Validators.pattern(this.emailPattern),
      // uniqueEmailValidator(this.userService) // async validator
    ]),
    phone: new FormControl('', [
      Validators.required,
      Validators.pattern(this.phonePattern)
    ]),
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(16),
      // uniqueUsernameValidator(this.userService) // async validator
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(16),
    
    ]),


  });
  get email() {
    return this.form.get('email')
  }
  ngOnInit(): void {
    this.getcustomer()
    
  }
  ID;
  Name;
  UserName;
  Password;
  Email;
  MobilePhone;
  Photo;
  BirthDate;
  ///////

  files = [];
  onFileChange(event) {
    this.files = event.target.files;
    setTimeout(() => {
      this.upload()
    }, 100);
  }


  upload() {
    let formData: FormData = new FormData();
    for (var j = 0; j < this.files.length; j++) {
      formData.append("file[]", this.files[j], this.files[j].name);
      this.customerservice.upload(formData).subscribe(
        (data: any) => {
          this.customer.Photo = data.Data[0].Path;
        },
        (err) => {
          console.log('err')
        })
    }
  }

  parseDate(dateString: string): Date {
    if (dateString) {
      return new Date(dateString);
    }
    return null;
  }
  customer: Customer;
  getcustomer() {
    this.customerservice.GetCustomerDetails(parseInt(localStorage.getItem('customerID'))).subscribe(
      (data: any) => {
        this.customer = data.Data;

      }
    )
  }

  OnUpdate() {
    
    this.customerservice.UpdateCustomer(parseInt(localStorage.getItem('customerID')), this.customer).subscribe(
      (data: any) => {
        this.dialogRef.close();
      },
      (err) => {
        console.log('err')
      })
  }


  

}
