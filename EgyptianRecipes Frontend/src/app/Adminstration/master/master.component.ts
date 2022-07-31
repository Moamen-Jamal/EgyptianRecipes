import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/Shared/Admin/admin.service';
import { AdminDashBoard } from 'src/app/Shared/Admin/AdminDashBoard.model';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.css']
})
export class MasterComponent implements OnInit {

  constructor(private adminService :AdminService) { }
  AdminDashBoard : AdminDashBoard
  ngOnInit(): void {
    this.getAdminDashboard()
  }
  getAdminDashboard(){
    this.adminService.GetAdminDashboard().subscribe(
      (data :any)=>{
        this.AdminDashBoard = data.Data      
      }
    )
  }

}
