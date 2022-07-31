import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/Shared/Customer/customer.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-addcustomer',
  templateUrl: './addcustomer.component.html',
  styleUrls: ['./addcustomer.component.css']
})
export class AddcustomerComponent implements OnInit {
  url = environment.apiURL;

  constructor(private customerservice: CustomerService,
     private toaster: ToastrService, public fb: FormBuilder,private dialogRef: MatDialogRef<AddcustomerComponent>) { }

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
    birthDate: new FormControl('', Validators.required),
    photo: new FormControl('', Validators.required),
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
    gender: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(16),
      Validators.pattern(this.characterPattern)
    ])
  });
  get email() {
    return this.form.get('email')
  }

  ngOnInit(): void {    
  }
  ID;
  Name;
  Email;
  UserName;
  Password;
  Phone;
  Photo;
  BirthDate;
  Gender;
  files = [];
  onFileChange(event) {
    this.files = event.target.files;
    setTimeout(() => {
      this.upload()
    }, 150);

  }

  upload() {
    let formData: FormData = new FormData();
    for (var j = 0; j < this.files.length; j++) {
      formData.append("file[]", this.files[j], this.files[j].name);
      this.customerservice.upload(formData).subscribe(
        (data: any) => {
          this.Photo = data.Data[0].Path;
        }
      )
    }
  }

  parseDate(dateString: string): Date {
    if (dateString) {
      return new Date(dateString);
    }
    return null;
  }
  OnAdd() {

    let Data = {
      ID: 0,
      Name: this.Name,
      Email: this.Email,
      UserName: this.UserName,
      Password: this.Password,
      Phone: this.Phone,
      Photo: this.Photo,
      BirthDate: this.BirthDate,
      Gender: this.Gender
    };
    this.customerservice.AddCustomer(Data).subscribe(
      (data: any) => {
        Data = data
        if (data.Successed) {
          this.toaster.success('Added Successfully', 'Egyptian Recipes');
          this.dialogRef.close();
        }
      }
    )
  }
  
  
  callAll() {
    this.upload();
    this.OnAdd()
  }
}
