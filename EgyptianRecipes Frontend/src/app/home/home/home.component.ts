import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MatDialog ,MatDialogConfig } from '@angular/material/dialog';
import { UserService } from 'src/app/Shared/User/user.service';
import { Router } from '@angular/router';
import { FormBuilder ,FormControl, Validators } from '@angular/forms';
import { User } from 'src/app/Shared/User/user.model';
import { Customer } from 'src/app/Shared/Customer/customer.model';
import { CustomerService } from 'src/app/Shared/Customer/customer.service';
import { Branch } from 'src/app/Shared/Branch/branch.model';
import { BranchService } from 'src/app/Shared/Branch/branch.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  constructor(private dialog :MatDialog ,private service: BranchService , private toaster: ToastrService, 
  private router :Router ,private customerService: CustomerService,   public fb: FormBuilder,
  private userService :UserService ) { }
  options = { itemsPerPage: 3, currentPage: 1, id: 'pagination', totalItems: 200 }
  DataList: Branch[];
  Title: any;
  ErrorMessage: string = "";
  BranchList: Branch[];
ngOnInit(): void {
  this.getUser();
  this.loadData();
}
loadData(){
  this.getAll(this.options.currentPage, 3);
  this.getBranches();
}
getBranches() {
  this.service.GetBranches().subscribe(
    (data: any) => { this.BranchList = data.Data }
  )
}

getBranchByTitle(title : any){
  this.Title = title;
}


getAll(pageIndex, pageSize) {
  this.service.GetBookedBranches(parseInt(localStorage.getItem('userID')),pageIndex, pageSize).subscribe(
    (data: any) => {
      this.options.totalItems = data.Data.Records;
      this.DataList = data.Data.Result
    },
  )
}
bookBranch(){
  if(!this.Title || this.Title == "Select"){
    this.toaster.warning("Please, select a branch first");
    return;

  }
  var DataFilter = this.DataList.filter(i => i.Title == this.Title);
  if(DataFilter.length > 0)
  {
    this.toaster.warning("This branch is booked before");
          return;
  }
  let branchID = this.BranchList.find(i => i.Title == this.Title)?.ID;
  let customerID = parseInt(localStorage.getItem('userID'));
  this.service.BookBranch(branchID, customerID).subscribe(
    (data: any) => {
      if (data.Successed) {
        this.toaster.success('Booked Successfully', 'Egyptian Recipes');
        this.getAll(this.options.currentPage, 3);
      }
    }
  )

}
getNextPrevData(pageIndex) {

  this.getAll(pageIndex, 3);
  this.options.currentPage = pageIndex;
}

CancelBookedBranch(branchID) {
  if (confirm('Are you sure to Cancel this Order?')) {
    let CustomerID = parseInt(localStorage.getItem('userID'));
    this.service.CancelBookedBranch(branchID, CustomerID).then((data :any) => {
        this.toaster.error('Canceled Successfully', 'Egyptian Recipes'); 
        this.getAll(this.options.currentPage, 3);
    })
  }
}
 

    
  user : User ;
  getUser(){
    if(localStorage.getItem('userToken')!= null){
    this.userService.getUser(parseInt(localStorage.getItem('userID'))).subscribe(
      (data :any)=>{
        this.user = data.Data
      }
    )
    }
  }
  

  
  customer :Customer 
  getCustomer(){
    if(localStorage.getItem('userToken')!= null){
    this.customerService.GetCustomerDetails(parseInt(localStorage.getItem('userID'))).subscribe(
      (data:any)=>{
        this.customer = data.Data;
      }
    )}
  }



  LogOut(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    this.router.navigate(['/Login']);
    
  }
  
  
  Login(){
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    this.router.navigate(['/Login']);
  }
  
  
}
