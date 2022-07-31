import { Component, OnInit, Inject, Output } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdatecustomerComponent } from 'src/app/Adminstration/customer-crud/updatecustomer/updatecustomer.component';
import { Customer } from 'src/app/Shared/Customer/customer.model';
import { CustomerService } from 'src/app/Shared/Customer/customer.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-customerdetailspopup',
  templateUrl: './customerdetailspopup.component.html',
  styleUrls: ['./customerdetailspopup.component.css']
})
export class CustomerdetailspopupComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public datacustomer,private dialogRef: MatDialogRef<CustomerdetailspopupComponent>,
  private service: CustomerService, private dialog: MatDialog, private router: Router
  , private toaster: ToastrService) { }

  Data: Customer;
  url = environment.apiURL
  ErrorMessage: string;
  ngOnInit(): void {
    this.GetDetails();
  }
  GetDetails() {
    this.service.GetCustomerDetails(this.datacustomer.id).subscribe(
      (data: any) => {
        this.Data = data.Data;
      }
    )
  }

  OnDelete() {
    if (confirm('Are you sure to delete this Customer?')) {
      if (localStorage.getItem('userID') == this.datacustomer.id) {
        this.service.DeleteCustomer(this.datacustomer.id).subscribe(
          (data: any) => {

            if (data.Successed) {
              localStorage.removeItem('userToken')
              this.router.navigate(['/Login'])
            }

          }
        )
      } else {
        this.service.DeleteCustomer(this.datacustomer.id).subscribe(
          (data: any) => {
            if (data.Successed) {
              this.toaster.error('Deleted Successfully', 'Address Book');
              this.dialogRef.close();
            }
          }
        )
      }
    }
  }

  updateCustomer(id) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "42%";
    dialogConfig.height = "90%";
    dialogConfig.position = {
      top: '50px'
    }

    dialogConfig.data = { id }
    this.dialog.open(UpdatecustomerComponent, dialogConfig).afterClosed().subscribe(res => {
      setTimeout(() => {
        this.ngOnInit()
      }, 250);
    });
  }
}
