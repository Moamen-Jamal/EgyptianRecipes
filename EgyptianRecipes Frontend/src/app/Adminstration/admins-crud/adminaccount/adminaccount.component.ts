import { Component, OnInit } from '@angular/core';
import { MatDialog ,MatDialogConfig } from '@angular/material/dialog';
import { UpdateadminComponent } from '../updateadmin/updateadmin.component';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { Admin } from 'src/app/Shared/Admin/Admin.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-adminaccount',
  templateUrl: './adminaccount.component.html',
  styleUrls: ['./adminaccount.component.css']
})
export class AdminaccountComponent implements OnInit {
  Admin : Admin ;
  constructor(private dialog :MatDialog , private service :AdminService) { }
  url = environment.apiURL
  ngOnInit(): void {
    this.getUser()
  }
  updateadmin(id){
    const  dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true ;
    dialogConfig.disableClose = true ;
    dialogConfig.width = "40%";
    dialogConfig.height="90%";
    dialogConfig.position={
      top:'55px'
    }
    dialogConfig.data = {id}
    this.dialog.open(UpdateadminComponent,dialogConfig).afterClosed().subscribe(res => {
      setTimeout(() => {
        this.ngOnInit();
      }, 350);
    });
  }
  getUser(){
    this.service.GetAdminDetails(parseInt(localStorage.getItem('userID'))).subscribe(
      (data:any)=>{
        this.Admin = data.Data;
      }
    )
  }

}
