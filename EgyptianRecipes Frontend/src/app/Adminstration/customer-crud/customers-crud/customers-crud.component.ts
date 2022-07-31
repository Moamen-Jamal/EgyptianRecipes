import { Component, OnInit } from '@angular/core';
import { MatDialog ,MatDialogConfig } from '@angular/material/dialog';
import { CustomerService } from 'src/app/Shared/Customer/customer.service';
import { Customer } from 'src/app/Shared/Customer/customer.model';
import { environment } from 'src/environments/environment';
import { AddcustomerComponent } from '../addcustomer/addcustomer.component';
import { CustomerdetailspopupComponent } from '../customerdetailspopup/customerdetailspopup.component';
@Component({
  selector: 'app-customers-crud',
  templateUrl: './customers-crud.component.html',
  styleUrls: ['./customers-crud.component.css']
})
export class CustomersCrudComponent implements OnInit {

  constructor(private dialog :MatDialog , private service :CustomerService) { }

  options={ itemsPerPage:3, currentPage:1, id :'pagination', totalItems:200 }
    DataList :Customer[];
    url = environment.apiURL
  ErrorMessage :string = "";
  ngOnInit(): void {
    this.getAll(this.options.currentPage,3)
    
  }
  getAll(pageIndex,pageSize){
    this.service.GetAllCustomers(pageIndex,pageSize).subscribe(
      (data:any)=>{
        this.options.totalItems=data.Data.Records;
        this.DataList = data.Data.Result
      },
    )
  }
  
   
  getNextPrevData(pageIndex){
    
    this.getAll(pageIndex,3);
    this.options.currentPage= pageIndex;
  }
  customerdetails(id){
    const  dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true ;
    dialogConfig.disableClose = true ;
    dialogConfig.width = "40%";
    dialogConfig.height="87%";
    dialogConfig.position = {
      top : '55px'
    }
    
    dialogConfig.data = {id}
    localStorage.setItem('customerID', id)
    this.dialog.open(CustomerdetailspopupComponent ,dialogConfig).afterClosed().subscribe(res => {
      this.getAll(this.options.currentPage,3);
    });
  }
  addcustomer() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    dialogConfig.height = "91%";
    dialogConfig.position = {
      top: '55px'
    }
    this.dialog.open(AddcustomerComponent, dialogConfig).afterClosed().subscribe(res => {
      this.getAll(this.options.currentPage,3);
    });
  }
}