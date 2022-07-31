import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BranchService } from 'src/app/Shared/Branch/branch.service';
import { Router } from '@angular/router';
import { Branch } from 'src/app/Shared/Branch/branch.model';
import { ToastrService } from 'ngx-toastr';
import { UpdatebranchComponent } from './updatebranch/updatebranch.component';
import { AddbranchComponent } from './addbranch/addbranch.component';
@Component({
  selector: 'app-branchs-crud',
  templateUrl: './branchs-crud.component.html',
  styleUrls: ['./branchs-crud.component.css']
})
export class BranchsCrudComponent implements OnInit {

  constructor(private dialog: MatDialog, private service: BranchService,
    private route: Router , private toaster: ToastrService) { }

    options = { itemsPerPage: 3, currentPage: 1, id: 'pagination', totalItems: 200 }
    DataList: Branch[];
    Title: any;
    ErrorMessage: string = "";
    BranchList: Branch[];
  ngOnInit(): void {
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

  getBranchByTitle(title : any, isSearch = false){
    if(title == "Select"){
      this.getAll(this.options.currentPage, 3);
      return;
    }
    if(isSearch){
      this.DataList = this.BranchList.filter(x => x.Title.includes(this.Title));
      return;
    }
    this.DataList = this.BranchList.filter(x => x.Title == title);
  }
  addBranch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "45%";
    dialogConfig.height="75%";
    dialogConfig.position = {
      top : '55px'
    }
    if(this.BranchList.length % 3 == 0)
            this.options.currentPage += this.options.currentPage;
    this.dialog.open(AddbranchComponent, dialogConfig).afterClosed().subscribe(res => {
        this.loadData()
    });
  }
  UpdateBranch(id) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "45%";
    dialogConfig.height="75%";
    dialogConfig.data = { id };
    dialogConfig.position = {
      top : '55px'
    }
    this.dialog.open(UpdatebranchComponent, dialogConfig).afterClosed().subscribe(res => {
        this.loadData()
    });
  }

  getAll(pageIndex, pageSize) {
    this.service.GetAllBranchs(pageIndex, pageSize).subscribe(
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
  search(){
    this.getBranchesByFilterAndSort(this.options.currentPage, 3)
  }
  getBranchesByFilterAndSort(pageIndex, pageSize) {
    if(!this.Title || this.Title.trim() == "")
      this.getAll(this.options.currentPage, 3)
    this.service.getAllBySearch
      (pageIndex, pageSize, this.Title).subscribe(
          (data: any) => {
            this.options.totalItems = data.Data.Records;
            this.DataList = data.Data.Result ;
          }
        )
  }
  OnDelete(id) {
    if (confirm('Are you sure to delete this Branch?')) {
      this.service.DeleteBranch(id).then((data :any) => {
        if(data.Successed){
          if(this.BranchList.length % 3 == 0)
            this.options.currentPage -= this.options.currentPage;
          this.loadData();         
          this.toaster.error('Deleted Successfully', 'Egyptian Recipes'); 
      }
      })
    }
  }

}

