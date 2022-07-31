import { Component, OnInit } from '@angular/core';
import { AdminDetailsComponent } from './admindetailspopup/admin-details/admin-details.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddadminComponent } from './addadmin/addadmin.component';
import { UpdateadminComponent } from './updateadmin/updateadmin.component';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { Admin } from 'src/app/Shared/Admin/Admin.model';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-admins-crud',
  templateUrl: './admins-crud.component.html',
  styleUrls: ['./admins-crud.component.css']
})
export class AdminsCrudComponent implements OnInit {

  constructor(private dialog: MatDialog, private service: AdminService,
    private route: Router) { }
    url = environment.apiURL
  DataList: Admin[];

  options = { itemsPerPage: 3, currentPage: 1, id: 'pagination', totalItems: 200 }

  ErrorMessage: string = "";
  ngOnInit(): void {
    this.getAll(this.options.currentPage, 3)
  }
  getAll(pageIndex, pageSize) {
    this.service.GetAllAdmines(pageIndex, pageSize).subscribe(
      (data: any) => {
        this.options.totalItems = data.Data.Records;
        this.DataList = data.Data.Result
      },
    )
  }
  getNextPrevData(pageIndex) {

    this.getAll(pageIndex, 3);
    this.options.currentPage = pageIndex;
  }
  admindetails(id) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    dialogConfig.height = "85%";
    dialogConfig.position = {
      top : '55px'
    }

    dialogConfig.data = { id }
    this.dialog.open(AdminDetailsComponent, dialogConfig).afterClosed().subscribe(res => {
      setTimeout(() => {
        this.ngOnInit()
      }, 400);
    });
  }
  addadmin() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    dialogConfig.height = "91%";
    dialogConfig.position = {
      top: '55px'
    }
    this.dialog.open(AddadminComponent, dialogConfig).afterClosed().subscribe(res => {
      setTimeout(() => {
        this.ngOnInit()
      }, 400);
    });
  }
}
