import { Component, OnInit, Inject, Input } from '@angular/core';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Admin } from 'src/app/Shared/Admin/Admin.model';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-updateadmin',
  templateUrl: './updateadmin.component.html',
  styleUrls: ['./updateadmin.component.css']
})
export class UpdateadminComponent implements OnInit {
  url = environment.apiURL
  constructor(@Inject(MAT_DIALOG_DATA) public admin,
    private service: AdminService, private toaster: ToastrService) { }

  files = [];
  ErrorMessage = "";
  ngOnInit(): void {
    this.getUser()
    setTimeout(() => {
      this.upload()
    }, 150);
  }
  Nameeee;
  oldData: Admin;
  getUser() {
    this.service.GetAdminDetails(this.admin.id).subscribe(
      (data: any) => {
        this.oldData = data.Data;
      }
    )
  }

  OnUpdate() {

    this.service.UpdateAdmin(this.admin.id, this.oldData).subscribe(
      (data: any) => {
        if(data.Successed){
        this.toaster.success('Updated Successfully', 'EgyptianRecipes')
        }
      }
    )
  }

  onFileChange(event) {
    this.files = event.target.files;
    setTimeout(() => {
      this.upload()
    }, 200);
  }
  upload() {
    let formData: FormData = new FormData();
    for (var j = 0; j < this.files.length; j++) {
      formData.append("file[]", this.files[j], this.files[j].name);
      this.service.upload(formData).subscribe(
        (data: any) => {
          this.oldData.Photo = data.Data[0].Path;
        }
      )
    }
  }


}
