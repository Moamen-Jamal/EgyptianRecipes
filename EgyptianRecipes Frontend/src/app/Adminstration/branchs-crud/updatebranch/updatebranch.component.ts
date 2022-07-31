import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BranchService } from 'src/app/Shared/Branch/branch.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Branch } from 'src/app/Shared/Branch/branch.model';
@Component({
  selector: 'app-updatebranch',
  templateUrl: './updatebranch.component.html',
  styleUrls: ['./updatebranch.component.css']
})
export class UpdatebranchComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data,
  private branchService: BranchService, private dialogRef: MatDialogRef<UpdatebranchComponent>,
  private toaster: ToastrService, public fb: FormBuilder) { }

  timePattern = "[0-9][0-9]:(00|30)";
  numberPattern = "[+-]?([0-9]*[.])?[0-9]+";
  characterPattern = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_ ]*$";
  form = this.fb.group({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(200),
      Validators.pattern(this.characterPattern)
    ]),
    managerName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(250),
      Validators.pattern(this.characterPattern)
    ]),
    openingHour: new FormControl('', [
      Validators.required,
      Validators.pattern(this.timePattern)
    ]),
    closingHour: new FormControl('', [
      Validators.required,
      Validators.pattern(this.timePattern)
    ]),
  });
  get title() {
    return this.form.get('title')
  }

  ngOnInit(): void {
    this.getByID()
  }
  branch: Branch;
  getByID() {
    this.branchService.GetBranchDetails(this.data.id).subscribe(
      (data: any) => {
        data.Data.OpeningHour = this.formatTime(data.Data.OpeningHour);
        data.Data.ClosingHour = this.formatTime(data.Data.ClosingHour);
        this.branch = data.Data;
      }
    )
  }
  formatTime(date: Date) {
    let newDate = new Date(date);
    return (
      [
        this.padTo2Digits(newDate.getHours()),
        this.padTo2Digits(newDate.getMinutes()),
      ].join(':')
    );
  }
  padTo2Digits(num: number) {
    return num.toString().padStart(2, '0');
  }
  validateTime(){
    let openingHours = this.branch.OpeningHour.split(":")[0];
    let openingMinutes = this.branch.OpeningHour.split(":")[1];
    let closingHours = this.branch.ClosingHour.split(":")[0];
    let closingMinutes = this.branch.ClosingHour.split(":")[1];
    if(closingHours > 12){
      if(closingHours - 12 > openingHours)
        return false;
      else if(closingHours - 12 == openingHours){
        if(closingMinutes > openingMinutes)
          return false;
      }
      return true;
    }
    else{
      if(closingHours > openingHours)
        return false;
      else if(closingHours == openingHours){
        if(closingMinutes > openingMinutes)
          return false;
    }
    return true;
  }
}
  parseTime(){
    debugger;
    let dateOpening: Date = new Date();
    let dateClosing: Date = new Date();
    dateOpening.setHours(this.branch.OpeningHour.split(":")[0]);
    dateOpening.setMinutes(this.branch.OpeningHour.split(":")[1]);
    dateClosing.setHours(this.branch.ClosingHour.split(":")[0]);
    dateClosing.setMinutes(this.branch.ClosingHour.split(":")[1]);
    this.branch.OpeningHour = dateOpening.toLocaleString();
    this.branch.ClosingHour = dateClosing.toLocaleString();
  }
  OnUpdate() {
    if(!this.validateTime()){
      this.toaster.warning('Closing time must be less than Opening Time');
      return;
    }
    this.parseTime()
    this.branchService.UpdateBranch(this.branch.ID, this.branch).subscribe(
      (data: any) => {
        this.toaster.success('Updated Successfully', 'Egyptian Recipes');
        this.dialogRef.close();
      }
    )
  }

}
