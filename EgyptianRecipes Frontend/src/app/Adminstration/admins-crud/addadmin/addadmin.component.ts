import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-addadmin',
  templateUrl: './addadmin.component.html',
  styleUrls: ['./addadmin.component.css']
})
export class AddadminComponent implements OnInit {


  constructor(private service: AdminService, private toaster: ToastrService) { }
  url = environment.apiURL
  ngOnInit(): void {
  }

  Name; UserName; Email; Phone; Photo; Password;


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
      this.service.upload(formData).subscribe(
        (data: any) => {
          this.Photo = data.Data[0].Path;
        }
      )
    }
  }


  OnAdd() {

    let Data = {
      ID: 0,
      Name: this.Name,
      Password: this.Password,
      UserName: this.UserName,
      Email: this.Email,
      Phone: this.Phone,
      Photo: this.Photo,
    };
    this.service.AddAdmin(Data).subscribe(
      (data: any) => {
        Data = data
        if(data.Successed){
        this.toaster.success('Added Successfully', 'EgyptianRecipes')
        }
      }
    )
  }

  callAll() {
    this.upload();
    this.OnAdd()
  }

}
