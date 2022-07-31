import { Component, OnInit, Inject, Output } from '@angular/core';
import { MatDialog, MatDialogConfig, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdateadminComponent } from '../../updateadmin/updateadmin.component';
import { Admin } from 'src/app/Shared/Admin/Admin.model';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-details',
  templateUrl: './admin-details.component.html',
  styleUrls: ['./admin-details.component.css']
})
export class AdminDetailsComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data,
    private service: AdminService, private dialog: MatDialog, private router: Router
    , private toaster: ToastrService) { }

  Data: Admin;
  url = environment.apiURL
  ErrorMessage: string;
  ngOnInit(): void {
    this.GetDetails();
  }
  GetDetails() {
    this.service.GetAdminDetails(this.data.id).subscribe(
      (data: any) => {
        this.Data = data.Data;
      }
    )
  }

  OnDelete() {
    if (confirm('Are you sure to delete this Admin?')) {
      if (localStorage.getItem('userID') == this.data.id) {
        this.service.DeleteAdmin(this.data.id).subscribe(
          (data: any) => {

            if (data.Successed) {
              localStorage.removeItem('userToken')
              this.router.navigate(['/Login'])
            }

          }
        )
      } else {
        this.service.DeleteAdmin(this.data.id).subscribe(
          (data: any) => {
            if (data.Successed) {
              this.toaster.error('Deleted Successfully', 'EgyptianRecipes')
            }
          }
        )
      }
    }
  }

  updateadmin(id) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "42%";
    dialogConfig.height = "90%";
    dialogConfig.position = {
      top: '50px'
    }

    dialogConfig.data = { id }
    this.dialog.open(UpdateadminComponent, dialogConfig).afterClosed().subscribe(res => {
      setTimeout(() => {
        this.ngOnInit()
      }, 250);
    });
  }
}
