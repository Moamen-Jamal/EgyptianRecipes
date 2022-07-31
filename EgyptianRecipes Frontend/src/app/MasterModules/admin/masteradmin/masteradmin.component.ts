import { Component, OnInit, Input } from '@angular/core';
import { MatDialog ,MatDialogConfig } from '@angular/material/dialog';
import { AdminaccountComponent } from 'src/app/Adminstration/admins-crud/adminaccount/adminaccount.component';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { Admin } from 'src/app/Shared/Admin/Admin.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-masteradmin',
  templateUrl: './masteradmin.component.html',
  styleUrls: ['./masteradmin.component.css']
})
export class MasteradminComponent implements OnInit {

  constructor(private dialog :MatDialog , private router :Router , private service :AdminService ) { }
  url = environment.apiURL
  User : Admin ;
  ngOnInit(): void {
  
      this.getUser()
    
    
  }
  
  getUser(){
    this.service.GetAdminDetails(parseInt(localStorage.getItem('userID'))).subscribe(
      (data:any)=>{
        this.User = data.Data;
      }
    )
  }
  adminaccount(){
    const  dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true ;
    dialogConfig.disableClose = true ;
    dialogConfig.width = "40%";
    dialogConfig.height="85%";
    dialogConfig.position={
      top: '60px'
    }
    
    this.dialog.open(AdminaccountComponent ,dialogConfig).afterClosed().subscribe(res => {
      this.getUser();
    });
  }
  Login(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    sessionStorage.removeItem('isUser')
    this.router.navigate(['/Login']);
  }
  
  
  LogOut(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    sessionStorage.removeItem('isUser')
    this.router.navigate(['/Home']);
  }
 
}
