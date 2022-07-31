import { Component, Inject, OnInit } from '@angular/core';
import { BranchService } from 'src/app/Shared/Branch/branch.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef} from '@angular/material/dialog';
@Component({
  selector: 'app-addbranch',
  templateUrl: './addbranch.component.html',
  styleUrls: ['./addbranch.component.css']
})
export class AddbranchComponent implements OnInit {

  constructor(private service: BranchService, private toaster: ToastrService,
    public fb: FormBuilder, private dialogRef: MatDialogRef<AddbranchComponent>) { }

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
  get openingHour() {
    return this.form.get('openingHour')
  }
  get closingHour() {
    return this.form.get('closingHour')
  }
  ngOnInit(): void {
  }

  Title;
  ManagerName;
  OpeningHour;
  ClosingHour;
  OnAdd() {
    if(!this.validateTime()){
      this.toaster.warning('Closing time must be less than Opening Time');
      return;
    }
    this.parseTime();
    let Data = {
      ID: 0,
      Title: this.Title,
      OpeningHour: this.OpeningHour,
      ClosingHour: this.ClosingHour,
      ManagerName: this.ManagerName
    };
    this.service.AddBranch(Data).subscribe(
      (data: any) => {
        if(data.Successed){
        Data = data
        this.toaster.success('Added Successfully', 'Egyptian Recipes');
        this.dialogRef.close();
        }
      },
    )
  }
  validateTime(){
    let openingHours = this.OpeningHour.split(":")[0];
    let openingMinutes = this.OpeningHour.split(":")[1];
    let closingHours = this.ClosingHour.split(":")[0];
    let closingMinutes = this.ClosingHour.split(":")[1];
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
    dateOpening.setHours(this.OpeningHour.split(":")[0]);
    dateOpening.setMinutes(this.OpeningHour.split(":")[1]);
    dateClosing.setHours(this.ClosingHour.split(":")[0]);
    dateClosing.setMinutes(this.ClosingHour.split(":")[1]);
    this.OpeningHour = dateOpening.toLocaleString();
    this.ClosingHour = dateClosing.toLocaleString();
  }
}
